Namespace BL
    Public Class Cost

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Cost.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Cost.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "CO" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Cost.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Private Shared Function GetNewCostNo(ByVal intCompanyID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = clsCompany.CompanyInitial & "-" & Format(dtmDate, "yyMM")
            strReturn = strReturn & Format(DL.Cost.GetMaxCostNo(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Cost, ByVal clsDataDetail() As VO.CostDet) As String
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.CostDate)
                    If clsData.CostNo.Trim = "" Then clsData.CostNo = GetNewCostNo(clsData.CompanyID, clsData.CostDate)
                    If DL.Cost.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.CostDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    ElseIf DL.Cost.DataExistsCostNo(clsData.CostNo) Then
                        Err.Raise(515, "", "Nomor " & clsData.CostNo & " sudah terpakai")
                    End If
                Else
                    If DL.Cost.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.Cost.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    ElseIf DL.Cost.DataExistsCostNo(clsData.CostNo, clsData.ID) Then
                        Err.Raise(515, "", "Nomor " & clsData.CostNo & " sudah terpakai")
                    End If
                    '# Delete Cost Item
                    DL.Cost.DeleteDataDetail(clsData.ID)

                    '# Delete Buku Besar Cost & Journal
                    DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.Cost.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.CostDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.Cost.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.CostID = clsData.ID
                    DL.Cost.SaveDataDetail(clsDetail)
                Next

                '# Save Data Status
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                '# Journal Cost
                BL.Cost.GenerateJournal(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Cost
            BL.Server.ServerDefault()
            Return DL.Cost.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Cost)
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.Cost.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.Cost.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    '# Delete Cost
                    DL.Cost.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Delete Buku Besar Cost & Journal
                    BL.Cost.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Function PrintFakturBiaya(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Dim dtData As DataTable = DL.Cost.PrintFakturBiaya(strID)
            For Each dr As DataRow In dtData.Rows
                dr.BeginEdit()
                dr.Item("TextOfTotalAmount") = SharedLib.StringUtility.ConvertNumIndo(dr.Item("TotalAmount"))
                dr.EndEdit()
            Next
            dtData.AcceptChanges()
            Return dtData
        End Function

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal clsData As VO.Cost)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Journal Cost
            Dim dtData As DataTable = DL.Cost.ListDataGenerateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            For Each dr As DataRow In dtData.Rows
                Dim dtItem As DataTable = DL.Cost.ListDataDetail(dr.Item("ID"))

                '# Generate Journal
                clsJournal = New VO.Journal
                clsJournal.CompanyID = clsData.CompanyID
                clsJournal.ProgramID = clsData.ProgramID
                clsJournal.CompanyID = dr.Item("CompanyID")
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("CostDate")
                clsJournal.TotalAmount = dr.Item("TotalAmount")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = dr.Item("Remarks")
                clsJournal.PaymentTo = dr.Item("PaymentTo")
                clsJournal.CashOrBankInfo = dr.Item("CoAName")
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(dtItem.Rows.Count) As VO.JournalDet            

                '# Expense
                For i As Integer = 0 To dtItem.Rows.Count - 1
                    clsJournalDet = New VO.JournalDet
                    clsJournalDet.JournalID = dr.Item("JournalID")
                    clsJournalDet.CoAID = dtItem.Rows(i).Item("CoAID")
                    clsJournalDet.CoAName = dtItem.Rows(i).Item("CoAName")
                    clsJournalDet.DebitAmount = dtItem.Rows(i).Item("Amount")
                    clsJournalDet.CreditAmount = 0
                    clsJournalDet.Remarks = dtItem.Rows(i).Item("Remarks")
                    clsJournalDetAll(i) = clsJournalDet

                    '# Save Buku Besar Expense
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("CostDate")
                    clsBukuBesar.COAIDParent = dtItem.Rows(i).Item("CoAID")
                    clsBukuBesar.COAIDChild = dr.Item("CoAID")
                    clsBukuBesar.DebitAmount = dtItem.Rows(i).Item("Amount")
                    clsBukuBesar.CreditAmount = 0
                    clsBukuBesar.Remarks = ""
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)

                    '# Save Buku Besar Cash / Bank
                    clsBukuBesar = New VO.BukuBesar
                    clsBukuBesar.CompanyID = clsData.CompanyID
                    clsBukuBesar.ProgramID = clsData.ProgramID
                    clsBukuBesar.ID = ""
                    clsBukuBesar.ReferencesID = dr.Item("ID")
                    clsBukuBesar.TransactionDate = dr.Item("CostDate")
                    clsBukuBesar.COAIDParent = dr.Item("CoAID")
                    clsBukuBesar.COAIDChild = dtItem.Rows(i).Item("CoAID")
                    clsBukuBesar.DebitAmount = 0
                    clsBukuBesar.CreditAmount = dtItem.Rows(i).Item("Amount")
                    clsBukuBesar.Remarks = dtItem.Rows(i).Item("Remarks")
                    clsBukuBesar.LogBy = UI.usUserApp.UserID
                    BL.BukuBesar.SaveData(clsBukuBesar)
                Next

                '# Cash / Bank
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = dr.Item("CoAID")
                clsJournalDet.CoAName = dr.Item("CoAName")
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                clsJournalDetAll(dtItem.Rows.Count) = clsJournalDet

                Dim strJournalID As String = BL.Journal.SaveData(bolNew, clsJournal, clsJournalDetAll, False)
                '# End Of Generate Journal

                '#Update Journal ID
                If strJournalID.Trim <> "" Then DL.Cost.UpdateJournalID(dr.Item("ID"), strJournalID)
            Next
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Cost
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.Cost = DL.Cost.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim dtData As DataTable = DL.Cost.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    Dim dtItem As DataTable = DL.Cost.ListDataDetail(dr.Item("ID"))

            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.CompanyID = dr.Item("CompanyID")
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("CostDate")
            '    clsJournal.TotalAmount = dr.Item("TotalAmount")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(dtItem.Rows.Count) As VO.JournalDet
            '    Dim clsBukuBesar As New VO.BukuBesar

            '    '# Expense
            '    For i As Integer = 0 To dtItem.Rows.Count - 1
            '        clsJournalDet = New VO.JournalDet
            '        clsJournalDet.JournalID = dr.Item("JournalID")
            '        clsJournalDet.CoAID = dtItem.Rows(i).Item("CoAID")
            '        clsJournalDet.CoAName = dtItem.Rows(i).Item("CoAName")
            '        clsJournalDet.DebitAmount = dtItem.Rows(i).Item("Amount")
            '        clsJournalDet.CreditAmount = 0
            '        clsJournalDet.Remarks = dtItem.Rows(i).Item("Remarks")
            '        clsJournalDetAll(i) = clsJournalDet

            '        '# Save Buku Besar Expense
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("CostDate")
            '        clsBukuBesar.COAIDParent = dtItem.Rows(i).Item("CoAID")
            '        clsBukuBesar.COAIDChild = dr.Item("CoAID")
            '        clsBukuBesar.DebitAmount = dtItem.Rows(i).Item("Amount")
            '        clsBukuBesar.CreditAmount = 0
            '        clsBukuBesar.Remarks = ""
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)

            '        '# Save Buku Besar Cash / Bank
            '        clsBukuBesar = New VO.BukuBesar
            '        clsBukuBesar.CompanyID = intCompanyID
            '        clsBukuBesar.ProgramID = intProgramID
            '        clsBukuBesar.ID = ""
            '        clsBukuBesar.ReferencesID = dr.Item("ID")
            '        clsBukuBesar.TransactionDate = dr.Item("CostDate")
            '        clsBukuBesar.COAIDParent = dr.Item("CoAID")
            '        clsBukuBesar.COAIDChild = dtItem.Rows(i).Item("CoAID")
            '        clsBukuBesar.DebitAmount = 0
            '        clsBukuBesar.CreditAmount = dtItem.Rows(i).Item("Amount")
            '        clsBukuBesar.Remarks = dtItem.Rows(i).Item("Remarks")
            '        clsBukuBesar.LogBy = UI.usUserApp.UserID
            '        BL.BukuBesar.SaveData(clsBukuBesar)
            '    Next

            '    '# Cash / Bank
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = dr.Item("CoAID")
            '    clsJournalDet.CoAName = dr.Item("CoAName")
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalAmount")
            '    clsJournalDetAll(dtItem.Rows.Count) = clsJournalDet

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '#Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.Cost.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.Cost.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.Cost.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.Cost.UpdateJournalID(dr.Item("ID"), "")
                DL.Cost.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strCostID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Cost.ListDataDetail(strCostID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Cost.ListDataStatus(strSalesReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strCostID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.CostStatus
            clsData.ID = strCostID & "-" & Format(DL.Cost.GetMaxIDStatus(strCostID), "000")
            clsData.CostID = strCostID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Cost.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class 

End Namespace

