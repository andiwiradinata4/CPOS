Namespace BL

    Public Class Sales

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, ByVal strCustomerCode As String, _
                                        ByVal strSupplierCode As String) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Sales.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus, strCustomerCode, strSupplierCode)
        End Function

        Public Shared Function ListDataRemarks(ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataRemarks(intCompanyID, intProgramID)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SO" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Sales.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Private Shared Function GetNewSalesNo(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SO" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Sales.GetMaxSalesNo(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.Sales, ByVal clsReceive As VO.Receive) As String
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                BL.Sales.SaveData(bolNew, clsData, clsReceive)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.SalesNo
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Sales, ByVal clsReceive As VO.Receive) As String
            Dim dtOutstandingDownPayment As New DataTable
            Dim decSalesAmount As Decimal = clsData.TotalPrice
            Dim decReceiveAmount As Decimal = clsReceive.TotalPrice1
            Dim clsDP As New VO.DownPaymentDet

            '# Sales
            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.SalesDate)
                If clsData.SalesNo.Trim = "" Then clsData.SalesNo = GetNewSalesNo(clsData.CompanyID, clsData.ProgramID, clsData.SalesDate)
                clsReceive.ReferencesID = clsData.ID

                If DL.Sales.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                ElseIf Format(clsData.SalesDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                ElseIf DL.Sales.DataExistsSalesNo(clsData.SalesNo) Then
                    Err.Raise(515, "", "Nomor " & clsData.SalesNo & " sudah terpakai")
                End If
            Else
                Dim strReturnID As String = DL.SalesReturn.GetReturnID(clsData.ID)
                Dim strInvoiceID As String = DL.AccountReceivable.GetInvoiceID(clsData.ID)

                If DL.Sales.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf strReturnID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                ElseIf DL.Sales.IsSplitReceive(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses split data pembelian")
                ElseIf DL.Sales.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                ElseIf DL.Sales.DataExistsSalesNo(clsData.SalesNo, clsData.ID) Then
                    Err.Raise(515, "", "Nomor " & clsData.SalesNo & " sudah terpakai")
                End If

                '# ---- Sales ----
                '# Delete Down Payment Detail
                Dim dtDownPaymentDetailSales As DataTable = DL.DownPayment.ListDataDetailByReferenceID(clsData.ID)
                DL.DownPayment.DeleteDataByReferenceID(clsData.ID)

                '# Calculate Down Payment Usage
                For Each dr As DataRow In dtDownPaymentDetailSales.Rows
                    DL.DownPayment.UpdateTotalUsage(dr.Item("DPID"))
                Next

                '# Delete Buku Besar Sales
                DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)
            End If

            DL.Sales.SaveData(bolNew, clsData)

            '# Down Payment Sales
            dtOutstandingDownPayment = DL.DownPayment.ListDataForLookup(clsData.CompanyID, clsData.ProgramID, clsData.BPID, VO.DownPayment.Type.Sales, clsReceive.BPID)
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
                    DL.Sales.RecalculateTotalDownPayment(clsData.ID)
                Next
            End With

            Dim clsOverDownPayment As VO.DownPayment = DL.DownPayment.GetDetailOverTotalAmount(clsData.ID)
            If Not clsOverDownPayment.ID Is Nothing Then
                Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan Panjar nomor " & clsData.ID & " telah dipakai melebihi nilai total panjar")
            End If

            '# Save Data Status Sales
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

            '# ---- Receive ----
            BL.Receive.SaveData(bolNew, clsReceive)

            '# Calculate Arrival Usage
            DL.Sales.CalculateArrivalUsage(clsData.ID)

            '# Journal Sales
            BL.Sales.GenerateJournal(bolNew, clsData)

            Return clsData.SalesNo
        End Function

        Public Shared Function ImportData(ByVal clsDataAll() As VO.Sales) As Boolean
            Dim bolSuccess As Boolean = True
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                For Each clsData As VO.Sales In clsDataAll
                    '# Receive
                    Dim clsReceive As New VO.Receive
                    clsReceive.ProgramID = clsData.ProgramID
                    clsReceive.CompanyID = clsData.CompanyID
                    clsReceive.ID = ""
                    clsReceive.ReceiveNo = ""
                    clsReceive.BPID = clsData.SupplierID
                    clsReceive.BPName = clsData.SupplierName
                    clsReceive.ReceiveDate = clsData.SalesDate
                    clsReceive.PaymentTerm = clsData.PaymentTerm
                    clsReceive.DueDate = clsData.DueDate
                    clsReceive.DriverName = clsData.DriverName
                    clsReceive.PlatNumber = clsData.PlatNumber
                    clsReceive.Remarks = clsData.Remarks
                    clsReceive.ItemID = clsData.ItemID
                    clsReceive.ItemCode = ""
                    clsReceive.ItemName = clsData.ItemName
                    clsReceive.UOMID = clsData.UOMID
                    clsReceive.ArrivalBrutto = clsData.ArrivalBrutto
                    clsReceive.ArrivalTarra = clsData.ArrivalTarra
                    clsReceive.ArrivalNettoBefore = clsData.ArrivalNettoBefore
                    clsReceive.ArrivalDeduction = clsData.ArrivalDeduction
                    clsReceive.ArrivalNettoAfter = clsData.ArrivalNettoAfter
                    clsReceive.Price1 = clsData.PurchasePrice1
                    clsReceive.TotalPrice1 = clsReceive.Price1 * clsReceive.ArrivalNettoAfter
                    clsReceive.Price2 = clsData.PurchasePrice2
                    clsReceive.TotalPrice2 = clsReceive.Price2 * clsReceive.ArrivalNettoAfter
                    clsReceive.DONumber = ""
                    clsReceive.SPBNumber = ""
                    clsReceive.SegelNumber = ""
                    clsReceive.Specification = ""
                    clsReceive.IDStatus = VO.Status.Values.Draft
                    clsReceive.LogBy = MPSLib.UI.usUserApp.UserID
                    clsReceive.JournalID = ""
                    clsReceive.ReferencesID = ""
                    clsReceive.Tolerance = clsData.Tolerance
                    clsReceive.JournalID = ""

                    BL.Sales.SaveData(True, clsData, clsReceive)
                Next

                'For Each clsData As VO.Sales In clsDataAll
                '    Dim dtOutstandingDownPayment As New DataTable
                '    Dim decSalesAmount As Decimal = clsData.TotalPrice
                '    Dim clsDP As New VO.DownPaymentDet

                '    '# Sales
                '    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.SalesDate)
                '    clsData.SalesNo = clsData.ID
                '    If DL.Sales.DataExists(clsData.ID) Then
                '        Err.Raise(515, "", "ID sudah ada sebelumnya")
                '    ElseIf Format(clsData.SalesDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                '        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                '    End If

                '    DL.Sales.SaveData(True, clsData)

                '    '# Save Data Status Sales
                '    SaveDataStatus(clsData.ID, "BARU", clsData.LogBy, clsData.Remarks)

                '    '# Receive
                '    Dim clsReceive As New VO.Receive
                '    clsReceive.ProgramID = clsData.ProgramID
                '    clsReceive.CompanyID = clsData.CompanyID
                '    clsReceive.ID = BL.Receive.GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.SalesDate)
                '    clsReceive.ReceiveNo = clsReceive.ID
                '    clsReceive.BPID = clsData.SupplierID
                '    clsReceive.BPName = clsData.SupplierName
                '    clsReceive.ReceiveDate = clsData.SalesDate
                '    clsReceive.PaymentTerm = clsData.PaymentTerm
                '    clsReceive.DueDate = clsData.DueDate
                '    clsReceive.DriverName = clsData.DriverName
                '    clsReceive.PlatNumber = clsData.PlatNumber
                '    clsReceive.Remarks = clsData.Remarks
                '    clsReceive.ItemID = clsData.ItemID
                '    clsReceive.ItemCode = ""
                '    clsReceive.ItemName = clsData.ItemName
                '    clsReceive.UOMID = clsData.UOMID
                '    clsReceive.ArrivalBrutto = clsData.ArrivalBrutto
                '    clsReceive.ArrivalTarra = clsData.ArrivalTarra
                '    clsReceive.ArrivalNettoBefore = clsData.ArrivalNettoBefore
                '    clsReceive.ArrivalDeduction = clsData.ArrivalDeduction
                '    clsReceive.ArrivalNettoAfter = clsData.ArrivalNettoAfter
                '    clsReceive.Price1 = clsData.PurchasePrice1
                '    clsReceive.TotalPrice1 = clsReceive.Price1 * clsReceive.ArrivalNettoAfter
                '    clsReceive.Price2 = clsData.PurchasePrice2
                '    clsReceive.TotalPrice2 = clsReceive.Price2 * clsReceive.ArrivalNettoAfter
                '    clsReceive.DONumber = ""
                '    clsReceive.SPBNumber = ""
                '    clsReceive.SegelNumber = ""
                '    clsReceive.Specification = ""
                '    clsReceive.IDStatus = VO.Status.Values.Draft
                '    clsReceive.LogBy = MPSLib.UI.usUserApp.UserID
                '    clsReceive.JournalID = ""
                '    clsReceive.ReferencesID = clsData.ID
                '    clsReceive.Tolerance = clsData.Tolerance
                '    DL.Receive.SaveData(True, clsReceive)

                '    '# Save Data Status Receive
                '    BL.Receive.SaveDataStatus(clsReceive.ID, "BARU", clsReceive.LogBy, clsReceive.Remarks)

                '    '# Calculate Arrival Usage
                '    DL.Sales.CalculateArrivalUsage(clsData.ID)
                'Next
                DL.SQL.CommitTransaction()
            Catch ex As Exception
                bolSuccess = False
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolSuccess
        End Function

        Public Shared Function SplitDataReceive(ByVal clsData As VO.Sales, ByVal clsReceive() As VO.Receive) As Boolean
            Dim bolReturn As Boolean = False
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                '# Checking Already Split Receive
                If DL.Sales.IsSplitReceive(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dilakukan split pembelian. Dikarenakan data telah dilakukan split pembelian sebelumnya.")
                End If

                '# Save Data Sales Supplier
                For Each clsItem As VO.Receive In clsReceive
                    BL.Receive.SaveData(True, clsItem)
                Next

                DL.Sales.SetIsSplitReceive(clsData.ID, True)
                DL.Sales.CalculateArrivalUsage(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "SPLIT PEMBELIAN", clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
                bolReturn = True
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolReturn
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Sales
            BL.Server.ServerDefault()
            Return DL.Sales.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim strReturnID As String = DL.SalesReturn.GetReturnID(clsData.ID)
                Dim strInvoiceID As String = DL.AccountReceivable.GetInvoiceID(clsData.ID)

                If DL.Sales.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf strReturnID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                ElseIf DL.Sales.IsSplitReceive(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses split data pembelian")
                ElseIf DL.Sales.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    '# ---- Sales ----
                    '# Delete Down Payment Detail
                    Dim dtDownPaymentDetail As DataTable = DL.DownPayment.ListDataDetailByReferenceID(clsData.ID)
                    DL.DownPayment.DeleteDataByReferenceID(clsData.ID)

                    'Re-Calculate Sales Total Down Payment
                    DL.Sales.RecalculateTotalDownPayment(clsData.ID)

                    '# Delete Buku Besar Sales & Journal
                    BL.Sales.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)

                    '# Delete Sales
                    DL.Sales.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Update Down Payment Allocation Amount
                    For Each dr As DataRow In dtDownPaymentDetail.Rows
                        Dim clsDownPayment As VO.DownPayment = DL.DownPayment.GetDetail(dr.Item("DPID"))
                        BL.DownPayment.SaveData(False, clsDownPayment)
                    Next

                    '# ---- Receive ----
                    BL.Receive.DeleteData(DL.Receive.GetDetail(clsData.ReceiveID))
                End If
                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Function ListDataSplitReceive(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataSplitReceive(strSalesID)
        End Function

        Public Shared Sub PrintFakturPenjualan(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.Sales.PrintFakturPenjualan(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "PRINT FAKTUR PENJUALAN", clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try

        End Sub

        Public Shared Function ListDataFakturPenjualan(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataFakturPenjualan(strID)
        End Function

        Public Shared Function ListDataOutstandingReturn(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataOutstandingReturn(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal clsData As VO.Sales)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Journal Sales
            Dim dtSalesJournal As DataTable = DL.Sales.ListDataGenerateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            For Each dr As DataRow In dtSalesJournal.Rows
                '# Generate Journal
                clsJournal = New VO.Journal
                clsJournal.CompanyID = clsData.CompanyID
                clsJournal.ProgramID = clsData.ProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("SalesDate")
                clsJournal.TotalAmount = dr.Item("TotalPrice")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = VO.Sales.JournalRemarks
                clsJournal.CashOrBankInfo = VO.Sales.JournalCashOrBankInfo
                clsJournal.PaymentTo = clsData.BPName
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll() As VO.JournalDet = Nothing
                Dim intIdx As Integer = 0
                If dr.Item("TotalDownPayment") > 0 And dr.Item("TotalPrice") - dr.Item("TotalDownPayment") > 0 Then
                    ReDim clsJournalDetAll(2)
                Else
                    ReDim clsJournalDetAll(1)
                End If

                '# Account Receivable
                If dr.Item("TotalPrice") - dr.Item("TotalDownPayment") > 0 Then
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
                    clsJournalDet.DebitAmount = dr.Item("TotalPrice") - dr.Item("TotalDownPayment")
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
                    clsBukuBesar.DebitAmount = dr.Item("TotalPrice") - dr.Item("TotalDownPayment")
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
                    clsBukuBesar.CreditAmount = dr.Item("TotalPrice") - dr.Item("TotalDownPayment")
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
                    clsJournalDet.DebitAmount = dr.Item("TotalDownPayment")
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
                    clsBukuBesar.DebitAmount = dr.Item("TotalDownPayment")
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
                    clsBukuBesar.CreditAmount = dr.Item("TotalDownPayment")
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
                clsJournalDet.CreditAmount = dr.Item("TotalPrice")
                clsJournalDetAll(intIdx) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(bolNew, clsJournal, clsJournalDetAll, False)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.Sales.UpdateJournalID(dr.Item("ID"), strJournalID)
            Next
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Sales
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.Sales = DL.Sales.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim dtData As DataTable = DL.Sales.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    Dim decCOGS As Decimal = BL.COGS.CalculateCOGS(intCompanyID, intProgramID, _
            '                                                   dr.Item("ItemID"), dr.Item("ArrivalNettoAfter"), _
            '                                                   dr.Item("ID"), dr.Item("SalesDate"))
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("SalesDate")
            '    clsJournal.TotalAmount = dr.Item("TotalPrice")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll() As VO.JournalDet = Nothing
            '    Dim clsBukuBesar As New VO.BukuBesar
            '    Dim intIdx As Integer = 0
            '    If dr.Item("TotalDownPayment") > 0 And dr.Item("TotalPrice") - dr.Item("TotalDownPayment") > 0 Then
            '        ReDim clsJournalDetAll(2)
            '    Else
            '        ReDim clsJournalDetAll(1)
            '    End If

            '    ''# COGS
            '    'clsJournalDet = New VO.JournalDet
            '    'clsJournalDet.JournalID = dr.Item("JournalID")
            '    'clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofCOGS
            '    'clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofCOGS
            '    'clsJournalDet.DebitAmount = decCOGS
            '    'clsJournalDet.CreditAmount = 0
            '    'clsJournalDetAll(0) = clsJournalDet

            '    ''# Stock
            '    'clsJournalDet = New VO.JournalDet
            '    'clsJournalDet.JournalID = dr.Item("JournalID")
            '    'clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '    'clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
            '    'clsJournalDet.DebitAmount = 0
            '    'clsJournalDet.CreditAmount = decCOGS
            '    'clsJournalDetAll(1) = clsJournalDet

            '    '# Account Receivable
            '    If dr.Item("TotalPrice") - dr.Item("TotalDownPayment") > 0 Then
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
            '        clsJournalDet.DebitAmount = dr.Item("TotalPrice") - dr.Item("TotalDownPayment")
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
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
            '        clsBukuBesar.DebitAmount = dr.Item("TotalPrice") - dr.Item("TotalDownPayment")
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
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
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
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
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
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
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
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalPrice")
            '    clsJournalDetAll(intIdx) = clsJournalDet

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.Sales.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.Sales.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.Sales.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                BL.COGS.UnpostCOGS(intCompanyID, intProgramID, dr.Item("ID"))
                DL.StockOut.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.Sales.UpdateJournalID(dr.Item("ID"), "")
                DL.Sales.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Supplier"

        Public Shared Function ListDataSupplier(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataSupplier(strSalesID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Sales.ListDataStatus(strSalesID)
        End Function

        Public Shared Sub SaveDataStatus(ByVal strSalesID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.SalesStatus
            clsData.ID = strSalesID & "-" & Format(DL.Sales.GetMaxIDStatus(strSalesID), "000")
            clsData.SalesID = strSalesID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Sales.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace