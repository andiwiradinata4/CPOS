Namespace BL
    Public Class BusinessPartnerAssign

        Public Shared Function ListData(ByVal intBPID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartnerAssign.ListData(intBPID)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.BusinessPartnerAssign.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal intBPID As Integer, ByVal clsDataAll() As VO.BusinessPartnerAssign) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.BusinessPartnerAssign.DeleteData(intBPID)

                For Each clsItem As VO.BusinessPartnerAssign In clsDataAll
                    clsItem.ID = DL.BusinessPartnerAssign.GetMaxID
                    DL.BusinessPartnerAssign.SaveData(clsItem)
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
                                           ByVal clsData As VO.BusinessPartnerAssign)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartnerAssign.SaveData(clsData)
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.BusinessPartnerAssign.DeleteDataAll()
        End Sub

    End Class

End Namespace

