Namespace BL

    Public Class SalesReturn

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.SalesReturn.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SR" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.SalesReturn.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.SalesReturn) As String
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                SaveData(bolNew, clsData)

                '# CaLculate Return Value
                DL.Sales.CalculateReturnValue(clsData.ReferencesID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function
        
        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.SalesReturn) As String
            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.SalesReturnDate)
                If DL.SalesReturn.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                ElseIf Format(clsData.SalesReturnDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                End If
            Else
                If DL.SalesReturn.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf DL.SalesReturn.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                End If
            End If

            DL.SalesReturn.SaveData(bolNew, clsData)

            '# Save Data Status
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.SalesReturn
            BL.Server.ServerDefault()
            Return DL.SalesReturn.GetDetail(strID)
        End Function
        
        Public Shared Sub DeleteData(ByVal clsData As VO.SalesReturn)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.SalesReturn.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                ElseIf DL.SalesReturn.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.SalesReturn.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# CaLculate Return Value
                    DL.Sales.CalculateReturnValue(clsData.ReferencesID)
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
            'dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            'Dim dtData As DataTable = DL.SalesReturn.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    Dim decCOGS As Decimal = BL.COGS.RevertCalculateCOGS(intCompanyID, intProgramID, _
            '                                                         dr.Item("ItemID"), dr.Item("ArrivalNettoAfter"), _
            '                                                         dr.Item("ID"), dr.Item("ReferencesID"), dr.Item("SalesReturnDate"))
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("SalesReturnDate")
            '    clsJournal.TotalAmount = dr.Item("TotalPrice")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(3) As VO.JournalDet

            '    '# Revenue 
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofRevenue
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofRevenue
            '    clsJournalDet.DebitAmount = dr.Item("TotalPrice")
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(0) = clsJournalDet

            '    '# Account Receivable
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountReceivable
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountReceivable
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalPrice")
            '    clsJournalDetAll(1) = clsJournalDet

            '    '# Stock
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
            '    clsJournalDet.DebitAmount = decCOGS
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(2) = clsJournalDet

            '    '# COGS
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofCOGS
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofCOGS
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = decCOGS
            '    clsJournalDetAll(3) = clsJournalDet

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '#Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.SalesReturn.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.SalesReturn.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.SalesReturn.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                '# Delete Stock In
                DL.StockIN.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '#Update Journal ID
                DL.SalesReturn.UpdateJournalID(dr.Item("ID"), "")
                DL.SalesReturn.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesReturn.ListDataStatus(strSalesReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strSalesReturnID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.SalesReturnStatus
            clsData.ID = strSalesReturnID & "-" & Format(DL.SalesReturn.GetMaxIDStatus(strSalesReturnID), "000")
            clsData.SalesReturnID = strSalesReturnID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.SalesReturn.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace