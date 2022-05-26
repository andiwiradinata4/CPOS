Namespace DL
    Public Class BukuBesar
        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        Optional ByVal intCOAIDParent As Integer = 0) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ProgramID, A.ID, A.TransactionDate, A.ReferencesID, A.COAIDParent, COAP.Code AS COAParentCode, COAP.Name AS COAParentName, " & vbNewLine & _
                   "     A.COAIDChild, COAC.Code AS COACodeChild, COAC.Name AS COANameChild, A.DebitAmount, A.CreditAmount,   " & vbNewLine & _
                   "     A.Balance, A.Remarks, A.CreatedBy, A.CreatedDate, RemarksInfo=CASE WHEN A.Remarks='' THEN COAC.Name ELSE A.Remarks END " & vbNewLine & _
                   "FROM traBukuBesar A " & vbNewLine & _
                   "INNER JOIN mstChartOfAccount COAP ON " & vbNewLine & _
                   "    A.COAIDParent=COAP.ID " & vbNewLine & _
                   "INNER JOIN mstChartOfAccount COAC ON " & vbNewLine & _
                   "    A.COAIDChild=COAC.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID" & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID" & vbNewLine & _
                   "    AND A.TransactionDate>=@DateFrom AND A.TransactionDate<=@DateTo " & vbNewLine

                If intCOAIDParent <> 0 Then
                    .CommandText += "    AND A.COAIDParent=@COAIDParent" & vbNewLine
                End If

                .CommandText += "ORDER BY A.TransactionDate, A.COAIDParent ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@COAIDParent", SqlDbType.Int).Value = intCOAIDParent
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal clsData As VO.BukuBesar)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "INSERT INTO traBukuBesar " & vbNewLine & _
                    "    (CompanyID, ProgramID, ID, TransactionDate, ReferencesID, COAIDParent, COAIDChild, DebitAmount, CreditAmount,   " & vbNewLine & _
                    "     Remarks, CreatedBy, CreatedDate)   " & vbNewLine & _
                    "VALUES " & vbNewLine & _
                    "    (@CompanyID, @ProgramID, @ID, @TransactionDate, @ReferencesID, @COAIDParent, @COAIDChild, @DebitAmount, @CreditAmount,   " & vbNewLine & _
                    "     @Remarks, @LogBy, GETDATE())  " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = clsData.TransactionDate
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@COAIDParent", SqlDbType.Int).Value = clsData.COAIDParent
                .Parameters.Add("@COAIDChild", SqlDbType.Int).Value = clsData.COAIDChild
                .Parameters.Add("@DebitAmount", SqlDbType.Decimal).Value = clsData.DebitAmount
                .Parameters.Add("@CreditAmount", SqlDbType.Decimal).Value = clsData.CreditAmount
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = clsData.Remarks
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traBukuBesar " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal strReferencesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traBukuBesar " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ProgramID=@ProgramID " & vbNewLine & _
                    "   AND CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ReferencesID=@ReferencesID " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strReferencesID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                     ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traBukuBesar " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    CompanyID=@CompanyID" & vbNewLine & _
                    "    AND ProgramID=@ProgramID" & vbNewLine & _
                    "    AND TransactionDate>=@DateFrom AND TransactionDate<=@DateTo " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
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
                        "   ID=ISNULL(RIGHT(MAX(ID),5),0) " & vbNewLine & _
                        "FROM traBukuBesar " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ID,16)=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 16).Value = strID
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

