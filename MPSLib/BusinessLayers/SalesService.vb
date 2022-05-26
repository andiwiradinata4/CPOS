Namespace BL
    Public Class SalesService

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                ByVal intIDStatus As Integer, ByVal strCustomerCode As String) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.SalesService.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus, strCustomerCode)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.SalesService.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SS" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.SalesService.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Private Shared Function GetNewSalesNo(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SS" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.SalesService.GetMaxSalesNo(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.SalesService, ByVal clsDataDetail() As VO.SalesServiceDet) As String
            Dim dtOutstandingDownPayment As New DataTable
            Dim decSalesAmount As Decimal = clsData.GrandTotal
            Dim clsDP As New VO.DownPaymentDet
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                '# Sales
                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.SalesDate)
                    If clsData.SalesNo.Trim = "" Then clsData.SalesNo = GetNewSalesNo(clsData.CompanyID, clsData.ProgramID, clsData.SalesDate)
                    If DL.SalesService.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.SalesDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    ElseIf DL.SalesService.DataExistsSalesNo(clsData.SalesNo) Then
                        Err.Raise(515, "", "Nomor " & clsData.SalesNo & " sudah terpakai")
                    End If
                Else
                    Dim strInvoiceID As String = DL.AccountReceivable.GetInvoiceID(clsData.ID)

                    If DL.SalesService.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf strInvoiceID.Trim <> "" Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                    ElseIf DL.SalesService.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    ElseIf DL.SalesService.DataExistsSalesNo(clsData.SalesNo, clsData.ID) Then
                        Err.Raise(515, "", "Nomor " & clsData.SalesNo & " sudah terpakai")
                    End If

                    '# Delete Down Payment Detail
                    Dim dtDownPaymentDetail As DataTable = DL.DownPayment.ListDataDetailByReferenceID(clsData.ID)
                    DL.DownPayment.DeleteDataByReferenceID(clsData.ID)

                    '# Calculate Down Payment Usage
                    For Each dr As DataRow In dtDownPaymentDetail.Rows
                        DL.DownPayment.UpdateTotalUsage(dr.Item("DPID"))
                    Next

                    '# Delete Buku Besar Sales Service
                    DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)

                    '# Delete Sales Service Detail
                    DL.SalesService.DeleteDataDetail(clsData.ID)
                End If

                DL.SalesService.SaveData(bolNew, clsData)

                '# Down Payment Sales
                dtOutstandingDownPayment = DL.DownPayment.ListDataForLookup(clsData.CompanyID, clsData.ProgramID, clsData.BPID, VO.DownPayment.Type.SalesService, 0)
                With dtOutstandingDownPayment
                    For i As Integer = 0 To .Rows.Count - 1
                        If decSalesAmount = 0 Then Exit For
                        If decSalesAmount > 0 Then
                            If .Rows(i).Item("TotalAmount") >= decSalesAmount Then
                                clsDP = New VO.DownPaymentDet
                                clsDP.DPID = .Rows(i).Item("ID")
                                clsDP.ID = clsDP.DPID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsDP.DPID), "000")
                                clsDP.ReferenceID = clsData.ID
                                clsDP.TotalAmount = decSalesAmount
                                clsDP.Remarks = ""
                                DL.DownPayment.SaveDataDetail(clsDP)
                                decSalesAmount = 0
                            Else
                                clsDP = New VO.DownPaymentDet
                                clsDP.DPID = .Rows(i).Item("ID")
                                clsDP.ID = clsDP.DPID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsDP.DPID), "000")
                                clsDP.ReferenceID = clsData.ID
                                clsDP.TotalAmount = .Rows(i).Item("TotalAmount")
                                clsDP.Remarks = ""
                                DL.DownPayment.SaveDataDetail(clsDP)
                                decSalesAmount = decSalesAmount - .Rows(i).Item("TotalAmount")
                            End If
                        End If

                        '# Update Total Usage Down Payment
                        DL.DownPayment.UpdateTotalUsage(.Rows(i).Item("ID"))

                        'Re-Calculate Sales Total Down Payment
                        DL.SalesService.RecalculateTotalDownPayment(clsData.ID)
                    Next
                End With

                '# Save Data Detail
                For Each clsDetail As VO.SalesServiceDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.SalesService.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.SalesServiceID = clsData.ID
                    DL.SalesService.SaveDataDetail(clsDetail)
                Next

                Dim clsOverDownPayment As VO.DownPayment = DL.DownPayment.GetDetailOverTotalAmount(clsData.ID)
                If Not clsOverDownPayment.ID Is Nothing Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan Panjar nomor " & clsData.ID & " telah dipakai melebihi nilai total panjar")
                End If

                '# Save Data Status Sales Service
                BL.Sales.SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                '# Journal Sales Service
                BL.SalesService.GenerateJournal(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.SalesNo
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.SalesService
            BL.Server.ServerDefault()
            Return DL.SalesService.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.SalesService)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim strInvoiceID As String = DL.AccountReceivable.GetInvoiceID(clsData.ID)

                If DL.SalesService.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                ElseIf DL.SalesService.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    '# Delete Down Payment Detail
                    Dim dtDownPaymentDetail As DataTable = DL.DownPayment.ListDataDetailByReferenceID(clsData.ID)
                    DL.DownPayment.DeleteDataByReferenceID(clsData.ID)

                    'Re-Calculate Sales Total Down Payment
                    DL.SalesService.RecalculateTotalDownPayment(clsData.ID)

                    '# Delete Buku Besar Sales & Journal
                    BL.SalesService.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)

                    '# Delete Sales
                    DL.SalesService.DeleteData(clsData.ID)

                    '# Save Data Status
                    BL.Sales.SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Update Down Payment Allocation Amount
                    For Each dr As DataRow In dtDownPaymentDetail.Rows
                        Dim clsDownPayment As VO.DownPayment = DL.DownPayment.GetDetail(dr.Item("DPID"))
                        BL.DownPayment.SaveData(False, clsDownPayment)
                    Next
                End If
                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim dtData As DataTable = DL.SalesService.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("SalesDate")
            '    clsJournal.TotalAmount = dr.Item("GrandTotal")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll() As VO.JournalDet = Nothing
            '    Dim clsBukuBesar As New VO.BukuBesar
            '    Dim intIdx As Integer = 0
            '    If dr.Item("TotalDownPayment") > 0 And dr.Item("GrandTotal") - dr.Item("TotalDownPayment") > 0 Then
            '        ReDim clsJournalDetAll(2)
            '    Else
            '        ReDim clsJournalDetAll(1)
            '    End If

            '    '# Account Receivable
            '    If dr.Item("GrandTotal") - dr.Item("TotalDownPayment") > 0 Then
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
            '        clsJournalDet.DebitAmount = dr.Item("GrandTotal") - dr.Item("TotalDownPayment")
            '        clsJournalDet.CreditAmount = 0
            '        clsJournalDetAll(intIdx) = clsJournalDet
            '        intIdx += 1

            '        '# Save Buku Account Receivable
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("SalesDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '        Select Case dr.Item("ServiceType")
            '            Case VO.SalesService.Type.RentalAlatBerat
            '                clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalAlatBerat
            '            Case VO.SalesService.Type.RentalTruk
            '                clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalTruk
            '        End Select
            '        clsBukuBesar.DebitAmount = dr.Item("GrandTotal") - dr.Item("TotalDownPayment")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Save Buku Besar Revenue
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("SalesDate")
            '        Select Case dr.Item("ServiceType")
            '            Case VO.SalesService.Type.RentalAlatBerat
            '                clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalAlatBerat
            '            Case VO.SalesService.Type.RentalTruk
            '                clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalTruk
            '        End Select
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dr.Item("TotalPrice") - dr.Item("TotalDownPayment")
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    End If

            '    '# Down Payment / Account Receivable -> Request Julia, Posting ke Piutang Dagang sehingga DP dimap ke Piutang Dagang
            '    If dr.Item("TotalDownPayment") > 0 Then
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPrepaidIncome
            '        clsJournalDet.DebitAmount = dr.Item("TotalDownPayment")
            '        clsJournalDet.CreditAmount = 0
            '        clsJournalDetAll(intIdx) = clsJournalDet
            '        intIdx += 1

            '        '# Save Buku Account Receivable
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("SalesDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
            '        Select Case dr.Item("ServiceType")
            '            Case VO.SalesService.Type.RentalAlatBerat
            '                clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalAlatBerat
            '            Case VO.SalesService.Type.RentalTruk
            '                clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalTruk
            '        End Select
            '        clsBukuBesar.DebitAmount = dr.Item("TotalDownPayment")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Save Buku Besar Revenue
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("SalesDate")
            '        Select Case dr.Item("ServiceType")
            '            Case VO.SalesService.Type.RentalAlatBerat
            '                clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalAlatBerat
            '            Case VO.SalesService.Type.RentalTruk
            '                clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalTruk
            '        End Select
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dr.Item("TotalDownPayment")
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    End If

            '    '# Revenue 
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    Select Case dr.Item("ServiceType")
            '        Case VO.SalesService.Type.RentalAlatBerat
            '            clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalAlatBerat
            '        Case VO.SalesService.Type.RentalTruk
            '            clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenueRentalTruk
            '    End Select
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalPrice")
            '    clsJournalDetAll(intIdx) = clsJournalDet

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.SalesService.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.SalesService.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    BL.Sales.SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.SalesService.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                BL.COGS.UnpostCOGS(intCompanyID, intProgramID, dr.Item("ID"))
                DL.StockOut.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.SalesService.UpdateJournalID(dr.Item("ID"), "")
                DL.SalesService.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                BL.Sales.SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

        Public Shared Function PrintBonFaktur(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesService.PrintBonFaktur(strID)
        End Function

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal clsData As VO.SalesService)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Journal Sales Service
            Dim dtSalesServiceJournal As DataTable = DL.SalesService.ListDataGenerateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            For Each dr As DataRow In dtSalesServiceJournal.Rows
                '# Generate Journal
                clsJournal = New VO.Journal
                clsJournal.CompanyID = clsData.CompanyID
                clsJournal.ProgramID = clsData.ProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("SalesDate")
                clsJournal.TotalAmount = dr.Item("GrandTotal")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = New VO.SalesService().JournalRemarks(clsData.ServiceType)
                clsJournal.CashOrBankInfo = VO.SalesService.JournalCashOrBankInfo
                clsJournal.PaymentTo = clsData.BPName
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll() As VO.JournalDet = Nothing
                Dim intIdx As Integer = 0, intTotalIdx As Integer = 0

                If dr.Item("TotalPPN") > 0 Then
                    intTotalIdx += 1
                End If

                If dr.Item("TotalDownPayment") > 0 Then
                    intTotalIdx += 1
                End If

                If dr.Item("GrandTotal") - dr.Item("TotalPPN") > dr.Item("TotalDownPayment") Then
                    intTotalIdx += 1
                End If

                ReDim clsJournalDetAll(intTotalIdx)

                '# Account Receivable
                If dr.Item("GrandTotal") - dr.Item("TotalPPN") > dr.Item("TotalDownPayment") Then
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
                    clsJournalDet.DebitAmount = dr.Item("GrandTotal") - dr.Item("TotalPPN") - dr.Item("TotalDownPayment")
                    clsJournalDet.CreditAmount = 0
                    clsJournalDetAll(intIdx) = clsJournalDet
                    intIdx += 1

                    '# Save Buku Account Receivable
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("SalesDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                    clsBukuBesar.DebitAmount = dr.Item("GrandTotal") - dr.Item("TotalPPN") - dr.Item("TotalDownPayment")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Save Buku Besar Revenue
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("SalesDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dr.Item("GrandTotal") - dr.Item("TotalPPN") - dr.Item("TotalDownPayment")
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)
                End If

                '# Down Payment / Account Receivable -> Request Julia, Posting ke Piutang Dagang sehingga DP dimap ke Piutang Dagang
                If dr.Item("TotalDownPayment") > 0 Then
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofPrepaidIncome
                    clsJournalDet.DebitAmount = IIf(dr.Item("GrandTotal") - dr.Item("TotalPPN") < dr.Item("TotalDownPayment"), dr.Item("GrandTotal") - dr.Item("TotalPPN"), dr.Item("TotalDownPayment"))
                    clsJournalDet.CreditAmount = 0
                    clsJournalDetAll(intIdx) = clsJournalDet
                    intIdx += 1

                    '# Save Buku Account Receivable
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("SalesDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                    clsBukuBesar.DebitAmount = IIf(dr.Item("GrandTotal") - dr.Item("TotalPPN") < dr.Item("TotalDownPayment"), dr.Item("GrandTotal") - dr.Item("TotalPPN"), dr.Item("TotalDownPayment"))
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Save Buku Besar Revenue
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("SalesDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofPrepaidIncome
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = IIf(dr.Item("GrandTotal") - dr.Item("TotalPPN") < dr.Item("TotalDownPayment"), dr.Item("GrandTotal") - dr.Item("TotalPPN"), dr.Item("TotalDownPayment"))
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)
                End If

                '# Tax
                If dr.Item("TotalPPN") > 0 Then
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofSalesTax
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofSalesTax
                    clsJournalDet.DebitAmount = dr.Item("TotalPPN")
                    clsJournalDet.CreditAmount = 0
                    clsJournalDetAll(intIdx) = clsJournalDet
                    intIdx += 1

                    '# Save Buku Account Receivable
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("SalesDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofSalesTax
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                    clsBukuBesar.DebitAmount = dr.Item("TotalPPN")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Save Buku Besar Revenue
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("SalesDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofSalesTax
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dr.Item("TotalPPN")
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)
                End If

                '# Revenue  
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("GrandTotal")
                clsJournalDetAll(intIdx) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(bolNew, clsJournal, clsJournalDetAll, False)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.SalesService.UpdateJournalID(dr.Item("ID"), strJournalID)
            Next
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Sales Service
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.SalesService = DL.SalesService.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesService.ListDataDetail(strSalesID)
        End Function

#End Region

    End Class

End Namespace

