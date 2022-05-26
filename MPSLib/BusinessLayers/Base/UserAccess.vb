Namespace BL
    Public Class UserAccess

        Public Shared Function ListData(ByVal strUserID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.UserAccess.ListData(strUserID)
        End Function

        Public Shared Function ListDataByModulesIDAndProgramID(ByVal strUserID As String, ByVal intProgramID As Integer, ByVal intModulesID As Integer) As DataTable
            BL.Server.ServerDefault()
            Dim dtData As DataTable = DL.UserAccess.ListDataByModulesIDAndProgramID(strUserID, intProgramID, intModulesID)
            dtData.Merge(DL.UserAccess.ListDataOutstandingByModulesIDAndProgramID(strUserID, intProgramID, intModulesID))
            Return dtData
        End Function

        Public Shared Function ListDataWithCompany(ByVal strUserID As String, ByVal intCompanyID As Integer) As DataTable
            BL.Server.ServerDefault()
            Return DL.UserAccess.ListDataWithCompany(strUserID, intCompanyID)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.UserAccess.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal clsDataAll() As VO.UserAccess, ByVal intProgramID As Integer, ByVal intModulesID As Integer, ByVal strUserID As String) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.UserAccess.DeleteDataByModulesIDAndProgramID(intProgramID, intModulesID, strUserID)

                For Each clsData As VO.UserAccess In clsDataAll
                    clsData.ID = DL.UserAccess.GetMaxID
                    DL.UserAccess.SaveData(clsData)
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
                                      ByVal clsData As VO.UserAccess)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.UserAccess.SaveData(clsData)
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.UserAccess.DeleteDataAll()
        End Sub

        Public Shared Function DuplicateUserAccess(ByVal strBaseUserID As String, ByVal strNewUserID As String) As Boolean
            Dim bolReturn As Boolean = False
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()
                Dim dtData As DataTable = DL.UserAccess.ListData(strBaseUserID)

                DL.UserAccess.DeleteDataByUserID(strNewUserID)

                Dim clsData As New VO.UserAccess
                For Each dr As DataRow In dtData.Rows
                    clsData = New VO.UserAccess
                    clsData.ID = DL.UserAccess.GetMaxID
                    clsData.UserID = strNewUserID
                    clsData.ProgramID = dr.Item("ProgramID")
                    clsData.ModulesID = dr.Item("ModulesID")
                    clsData.AccessID = dr.Item("AccessID")
                    DL.UserAccess.SaveData(clsData)
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

        Public Shared Function IsCanAccess(ByVal strUserID As String, ByVal intProgramID As Integer, ByVal intModulesID As Integer, ByVal intAccessID As Integer) As Boolean
            BL.Server.ServerDefault()
            Dim bolReturn As Boolean = False

            If UI.usUserApp.IsSuperUser Then
                bolReturn = True
            Else
                bolReturn = DL.UserAccess.IsCanAccess(strUserID, intProgramID, intModulesID, intAccessID)
            End If

            Return bolReturn
        End Function

    End Class

End Namespace

