Namespace DL
    Public Class ChartOfAccountAssign

        Public Shared Function ListData(ByVal intCOAID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    CAST(A.ID AS VARCHAR(30)) AS ID, A.COAID, A.ProgramID, MP.Name AS ProgramName, " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.FirstBalance, A.FirstBalanceDate  " & vbNewLine & _
                   "FROM mstChartOfAccountAssign A " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.COAID=@COAID" & vbNewLine

                .Parameters.Add("@COAID", SqlDbType.Int).Value = intCOAID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [CompanyID]	" & vbNewLine & _
                    "      ,[ProgramID]	" & vbNewLine & _
                    "      ,[ID]	" & vbNewLine & _
                    "      ,[COAID]	" & vbNewLine & _
                    "      ,[FirstBalance]	" & vbNewLine & _
                    "      ,[FirstBalanceDate]	" & vbNewLine & _
                    "  FROM [dbo].[mstChartOfAccountAssign]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal clsData As VO.ChartOfAccountAssign)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "INSERT INTO mstChartOfAccountAssign " & vbNewLine & _
                    "    (ID, COAID, ProgramID, CompanyID, FirstBalance, FirstBalanceDate)   " & vbNewLine & _
                    "VALUES " & vbNewLine & _
                    "    (@ID, @COAID, @ProgramID, @CompanyID, @FirstBalance, @FirstBalanceDate)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@COAID", SqlDbType.Int).Value = clsData.COAID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@FirstBalance", SqlDbType.Decimal).Value = clsData.FirstBalance
                .Parameters.Add("@FirstBalanceDate", SqlDbType.DateTime).Value = clsData.FirstBalanceDate
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal intCOAID As Integer)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM mstChartOfAccountAssign " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   COAID=@COAID " & vbNewLine

                .Parameters.Add("@COAID", SqlDbType.Int).Value = intCOAID
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
                    "DELETE mstChartOfAccountAssign " & vbNewLine

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
                        "FROM mstChartOfAccountAssign " & vbNewLine

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
                        "FROM mstChartOfAccountAssign " & vbNewLine & _
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
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolExists
        End Function

    End Class

End Namespace

