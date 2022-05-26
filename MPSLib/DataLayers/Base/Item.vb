Namespace DL
    Public Class Item

        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "   MI.ID, MI.Code, MI.Name, MI.UomID, MU.Code AS UomCode, MI.MinQty,   	" & vbNewLine & _
                    "   MI.BalanceQty, MI.SalesPrice, MI.PurchasePrice1, MI.PurchasePrice2, MI.Tolerance, MI.IDStatus, MS.Name AS StatusInfo, MI.CreatedBy, MI.CreatedDate, MI.LogBy,   	" & vbNewLine & _
                    "   MI.LogDate " & vbNewLine & _
                    "FROM mstItem MI 	" & vbNewLine & _
                    "INNER JOIN mstStatus MS ON 	" & vbNewLine & _
                    "	MI.IDStatus=MS.ID 	" & vbNewLine & _
                    "INNER JOIN mstUOM MU ON 	" & vbNewLine & _
                    "	MI.UomID=MU.ID 	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ID]	" & vbNewLine & _
                    "      ,[Code]	" & vbNewLine & _
                    "      ,[Name]	" & vbNewLine & _
                    "      ,[UomID]	" & vbNewLine & _
                    "      ,[MinQty]	" & vbNewLine & _
                    "      ,[BalanceQty]	" & vbNewLine & _
                    "      ,[SalesPrice]	" & vbNewLine & _
                    "      ,[PurchasePrice1]	" & vbNewLine & _
                    "      ,[PurchasePrice2]	" & vbNewLine & _
                    "      ,[Tolerance]	" & vbNewLine & _
                    "      ,[IDStatus]	" & vbNewLine & _
                    "      ,[CreatedBy]	" & vbNewLine & _
                    "      ,[CreatedDate]	" & vbNewLine & _
                    "      ,[LogBy]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "      ,[LogInc]	" & vbNewLine & _
                    "  FROM [dbo].[mstItem]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Item)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstItem " & vbNewLine & _
                       "    (ID, Code, Name, UomID, MinQty,   " & vbNewLine & _
                       "     BalanceQty, SalesPrice, PurchasePrice1, PurchasePrice2, Tolerance, IDStatus, CreatedBy, CreatedDate, LogBy,   " & vbNewLine & _
                       "     LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @Code, @Name, @UomID, @MinQty,   " & vbNewLine & _
                       "     @BalanceQty, @SalesPrice, @PurchasePrice1, @PurchasePrice2, @Tolerance, @IDStatus, @LogBy, GETDATE(), @LogBy,   " & vbNewLine & _
                       "     GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstItem SET " & vbNewLine & _
                    "    Code=@Code, " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    UomID=@UomID, " & vbNewLine & _
                    "    MinQty=@MinQty, " & vbNewLine & _
                    "    BalanceQty=@BalanceQty, " & vbNewLine & _
                    "    SalesPrice=@SalesPrice, " & vbNewLine & _
                    "    PurchasePrice1=@PurchasePrice1, " & vbNewLine & _
                    "    PurchasePrice2=@PurchasePrice2, " & vbNewLine & _
                    "    Tolerance=@Tolerance, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = clsData.Name
                .Parameters.Add("@UomID", SqlDbType.VarChar, 10).Value = clsData.UomID
                .Parameters.Add("@MinQty", SqlDbType.Decimal).Value = clsData.MinQty
                .Parameters.Add("@BalanceQty", SqlDbType.Decimal).Value = clsData.BalanceQty
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
                .Parameters.Add("@Tolerance", SqlDbType.Decimal).Value = clsData.Tolerance
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.Item)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstItem " & vbNewLine & _
                   "    (ID, Code, Name, UomID, MinQty,   " & vbNewLine & _
                   "     BalanceQty, SalesPrice, PurchasePrice1, PurchasePrice2, Tolerance, IDStatus, CreatedBy, CreatedDate, LogBy,   " & vbNewLine & _
                   "     LogDate, LogInc)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @Code, @Name, @UomID, @MinQty,   " & vbNewLine & _
                   "     @BalanceQty, @SalesPrice, @PurchasePrice1, @PurchasePrice2, @Tolerance, @IDStatus, @CreatedBy, @CreatedDate, @LogBy,   " & vbNewLine & _
                   "     @LogDate, @LogInc)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = clsData.Name
                .Parameters.Add("@UomID", SqlDbType.VarChar, 10).Value = clsData.UomID
                .Parameters.Add("@MinQty", SqlDbType.Decimal).Value = clsData.MinQty
                .Parameters.Add("@BalanceQty", SqlDbType.Decimal).Value = clsData.BalanceQty
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
                .Parameters.Add("@Tolerance", SqlDbType.Decimal).Value = clsData.Tolerance
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

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.Item
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Item
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.Code, A.Name, A.UomID, A.MinQty,   " & vbNewLine & _
                       "    A.BalanceQty, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2, A.Tolerance, A.IDStatus, A.LogBy, A.LogDate " & vbNewLine & _
                       "FROM mstItem A " & vbNewLine & _
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
                        voReturn.Code = .Item("Code")
                        voReturn.Name = .Item("Name")
                        voReturn.UomID = .Item("UomID")
                        voReturn.MinQty = .Item("MinQty")
                        voReturn.BalanceQty = .Item("BalanceQty")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
                        voReturn.Tolerance = .Item("Tolerance")
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

        Public Shared Function GetDetail(ByVal strItemCode As String) As VO.Item
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Item
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.Code, A.Name, A.UomID, A.MinQty,   " & vbNewLine & _
                       "    A.BalanceQty, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2, A.Tolerance, A.IDStatus, A.LogBy, A.LogDate " & vbNewLine & _
                       "FROM mstItem A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    Code=@Code " & vbNewLine

                    .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = strItemCode
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.Code = .Item("Code")
                        voReturn.Name = .Item("Name")
                        voReturn.UomID = .Item("UomID")
                        voReturn.MinQty = .Item("MinQty")
                        voReturn.BalanceQty = .Item("BalanceQty")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
                        voReturn.Tolerance = .Item("Tolerance")
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
                    "UPDATE mstItem " & vbNewLine & _
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
                    "DELETE mstItem " & vbNewLine 

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
                        "FROM mstItem " & vbNewLine

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
                        "FROM mstItem " & vbNewLine & _
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
                        "FROM mstItem " & vbNewLine & _
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

