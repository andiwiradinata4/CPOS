Namespace DL
    Public Class StockOut

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ProgramID]	" & vbNewLine & _
                    "      ,[CompanyID]	" & vbNewLine & _
                    "      ,[ID]	" & vbNewLine & _
                    "      ,[IDStockIN]	" & vbNewLine & _
                    "      ,[ItemID]	" & vbNewLine & _
                    "      ,[ReferencesID]	" & vbNewLine & _
                    "      ,[ReferencesDate]	" & vbNewLine & _
                    "      ,[Qty]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "  FROM [dbo].[sysStockOut]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

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
                        "FROM sysStockOut " & vbNewLine

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

        Public Shared Sub SaveData(ByVal clsData As VO.StockOut)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO sysStockOut " & vbNewLine & _
                   "    (ProgramID, CompanyID, ID, IDStockIN, ItemID, ReferencesID, ReferencesDate, Qty,   " & vbNewLine & _
                   "     LogDate)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ProgramID, @CompanyID, @ID, @IDStockIN, @ItemID, @ReferencesID, @ReferencesDate, @Qty,   " & vbNewLine & _
                   "     GETDATE())  " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ID", SqlDbType.BigInt).Value = clsData.ID
                .Parameters.Add("@IDStockIN", SqlDbType.Int).Value = clsData.IDStockIN
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@ReferencesDate", SqlDbType.DateTime).Value = clsData.ReferencesDate
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.StockOut)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO sysStockOut " & vbNewLine & _
                   "    (ProgramID, CompanyID, ID, IDStockIN, ItemID, ReferencesID, ReferencesDate, Qty,   " & vbNewLine & _
                   "     LogDate)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ProgramID, @CompanyID, @ID, @IDStockIN, @ItemID, @ReferencesID, @ReferencesDate, @Qty,   " & vbNewLine & _
                   "     @LogDate)  " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ID", SqlDbType.BigInt).Value = clsData.ID
                .Parameters.Add("@IDStockIN", SqlDbType.Int).Value = clsData.IDStockIN
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@ReferencesDate", SqlDbType.DateTime).Value = clsData.ReferencesDate
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
                .Parameters.Add("@LogDate", SqlDbType.DateTime).Value = clsData.LogDate
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function ListDataByReferencesID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal strReferencesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT " & vbNewLine & _
                    "   A.ID, A.IDStockIN, A.ItemID, A.ReferencesID, A.ReferencesDate, A.Qty,   " & vbNewLine & _
                    "   A.LogDate  " & vbNewLine & _
                    "FROM sysStockOut A " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   A.ProgramID=@ProgramID " & vbNewLine & _
                    "   AND A.CompanyID=@CompanyID " & vbNewLine & _
                    "   AND A.ReferencesID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strReferencesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub DeleteDataByReferencesID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal strReferencesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM sysStockOut " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ProgramID=@ProgramID " & vbNewLine & _
                    "   AND CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ReferencesID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strReferencesID
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
                    "DELETE sysStockOut " & vbNewLine

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

    End Class

End Namespace

