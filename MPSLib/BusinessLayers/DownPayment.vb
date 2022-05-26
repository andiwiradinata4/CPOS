Namespace BL
    Public Class DownPayment

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, ByVal intDPType As VO.DownPayment.Type) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus, intDPType)
        End Function

        Public Shared Function ListDataForLookup(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                 ByVal intBPID As Integer, ByVal intDPType As VO.DownPayment.Type, _
                                                 ByVal intBPID2 As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListDataForLookup(intCompanyID, intProgramID, intBPID, intDPType, intBPID2)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal intDPType As VO.DownPayment.Type, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "DP" & "-"

            If intDPType = VO.DownPayment.Type.Sales Then
                strReturn += "SO"
            ElseIf intDPType = VO.DownPayment.Type.Purchase Then
                strReturn += "RV"
            ElseIf intDPType = VO.DownPayment.Type.SalesService Then
                strReturn += "SS"
            End If

            strReturn += "-" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.DownPayment.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.DownPayment) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                BL.DownPayment.SaveData(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.DownPayment) As String
            Dim dtReferencesID, dtOutstandingDownPayment, _
                dtSalesOutstandingPayment As New DataTable, _
                dtReceiveOutstandingPayment As New DataTable, _
                dtSalesServiceOutstandingPayment As New DataTable
            Dim decSalesAmount As Decimal = 0, decReceiveAmount As Decimal = 0, decSalesServiceAmount As Decimal = 0, decDPAmount As Decimal = 0
            Dim clsSales As New VO.Sales, clsReceive As New VO.Receive, clsSalesService As New VO.SalesService, clsDP As New VO.DownPaymentDet
            Dim AllReferencesID As New List(Of String)


            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.DPType, clsData.DPDate)
                If DL.DownPayment.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                ElseIf Format(clsData.DPDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                End If
            Else
                If DL.DownPayment.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf DL.DownPayment.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                End If

                '# Get All References ID for Revert and Calculate Total Down Payment For Each References
                dtReferencesID = DL.DownPayment.ListDataReferencesIDByDPID(clsData.ID)

                '# Delete All References ID
                DL.DownPayment.DeleteDataByDPID(clsData.ID)

                '# Update Total Usage Down Payment | Revert
                DL.DownPayment.UpdateTotalUsage(clsData.ID)

                '# Re-Calculate Total Down Payment Amount And Update All Journal References
                For Each dr As DataRow In dtReferencesID.Rows

                    'Re-Calculate Total Down Payment Amount
                    If clsData.DPType = VO.DownPayment.Type.Sales Then '# Sales
                        DL.Sales.RecalculateTotalDownPayment(dr.Item("ReferenceID"))
                    ElseIf clsData.DPType = VO.DownPayment.Type.Purchase Then '# Receive
                        DL.Receive.RecalculateTotalDownPayment(dr.Item("ReferenceID"))
                    ElseIf clsData.DPType = VO.DownPayment.Type.SalesService Then '# Sales Service
                        DL.SalesService.RecalculateTotalDownPayment(dr.Item("ReferenceID"))
                    End If

                    '# Update Journal References
                    BL.DownPayment.UpdateJournalReferences(clsData, dr.Item("ReferenceID"))
                Next
            End If

            '# Save Data Down Payment
            DL.DownPayment.SaveData(bolNew, clsData)

            '# Allocate Down Payment Amount to All Outstanding Sales / Receive for Payment
            decDPAmount = clsData.TotalAmount
            If clsData.DPType = VO.DownPayment.Type.Sales Then '# Allocate All Outstanding Down Payment Sales
                '# Get all outstanding Sales for Payment
                dtSalesOutstandingPayment = DL.Sales.ListDataOutstandingPayment(clsData.CompanyID, clsData.ProgramID, clsData.BPID, clsData.BPID2)
                With dtSalesOutstandingPayment
                    For Each dr As DataRow In .Rows
                        If decDPAmount = 0 Then Exit For
                        '# Keep All References ID
                        AllReferencesID.Add(dr.Item("ID"))
                        '---------------------------------
                        decSalesAmount = dr.Item("OutstandingPayment")
                        clsSales = New VO.Sales
                        clsSales.ID = dr.Item("ID")

                        '# Update Total Down Payment in Sales
                        If decDPAmount >= decSalesAmount Then
                            '# Save detail Down Payment
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = clsData.ID
                            clsDP.ID = clsData.ID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsData.ID), "000")
                            clsDP.ReferenceID = clsSales.ID
                            clsDP.TotalAmount = decSalesAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)

                            decDPAmount -= decSalesAmount
                            decSalesAmount = 0
                        Else
                            '# Save detail Down Payment
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = clsData.ID
                            clsDP.ID = clsData.ID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsData.ID), "000")
                            clsDP.ReferenceID = clsSales.ID
                            clsDP.TotalAmount = decDPAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)

                            decSalesAmount -= decDPAmount
                            decDPAmount = 0
                        End If

                        'Re-Calculate Sales Total Down Payment
                        DL.Sales.RecalculateTotalDownPayment(clsSales.ID)
                    Next
                End With
            ElseIf clsData.DPType = VO.DownPayment.Type.Purchase Then '# Allocate All Outstanding Down Payment Receive
                '# Get all outstanding Receive for Payment
                dtReceiveOutstandingPayment = DL.Receive.ListDataOutstandingPayment(clsData.CompanyID, clsData.ProgramID, clsData.BPID)
                With dtReceiveOutstandingPayment
                    For Each dr As DataRow In .Rows
                        If decDPAmount = 0 Then Exit For
                        '# Keep All References ID
                        AllReferencesID.Add(dr.Item("ID"))
                        '---------------------------------
                        decReceiveAmount = dr.Item("OutstandingPayment")
                        clsReceive = New VO.Receive
                        clsReceive.ID = dr.Item("ID")

                        '# Update Total Down Payment in Receive
                        If decDPAmount >= decReceiveAmount Then
                            '# Save detail Down Payment
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = clsData.ID
                            clsDP.ID = clsData.ID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsData.ID), "000")
                            clsDP.ReferenceID = clsReceive.ID
                            clsDP.TotalAmount = decReceiveAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)

                            decDPAmount -= decReceiveAmount
                            decReceiveAmount = 0
                        Else
                            '# Save detail Down Payment
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = clsData.ID
                            clsDP.ID = clsData.ID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsData.ID), "000")
                            clsDP.ReferenceID = clsReceive.ID
                            clsDP.TotalAmount = decDPAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)

                            decReceiveAmount -= decDPAmount
                            decDPAmount = 0
                        End If

                        'Re-Calculate Receive Total Down Payment
                        DL.Receive.RecalculateTotalDownPayment(clsReceive.ID)
                    Next
                End With
            ElseIf clsData.DPType = VO.DownPayment.Type.SalesService Then '# Allocate All Outstanding Down Payment Sales Service
                '# Get all outstanding Sales Service for Payment
                dtSalesServiceOutstandingPayment = DL.SalesService.ListDataOutstandingPayment(clsData.CompanyID, clsData.ProgramID, clsData.BPID)
                With dtSalesServiceOutstandingPayment
                    For Each dr As DataRow In .Rows
                        If decDPAmount = 0 Then Exit For
                        '# Keep All References ID
                        AllReferencesID.Add(dr.Item("ID"))
                        '---------------------------------
                        decSalesServiceAmount = dr.Item("OutstandingPayment")
                        clsSalesService = New VO.SalesService
                        clsSalesService.ID = dr.Item("ID")

                        '# Update Total Down Payment in Sales Service
                        If decDPAmount >= decSalesServiceAmount Then
                            '# Save detail Down Payment
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = clsData.ID
                            clsDP.ID = clsData.ID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsData.ID), "000")
                            clsDP.ReferenceID = clsSalesService.ID
                            clsDP.TotalAmount = decSalesServiceAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)

                            decDPAmount -= decSalesServiceAmount
                            decSalesServiceAmount = 0
                        Else
                            '# Save detail Down Payment
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = clsData.ID
                            clsDP.ID = clsData.ID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsData.ID), "000")
                            clsDP.ReferenceID = clsSalesService.ID
                            clsDP.TotalAmount = decDPAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)

                            decSalesServiceAmount -= decDPAmount
                            decDPAmount = 0
                        End If

                        'Re-Calculate Sales Service Total Down Payment
                        DL.SalesService.RecalculateTotalDownPayment(clsSalesService.ID)
                    Next
                End With
            End If

            '# Update Total Usage Down Payment
            DL.DownPayment.UpdateTotalUsage(clsData.ID)

            '# Save Data Status
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

            '# Delete Buku Besar Down Payment
            DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)

            '# Journal Down Payment
            Dim dtDownPaymentJournal As DataTable = DL.DownPayment.ListDataReCalculateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            BL.DownPayment.GenerateJournal(bolNew, dtDownPaymentJournal, clsData.DPType)

            '# Update Journal References
            AllReferencesID.Sort()
            Dim strPrevReferencesID As String = ""
            For Each ref As String In AllReferencesID
                If strPrevReferencesID.Trim <> ref.Trim Then
                    strPrevReferencesID = ref.Trim
                    BL.DownPayment.UpdateJournalReferences(clsData, ref.Trim)
                End If
            Next
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.DownPayment
            BL.Server.ServerDefault()
            Return DL.DownPayment.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.DownPayment)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim dtReferencesID As DataTable = DL.DownPayment.ListDataDetailByDPID(clsData.ID)
                If DL.DownPayment.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.DownPayment.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.DownPayment.DeleteData(clsData.ID)
                    DL.DownPayment.DeleteDataByDPID(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Update Total DownPayment References
                    If clsData.DPType = VO.DownPayment.Type.Sales Then '# Calculate Sales
                        For Each dr As DataRow In dtReferencesID.Rows
                            DL.Sales.RecalculateTotalDownPayment(dr.Item("ReferenceID"))
                            DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, dr.Item("ID"))
                            BL.Sales.GenerateJournal(False, DL.Sales.GetDetail(dr.Item("ReferenceID")))
                        Next
                    ElseIf clsData.DPType = VO.DownPayment.Type.Purchase Then
                        For Each dr As DataRow In dtReferencesID.Rows
                            DL.Receive.RecalculateTotalDownPayment(dr.Item("ReferenceID"))
                            DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, dr.Item("ID"))
                            BL.Receive.GenerateJournal(False, DL.Receive.GetDetail(dr.Item("ReferenceID")))
                        Next
                    ElseIf clsData.DPType = VO.DownPayment.Type.SalesService Then
                        For Each dr As DataRow In dtReferencesID.Rows
                            DL.SalesService.RecalculateTotalDownPayment(dr.Item("ReferenceID"))
                            DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, dr.Item("ID"))
                            BL.SalesService.GenerateJournal(False, DL.SalesService.GetDetail(dr.Item("ReferenceID")))
                        Next
                    End If

                    '# Update Total Usage
                    DL.DownPayment.UpdateTotalUsage(clsData.ID)

                    'Delete Buku Besar & Journal
                    BL.DownPayment.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal dtData As DataTable, ByVal bytType As VO.DownPayment.Type)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Generate Journal
            If bytType = VO.DownPayment.Type.Sales Then '# Sales
                For Each dr As DataRow In dtData.Rows
                    '# Generate Journal
                    clsJournal = New VO.Journal
                    clsJournal.CompanyID = dr.Item("CompanyID")
                    clsJournal.ProgramID = dr.Item("ProgramID")
                    clsJournal.ID = dr.Item("JournalID")
                    clsJournal.ReferencesID = dr.Item("ID")
                    clsJournal.JournalDate = dr.Item("DPDate")
                    clsJournal.TotalAmount = dr.Item("TotalAmount")
                    clsJournal.IsAutoGenerate = True
                    clsJournal.IDStatus = VO.Status.Values.Draft
                    clsJournal.Remarks = VO.DownPayment.JournalRemarksSales
                    clsJournal.CashOrBankInfo = VO.DownPayment.JournalCashOrBankInfoSales
                    clsJournal.PaymentTo = dr.Item("BPName")
                    clsJournal.LogBy = UI.usUserApp.UserID

                    Dim clsJournalDet As VO.JournalDet
                    Dim clsJournalDetAll(1) As VO.JournalDet

                    '# Cash / Bank
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = dr.Item("CoAIDOfActiva")
                    clsJournalDet.CoAName = ""
                    clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                    clsJournalDet.CreditAmount = 0
                    clsJournalDetAll(0) = clsJournalDet

                    '# Save Buku Besar
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = dr.Item("CompanyID")
                    clsBukuBesar.ProgramID = dr.Item("ProgramID")
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("DPDate")
                    clsBukuBesar.COAIDParent = dr.Item("CoAIDOfActiva")
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Account Receivable -> Request Julia, Posting ke Piutang Dagang sehingga DP dimap ke Piutang Dagang
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPrepaidIncome
                    clsJournalDet.DebitAmount = 0
                    clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                    clsJournalDetAll(1) = clsJournalDet

                    '# Save Buku Besar
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = dr.Item("CompanyID")
                    clsBukuBesar.ProgramID = dr.Item("ProgramID")
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("DPDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsBukuBesar.COAIDChild = dr.Item("CoAIDOfActiva")
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    Dim strJournalID As String = BL.Journal.SaveData(IIf(clsJournal.ID.Trim = "", True, False), clsJournal, clsJournalDetAll, False)
                    '# End Of Generate Journal

                    '# Update Journal ID
                    If strJournalID.Trim <> "" Then DL.DownPayment.UpdateJournalID(dr.Item("ID"), strJournalID)
                Next
            ElseIf bytType = VO.DownPayment.Type.Purchase Then '# Receive
                For Each dr As DataRow In dtData.Rows
                    '# Generate Journal
                    clsJournal = New VO.Journal
                    clsJournal.CompanyID = dr.Item("CompanyID")
                    clsJournal.ProgramID = dr.Item("ProgramID")
                    clsJournal.ID = dr.Item("JournalID")
                    clsJournal.ReferencesID = dr.Item("ID")
                    clsJournal.JournalDate = dr.Item("DPDate")
                    clsJournal.TotalAmount = dr.Item("TotalAmount")
                    clsJournal.IsAutoGenerate = True
                    clsJournal.IDStatus = VO.Status.Values.Draft
                    clsJournal.Remarks = VO.DownPayment.JournalRemarksPurchase
                    clsJournal.CashOrBankInfo = VO.DownPayment.JournalCashOrBankInfoPurchase
                    clsJournal.PaymentTo = dr.Item("BPName")
                    clsJournal.LogBy = UI.usUserApp.UserID

                    Dim clsJournalDet As VO.JournalDet
                    Dim clsJournalDetAll(1) As VO.JournalDet

                    If dr.Item("DPType") = VO.DownPayment.Type.Purchase Then
                        '# Account Payable -> Request Julia, Posting ke Hutang Dagang sehingga DP dimap ke Hutang Dagang
                        clsJournalDet = New VO.JournalDet
                        clsJournalDet.JournalID = dr.Item("JournalID")
                        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
                        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAdvancePayment
                        clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                        clsJournalDet.CreditAmount = 0
                        clsJournalDetAll(0) = clsJournalDet

                        '# Save Buku Besar
                        clsBukuBesar = New VO.BukuBesar
                        clsBukuBesar.CompanyID = dr.Item("CompanyID")
                        clsBukuBesar.ProgramID = dr.Item("ProgramID")
                        clsBukuBesar.ID = ""
                        clsBukuBesar.ReferencesID = dr.Item("ID")
                        clsBukuBesar.TransactionDate = dr.Item("DPDate")
                        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
                        clsBukuBesar.COAIDChild = dr.Item("CoAIDOfActiva")
                        clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
                        clsBukuBesar.CreditAmount = 0
                        clsBukuBesar.Remarks = ""
                        clsBukuBesar.LogBy = UI.usUserApp.UserID
                        BL.BukuBesar.SaveData(clsBukuBesar)

                        '# Cash / Bank
                        clsJournalDet = New VO.JournalDet
                        clsJournalDet.JournalID = dr.Item("JournalID")
                        clsJournalDet.CoAID = dr.Item("CoAIDOfActiva")
                        clsJournalDet.CoAName = ""
                        clsJournalDet.DebitAmount = 0
                        clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                        clsJournalDetAll(1) = clsJournalDet

                        '# Save Buku Besar
                        clsBukuBesar = New VO.BukuBesar
                        clsBukuBesar.CompanyID = dr.Item("CompanyID")
                        clsBukuBesar.ProgramID = dr.Item("ProgramID")
                        clsBukuBesar.ID = ""
                        clsBukuBesar.ReferencesID = dr.Item("ID")
                        clsBukuBesar.TransactionDate = dr.Item("DPDate")
                        clsBukuBesar.COAIDParent = dr.Item("CoAIDOfActiva")
                        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
                        clsBukuBesar.DebitAmount = 0
                        clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
                        clsBukuBesar.Remarks = ""
                        clsBukuBesar.LogBy = UI.usUserApp.UserID
                        BL.BukuBesar.SaveData(clsBukuBesar)
                    Else
                        '# Cash / Bank
                        clsJournalDet = New VO.JournalDet
                        clsJournalDet.JournalID = dr.Item("JournalID")
                        clsJournalDet.CoAID = dr.Item("CoAIDOfActiva")
                        clsJournalDet.CoAName = ""
                        clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                        clsJournalDet.CreditAmount = 0
                        clsJournalDetAll(0) = clsJournalDet

                        '# Save Buku Besar
                        clsBukuBesar = New VO.BukuBesar
                        clsBukuBesar.CompanyID = dr.Item("CompanyID")
                        clsBukuBesar.ProgramID = dr.Item("ProgramID")
                        clsBukuBesar.ID = ""
                        clsBukuBesar.ReferencesID = dr.Item("ID")
                        clsBukuBesar.TransactionDate = dr.Item("DPDate")
                        clsBukuBesar.COAIDParent = dr.Item("CoAIDOfActiva")
                        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                        clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
                        clsBukuBesar.CreditAmount = 0
                        clsBukuBesar.Remarks = ""
                        clsBukuBesar.LogBy = UI.usUserApp.UserID
                        BL.BukuBesar.SaveData(clsBukuBesar)

                        '# Account Receivable -> Request Julia, Posting ke Piutang Dagang sehingga DP dimap ke Piutang Dagang
                        clsJournalDet = New VO.JournalDet
                        clsJournalDet.JournalID = dr.Item("JournalID")
                        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPrepaidIncome
                        clsJournalDet.DebitAmount = 0
                        clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                        clsJournalDetAll(1) = clsJournalDet

                        '# Save Buku Besar
                        clsBukuBesar = New VO.BukuBesar
                        clsBukuBesar.CompanyID = dr.Item("CompanyID")
                        clsBukuBesar.ProgramID = dr.Item("ProgramID")
                        clsBukuBesar.ID = ""
                        clsBukuBesar.ReferencesID = dr.Item("ID")
                        clsBukuBesar.TransactionDate = dr.Item("DPDate")
                        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                        clsBukuBesar.COAIDChild = dr.Item("CoAIDOfActiva")
                        clsBukuBesar.DebitAmount = 0
                        clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
                        clsBukuBesar.Remarks = ""
                        clsBukuBesar.LogBy = UI.usUserApp.UserID
                        BL.BukuBesar.SaveData(clsBukuBesar)
                    End If
                    Dim strJournalID As String = BL.Journal.SaveData(IIf(clsJournal.ID.Trim = "", True, False), clsJournal, clsJournalDetAll, False)
                    '# End Of Generate Journal

                    '# Update Journal ID
                    If strJournalID.Trim <> "" Then DL.DownPayment.UpdateJournalID(dr.Item("ID"), strJournalID)
                Next
            ElseIf bytType = VO.DownPayment.Type.SalesService Then '# Sales Service
                For Each dr As DataRow In dtData.Rows
                    '# Generate Journal
                    clsJournal = New VO.Journal
                    clsJournal.CompanyID = dr.Item("CompanyID")
                    clsJournal.ProgramID = dr.Item("ProgramID")
                    clsJournal.ID = dr.Item("JournalID")
                    clsJournal.ReferencesID = dr.Item("ID")
                    clsJournal.JournalDate = dr.Item("DPDate")
                    clsJournal.TotalAmount = dr.Item("TotalAmount")
                    clsJournal.IsAutoGenerate = True
                    clsJournal.IDStatus = VO.Status.Values.Draft
                    clsJournal.Remarks = IIf(dr.Item("ProgramID") = VO.Program.Values.RentalAlatBerat, VO.DownPayment.JournalRemarksSalesServiceRentalAlatBerat, VO.DownPayment.JournalRemarksSalesServiceRentalTruk)
                    clsJournal.CashOrBankInfo = VO.DownPayment.JournalCashOrBankInfoSales
                    clsJournal.PaymentTo = dr.Item("BPName")
                    clsJournal.LogBy = UI.usUserApp.UserID

                    Dim clsJournalDet As VO.JournalDet
                    Dim clsJournalDetAll(1) As VO.JournalDet

                    '# Cash / Bank
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = dr.Item("CoAIDOfActiva")
                    clsJournalDet.CoAName = ""
                    clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                    clsJournalDet.CreditAmount = 0
                    clsJournalDetAll(0) = clsJournalDet

                    '# Save Buku Besar
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = dr.Item("CompanyID")
                    clsBukuBesar.ProgramID = dr.Item("ProgramID")
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("DPDate")
                    clsBukuBesar.COAIDParent = dr.Item("CoAIDOfActiva")
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Account Receivable -> Request Julia, Posting ke Piutang Dagang sehingga DP dimap ke Piutang Dagang
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPrepaidIncome
                    clsJournalDet.DebitAmount = 0
                    clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                    clsJournalDetAll(1) = clsJournalDet

                    '# Save Buku Besar
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = dr.Item("CompanyID")
                    clsBukuBesar.ProgramID = dr.Item("ProgramID")
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("DPDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsBukuBesar.COAIDChild = dr.Item("CoAIDOfActiva")
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    Dim strJournalID As String = BL.Journal.SaveData(IIf(clsJournal.ID.Trim = "", True, False), clsJournal, clsJournalDetAll, False)
                    '# End Of Generate Journal

                    '# Update Journal ID
                    If strJournalID.Trim <> "" Then DL.DownPayment.UpdateJournalID(dr.Item("ID"), strJournalID)
                Next
            End If
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Down Payment
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.DownPayment = DL.DownPayment.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

        Public Shared Sub UpdateJournalReferences(ByVal clsData As VO.DownPayment, ByVal strReferencesID As String)
            Dim clsSales As VO.Sales, clsReceive As VO.Receive, clsSalesService As VO.SalesService
            If clsData.DPType = VO.DownPayment.Type.Sales Then '# Sales

                clsSales = New VO.Sales
                clsSales = DL.Sales.GetDetail(strReferencesID)

                '# Delete Buku Besar Sales
                DL.BukuBesar.DeleteData(clsSales.ProgramID, clsSales.CompanyID, clsSales.ID)

                '# Update Journal of Sales
                BL.Sales.GenerateJournal(False, clsSales)
            ElseIf clsData.DPType = VO.DownPayment.Type.Purchase Then '# Receive

                clsReceive = New VO.Receive
                clsReceive = DL.Receive.GetDetail(strReferencesID)

                '# Delete Buku Besar Receive
                DL.BukuBesar.DeleteData(clsReceive.ProgramID, clsReceive.CompanyID, clsReceive.ID)

                '# Update Journal of Receive
                BL.Receive.GenerateJournal(False, clsReceive)
            ElseIf clsData.DPType = VO.DownPayment.Type.SalesService Then '# Sales Service

                clsSalesService = New VO.SalesService
                clsSalesService = DL.SalesService.GetDetail(strReferencesID)

                '# Delete Buku Besar Sales Service
                DL.BukuBesar.DeleteData(clsSalesService.ProgramID, clsSalesService.CompanyID, clsSalesService.ID)

                '# Update Journal of Sales Service
                BL.SalesService.GenerateJournal(False, clsSalesService)
            End If
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim dtData As DataTable = DL.DownPayment.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("DPDate")
            '    clsJournal.TotalAmount = dr.Item("TotalAmount")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(1) As VO.JournalDet
            '    Dim clsBukuBesar As New VO.BukuBesar

            '    If dr.Item("DPType") = VO.DownPayment.Type.Purchase Then
            '        '# Account Payable -> Request Julia, Posting ke Hutang Dagang sehingga DP dimap ke Hutang Dagang
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAdvancePayment
            '        clsJournalDet.DebitAmount = dr.Item("TotalAmount")
            '        clsJournalDet.CreditAmount = 0
            '        clsJournalDetAll(0) = clsJournalDet

            '        '# Save Buku Besar
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("DPDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
            '        clsBukuBesar.COAIDChild = dr.Item("CoAIDOfActiva")
            '        clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Cash / Bank
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = dr.Item("CoAIDOfActiva")
            '        clsJournalDet.CoAName = ""
            '        clsJournalDet.DebitAmount = 0
            '        clsJournalDet.CreditAmount = dr.Item("TotalAmount")
            '        clsJournalDetAll(1) = clsJournalDet

            '        '# Save Buku Besar
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("DPDate")
            '        clsBukuBesar.COAIDParent = dr.Item("CoAIDOfActiva")
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    Else
            '        '# Cash / Bank
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = dr.Item("CoAIDOfActiva")
            '        clsJournalDet.CoAName = ""
            '        clsJournalDet.DebitAmount = dr.Item("TotalAmount")
            '        clsJournalDet.CreditAmount = 0
            '        clsJournalDetAll(0) = clsJournalDet

            '        '# Save Buku Besar
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("DPDate")
            '        clsBukuBesar.COAIDParent = dr.Item("CoAIDOfActiva")
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
            '        clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Account Receivable -> Request Julia, Posting ke Piutang Dagang sehingga DP dimap ke Piutang Dagang
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPrepaidIncome
            '        clsJournalDet.DebitAmount = 0
            '        clsJournalDet.CreditAmount = dr.Item("TotalAmount")
            '        clsJournalDetAll(1) = clsJournalDet

            '        '# Save Buku Besar
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("DPDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
            '        clsBukuBesar.COAIDChild = dr.Item("CoAIDOfActiva")
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    End If
            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.DownPayment.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.DownPayment.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.DownPayment.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All, VO.DownPayment.Type.Purchase)
            dtData.Merge(DL.DownPayment.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All, VO.DownPayment.Type.Sales))
            For Each dr As DataRow In dtData.Rows
                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.DownPayment.UpdateJournalID(dr.Item("ID"), "")
                DL.DownPayment.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetailByDPID(ByVal strDPID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListDataDetailByDPID(strDPID)
        End Function

        Public Shared Function ListDataDetailByReferenceID(ByVal strReferenceID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListDataDetailByReferenceID(strReferenceID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strDPID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.DownPayment.ListDataStatus(strDPID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strDPID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                          ByVal strRemarks As String)
            Dim clsData As New VO.DownPaymentStatus
            clsData.ID = strDPID & "-" & Format(DL.DownPayment.GetMaxIDStatus(strDPID), "000")
            clsData.DPID = strDPID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.DownPayment.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

