Namespace DL
    Public Class UserAccess

        Public Shared Function ListData(ByVal strUserID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.UserID, A.ModulesID, MM.Name AS ModulesName, A.AccessID, MA.Name AS AccessName, A.ProgramID, MP.Name AS ProgramName  " & vbNewLine & _
                   "FROM mstUserAccess A " & vbNewLine & _
                   "INNER JOIN mstAccess MA ON  " & vbNewLine & _
                   "     A.AccessID=MA.ID " & vbNewLine & _
                   "INNER JOIN mstModules MM ON  " & vbNewLine & _
                   "     A.ModulesID=MM.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON  " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.UserID=@UserID" & vbNewLine

                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataByModulesIDAndProgramID(ByVal strUserID As String, ByVal intProgramID As Integer, ByVal intModulesID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(1 AS BIT) AS Pick, A.AccessID, MA.Name AS AccessName " & vbNewLine & _
                   "FROM mstUserAccess A " & vbNewLine & _
                   "INNER JOIN mstAccess MA ON  " & vbNewLine & _
                   "     A.AccessID=MA.ID " & vbNewLine & _
                   "INNER JOIN mstModules MM ON  " & vbNewLine & _
                   "     A.ModulesID=MM.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.UserID=@UserID" & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID" & vbNewLine & _
                   "    AND A.ModulesID=@ModulesID" & vbNewLine

                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ModulesID", SqlDbType.Int).Value = intModulesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataOutstandingByModulesIDAndProgramID(ByVal strUserID As String, ByVal intProgramID As Integer, ByVal intModulesID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(0 AS BIT) AS Pick, A.AccessID, MA.Name AS AccessName " & vbNewLine & _
                   "FROM mstModulesAccess A " & vbNewLine & _
                   "INNER JOIN mstAccess MA ON  " & vbNewLine & _
                   "     A.AccessID=MA.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ModulesID=@ModulesID" & vbNewLine & _
                   "    AND A.AccessID NOT IN " & vbNewLine & _
                   "    (" & vbNewLine & _
                   "        SELECT " & vbNewLine & _
                   "            AccessID " & vbNewLine & _
                   "        FROM mstUserAccess" & vbNewLine & _
                   "        WHERE " & vbNewLine & _
                   "            UserID=@UserID " & vbNewLine & _
                   "            AND ProgramID=@ProgramID " & vbNewLine & _
                   "            AND ModulesID=@ModulesID " & vbNewLine & _
                   "    )" & vbNewLine

                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ModulesID", SqlDbType.Int).Value = intModulesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataWithCompany(ByVal strUserID As String, ByVal intCompanyID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	UA.ID, UA.UserID, UA.ProgramID, MP.Name AS ProgramName, UA.ModulesID, MM.Name AS ModulesName, UA.AccessID, MA.Name AS AccessName, 	" & vbNewLine & _
                    "	UC.CompanyID, MC.Name AS CompanyName, MC.Address, MC.CompanyInitial 	" & vbNewLine & _
                    "FROM mstUserAccess UA 	" & vbNewLine & _
                    "INNER JOIN mstProgram MP ON 	" & vbNewLine & _
                    "	UA.ProgramID=MP.ID 	" & vbNewLine & _
                    "INNER JOIN mstModules MM ON 	" & vbNewLine & _
                    "	UA.ModulesID=MM.ID 	" & vbNewLine & _
                    "INNER JOIN mstAccess MA ON 	" & vbNewLine & _
                    "	UA.AccessID=MA.ID 	" & vbNewLine & _
                    "INNER JOIN mstUserCompany UC ON 	" & vbNewLine & _
                    "	UA.UserID=UC.UserID 	" & vbNewLine & _
                    "INNER JOIN mstCompany MC ON 	" & vbNewLine & _
                    "	UC.CompanyID=MC.ID 	" & vbNewLine & _
                    "WHERE 1=1 " & vbNewLine

                If Not UI.usUserApp.IsSuperUser Then
                    .CommandText += "   AND UA.UserID=@UserID	" & vbNewLine
                End If

                If intCompanyID <> 0 Then
                    .CommandText += "   AND UC.CompanyID=@CompanyID " & vbNewLine
                End If

                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ID]	" & vbNewLine & _
                    "      ,[UserID]	" & vbNewLine & _
                    "      ,[ProgramID]	" & vbNewLine & _
                    "      ,[ModulesID]	" & vbNewLine & _
                    "      ,[AccessID]	" & vbNewLine & _
                    "  FROM [dbo].[mstUserAccess]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal clsData As VO.UserAccess)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstUserAccess " & vbNewLine & _
                   "    (ID, UserID, ProgramID, ModulesID, AccessID)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @UserID, @ProgramID, @ModulesID, @AccessID)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = clsData.UserID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ModulesID", SqlDbType.Int).Value = clsData.ModulesID
                .Parameters.Add("@AccessID", SqlDbType.Int).Value = clsData.AccessID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function IsCanAccess(ByVal strUserID As String, ByVal intProgramID As Integer, ByVal intModulesID As Integer, ByVal intAccessID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolReturn As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.UserID, A.ProgramID, A.ModulesID, A.AccessID  " & vbNewLine & _
                       "FROM mstUserAccess A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    UserID=@UserID " & vbNewLine & _
                       "    AND ProgramID=@ProgramID " & vbNewLine & _
                       "    AND ModulesID=@ModulesID " & vbNewLine & _
                       "    AND AccessID=@AccessID " & vbNewLine

                    .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    .Parameters.Add("@ModulesID", SqlDbType.Int).Value = intModulesID
                    .Parameters.Add("@AccessID", SqlDbType.Int).Value = intAccessID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolReturn = True
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolReturn
        End Function

        Public Shared Sub DeleteDataByModulesIDAndProgramID(ByVal intProgramID As Integer, ByVal intModulesID As Integer, ByVal strUserID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM mstUserAccess " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ProgramID=@ProgramID " & vbNewLine & _
                    "   AND ModulesID=@ModulesID " & vbNewLine & _
                    "   AND UserID=@UserID " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ModulesID", SqlDbType.Int).Value = intModulesID
                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataByUserID(ByVal strUserID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM mstUserAccess " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   UserID=@UserID " & vbNewLine

                .Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = strUserID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataAll()
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM mstUserAccess " & vbNewLine 

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstUserAccess " & vbNewLine

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("ID") + 1
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return intReturn
        End Function

    End Class

End Namespace

