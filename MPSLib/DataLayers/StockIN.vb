Namespace DL
    Public Class StockIN

        Public Shared Function ListDataOutstanding(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.ItemID, A.ReferencesID, A.ReferencesDate, A.Qty, A.Price,   " & vbNewLine & _
                   "     A.NetPrice, A.QtyOut, A.LogDate  " & vbNewLine & _
                   "FROM sysStockIN A " & vbNewLine & _
                   "WHERE " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.ItemID=@ItemID " & vbNewLine & _
                   "    AND A.Qty-QtyOut>0  " & vbNewLine & _
                   "ORDER BY A.ID ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ProgramID]	" & vbNewLine & _
                    "      ,[CompanyID]	" & vbNewLine & _
                    "      ,[ID]	" & vbNewLine & _
                    "      ,[ItemID]	" & vbNewLine & _
                    "      ,[ReferencesID]	" & vbNewLine & _
                    "      ,[ReferencesDate]	" & vbNewLine & _
                    "      ,[Qty]	" & vbNewLine & _
                    "      ,[Price]	" & vbNewLine & _
                    "      ,[NetPrice]	" & vbNewLine & _
                    "      ,[QtyOut]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "  FROM [dbo].[sysStockIN]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub UpdateQtyStockOut(ByVal intID As Integer, ByVal decQtyOut As Decimal)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                "UPDATE sysStockIN SET " & vbNewLine & _
                "   QtyOut=@QtyOut, " & vbNewLine & _
                "   LogDate=GETDATE() " & vbNewLine & _
                "WHERE " & vbNewLine & _
                "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = intID
                .Parameters.Add("@QtyOut", SqlDbType.Decimal).Value = decQtyOut
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
                        "FROM sysStockIN " & vbNewLine

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

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.StockIN)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO sysStockIN " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, ItemID, ReferencesID, ReferencesDate, Qty, Price,   " & vbNewLine & _
                       "     NetPrice, QtyOut, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @ItemID, @ReferencesID, @ReferencesDate, @Qty, @Price,   " & vbNewLine & _
                       "     @NetPrice, @QtyOut, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE sysStockIN SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    ProgramID=@ProgramID, " & vbNewLine & _
                    "    ItemID=@ItemID, " & vbNewLine & _
                    "    ReferencesID=@ReferencesID, " & vbNewLine & _
                    "    ReferencesDate=@ReferencesDate, " & vbNewLine & _
                    "    Qty=@Qty, " & vbNewLine & _
                    "    Price=@Price, " & vbNewLine & _
                    "    NetPrice=@NetPrice, " & vbNewLine & _
                    "    QtyOut=@QtyOut, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.BigInt).Value = clsData.ID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@ReferencesDate", SqlDbType.DateTime).Value = clsData.ReferencesDate
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@NetPrice", SqlDbType.Decimal).Value = clsData.NetPrice
                .Parameters.Add("@QtyOut", SqlDbType.Decimal).Value = clsData.QtyOut
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.StockIN)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO sysStockIN " & vbNewLine & _
                   "    (CompanyID, ProgramID, ID, ItemID, ReferencesID, ReferencesDate, Qty, Price,   " & vbNewLine & _
                   "     NetPrice, QtyOut, LogDate)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@CompanyID, @ProgramID, @ID, @ItemID, @ReferencesID, @ReferencesDate, @Qty, @Price,   " & vbNewLine & _
                   "     @NetPrice, @QtyOut, @LogDate)  " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.BigInt).Value = clsData.ID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@ReferencesDate", SqlDbType.DateTime).Value = clsData.ReferencesDate
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@NetPrice", SqlDbType.Decimal).Value = clsData.NetPrice
                .Parameters.Add("@QtyOut", SqlDbType.Decimal).Value = clsData.QtyOut
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
                    "SELECT 	" & vbNewLine & _
                    "	SI.ID, SO.Qty, SI.Price, SI.NetPrice 	" & vbNewLine & _
                    "FROM sysStockOut SO 	" & vbNewLine & _
                    "INNER JOIN sysStockIN SI ON 	" & vbNewLine & _
                    "	SO.IDStockIN=SI.ID 	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   SO.CompanyID=@CompanyID " & vbNewLine & _
                    "   AND SO.ProgramID=@ProgramID " & vbNewLine & _
                    "   AND SO.ReferencesID=@ReferencesID " & vbNewLine & _
                    "ORDER BY SO.ID DESC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strReferencesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub DeleteDataByReferencesID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal strReferencesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM sysStockIN " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
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

        Public Shared Sub DeleteDataAll()
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE sysStockIN " & vbNewLine

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DecreaseQtyStockOut(ByVal intID As Integer, ByVal decQty As Decimal)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                "UPDATE sysStockIN SET " & vbNewLine & _
                "    QtyOut=QtyOut-@Qty, " & vbNewLine & _
                "    LogDate=GETDATE() " & vbNewLine & _
                "WHERE " & vbNewLine & _
                "    ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.BigInt).Value = intID
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = decQty
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

    End Class

End Namespace

