Namespace BL
    Public Class AccountPayable

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataAll
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "AP" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.AccountPayable.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.AccountPayable, ByVal clsDataDetail() As VO.AccountPayableDet) As String
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.APDate)
                    If DL.AccountPayable.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.APDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.AccountPayable.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.AccountPayable.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                    '# Delete Item of Account Payable
                    dtPreviousItem = DL.AccountPayable.ListDataDetail(clsData.ID)
                    DL.AccountPayable.DeleteDataDetail(clsData.ID)

                    '# Revert Total Payment
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Receive.CalculateTotalPayment(dr.Item("ReceiveID"))
                    Next

                    '# Delete Buku Besar Account Payable
                    DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.AccountPayable.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.AccountPayableDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.AccountPayable.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.APID = clsData.ID
                    DL.AccountPayable.SaveDataDetail(clsDetail)

                    '# Re-Calculate Receive Payment
                    DL.Receive.CalculateTotalPayment(clsDetail.ReceiveID)
                Next

                '# Save Data Status
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                '# Journal Account Payable
                BL.AccountPayable.GenerateJournal(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.AccountPayable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.AccountPayable)
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.AccountPayable.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.AccountPayable.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    '# Delete Account Payable
                    DL.AccountPayable.DeleteData(clsData.ID)

                    '# Revert Total Payment
                    dtPreviousItem = DL.AccountPayable.ListDataDetail(clsData.ID)
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Receive.CalculateTotalPayment(dr.Item("ReceiveID"))
                    Next

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Delete Buku Besar Account Payable & Journal
                    BL.AccountPayable.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.AccountPayable.DeleteDataAll()
        End Sub

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal clsData As VO.AccountPayable)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Journal Account Payable
            Dim dtData As DataTable = DL.AccountPayable.ListDataGenerateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            For Each dr As DataRow In dtData.Rows
                '# Generate Journal
                clsJournal = New VO.Journal
                clsJournal.CompanyID = clsData.CompanyID
                clsJournal.ProgramID = clsData.ProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("APDate")
                clsJournal.TotalAmount = dr.Item("TotalAmount")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = VO.Receive.JournalRemarks
                clsJournal.CashOrBankInfo = VO.Receive.JournalCashOrBankInfo
                clsJournal.PaymentTo = clsData.BPName
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(1) As VO.JournalDet

                '# Account Payable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
                clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Save Buku Besar Account Payable
                clsBukuBesar = New VO.BukuBesar
                clsBukuBesar.CompanyID = clsData.CompanyID
                clsBukuBesar.ProgramID = clsData.ProgramID
                clsBukuBesar.ID = ""
                clsBukuBesar.ReferencesID = dr.Item("ID")
                clsBukuBesar.TransactionDate = dr.Item("APDate")
                clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                clsBukuBesar.COAIDChild = dr.Item("CoAIDOfOutgoingPayment")
                clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
                clsBukuBesar.CreditAmount = 0
                clsBukuBesar.Remarks = ""
                clsBukuBesar.LogBy = UI.usUserApp.UserID
                BL.BukuBesar.SaveData(clsBukuBesar)

                '# Cash / Bank
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = dr.Item("CoAIDOfOutgoingPayment")
                clsJournalDet.CoAName = ""
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                clsJournalDetAll(1) = clsJournalDet

                '# Save Buku Besar Cash / Bank
                clsBukuBesar = New VO.BukuBesar
                clsBukuBesar.CompanyID = clsData.CompanyID
                clsBukuBesar.ProgramID = clsData.ProgramID
                clsBukuBesar.ID = ""
                clsBukuBesar.ReferencesID = dr.Item("ID")
                clsBukuBesar.TransactionDate = dr.Item("APDate")
                clsBukuBesar.COAIDParent = dr.Item("CoAIDOfOutgoingPayment")
                clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
                clsBukuBesar.DebitAmount = 0
                clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
                clsBukuBesar.Remarks = ""
                clsBukuBesar.LogBy = UI.usUserApp.UserID
                BL.BukuBesar.SaveData(clsBukuBesar)

                Dim strJournalID As String = BL.Journal.SaveData(bolNew, clsJournal, clsJournalDetAll, False)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.AccountPayable.UpdateJournalID(dr.Item("ID"), strJournalID)
            Next
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Account Payable
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.AccountPayable = DL.AccountPayable.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim dtData As DataTable = DL.AccountPayable.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("APDate")
            '    clsJournal.TotalAmount = dr.Item("TotalAmount")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(1) As VO.JournalDet
            '    Dim clsBukuBesar As New VO.BukuBesar

            '    '# Account Payable
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
            '    clsJournalDet.DebitAmount = dr.Item("TotalAmount")
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(0) = clsJournalDet

            '    '# Save Buku Besar Account Payable
            '    clsBukuBesar = New VO.BukuBesar
            '    clsBukuBesar.CompanyID = intCompanyID
            '    clsBukuBesar.ProgramID = intProgramID
            '    clsBukuBesar.ID = ""
            '    clsBukuBesar.ReferencesID = dr.Item("ID")
            '    clsBukuBesar.TransactionDate = dr.Item("APDate")
            '    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '    clsBukuBesar.COAIDChild = dr.Item("CoAIDOfOutgoingPayment")
            '    clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
            '    clsBukuBesar.CreditAmount = 0
            '    clsBukuBesar.Remarks = ""
            '    clsBukuBesar.LogBy = UI.usUserApp.UserID
            '    BL.BukuBesar.SaveData(clsBukuBesar)

            '    '# Cash / Bank
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = dr.Item("CoAIDOfOutgoingPayment")
            '    clsJournalDet.CoAName = ""
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalAmount")
            '    clsJournalDetAll(1) = clsJournalDet

            '    '# Save Buku Besar Cash / Bank
            '    clsBukuBesar = New VO.BukuBesar
            '    clsBukuBesar.CompanyID = intCompanyID
            '    clsBukuBesar.ProgramID = intProgramID
            '    clsBukuBesar.ID = ""
            '    clsBukuBesar.ReferencesID = dr.Item("ID")
            '    clsBukuBesar.TransactionDate = dr.Item("APDate")
            '    clsBukuBesar.COAIDParent = dr.Item("CoAIDOfOutgoingPayment")
            '    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '    clsBukuBesar.DebitAmount = 0
            '    clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
            '    clsBukuBesar.Remarks = ""
            '    clsBukuBesar.LogBy = UI.usUserApp.UserID
            '    BL.BukuBesar.SaveData(clsBukuBesar)

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.AccountPayable.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.AccountPayable.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.AccountPayable.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.AccountPayable.UpdateJournalID(dr.Item("ID"), "")
                DL.AccountPayable.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetailWithOutstanding(ByVal strAPID As String, ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataDetailWithOutstanding(strAPID, intBPID)
        End Function

        Public Shared Function ListDataDetail(ByVal strAPID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataDetail(strAPID)
        End Function

        Public Shared Function ListDataDetailOutstanding(ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataDetailOutstanding(intBPID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strAPID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountPayable.ListDataStatus(strAPID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strAPID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                          ByVal strRemarks As String)
            Dim clsData As New VO.AccountPayableStatus
            clsData.ID = strAPID & "-" & Format(DL.AccountPayable.GetMaxIDStatus(strAPID), "000")
            clsData.APID = strAPID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.AccountPayable.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class
End Namespace