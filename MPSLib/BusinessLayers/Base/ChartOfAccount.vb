Namespace BL
    Public Class ChartOfAccount

        Public Shared Function ListData(ByVal enumFilterGroup As VO.ChartOfAccount.FilterGroup, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.ChartOfAccount.ListData(enumFilterGroup, intCompanyID, intProgramID)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.ChartOfAccount.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ChartOfAccount) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.ChartOfAccount.GetMaxID
                    If DL.ChartOfAccount.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                If DL.ChartOfAccount.CodeExists(clsData.Code, clsData.ID) Then
                    Err.Raise(515, "", "Kode akun sudah ada sebelumnya")
                End If

                DL.ChartOfAccount.SaveData(bolNew, clsData)

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
                                      ByVal clsData As VO.ChartOfAccount)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.ChartOfAccount.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.ChartOfAccount
            BL.Server.ServerDefault()
            Return DL.ChartOfAccount.GetDetail(intID)
        End Function

        Public Shared Function GetDetail(ByVal strCode As String) As VO.ChartOfAccount
            BL.Server.ServerDefault()
            Return DL.ChartOfAccount.GetDetail(strCode)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.ChartOfAccount.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.ChartOfAccount.DeleteData(intID)
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
            DL.ChartOfAccount.DeleteDataAll()
        End Sub

    End Class

End Namespace

