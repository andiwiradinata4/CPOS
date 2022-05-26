Namespace BL
    Public Class ChartOfAccountAssign

        Public Shared Function ListData(ByVal intCOAID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.ChartOfAccountAssign.ListData(intCOAID)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.ChartOfAccountAssign.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal intCOAID As Integer, ByVal clsDataAll() As VO.ChartOfAccountAssign) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.ChartOfAccountAssign.DeleteData(intCOAID)

                For Each clsItem As VO.ChartOfAccountAssign In clsDataAll
                    clsItem.ID = DL.ChartOfAccountAssign.GetMaxID
                    DL.ChartOfAccountAssign.SaveData(clsItem)
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

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.ChartOfAccountAssign)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.ChartOfAccountAssign.SaveData(clsData)
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.ChartOfAccountAssign.DeleteDataAll()
        End Sub
    End Class

End Namespace

