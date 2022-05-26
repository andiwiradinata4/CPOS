Namespace BL

    Public Class Receive

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, ByVal strSupplierCode As String) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Receive.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus, strSupplierCode)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "RV" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Receive.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function GetNewReceiveNo(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "RV" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Receive.GetMaxReceiveNo(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.Receive) As String
            Dim strID As String = ""
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                strID = SaveData(bolNew, clsData)

                DL.Sales.CalculateArrivalUsage(clsData.ReferencesID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return strID
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Receive) As String
            Dim dtOutstandingDownPayment As New DataTable
            Dim decReceiveAmount As Decimal = clsData.TotalPrice1
            Dim clsDP As New VO.DownPaymentDet
            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.ReceiveDate)
                If clsData.ReceiveNo.Trim = "" Then clsData.ReceiveNo = GetNewReceiveNo(clsData.CompanyID, clsData.ProgramID, clsData.ReceiveDate)
                If DL.Receive.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                ElseIf Format(clsData.ReceiveDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                ElseIf DL.Receive.DataExistsReceiveNo(clsData.ReceiveNo) Then
                    Err.Raise(515, "", "Nomor " & clsData.ReceiveNo & " sudah terpakai")
                End If
            Else
                If clsData.ReceiveNo Is Nothing Then clsData.ReceiveNo = DL.Receive.GetReceiveNo(clsData.ID)
                If clsData.ReceiveNo.Trim = "" Then clsData.ReceiveNo = clsData.ID

                Dim strReturnID As String = DL.ReceiveReturn.GetReturnID(clsData.ID)
                Dim strInvoiceID As String = DL.AccountPayable.GetInvoiceID(clsData.ID)
                If DL.Receive.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf strReturnID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                ElseIf strInvoiceID.Trim <> "" Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses pembayaran dengan nomor " & strInvoiceID)
                ElseIf DL.Receive.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                ElseIf DL.Receive.DataExistsReceiveNo(clsData.ReceiveNo, clsData.ID) Then
                    Err.Raise(515, "", "Nomor " & clsData.ReceiveNo & " sudah terpakai")
                End If

                '# Delete Down Payment Detail
                Dim dtDownPaymentDetail As DataTable = DL.DownPayment.ListDataDetailByReferenceID(clsData.ID)
                DL.DownPayment.DeleteDataByReferenceID(clsData.ID)

                '# Calculate Down Payment Usage
                For Each dr As DataRow In dtDownPaymentDetail.Rows
                    DL.DownPayment.UpdateTotalUsage(dr.Item("DPID"))
                Next

                '# Delete Buku Besar Receive
                DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)
            End If

            DL.Receive.SaveData(bolNew, clsData)

            '# Down Payment Receive
            dtOutstandingDownPayment = DL.DownPayment.ListDataForLookup(clsData.CompanyID, clsData.ProgramID, clsData.BPID, VO.DownPayment.Type.Purchase, 0)
            With dtOutstandingDownPayment
                For i As Integer = 0 To .Rows.Count - 1
                    If decReceiveAmount = 0 Then Exit For
                    If decReceiveAmount > 0 Then
                        If .Rows(i).Item("TotalAmount") >= decReceiveAmount Then
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = .Rows(i).Item("ID")
                            clsDP.ID = clsDP.DPID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsDP.DPID), "000")
                            clsDP.ReferenceID = clsData.ID
                            clsDP.TotalAmount = decReceiveAmount
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)
                            decReceiveAmount = 0
                        Else
                            clsDP = New VO.DownPaymentDet
                            clsDP.DPID = .Rows(i).Item("ID")
                            clsDP.ID = clsDP.DPID & "-" & Format(DL.DownPayment.GetMaxIDDetail(clsDP.DPID), "000")
                            clsDP.ReferenceID = clsData.ID
                            clsDP.TotalAmount = .Rows(i).Item("TotalAmount")
                            clsDP.Remarks = ""
                            DL.DownPayment.SaveDataDetail(clsDP)
                            decReceiveAmount = decReceiveAmount - .Rows(i).Item("TotalAmount")
                        End If
                    End If

                    '# Update Total Usage Down Payment
                    DL.DownPayment.UpdateTotalUsage(.Rows(i).Item("ID"))

                    'Re-Calculate Receive Total Down Payment
                    DL.Receive.RecalculateTotalDownPayment(clsData.ID)
                Next
            End With

            Dim clsOverDownPayment As VO.DownPayment = DL.DownPayment.GetDetailOverTotalAmount(clsData.ID)
            If Not clsOverDownPayment.ID Is Nothing Then
                Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan Panjar nomor " & clsData.ID & " telah dipakai melebihi nilai total panjar")
            End If

            '# Save Data Status
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

            '# Journal Receive
            BL.Receive.GenerateJournal(bolNew, clsData)
            Return clsData.ReceiveNo
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Receive
            BL.Server.ServerDefault()
            Return DL.Receive.GetDetail(strID)
        End Function

        Public Shared Sub DeleteDataDefault(ByVal clsData As VO.Receive)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DeleteData(clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal clsData As VO.Receive)
            Dim strReturnID As String = DL.ReceiveReturn.GetReturnID(clsData.ID)
            Dim strInvoiceID As String = DL.AccountPayable.GetInvoiceID(clsData.ID)

            If DL.Receive.IsDeleted(clsData.ID) Then
                Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
            ElseIf strReturnID.Trim <> "" Then
                Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
            ElseIf strInvoiceID.Trim <> "" Then
                Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses pembayaran dengan nomor " & strInvoiceID)
            ElseIf DL.Receive.IsPostedGL(clsData.ID) Then
                Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
            Else

                '# Delete Down Payment Detail
                Dim dtDownPaymentDetail As DataTable = DL.DownPayment.ListDataDetailByReferenceID(clsData.ID)
                DL.DownPayment.DeleteDataByReferenceID(clsData.ID)

                'Re-Calculate Receive Total Down Payment
                DL.Receive.RecalculateTotalDownPayment(clsData.ID)

                '# Delete Buku Besar Receive & Journal
                BL.Receive.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)

                '# Delete Receive
                DL.Receive.DeleteData(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                '# Re-Calculate Arrival Usage
                DL.Sales.CalculateArrivalUsage(clsData.ReferencesID)

                '# Update Down Payment Allocation Amount
                For Each dr As DataRow In dtDownPaymentDetail.Rows
                    Dim clsDownPayment As VO.DownPayment = DL.DownPayment.GetDetail(dr.Item("DPID"))
                    BL.DownPayment.SaveData(False, clsDownPayment)
                Next
            End If
        End Sub

        Public Shared Sub PrintSlipTimbang(ByVal clsData As VO.Sales)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.Receive.PrintSlipTimbang(clsData.ID)

                '# Save Data Status
                SaveDataStatus(clsData.ID, "PRINT SLIP TIMBANGAN", clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Function ListDataSlipTimbang(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataSlipTimbang(strID)
        End Function

        Public Shared Function ListDataOutstandingReturn(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataOutstandingReturn(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal clsData As VO.Receive)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Journal Receive
            Dim dtReceiveJournal As DataTable = DL.Receive.ListDataGenerateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            For Each dr As DataRow In dtReceiveJournal.Rows
                '# Generate Journal
                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll() As VO.JournalDet = Nothing
                Dim intIdx As Integer = 0
                If dr.Item("TotalDownPayment") > 0 And dr.Item("TotalPrice1") - dr.Item("TotalDownPayment") > 0 Then
                    ReDim clsJournalDetAll(2)
                Else
                    ReDim clsJournalDetAll(1)
                End If

                clsJournal = New VO.Journal
                clsJournal.CompanyID = clsData.CompanyID
                clsJournal.ProgramID = clsData.ProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("ReceiveDate")
                clsJournal.TotalAmount = dr.Item("TotalPrice1")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = VO.Receive.JournalRemarks
                clsJournal.CashOrBankInfo = VO.Receive.JournalCashOrBankInfo
                clsJournal.PaymentTo = clsData.BPName
                clsJournal.LogBy = UI.usUserApp.UserID

                '# Stock -> Request Julia, Posting ke Pembelian TBS
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
                clsJournalDet.DebitAmount = dr.Item("TotalPrice1")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(intIdx) = clsJournalDet
                intIdx += 1

                '# Account Payable
                If dr.Item("TotalPrice1") - dr.Item("TotalDownPayment") > 0 Then
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
                    clsJournalDet.DebitAmount = 0
                    clsJournalDet.CreditAmount = dr.Item("TotalPrice1") - dr.Item("TotalDownPayment")
                    clsJournalDetAll(intIdx) = clsJournalDet
                    intIdx += 1

                    '# Save Buku Besar Stock
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                    clsBukuBesar.DebitAmount = dr.Item("TotalPrice1") - dr.Item("TotalDownPayment")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Save Buku Besar Account Payable
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dr.Item("TotalPrice1") - dr.Item("TotalDownPayment")
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)
                End If

                '# Down Payment / Advance Payment
                If dr.Item("TotalDownPayment") > 0 Then
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
                    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAdvancePayment
                    clsJournalDet.DebitAmount = 0
                    clsJournalDet.CreditAmount = dr.Item("TotalDownPayment")
                    clsJournalDetAll(intIdx) = clsJournalDet

                    '# Save Buku Besar Stock
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
                    clsBukuBesar.DebitAmount = dr.Item("TotalDownPayment")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Save Buku Besar Down Payment / Advance Payment
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
                    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
                    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofStock
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dr.Item("TotalDownPayment")
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)
                End If

                Dim strJournalID As String = BL.Journal.SaveData(bolNew, clsJournal, clsJournalDetAll, False)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.Receive.UpdateJournalID(dr.Item("ID"), strJournalID)
            Next
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Receive
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.Receive = DL.Receive.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim clsStockIn As New VO.StockIN
            'Dim dtData As DataTable = DL.Receive.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)

            'For Each dr As DataRow In dtData.Rows
            '    clsStockIn = New VO.StockIN
            '    clsStockIn.CompanyID = intCompanyID
            '    clsStockIn.ProgramID = intProgramID
            '    clsStockIn.ID = DL.StockIN.GetMaxID
            '    clsStockIn.ItemID = dr.Item("ItemID")
            '    clsStockIn.ReferencesID = dr.Item("ID")
            '    clsStockIn.ReferencesDate = dr.Item("ReceiveDate")
            '    clsStockIn.Qty = dr.Item("ArrivalNettoAfter")
            '    clsStockIn.Price = dr.Item("Price1")
            '    clsStockIn.NetPrice = dr.Item("Price1")
            '    clsStockIn.QtyOut = 0
            '    DL.StockIN.SaveData(True, clsStockIn)

            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll() As VO.JournalDet = Nothing
            '    Dim clsBukuBesar As New VO.BukuBesar
            '    Dim intIdx As Integer = 0
            '    If dr.Item("TotalDownPayment") > 0 And dr.Item("TotalPrice1") - dr.Item("TotalDownPayment") > 0 Then
            '        ReDim clsJournalDetAll(2)
            '    Else
            '        ReDim clsJournalDetAll(1)
            '    End If

            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("ReceiveDate")
            '    clsJournal.TotalAmount = dr.Item("TotalPrice1")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    '# Stock -> Request Julia, Posting ke Pembelian TBS
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
            '    clsJournalDet.DebitAmount = dr.Item("TotalPrice1")
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(intIdx) = clsJournalDet
            '    intIdx += 1

            '    '# Account Payable
            '    If dr.Item("TotalPrice1") - dr.Item("TotalDownPayment") > 0 Then
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
            '        clsJournalDet.DebitAmount = 0
            '        clsJournalDet.CreditAmount = dr.Item("TotalPrice1") - dr.Item("TotalDownPayment")
            '        clsJournalDetAll(intIdx) = clsJournalDet
            '        intIdx += 1

            '        '# Save Buku Besar Stock
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '        clsBukuBesar.DebitAmount = dr.Item("TotalPrice1") - dr.Item("TotalDownPayment")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Save Buku Besar Account Payable
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dr.Item("TotalPrice1") - dr.Item("TotalDownPayment")
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    End If

            '    '# Down Payment / Advance Payment
            '    If dr.Item("TotalDownPayment") > 0 Then
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
            '        clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAdvancePayment
            '        clsJournalDet.DebitAmount = 0
            '        clsJournalDet.CreditAmount = dr.Item("TotalDownPayment")
            '        clsJournalDetAll(intIdx) = clsJournalDet

            '        '# Save Buku Besar Stock
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
            '        clsBukuBesar.DebitAmount = dr.Item("TotalDownPayment")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Save Buku Besar Down Payment / Advance Payment
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("ReceiveDate")
            '        clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAdvancePayment
            '        clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dr.Item("TotalDownPayment")
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    End If

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.Receive.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.Receive.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.Receive.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Stock In
                DL.StockIN.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '#Update Journal ID
                DL.Receive.UpdateJournalID(dr.Item("ID"), "")
                DL.Receive.UnpostGL(dr.Item("ID"))

                '#Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strReceiveID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Receive.ListDataStatus(strReceiveID)
        End Function

        Public Shared Sub SaveDataStatus(ByVal strReceiveID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.ReceiveStatus
            clsData.ID = strReceiveID & "-" & Format(DL.Receive.GetMaxIDStatus(strReceiveID), "000")
            clsData.ReceiveID = strReceiveID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Receive.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace