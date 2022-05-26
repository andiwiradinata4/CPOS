Namespace BL
    Public Class User

        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.User.ListData
        End Function

        Public Shared Function ListDataByUserIDAndPassword(ByVal strUserID As String, ByVal strPassword As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.User.ListDataByUserIDAndPassword(strUserID, strPassword)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.User.ListDataAll
        End Function

        Private Shared Function GetNewID()
            Dim strReturn As String = "S" & Format(Now, "yyMMdd")
            strReturn = strReturn & Format(DL.User.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.User, ByVal clsDataCompany() As VO.UserCompany) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.StaffID = GetNewID()
                    If DL.User.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "User ID sudah ada sebelumnya")
                    End If
                End If

                DL.User.SaveData(bolNew, clsData)

                '# Save User Company
                DL.UserCompany.DeleteDataByUserID(clsData.ID)
                For Each clsItem As VO.UserCompany In clsDataCompany
                    clsItem.ID = DL.UserCompany.GetMaxID
                    clsItem.UserID = clsData.ID
                    DL.UserCompany.SaveData(clsItem)
                Next

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
                                      ByVal clsData As VO.User)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.User.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.User
            BL.Server.ServerDefault()
            Dim clsData As VO.User = DL.User.GetDetail(strID)
            clsData.Password = Encryption.Decrypt(clsData.Password)
            Return clsData
        End Function

        Public Shared Sub DeleteData(ByVal strID As String)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.User.GetIDStatus(strID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.User.DeleteData(strID)
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
            DL.User.DeleteDataAll()
        End Sub

        Public Shared Function ValidateUser(ByVal strUserID As String, ByVal strPassword As String) As Boolean
            BL.Server.ServerDefault()
            Dim bolReturn As Boolean = False
            Dim clsData As VO.User = DL.User.GetDetail(strUserID)
            Dim strDecPassword As String = Encryption.Decrypt(clsData.Password)
            If strDecPassword = strPassword Then
                bolReturn = True
            End If
            Return bolReturn
        End Function

        Public Shared Function ChangePassword(ByVal clsData As VO.User, ByVal strOldPassword As String) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                Dim dtData As DataTable = DL.User.ListDataByUserIDAndPassword(clsData.ID, Encryption.Encrypt(strOldPassword))
                If dtData.Rows.Count = 0 Then
                    Err.Raise(515, "", "Password sebelumnya tidak valid")
                End If

                clsData.Password = Encryption.Encrypt(clsData.Password)
                DL.User.ChangePassword(clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function ResetPassword(ByVal clsData As VO.User) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                clsData.Password = Encryption.Encrypt(clsData.Password)

                DL.User.ChangePassword(clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function


    End Class

End Namespace

