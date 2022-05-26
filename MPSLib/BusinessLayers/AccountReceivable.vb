Namespace BL
    Public Class AccountReceivable

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataSyncJournal(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "AR" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.AccountReceivable.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.AccountReceivable, ByVal clsDataDetail() As VO.AccountReceivableDet) As String
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.ARDate)
                    If DL.AccountReceivable.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    ElseIf Format(clsData.ARDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                        Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                    End If
                Else
                    If DL.AccountReceivable.IsDeleted(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                    ElseIf DL.AccountReceivable.IsPostedGL(clsData.ID) Then
                        Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                    End If
                    '# Delete Item of Account Receivable
                    dtPreviousItem = DL.AccountReceivable.ListDataDetail(clsData.CompanyID, clsData.ProgramID, clsData.ID)
                    DL.AccountReceivable.DeleteDataDetail(clsData.ID)

                    '# Revert Total Payment
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Sales.CalculateTotalPayment(dr.Item("SalesID"))
                    Next

                    '# Delete Buku Besar Account Receivable
                    DL.BukuBesar.DeleteData(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.AccountReceivable.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.AccountReceivableDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.AccountReceivable.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.ARID = clsData.ID
                    DL.AccountReceivable.SaveDataDetail(clsDetail)

                    '# Calculate Sales
                    DL.Sales.CalculateTotalPayment(clsDetail.SalesID)

                    '# Calculate Sales Service
                    DL.SalesService.CalculateTotalPayment(clsDetail.SalesID)
                Next

                '# Save Data Status
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                '# Journal Account Receivable
                BL.AccountReceivable.GenerateJournal(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.AccountReceivable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.AccountReceivable)
            Dim dtPreviousItem As New DataTable
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.AccountReceivable.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.AccountReceivable.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    '# Delete Account Receivable
                    DL.AccountReceivable.DeleteData(clsData.ID)

                    '# Revert Total Payment
                    dtPreviousItem = DL.AccountReceivable.ListDataDetail(clsData.CompanyID, clsData.ProgramID, clsData.ID)
                    For Each dr As DataRow In dtPreviousItem.Rows
                        DL.Sales.CalculateTotalPayment(dr.Item("SalesID"))
                        DL.SalesService.CalculateTotalPayment(dr.Item("SalesID"))
                    Next

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# Delete Buku Besar Account Receivable & Journal
                    BL.AccountReceivable.DeleteJournal(clsData.ProgramID, clsData.CompanyID, clsData.ID)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

        Public Shared Sub GenerateJournal(ByVal bolNew As Boolean, ByVal clsData As VO.AccountReceivable)
            Dim clsJournal As VO.Journal = New VO.Journal
            Dim clsBukuBesar As New VO.BukuBesar

            '# Journal Account Payable
            Dim dtData As DataTable = DL.AccountReceivable.ListDataGenerateJournal(clsData.CompanyID, clsData.ProgramID, clsData.ID)
            For Each dr As DataRow In dtData.Rows
                '# Generate Journal
                clsJournal = New VO.Journal
                clsJournal.CompanyID = clsData.CompanyID
                clsJournal.ProgramID = clsData.ProgramID
                clsJournal.ID = dr.Item("JournalID")
                clsJournal.ReferencesID = dr.Item("ID")
                clsJournal.JournalDate = dr.Item("ARDate")
                clsJournal.TotalAmount = dr.Item("TotalAmount")
                clsJournal.IsAutoGenerate = True
                clsJournal.IDStatus = VO.Status.Values.Draft
                clsJournal.Remarks = VO.Sales.JournalRemarks
                clsJournal.CashOrBankInfo = VO.Sales.JournalCashOrBankInfo
                clsJournal.PaymentTo = clsData.BPName
                clsJournal.LogBy = UI.usUserApp.UserID

                Dim clsJournalDet As VO.JournalDet
                Dim clsJournalDetAll(1) As VO.JournalDet

                '# Cash / Bank
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = dr.Item("CoAIDOfIncomePayment")
                clsJournalDet.CoAName = ""
                clsJournalDet.DebitAmount = dr.Item("TotalAmount")
                clsJournalDet.CreditAmount = 0
                clsJournalDetAll(0) = clsJournalDet

                '# Save Buku Besar Cash / Bank
                clsBukuBesar = New VO.BukuBesar
                clsBukuBesar.CompanyID = clsData.CompanyID
                clsBukuBesar.ProgramID = clsData.ProgramID
                clsBukuBesar.ID = ""
                clsBukuBesar.ReferencesID = dr.Item("ID")
                clsBukuBesar.TransactionDate = dr.Item("ARDate")
                clsBukuBesar.COAIDParent = dr.Item("CoAIDOfIncomePayment")
                clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
                clsBukuBesar.CreditAmount = 0
                clsBukuBesar.Remarks = ""
                clsBukuBesar.LogBy = UI.usUserApp.UserID
                BL.BukuBesar.SaveData(clsBukuBesar)

                '# Account Receivable
                clsJournalDet = New VO.JournalDet
                clsJournalDet.JournalID = dr.Item("JournalID")
                clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                clsJournalDet.CoAName = ""
                clsJournalDet.DebitAmount = 0
                clsJournalDet.CreditAmount = dr.Item("TotalAmount")
                clsJournalDetAll(1) = clsJournalDet

                '# Save Buku Besar Account Receivable
                clsBukuBesar = New VO.BukuBesar
                clsBukuBesar.CompanyID = clsData.CompanyID
                clsBukuBesar.ProgramID = clsData.ProgramID
                clsBukuBesar.ID = ""
                clsBukuBesar.ReferencesID = dr.Item("ID")
                clsBukuBesar.TransactionDate = dr.Item("ARDate")
                clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
                clsBukuBesar.COAIDChild = dr.Item("CoAIDOfIncomePayment")
                clsBukuBesar.DebitAmount = 0
                clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
                clsBukuBesar.Remarks = ""
                clsBukuBesar.LogBy = UI.usUserApp.UserID
                BL.BukuBesar.SaveData(clsBukuBesar)

                Dim strJournalID As String = BL.Journal.SaveData(bolNew, clsJournal, clsJournalDetAll, False)
                '# End Of Generate Journal

                '# Update Journal ID
                If strJournalID.Trim <> "" Then DL.AccountReceivable.UpdateJournalID(dr.Item("ID"), strJournalID)
            Next
        End Sub

        Public Shared Sub DeleteJournal(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, strID As String)
            '# Delete Buku Besar Account Receivable
            DL.BukuBesar.DeleteData(intProgramID, intCompanyID, strID)

            '# Delete Journal
            Dim clsData As VO.AccountReceivable = DL.AccountReceivable.GetDetail(strID)
            DL.Journal.DeleteData(clsData.JournalID)
        End Sub

        Public Shared Sub PostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            'Dim dtData As DataTable = DL.AccountReceivable.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("ARDate")
            '    clsJournal.TotalAmount = dr.Item("TotalAmount")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(1) As VO.JournalDet
            '    Dim clsBukuBesar As New VO.BukuBesar

            '    '# Cash / Bank
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = dr.Item("CoAIDOfIncomePayment")
            '    clsJournalDet.CoAName = ""
            '    clsJournalDet.DebitAmount = dr.Item("TotalAmount")
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(0) = clsJournalDet

            '    '# Save Buku Besar Cash / Bank
            '    clsBukuBesar = New VO.BukuBesar
            '    clsBukuBesar.CompanyID = intCompanyID
            '    clsBukuBesar.ProgramID = intProgramID
            '    clsBukuBesar.ID = ""
            '    clsBukuBesar.ReferencesID = dr.Item("ID")
            '    clsBukuBesar.TransactionDate = dr.Item("ARDate")
            '    clsBukuBesar.COAIDParent = dr.Item("CoAIDOfIncomePayment")
            '    clsBukuBesar.COAIDChild = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '    clsBukuBesar.DebitAmount = dr.Item("TotalAmount")
            '    clsBukuBesar.CreditAmount = 0
            '    clsBukuBesar.Remarks = ""
            '    clsBukuBesar.LogBy = UI.usUserApp.UserID
            '    BL.BukuBesar.SaveData(clsBukuBesar)

            '    '# Account Receivable
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '    clsJournalDet.CoAName = ""
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalAmount")
            '    clsJournalDetAll(1) = clsJournalDet

            '    '# Save Buku Besar Account Receivable
            '    clsBukuBesar = New VO.BukuBesar
            '    clsBukuBesar.CompanyID = intCompanyID
            '    clsBukuBesar.ProgramID = intProgramID
            '    clsBukuBesar.ID = ""
            '    clsBukuBesar.ReferencesID = dr.Item("ID")
            '    clsBukuBesar.TransactionDate = dr.Item("ARDate")
            '    clsBukuBesar.COAIDParent = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '    clsBukuBesar.COAIDChild = dr.Item("CoAIDOfIncomePayment")
            '    clsBukuBesar.DebitAmount = 0
            '    clsBukuBesar.CreditAmount = dr.Item("TotalAmount")
            '    clsBukuBesar.Remarks = ""
            '    clsBukuBesar.LogBy = UI.usUserApp.UserID
            '    BL.BukuBesar.SaveData(clsBukuBesar)

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll, False)
            '    '# End Of Generate Journal

            '    '# Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.AccountReceivable.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.AccountReceivable.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.AccountReceivable.ListData(intCompanyID, intCompanyID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.AccountReceivable.UpdateJournalID(dr.Item("ID"), "")
                DL.AccountReceivable.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetailWithOutstanding(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal strARID As String, ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Dim dtData As New DataTable
            dtData = DL.AccountReceivable.ListDataDetail(intCompanyID, intProgramID, strARID)
            dtData.Merge(DL.AccountReceivable.ListDataDetailOutstanding(intCompanyID, intProgramID, strARID, intBPID))
            Return dtData 'DL.AccountReceivable.ListDataDetailWithOutstanding(intCompanyID, intProgramID, strARID, intBPID)
        End Function

        Public Shared Function ListDataDetail(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal strARID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataDetail(intCompanyID, intProgramID, strARID)
        End Function

        Public Shared Function ListDataDetailOutstanding(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Dim dtData As New DataTable
            Return DL.AccountReceivable.ListDataDetailOutstanding(intCompanyID, intProgramID, intBPID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strARID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.AccountReceivable.ListDataStatus(strARID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strARID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                          ByVal strRemarks As String)
            Dim clsData As New VO.AccountReceivableStatus
            clsData.ID = strARID & "-" & Format(DL.AccountReceivable.GetMaxIDStatus(strARID), "000")
            clsData.ARID = strARID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.AccountReceivable.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

