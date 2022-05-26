Namespace DL
    Public Class User

        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.StaffID, A.Name, A.Password, A.Position, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "     A.CreatedDate, A.LogBy, A.LogDate  " & vbNewLine & _
                   "FROM mstUser A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataByUserIDAndPassword(ByVal strUserID As String, ByVal strPassword As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.ID, A.StaffID, A.Name, A.Password, A.Position, A.IsSuperUser, A.IsFirstCreated " & vbNewLine & _
                   "FROM mstUser A " & vbNewLine & _
                   "WHERE " & vbNewLine & _
                   "    A.ID=@ID " & vbNewLine & _
                   "    AND A.Password=@Password " & vbNewLine & _
                   "    AND A.IDStatus=@IDStatus " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strUserID
                .Parameters.Add("@Password", SqlDbType.NVarChar).Value = strPassword
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Active
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ID]	" & vbNewLine & _
                    "      ,[StaffID]	" & vbNewLine & _
                    "      ,[Name]	" & vbNewLine & _
                    "      ,[Password]	" & vbNewLine & _
                    "      ,[Position]	" & vbNewLine & _
                    "      ,[IDStatus]	" & vbNewLine & _
                    "      ,[CreatedBy]	" & vbNewLine & _
                    "      ,[CreatedDate]	" & vbNewLine & _
                    "      ,[LogBy]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "      ,[LogInc]	" & vbNewLine & _
                    "      ,[IsSuperUser]	" & vbNewLine & _
                    "      ,[IsFirstCreated]	" & vbNewLine & _
                    "  FROM [dbo].[mstUser]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.User)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstUser " & vbNewLine & _
                       "    (ID, StaffID, Name, Password, Position, IDStatus, CreatedBy,   " & vbNewLine & _
                       "      CreatedDate, LogBy, LogDate, IsSuperUser, IsFirstCreated)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @StaffID, @Name, @Password, @Position, @IDStatus, @LogBy,   " & vbNewLine & _
                       "      GETDATE(), @LogBy, GETDATE(), @IsSuperUser, 1)  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstUser SET " & vbNewLine & _
                    "    StaffID=@StaffID, " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    Password=@Password, " & vbNewLine & _
                    "    Position=@Position, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@StaffID", SqlDbType.VarChar, 10).Value = clsData.StaffID
                .Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = clsData.Name
                .Parameters.Add("@Password", SqlDbType.NVarChar).Value = clsData.Password
                .Parameters.Add("@Position", SqlDbType.VarChar, 100).Value = clsData.Position
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@IsSuperUser", SqlDbType.Bit).Value = clsData.IsSuperUser
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.User)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstUser " & vbNewLine & _
                   "    (ID, StaffID, Name, Password, Position, IDStatus, CreatedBy,   " & vbNewLine & _
                   "     CreatedDate, LogBy, LogDate, LogInc, IsSuperUser, IsFirstCreated)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @StaffID, @Name, @Password, @Position, @IDStatus, @CreatedBy,   " & vbNewLine & _
                   "     @CreatedDate, @LogBy, @LogDate, @LogInc, @IsSuperUser, @IsFirstCreated)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@StaffID", SqlDbType.VarChar, 10).Value = clsData.StaffID
                .Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = clsData.Name
                .Parameters.Add("@Password", SqlDbType.NVarChar).Value = clsData.Password
                .Parameters.Add("@Position", SqlDbType.VarChar, 100).Value = clsData.Position
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 20).Value = clsData.CreatedBy
                .Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = clsData.CreatedDate
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@LogDate", SqlDbType.DateTime).Value = clsData.LogDate
                .Parameters.Add("@LogInc", SqlDbType.Int).Value = clsData.LogInc
                .Parameters.Add("@IsSuperUser", SqlDbType.Bit).Value = clsData.IsSuperUser
                .Parameters.Add("@IsFirstCreated", SqlDbType.Bit).Value = clsData.IsFirstCreated
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.User
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.User
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.StaffID, A.Name, A.Password, A.Position, A.IDStatus, A.LogBy,   " & vbNewLine & _
                       "    A.LogDate, IsSuperUser, IsFirstCreated" & vbNewLine & _
                       "FROM mstUser A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.StaffID = .Item("StaffID")
                        voReturn.Name = .Item("Name")
                        voReturn.Password = .Item("Password")
                        voReturn.Position = .Item("Position")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.IsSuperUser = .Item("IsSuperUser")
                        voReturn.IsFirstCreated = .Item("IsFirstCreated")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return voReturn
        End Function

        Public Shared Sub DeleteData(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE mstUser " & vbNewLine & _
                    "SET IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.InActive
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
                    "DELETE mstUser " & vbNewLine 

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID(ByVal strID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(StaffID),3),0) " & vbNewLine & _
                        "FROM mstUser " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(StaffID,7)=@StaffID " & vbNewLine

                    .Parameters.Add("@StaffID", SqlDbType.VarChar, 7).Value = strID

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

        Public Shared Function DataExists(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM mstUser " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolExists = True
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolExists
        End Function

        Public Shared Function GetIDStatus(ByVal intID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = VO.Status.Values.Active
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   IDStatus " & vbNewLine & _
                        "FROM mstUser " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = intID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intReturn = .Item("IDStatus")
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

        Public Shared Sub ChangePassword(ByVal clsData As VO.User)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                "UPDATE mstUser SET " & vbNewLine & _
                "    Password=@Password, " & vbNewLine & _
                "    LogBy=@LogBy, " & vbNewLine & _
                "    LogInc=LogInc+1, " & vbNewLine & _
                "    LogDate=GETDATE() " & vbNewLine & _
                "WHERE " & vbNewLine & _
                "    ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = clsData.ID
                .Parameters.Add("@Password", SqlDbType.NVarChar).Value = clsData.Password
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

    End Class

End Namespace

