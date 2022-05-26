Namespace BL
    Public Class ReceiveReturn

#Region "Main"
        
        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus)
        End Function

        Public Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "RR" & Format(Now, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.ReceiveReturn.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveDataDefault(ByVal bolNew As Boolean, ByVal clsData As VO.ReceiveReturn) As String
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                SaveData(bolNew, clsData)

                '# CaLculate Return Value
                DL.Receive.CalculateReturnValue(clsData.ReferencesID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ReceiveReturn) As String
            If bolNew Then
                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID)
                If DL.ReceiveReturn.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID sudah ada sebelumnya")
                ElseIf Format(clsData.ReceiveReturnDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
                End If
            Else
                If DL.ReceiveReturn.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah dihapus")
                ElseIf DL.ReceiveReturn.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat diedit. Dikarenakan data telah diproses posting data transaksi")
                End If
            End If

            DL.ReceiveReturn.SaveData(bolNew, clsData)

            '# Save Data Status
            SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.ReceiveReturn
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.ReceiveReturn)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                'Dim strReturnID As String = DL.ReceiveReturnReturn.AlreadyCreatedReturn(clsData.ID)
                'Dim strInvoiceID As String = DL.AccountReceivable.AlreadyCreatedInvoice(clsData.ID)

                If DL.ReceiveReturn.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                    'ElseIf strReturnID.Trim <> "" Then
                    '    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dibuat retur dengan nomor " & strReturnID)
                    'ElseIf strInvoiceID.Trim <> "" Then
                    '    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses penagihan dengan nomor " & strInvoiceID)
                ElseIf DL.ReceiveReturn.IsPostedGL(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah diproses posting data transaksi")
                Else
                    DL.ReceiveReturn.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)

                    '# CaLculate Return Value
                    DL.Receive.CalculateReturnValue(clsData.ReferencesID)
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
            'Dim dtData As DataTable = DL.ReceiveReturn.ListDataOutstandingPostGL(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
            'For Each dr As DataRow In dtData.Rows
            '    Dim decCOGS As Decimal = BL.COGS.CalculateCOGS(intCompanyID, intProgramID, _
            '                                                   dr.Item("ItemID"), dr.Item("ArrivalNettoAfter"), _
            '                                                   dr.Item("ID"), dr.Item("ReceiveReturnDate"))
            '    '# Generate Journal
            '    Dim clsJournal As VO.Journal = New VO.Journal
            '    clsJournal.CompanyID = intCompanyID
            '    clsJournal.ProgramID = intProgramID
            '    clsJournal.ID = dr.Item("JournalID")
            '    clsJournal.ReferencesID = dr.Item("ID")
            '    clsJournal.JournalDate = dr.Item("ReceiveReturnDate")
            '    clsJournal.TotalAmount = dr.Item("TotalPrice1")
            '    clsJournal.IsAutoGenerate = True
            '    clsJournal.IDStatus = VO.Status.Values.Draft
            '    clsJournal.Remarks = dr.Item("Remarks")
            '    clsJournal.LogBy = UI.usUserApp.UserID

            '    Dim clsJournalDet As VO.JournalDet
            '    Dim clsJournalDetAll(1) As VO.JournalDet

            '    '# Account Payable
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofAccountPayable
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofAccountPayable
            '    clsJournalDet.DebitAmount = dr.Item("TotalPrice1")
            '    clsJournalDet.CreditAmount = 0
            '    clsJournalDetAll(0) = clsJournalDet

            '    '# Stock
            '    clsJournalDet = New VO.JournalDet
            '    clsJournalDet.JournalID = dr.Item("JournalID")
            '    clsJournalDet.CoAID = MPSLib.UI.usUserApp.JournalPost.CoAofStock
            '    clsJournalDet.CoAName = MPSLib.UI.usUserApp.JournalPost.CoANameofStock
            '    clsJournalDet.DebitAmount = 0
            '    clsJournalDet.CreditAmount = dr.Item("TotalPrice1")
            '    clsJournalDetAll(1) = clsJournalDet

            '    Dim strJournalID As String = BL.Journal.SaveData(True, clsJournal, clsJournalDetAll)
            '    '# End Of Generate Journal

            '    '#Update Journal ID
            '    If strJournalID.Trim <> "" Then DL.ReceiveReturn.UpdateJournalID(dr.Item("ID"), strJournalID)

            '    DL.ReceiveReturn.PostGL(dr.Item("ID"), UI.usUserApp.UserID)

            '    '# Save Data Status
            '    SaveDataStatus(dr.Item("ID"), "POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            'Next
        End Sub

        Public Shared Sub UnpostData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim dtData As DataTable = DL.ReceiveReturn.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, VO.Status.Values.All)
            For Each dr As DataRow In dtData.Rows
                BL.COGS.UnpostCOGS(intCompanyID, intProgramID, dr.Item("ID"))
                DL.StockOut.DeleteDataByReferencesID(intCompanyID, intProgramID, dr.Item("ID"))

                '# Delete Journal
                DL.Journal.DeleteDataDetail(dr.Item("JournalID"))
                DL.Journal.DeleteDataPure(dr.Item("JournalID"))

                '# Update Journal ID
                DL.ReceiveReturn.UpdateJournalID(dr.Item("ID"), "")
                DL.ReceiveReturn.UnpostGL(dr.Item("ID"))

                '# Save Data Status
                SaveDataStatus(dr.Item("ID"), "CANCEL POSTING DATA TRANSAKSI", UI.usUserApp.UserID, "")
            Next
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strReceiveReturnID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.ReceiveReturn.ListDataStatus(strReceiveReturnID)
        End Function

        Private Shared Sub SaveDataStatus(ByVal strReceiveReturnID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.ReceiveReturnStatus
            clsData.ID = strReceiveReturnID & "-" & Format(DL.ReceiveReturn.GetMaxIDStatus(strReceiveReturnID), "000")
            clsData.ReceiveReturnID = strReceiveReturnID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.ReceiveReturn.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace