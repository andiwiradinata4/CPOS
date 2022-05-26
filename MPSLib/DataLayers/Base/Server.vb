Namespace DL
    Public Class Server
        Public Shared Function ServerList() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If VO.DefaultServer.ReportingServer = 0 Then
                    .CommandText = _
                    "SELECT " & vbNewLine & _
                    "   Server, DBName, UserID, UserPassword " & vbNewLine & _
                    "FROM QMS_sysServerList " & vbNewLine & _
                    "WHERE IsActive = 1 " & vbNewLine & _
                    "GROUP BY " & vbNewLine & _
                    "   Server, DBName, UserID, UserPassword " & vbNewLine & _
                    "ORDER BY Server "
                Else
                    .CommandText = _
                    "SELECT " & vbNewLine & _
                    "   ServerReport as Server, DBName, UserID, UserPassword " & vbNewLine & _
                    "FROM QMS_sysServerList " & vbNewLine & _
                    "WHERE IsActive = 1 " & vbNewLine & _
                    "AND ServerReport <> '' " & vbNewLine & _
                    "GROUP BY " & vbNewLine & _
                    "   ServerReport, DBName, UserID, UserPassword " & vbNewLine & _
                    "ORDER BY ServerReport "
                End If

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ServerCount() As Integer
            Dim intCount As Integer = 0
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    If VO.DefaultServer.ReportingServer = 0 Then
                        .CommandText = _
                        "SELECT COUNT(*) AS Total " & vbNewLine & _
                        "FROM " & vbNewLine & _
                        "   (SELECT Server, DBName " & vbNewLine & _
                        "   FROM QMS_sysServerList " & vbNewLine & _
                        "   GROUP BY Server, DBName) A "
                    Else
                        .CommandText = _
                        "SELECT COUNT(*) AS Total " & vbNewLine & _
                        "FROM " & vbNewLine & _
                        "   (SELECT ServerReport as Server, DBName " & vbNewLine & _
                        "   FROM QMS_sysServerList " & vbNewLine & _
                        "   WHERE IsActive = 1 " & vbNewLine & _
                        "   AND ServerReport <> '' " & vbNewLine & _
                        "   GROUP BY ServerReport, DBName) A "
                    End If

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        intCount = .Item("Total")
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return intCount
        End Function

        Public Shared Function ServerAllCompanyList() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If VO.DefaultServer.ReportingServer = 0 Then
                    .CommandText = _
                    "SELECT " & vbNewLine & _
                    "   CompanyID, Server, DBName, UserID, UserPassword " & vbNewLine & _
                    "FROM QMS_sysServerList " & vbNewLine & _
                    "WHERE IsActive = 1 " & vbNewLine & _
                    "ORDER BY " & vbNewLine & _
                    "   CompanyID "
                Else
                    .CommandText = _
                    "SELECT " & vbNewLine & _
                    "   CompanyID, ServerReport as Server, DBName, UserID, UserPassword " & vbNewLine & _
                    "FROM QMS_sysServerList " & vbNewLine & _
                    "WHERE IsActive = 1 " & vbNewLine & _
                    "AND ServerReport <> '' " & vbNewLine & _
                    "ORDER BY " & vbNewLine & _
                    "   CompanyID "
                End If

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ServerByCompany(ByVal strCompanyID As String) As VO.Server
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader
            Dim clsReturn As New VO.Server
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    If VO.DefaultServer.ReportingServer = 0 Then
                        .CommandText = _
                        "SELECT TOP 1  " & vbNewLine & _
                        "   Server, DBName, UserID, UserPassword " & vbNewLine & _
                        "FROM QMS_sysServerList " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "   CompanyID=@CompanyID "
                    Else
                        .CommandText = _
                        "SELECT TOP 1  " & vbNewLine & _
                        "   ServerReport as Server, DBName, UserID, UserPassword " & vbNewLine & _
                        "FROM QMS_sysServerList " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "   CompanyID=@CompanyID "
                    End If

                    .Parameters.Add("@CompanyID", SqlDbType.VarChar, 10).Value = strCompanyID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans

                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        clsReturn.CompanyID = strCompanyID
                        clsReturn.Server = .Item("Server")
                        clsReturn.DBName = .Item("DBName")
                        clsReturn.UserID = .Item("UserID")
                        clsReturn.UserPassword = .Item("UserPassword")
                    End If
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return clsReturn
        End Function
    End Class
End Namespace
