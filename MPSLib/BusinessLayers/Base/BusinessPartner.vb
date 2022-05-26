Namespace BL
    Public Class BusinessPartner

#Region "Main"

        Public Shared Function ListDataAll(ByVal decOnAmount As Decimal, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListData(decOnAmount, intCompanyID, intProgramID)
        End Function

        Public Shared Function ListData(ByVal decOnAmount As Decimal, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListData(decOnAmount, intCompanyID, intProgramID)
        End Function

        Public Shared Function ListDataForFilter(ByVal intProgramID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataForFilter(intProgramID)
        End Function

        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListData
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.BusinessPartner) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.BusinessPartner.GetMaxID
                    clsData.Code = IIf(clsData.Code.Trim = "", Format(clsData.ID, "000"), clsData.Code)
                    If DL.BusinessPartner.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                DL.BusinessPartner.SaveData(bolNew, clsData)

                '# Save Data Status
                Dim strRemarks As String = ""
                If clsData.SalesPrice > 0 Then strRemarks = "HARGA JUAL: " & Format(clsData.SalesPrice, "#,##0.00") & " "
                If clsData.PurchasePrice1 > 0 Then strRemarks = "HARGA BELI 1: " & Format(clsData.PurchasePrice1, "#,##0.00") & " "
                If clsData.PurchasePrice2 > 0 Then strRemarks = "HARGA BELI 2: " & Format(clsData.PurchasePrice2, "#,##0.00") & " "
                SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, strRemarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.BusinessPartner)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartner.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.BusinessPartner
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.GetDetail(intID)
        End Function

        Public Shared Function GetDetail(ByVal strCode As String) As VO.BusinessPartner
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.GetDetail(strCode)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.BusinessPartner.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.BusinessPartner.DeleteData(intID)
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
            DL.BusinessPartner.DeleteDataAll()
        End Sub

        Public Shared Function UpdatePrice(ByVal clsDataAll() As VO.BusinessPartner) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim strRemarks As String = ""
                For Each clsData As VO.BusinessPartner In clsDataAll
                    strRemarks = ""
                    DL.BusinessPartner.UpdatePrice(clsData)

                    '# Save Data Status
                    If clsData.SalesPrice > 0 Then strRemarks += "HARGA JUAL: " & Format(clsData.SalesPrice, "#,##0.00") & " "
                    If clsData.PurchasePrice1 > 0 Then strRemarks += "HARGA BELI 1: " & Format(clsData.PurchasePrice1, "#,##0.00") & " "
                    If clsData.PurchasePrice2 > 0 Then strRemarks += "HARGA BELI 2: " & Format(clsData.PurchasePrice2, "#,##0.00") & " "
                    SaveDataStatus(clsData.ID, "UBAH HARGA", clsData.LogBy, strRemarks)
                Next

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

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataStatus(intBPID)
        End Function

        Public Shared Function ListDataAllStatus() As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataAllStatus
        End Function

        Public Shared Sub SaveDataStatus(ByVal intBPID As Integer, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.BusinessPartnerStatus
            clsData.ID = DL.BusinessPartner.GetMaxIDStatus
            clsData.BPID = intBPID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.BusinessPartner.SaveDataStatus(clsData)
        End Sub

        Public Shared Sub SaveDataAllStatus(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                            ByVal clsData As VO.BusinessPartnerStatus)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartner.SaveDataStatus(clsData)
        End Sub

        Public Shared Sub DeleteDataAllStatus(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartner.DeleteDataAllStatus()
        End Sub

#End Region

#Region "Price"

        Public Shared Function ListDataPrice(ByVal intBPID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataPrice(intBPID, dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function ListDataAllPrice() As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.ListDataAllPrice
        End Function

        Public Shared Function SaveDataPrice(ByVal bolNew As Boolean, ByVal clsData As VO.BusinessPartnerPrice) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then clsData.ID = DL.BusinessPartner.GetMaxIDPrice

                If DL.BusinessPartner.IsExistDateFrom(clsData.DateFrom.Date, clsData.ID, clsData.BPID) Then
                    Err.Raise(515, "", "Data tidak dapat disimpan. Periode dari tanggal " & Format(clsData.DateFrom, "dd/MM/yyyy") & " telah ada sebelumnya.")
                End If

                Dim clsPrev As VO.BusinessPartnerPrice = DL.BusinessPartner.GetDetailLastPrice(clsData.BPID, clsData.ID, clsData.DateFrom, clsData.DateTo)
                If clsPrev.ID <> 0 Then
                    clsPrev.DateTo = clsData.DateFrom.AddDays(-1)
                    If Format(clsData.DateFrom, "yyyyMMdd") <> Format(clsPrev.DateTo, "yyyyMMdd") Then
                        clsPrev.DateTo = clsPrev.DateTo
                        DL.BusinessPartner.SaveDataPrice(False, clsPrev)
                    End If
                End If

                DL.BusinessPartner.SaveDataPrice(bolNew, clsData)

                '# Save Data Status
                Dim strRemarks As String = ""
                strRemarks += "HARGA JUAL: " & Format(clsData.SalesPrice, "#,##0.00") & " "
                strRemarks += "HARGA BELI 1: " & Format(clsData.PurchasePrice1, "#,##0.00") & " "
                strRemarks += "HARGA BELI 2: " & Format(clsData.PurchasePrice2, "#,##0.00")
                SaveDataStatus(clsData.ID, "SET HARGA", clsData.LogBy, strRemarks)

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

        Public Shared Sub SaveDataAllPrice(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                           ByVal clsData As VO.BusinessPartnerPrice)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartner.SaveDataPrice(True, clsData)
        End Sub

        Public Shared Function GetDetailPrice(ByVal intID As Integer) As VO.BusinessPartnerPrice
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.GetDetailPrice(intID)
        End Function

        Public Shared Function GetDetailPrice(ByVal intBPID As Integer, ByVal dtmDate As DateTime) As VO.BusinessPartnerPrice
            BL.Server.ServerDefault()
            Return DL.BusinessPartner.GetDetailPrice(intBPID, dtmDate)
        End Function

        Public Shared Sub DeleteDataPrice(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.BusinessPartner.DeleteDataPrice(intID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub
        
        Public Shared Sub DeleteDataAllPrice(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartner.DeleteDataAllPrice()
        End Sub

#End Region

    End Class

End Namespace

