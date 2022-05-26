Namespace DL
    Public Class ChartOfAccount

        Public Shared Function ListData(ByVal enumFilterGroup As VO.ChartOfAccount.FilterGroup, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT CAST(0 AS BIT) AS Pick, " & vbNewLine & _
                    "	COA.ID, COA.AccountGroupID, COAG.Name AS AccountGroupName, COAG.AliasName + ' ' + COAG.Name AS GroupAccount, COAT.Name AS TypeAccount, COA.Code, COA.Name, 	" & vbNewLine & _
                    "	COA.FirstBalance AS Balance, COA.IDStatus, MS.Name AS StatusInfo, COA.CreatedBy, COA.CreatedDate, COA.LogBy, COA.LogDate, COA.LogInc 	" & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccountGroup COAG ON  	" & vbNewLine & _
                    "    COA.AccountGroupID=COAG.ID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccountType COAT ON  	" & vbNewLine & _
                    "    COAG.COAType=COAT.ID 	" & vbNewLine & _
                    "INNER JOIN mstStatus MS ON  	" & vbNewLine & _
                    "    COA.IDStatus=MS.ID 	" & vbNewLine

                If intCompanyID <> 0 And intProgramID <> 0 Then
                    .CommandText += _
                        "INNER JOIN mstChartOfAccountAssign COAA ON  	" & vbNewLine & _
                        "    COA.ID=COAA.COAID " & vbNewLine & _
                        "    AND COAA.CompanyID=@CompanyID " & vbNewLine & _
                        "    AND COAA.ProgramID=@ProgramID " & vbNewLine
                End If

                If enumFilterGroup = VO.ChartOfAccount.FilterGroup.CashOrBank Then
                    .CommandText += "WHERE COAG.ID IN (1,2)"
                ElseIf enumFilterGroup = VO.ChartOfAccount.FilterGroup.Expense Then
                    .CommandText += "WHERE COAT.ID=6 "
                End If

                .CommandText += _
                    "ORDER BY " & vbNewLine & _
                    "    COAT.ID, COA.Code ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ID]	" & vbNewLine & _
                    "      ,[AccountGroupID]	" & vbNewLine & _
                    "      ,[Code]	" & vbNewLine & _
                    "      ,[Name]	" & vbNewLine & _
                    "      ,[FirstBalance]	" & vbNewLine & _
                    "      ,[FirstBalanceDate]	" & vbNewLine & _
                    "      ,[IDStatus]	" & vbNewLine & _
                    "      ,[CreatedBy]	" & vbNewLine & _
                    "      ,[CreatedDate]	" & vbNewLine & _
                    "      ,[LogBy]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "      ,[LogInc]	" & vbNewLine & _
                    "  FROM [dbo].[mstChartOfAccount]	" & vbNewLine
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ChartOfAccount)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstChartOfAccount " & vbNewLine & _
                       "    (ID, AccountGroupID, Code, Name, FirstBalance, FirstBalanceDate, IDStatus,   " & vbNewLine & _
                       "      CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @AccountGroupID, @Code, @Name, @FirstBalance, @FirstBalanceDate, @IDStatus,   " & vbNewLine & _
                       "      @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstChartOfAccount SET " & vbNewLine & _
                    "    AccountGroupID=@AccountGroupID, " & vbNewLine & _
                    "    Code=@Code, " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    FirstBalance=@FirstBalance, " & vbNewLine & _
                    "    FirstBalanceDate=@FirstBalanceDate, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@AccountGroupID", SqlDbType.Int).Value = clsData.AccountGroupID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = clsData.Name
                .Parameters.Add("@FirstBalance", SqlDbType.Decimal).Value = clsData.FirstBalance
                .Parameters.Add("@FirstBalanceDate", SqlDbType.DateTime).Value = clsData.FirstBalanceDate
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.ChartOfAccount)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstChartOfAccount " & vbNewLine & _
                   "    (ID, AccountGroupID, Code, Name, FirstBalance, FirstBalanceDate, IDStatus,   " & vbNewLine & _
                   "     CreatedBy, CreatedDate, LogBy, LogDate, LogInc)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @AccountGroupID, @Code, @Name, @FirstBalance, @FirstBalanceDate, @IDStatus,   " & vbNewLine & _
                   "     @CreatedBy, @CreatedDate, @LogBy, @LogDate, @LogInc)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@AccountGroupID", SqlDbType.Int).Value = clsData.AccountGroupID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = clsData.Name
                .Parameters.Add("@FirstBalance", SqlDbType.Decimal).Value = clsData.FirstBalance
                .Parameters.Add("@FirstBalanceDate", SqlDbType.DateTime).Value = clsData.FirstBalanceDate
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@CreatedBy", SqlDbType.VarChar, 20).Value = clsData.CreatedBy
                .Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = clsData.CreatedDate
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@LogDate", SqlDbType.DateTime).Value = clsData.LogDate
                .Parameters.Add("@LogInc", SqlDbType.Int).Value = clsData.LogInc
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.ChartOfAccount
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.ChartOfAccount
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.AccountGroupID, A.Code, A.Name, A.FirstBalance, A.FirstBalanceDate, A.IDStatus,   " & vbNewLine & _
                       "    A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstChartOfAccount A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.AccountGroupID = .Item("AccountGroupID")
                        voReturn.Code = .Item("Code")
                        voReturn.Name = .Item("Name")
                        voReturn.FirstBalance = .Item("FirstBalance")
                        voReturn.FirstBalanceDate = .Item("FirstBalanceDate")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
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

        Public Shared Function GetDetail(ByVal strCode As String) As VO.ChartOfAccount
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.ChartOfAccount
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.AccountGroupID, A.Code, A.Name, A.FirstBalance, A.FirstBalanceDate, A.IDStatus,   " & vbNewLine & _
                       "    A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstChartOfAccount A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    Code=@Code " & vbNewLine

                    .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = strCode
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.AccountGroupID = .Item("AccountGroupID")
                        voReturn.Code = .Item("Code")
                        voReturn.Name = .Item("Name")
                        voReturn.FirstBalance = .Item("FirstBalance")
                        voReturn.FirstBalanceDate = .Item("FirstBalanceDate")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
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

        Public Shared Sub DeleteData(ByVal intID As Integer)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE mstChartOfAccount " & vbNewLine & _
                    "SET IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = intID
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
                    "DELETE mstChartOfAccount " & vbNewLine 

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxID() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine

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

        Public Shared Function DataExists(ByVal intID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID

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

        Public Shared Function CodeExists(ByVal strCode As String, ByVal intID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   Code=@Code " & vbNewLine & _
                        "   AND ID<>@ID " & vbNewLine

                    .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = strCode
                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
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

        Public Shared Function GetIDStatus(ByVal intID As Integer) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = VO.Status.Values.Active
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   IDStatus " & vbNewLine & _
                        "FROM mstChartOfAccount " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
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

    End Class

End Namespace

