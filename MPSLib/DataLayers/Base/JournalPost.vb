Namespace DL

    Public Class JournalPost

        Public Shared Function ListDataAll() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ProgramID]	" & vbNewLine & _
                    "      ,[CoAofRevenue]	" & vbNewLine & _
                    "      ,[CoAofAccountReceivable]	" & vbNewLine & _
                    "      ,[CoAofSalesDisc]	" & vbNewLine & _
                    "      ,[CoAofPrepaidIncome]	" & vbNewLine & _
                    "      ,[CoAofCOGS]	" & vbNewLine & _
                    "      ,[CoAofStock]	" & vbNewLine & _
                    "      ,[CoAofCash]	" & vbNewLine & _
                    "      ,[CoAofAccountPayable]	" & vbNewLine & _
                    "      ,[CoAofPurchaseDisc]	" & vbNewLine & _
                    "      ,[CoAofPurchaseEquipments]	" & vbNewLine & _
                    "      ,[CoAofAdvancePayment]	" & vbNewLine & _
                    "      ,[CoAofSalesTax]	" & vbNewLine & _
                    "      ,[CoAofPurchaseTax]	" & vbNewLine & _
                    "      ,[Remarks]	" & vbNewLine & _
                    "      ,[CreatedBy]	" & vbNewLine & _
                    "      ,[CreatedDate]	" & vbNewLine & _
                    "      ,[LogInc]	" & vbNewLine & _
                    "      ,[LogBy]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "  FROM [dbo].[sysJournalPost]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.JournalPost)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO sysJournalPost " & vbNewLine & _
                       "    (ProgramID, CoAofRevenue, CoAofAccountReceivable, CoAofSalesDisc, CoAofPrepaidIncome, CoAofCOGS, CoAofStock, CoAofCash, CoAofAccountPayable,   " & vbNewLine & _
                       "     CoAofPurchaseDisc, CoAofPurchaseEquipments, CoAofAdvancePayment, CoAofSalesTax, CoAofPurchaseTax, Remarks, CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ProgramID, @CoAofRevenue, @CoAofAccountReceivable, @CoAofSalesDisc, @CoAofPrepaidIncome, @CoAofCOGS, @CoAofStock, @CoAofCash, @CoAofAccountPayable,   " & vbNewLine & _
                       "     @CoAofPurchaseDisc, @CoAofPurchaseEquipments, @CoAofAdvancePayment, @CoAofSalesTax, @CoAofPurchaseTax, @Remarks, @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE sysJournalPost SET " & vbNewLine & _
                    "    CoAofRevenue=@CoAofRevenue, " & vbNewLine & _
                    "    CoAofAccountReceivable=@CoAofAccountReceivable, " & vbNewLine & _
                    "    CoAofSalesDisc=@CoAofSalesDisc, " & vbNewLine & _
                    "    CoAofPrepaidIncome=@CoAofPrepaidIncome, " & vbNewLine & _
                    "    CoAofCOGS=@CoAofCOGS, " & vbNewLine & _
                    "    CoAofStock=@CoAofStock, " & vbNewLine & _
                    "    CoAofCash=@CoAofCash, " & vbNewLine & _
                    "    CoAofAccountPayable=@CoAofAccountPayable, " & vbNewLine & _
                    "    CoAofPurchaseDisc=@CoAofPurchaseDisc, " & vbNewLine & _
                    "    CoAofPurchaseEquipments=@CoAofPurchaseEquipments, " & vbNewLine & _
                    "    CoAofAdvancePayment=@CoAofAdvancePayment, " & vbNewLine & _
                    "    CoAofSalesTax=@CoAofSalesTax, " & vbNewLine & _
                    "    CoAofPurchaseTax=@CoAofPurchaseTax, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE ProgramID=@ProgramID " & vbNewLine
                End If

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CoAofRevenue", SqlDbType.Int).Value = clsData.CoAofRevenue
                .Parameters.Add("@CoAofAccountReceivable", SqlDbType.Int).Value = clsData.CoAofAccountReceivable
                .Parameters.Add("@CoAofSalesDisc", SqlDbType.Int).Value = clsData.CoAofSalesDisc
                .Parameters.Add("@CoAofPrepaidIncome", SqlDbType.Int).Value = clsData.CoAofPrepaidIncome
                .Parameters.Add("@CoAofCOGS", SqlDbType.Int).Value = clsData.CoAofCOGS
                .Parameters.Add("@CoAofStock", SqlDbType.Int).Value = clsData.CoAofStock
                .Parameters.Add("@CoAofCash", SqlDbType.Int).Value = clsData.CoAofCash
                .Parameters.Add("@CoAofAccountPayable", SqlDbType.Int).Value = clsData.CoAofAccountPayable
                .Parameters.Add("@CoAofPurchaseDisc", SqlDbType.Int).Value = clsData.CoAofPurchaseDisc
                .Parameters.Add("@CoAofPurchaseEquipments", SqlDbType.Int).Value = clsData.CoAofPurchaseEquipments
                .Parameters.Add("@CoAofAdvancePayment", SqlDbType.Int).Value = clsData.CoAofAdvancePayment
                .Parameters.Add("@CoAofSalesTax", SqlDbType.Int).Value = clsData.CoAofSalesTax
                .Parameters.Add("@CoAofPurchaseTax", SqlDbType.Int).Value = clsData.CoAofPurchaseTax
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.JournalPost)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO sysJournalPost " & vbNewLine & _
                   "    (ProgramID, CoAofRevenue, CoAofAccountReceivable, CoAofSalesDisc, CoAofPrepaidIncome, CoAofCOGS, CoAofStock, CoAofCash, CoAofAccountPayable,   " & vbNewLine & _
                   "     CoAofPurchaseDisc, CoAofPurchaseEquipments, CoAofAdvancePayment, CoAofSalesTax, CoAofPurchaseTax, Remarks, CreatedBy, CreatedDate, LogBy, LogDate, LogInc)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ProgramID, @CoAofRevenue, @CoAofAccountReceivable, @CoAofSalesDisc, @CoAofPrepaidIncome, @CoAofCOGS, @CoAofStock, @CoAofCash, @CoAofAccountPayable,   " & vbNewLine & _
                   "     @CoAofPurchaseDisc, @CoAofPurchaseEquipments, @CoAofAdvancePayment, @CoAofSalesTax, @CoAofPurchaseTax, @Remarks, @CreatedBy, @CreatedDate, @LogBy, @LogDate, @LogInc)  " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CoAofRevenue", SqlDbType.Int).Value = clsData.CoAofRevenue
                .Parameters.Add("@CoAofAccountReceivable", SqlDbType.Int).Value = clsData.CoAofAccountReceivable
                .Parameters.Add("@CoAofSalesDisc", SqlDbType.Int).Value = clsData.CoAofSalesDisc
                .Parameters.Add("@CoAofPrepaidIncome", SqlDbType.Int).Value = clsData.CoAofPrepaidIncome
                .Parameters.Add("@CoAofCOGS", SqlDbType.Int).Value = clsData.CoAofCOGS
                .Parameters.Add("@CoAofStock", SqlDbType.Int).Value = clsData.CoAofStock
                .Parameters.Add("@CoAofCash", SqlDbType.Int).Value = clsData.CoAofCash
                .Parameters.Add("@CoAofAccountPayable", SqlDbType.Int).Value = clsData.CoAofAccountPayable
                .Parameters.Add("@CoAofPurchaseDisc", SqlDbType.Int).Value = clsData.CoAofPurchaseDisc
                .Parameters.Add("@CoAofPurchaseEquipments", SqlDbType.Int).Value = clsData.CoAofPurchaseEquipments
                .Parameters.Add("@CoAofAdvancePayment", SqlDbType.Int).Value = clsData.CoAofAdvancePayment
                .Parameters.Add("@CoAofSalesTax", SqlDbType.Int).Value = clsData.CoAofSalesTax
                .Parameters.Add("@CoAofPurchaseTax", SqlDbType.Int).Value = clsData.CoAofPurchaseTax
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
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

        Public Shared Function GetDetail(ByVal intProgramID As Integer) As VO.JournalPost
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.JournalPost
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 	" & vbNewLine & _
                        "   A.CoAofRevenue, ISNULL(CR.Code,'') AS CoACodeofRevenue, ISNULL(CR.Name,'') AS CoANameofRevenue, 	" & vbNewLine & _
                        "	A.CoAofAccountReceivable, ISNULL(CAR.Code,'') AS CoACodeofAccountReceivable, ISNULL(CAR.Name,'') AS CoANameofAccountReceivable, 	" & vbNewLine & _
                        "	A.CoAofSalesDisc, ISNULL(CSD.Code,'') AS CoACodeofSalesDisc, ISNULL(CSD.Name,'') AS CoANameofSalesDisc, 	" & vbNewLine & _
                        "	A.CoAofPrepaidIncome, ISNULL(CPI.Code,'') AS CoACodeofPrepaidIncome, ISNULL(CPI.Name,'') AS CoANameofPrepaidIncome, 	" & vbNewLine & _
                        "	A.CoAofCOGS, ISNULL(CCOGS.Code,'') AS CoACodeofCOGS, ISNULL(CCOGS.Name,'') AS CoANameofCOGS, 	" & vbNewLine & _
                        "	A.CoAofStock, ISNULL(CST.Code,'') AS CoACodeofStock, ISNULL(CST.Name,'') AS CoANameofStock, 	" & vbNewLine & _
                        "	A.CoAofCash, ISNULL(CCASH.Code,'') AS CoACodeofCash, ISNULL(CCASH.Name,'') AS CoANameofCash, 	" & vbNewLine & _
                        "	A.CoAofAccountPayable, ISNULL(CAP.Code,'') AS CoACodeofAccountPayable, ISNULL(CAP.Name,'') AS CoANameofAccountPayable, 	" & vbNewLine & _
                        "   A.CoAofPurchaseDisc, ISNULL(CPD.Code,'') AS CoACodeofPurchaseDisc, ISNULL(CPD.Name,'') AS CoANameofPurchaseDisc, 	" & vbNewLine & _
                        "	A.CoAofPurchaseEquipments, ISNULL(CPE.Code,'') AS CoACodeofPurchaseEquipments, ISNULL(CPE.Name,'') AS CoANameofPurchaseEquipments, 	" & vbNewLine & _
                        "	A.CoAofAdvancePayment, ISNULL(CAPP.Code,'') AS CoACodeofAdvancePayment, ISNULL(CAPP.Name,'') AS CoANameofAdvancePayment, 	" & vbNewLine & _
                        "	A.CoAofSalesTax, ISNULL(CSTX.Code,'') AS CoACodeofSalesTax, ISNULL(CSTX.Name,'') AS CoANameofSalesTax, 	" & vbNewLine & _
                        "	A.CoAofPurchaseTax, ISNULL(CPTX.Code,'') AS CoACodeofPurchaseTax, ISNULL(CPTX.Name,'') AS CoANameofPurchaseTax, 	" & vbNewLine & _
                        "	A.Remarks, A.LogBy, A.LogDate, A.LogInc  	" & vbNewLine & _
                        "FROM sysJournalPost A 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CR ON 	" & vbNewLine & _
                        "	A.CoAofRevenue=CR.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CAR ON 	" & vbNewLine & _
                        "	A.CoAofAccountReceivable=CAR.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CSD ON 	" & vbNewLine & _
                        "	A.CoAofSalesDisc=CSD.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CPI ON 	" & vbNewLine & _
                        "	A.CoAofPrepaidIncome=CPI.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CCOGS ON 	" & vbNewLine & _
                        "	A.CoAofCOGS=CCOGS.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CST ON 	" & vbNewLine & _
                        "	A.CoAofStock=CST.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CCASH ON 	" & vbNewLine & _
                        "	A.CoAofCash=CCASH.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CAP ON 	" & vbNewLine & _
                        "	A.CoAofAccountPayable=CAP.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CPD ON 	" & vbNewLine & _
                        "	A.CoAofPurchaseDisc=CPD.ID 	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CPE ON 	" & vbNewLine & _
                        "	A.CoAofPurchaseEquipments=CPE.ID	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CAPP ON 	" & vbNewLine & _
                        "	A.CoAofAdvancePayment=CAPP.ID	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CSTX ON 	" & vbNewLine & _
                        "	A.CoAofSalesTax=CSTX.ID	" & vbNewLine & _
                        "LEFT JOIN mstChartOfAccount CPTX ON 	" & vbNewLine & _
                        "	A.CoAofPurchaseTax=CPTX.ID	" & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "	A.ProgramID=@ProgramID " & vbNewLine

                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.CoAofRevenue = .Item("CoAofRevenue")
                        voReturn.CoACodeofRevenue = .Item("CoACodeofRevenue")
                        voReturn.CoANameofRevenue = .Item("CoANameofRevenue")

                        voReturn.CoAofAccountReceivable = .Item("CoAofAccountReceivable")
                        voReturn.CoACodeofAccountReceivable = .Item("CoACodeofAccountReceivable")
                        voReturn.CoANameofAccountReceivable = .Item("CoANameofAccountReceivable")

                        voReturn.CoAofSalesDisc = .Item("CoAofSalesDisc")
                        voReturn.CoACodeofSalesDisc = .Item("CoACodeofSalesDisc")
                        voReturn.CoANameofSalesDisc = .Item("CoANameofSalesDisc")

                        voReturn.CoAofPrepaidIncome = .Item("CoAofPrepaidIncome")
                        voReturn.CoACodeofPrepaidIncome = .Item("CoACodeofPrepaidIncome")
                        voReturn.CoANameofPrepaidIncome = .Item("CoANameofPrepaidIncome")

                        voReturn.CoAofCOGS = .Item("CoAofCOGS")
                        voReturn.CoACodeofCOGS = .Item("CoACodeofCOGS")
                        voReturn.CoANameofCOGS = .Item("CoANameofCOGS")

                        voReturn.CoAofStock = .Item("CoAofStock")
                        voReturn.CoACodeofStock = .Item("CoACodeofStock")
                        voReturn.CoANameofStock = .Item("CoANameofStock")

                        voReturn.CoAofCash = .Item("CoAofCash")
                        voReturn.CoACodeofCash = .Item("CoACodeofCash")
                        voReturn.CoANameofCash = .Item("CoANameofCash")

                        voReturn.CoAofAccountPayable = .Item("CoAofAccountPayable")
                        voReturn.CoACodeofAccountPayable = .Item("CoACodeofAccountPayable")
                        voReturn.CoANameofAccountPayable = .Item("CoANameofAccountPayable")

                        voReturn.CoAofPurchaseDisc = .Item("CoAofPurchaseDisc")
                        voReturn.CoACodeofPurchaseDisc = .Item("CoACodeofPurchaseDisc")
                        voReturn.CoANameofPurchaseDisc = .Item("CoANameofPurchaseDisc")

                        voReturn.CoAofPurchaseEquipments = .Item("CoAofPurchaseEquipments")
                        voReturn.CoACodeofPurchaseEquipments = .Item("CoACodeofPurchaseEquipments")
                        voReturn.CoANameofPurchaseEquipments = .Item("CoANameofPurchaseEquipments")

                        voReturn.CoAofAdvancePayment = .Item("CoAofAdvancePayment")
                        voReturn.CoACodeofAdvancePayment = .Item("CoACodeofAdvancePayment")
                        voReturn.CoANameofAdvancePayment = .Item("CoANameofAdvancePayment")

                        voReturn.CoAofSalesTax = .Item("CoAofSalesTax")
                        voReturn.CoACodeofSalesTax = .Item("CoACodeofSalesTax")
                        voReturn.CoANameofSalesTax = .Item("CoANameofSalesTax")

                        voReturn.CoAofPurchaseTax = .Item("CoAofPurchaseTax")
                        voReturn.CoACodeofPurchaseTax = .Item("CoACodeofPurchaseTax")
                        voReturn.CoANameofPurchaseTax = .Item("CoANameofPurchaseTax")

                        voReturn.Remarks = .Item("Remarks")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.LogInc = .Item("LogInc")
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

        Public Shared Function DataExists() As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   CoAofRevenue " & vbNewLine & _
                        "FROM sysJournalPost " & vbNewLine & _
                        "WHERE ProgramID=@ProgramID " & vbNewLine

                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = UI.usUserApp.ProgramID
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

        Public Shared Sub DeleteDataAll()
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "DELETE sysJournalPost " & vbNewLine

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

    End Class

End Namespace

