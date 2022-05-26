Namespace BL
    Public Class JournalPost

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.JournalPost.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal clsData As VO.JournalPost) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.JournalPost.SaveData(Not DL.JournalPost.DataExists(), clsData)

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
                                      ByVal clsData As VO.JournalPost)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.JournalPost.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intProgramID As Integer) As VO.JournalPost
            BL.Server.ServerDefault()
            Return DL.JournalPost.GetDetail(intProgramID)
        End Function

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.JournalPost.DeleteDataAll()
        End Sub

    End Class

End Namespace

