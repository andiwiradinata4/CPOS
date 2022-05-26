Namespace DL

    Public Class SQL

        Public Shared sqlConn As SqlConnection
        Public Shared sqlTrans As SqlTransaction
        Public Shared strServer, strDatabase As String
        Public Shared strSAID As String = "", strSAPassword As String = ""
        Public Shared bolUseTrans As Boolean = False
        Public Shared strAplicationName As String = "TMS"

        '<System.Diagnostics.DebuggerStepThrough()>
        Public Shared Function OpenConnection() As Boolean
            Dim strSqlConnect As String = "Server=" & strServer & ";" & _
                                          "DataBase=" & strDatabase & ";" & _
                                          "Application Name=" & strAplicationName & ";"
            Dim bolSuccess As Boolean = False
            'If strSAID = "" Then
            '    strSqlConnect += "Trusted_Connection=SSPI;"
            'Else
            '    strSqlConnect += "Trusted_Connection=FALSE;" & _
            '                     "User Id=" & strSAID & ";" & _
            '                     "Password=" & strSAPassword & ";"
            'End If
            If strServer.Trim = "LOCALHOST" Then
                strSqlConnect += "Trusted_Connection=SSPI;"
            Else
                strSqlConnect += "Trusted_Connection=FALSE;" & _
                                 "User Id=" & MPSLib.UI.usUserApp.UserID & ";" & _
                                 "Password=" & strSAPassword & ";"
            End If

            Try
                sqlConn = New SqlConnection(strSqlConnect)
                sqlConn.Open()
                bolSuccess = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bolSuccess
        End Function

        <System.Diagnostics.DebuggerStepThrough()>
        Public Shared Sub CloseConnection()
            If sqlConn.State <> ConnectionState.Closed Then sqlConn.Close()
        End Sub

        <System.Diagnostics.DebuggerStepThrough()>
        Public Shared Sub BeginTransaction()
            sqlTrans = sqlConn.BeginTransaction()
            bolUseTrans = True
        End Sub

        <System.Diagnostics.DebuggerStepThrough()>
        Public Shared Sub CommitTransaction()
            sqlTrans.Commit()
            bolUseTrans = False
        End Sub

        <System.Diagnostics.DebuggerStepThrough>
        Public Shared Sub RollBackTransaction()
            sqlTrans.Rollback()
            bolUseTrans = False
        End Sub

        <System.Diagnostics.DebuggerStepThrough()>
        Public Shared Function QueryDataTable(ByVal sqlcmdExecute As SqlCommand) As DataTable
            Dim sqldapQuery As New SqlDataAdapter
            Dim dtbQuerry As New DataTable
            Try
                If Not bolUseTrans Then OpenConnection()
                With sqlcmdExecute
                    .Connection = sqlConn
                    .CommandTimeout = 300
                    If bolUseTrans Then .Transaction = sqlTrans
                End With
                sqldapQuery.SelectCommand = sqlcmdExecute
                sqldapQuery.Fill(dtbQuerry)
                If Not bolUseTrans Then CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbQuerry
        End Function

        <System.Diagnostics.DebuggerStepThrough>
        Public Shared Function ExecuteNonQuery(ByRef sqlcmdExecute As SqlCommand) As Boolean
            Dim bolSuccess As Boolean = False
            Try
                If Not bolUseTrans Then OpenConnection()
                With sqlcmdExecute
                    .Connection = sqlConn
                    .CommandTimeout = 300
                    If bolUseTrans Then .Transaction = sqlTrans
                    .ExecuteNonQuery()
                End With
                If Not bolUseTrans Then CloseConnection()
                bolSuccess = True
            Catch ex As Exception
                Throw ex
            End Try
            Return bolSuccess
        End Function

        Public Shared Function QueryDataTableMax(ByVal sqlcmdExecute As SqlCommand) As DataTable
            Dim sqldapQuery As New SqlDataAdapter
            Dim dtbQuerry As New DataTable
            Try
                If Not bolUseTrans Then OpenConnection()
                With sqlcmdExecute
                    .Connection = sqlConn
                    .CommandTimeout = 1800
                    If bolUseTrans Then .Transaction = sqlTrans
                End With
                sqldapQuery.SelectCommand = sqlcmdExecute
                sqldapQuery.Fill(dtbQuerry)
                If Not bolUseTrans Then CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbQuerry
        End Function

    End Class

End Namespace