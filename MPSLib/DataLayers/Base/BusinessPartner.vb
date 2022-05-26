Namespace DL
    Public Class BusinessPartner

#Region "Main"

        Public Shared Function ListDataAll(ByVal decOnAmount As Decimal, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "--TEMP TABLE" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPID, DPType, SUM(TotalAmount) AS TotalDownPayment 	" & vbNewLine & _
                    "INTO #T_DownPayment 	" & vbNewLine & _
                    "FROM traDownPayment 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   IsPostedGL=1 	" & vbNewLine & _
                    "   AND IsDeleted=0 	" & vbNewLine & _
                    "   AND ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND CompanyID=@CompanyID	" & vbNewLine & _
                    "GROUP BY BPID, DPType	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPID, SUM(TotalPrice1)-SUM(TotalReturn1)-SUM(TotalPayment) AS TotalReceive 	" & vbNewLine & _
                    "INTO #T_Receive 	" & vbNewLine & _
                    "FROM traReceive  	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   IsPostedGL=1 	" & vbNewLine & _
                    "   AND IsDeleted=0 	" & vbNewLine & _
                    "   AND ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND CompanyID=@CompanyID	" & vbNewLine & _
                    "GROUP BY BPID 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPID, SUM(TotalPrice)-SUM(TotalReturn)-SUM(TotalPayment) AS TotalSales 	" & vbNewLine & _
                    "INTO #T_Sales	" & vbNewLine & _
                    "FROM traSales	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   IsPostedGL=1 	" & vbNewLine & _
                    "   AND IsDeleted=0 	" & vbNewLine & _
                    "   AND ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND CompanyID=@CompanyID	" & vbNewLine & _
                    "GROUP BY BPID	" & vbNewLine & _
                    "" & vbNewLine & _
                    "SELECT  	" & vbNewLine & _
                    "   A.ID, A.Code, A.Name, A.Address, A.PICName, A.PICPhoneNumber, A.PaymentTermID, A.IsUsePurchaseLimit, A.MaxPurchaseLimit, 	" & vbNewLine & _
                    "   ISNULL(TR.TotalPrice1,0) AS TotalPurchase1, ISNULL(TR.TotalPrice2,0) AS TotalPurchase2, ISNULL(TRH.TotalReceive,0)-ISNULL(DPP.TotalDownPayment,0) AS APBalance, " & vbNewLine & _
                    "   ISNULL(TSH.TotalSales,0)-ISNULL(DPS.TotalDownPayment,0) AS ARBalance, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy, A.CreatedDate, A.LogBy, A.LogDate,   	" & vbNewLine & _
                    "   IsAssign=CAST(CASE WHEN ISNULL((SELECT TOP 1 ID FROM mstBusinessPartnerAssign WHERE BPID=A.ID),0)=0 THEN 0 ELSE 1 END AS BIT), " & vbNewLine & _
                    "   A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2 " & vbNewLine & _
                    "FROM mstBusinessPartner A  	" & vbNewLine & _
                    "INNER JOIN mstStatus B ON   	" & vbNewLine & _
                    "    A.IDStatus=B.ID 	" & vbNewLine & _
                    "LEFT JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		TR.BPID, SUM(TR.TotalPrice1) AS TotalPrice1, SUM(TR.TotalPrice2) AS TotalPrice2	" & vbNewLine & _
                    "	FROM traReceive TR 	" & vbNewLine & _
                    "	WHERE	" & vbNewLine & _
                    "		TR.IsDeleted=0 	" & vbNewLine & _
                    "		AND YEAR(TR.ReceiveDate)=YEAR(GETDATE())	" & vbNewLine & _
                    "	GROUP BY 	" & vbNewLine & _
                    "		TR.BPID 	" & vbNewLine & _
                    ") TR ON A.ID=TR.BPID 	" & vbNewLine & _
                    "LEFT JOIN #T_Receive TRH ON 	" & vbNewLine & _
                    "	A.ID=TRH.BPID 	" & vbNewLine & _
                    "LEFT JOIN #T_DownPayment DPP ON 	" & vbNewLine & _
                    "	A.ID=DPP.BPID 	" & vbNewLine & _
                    "	AND DPP.DPType=@DPTypePurchase 	" & vbNewLine & _
                    "LEFT JOIN #T_Sales TSH ON 	" & vbNewLine & _
                    "	A.ID=TSH.BPID 	" & vbNewLine & _
                    "LEFT JOIN #T_DownPayment DPS ON 	" & vbNewLine & _
                    "	A.ID=DPS.BPID 	" & vbNewLine & _
                    "	AND DPS.DPType=@DPTypeSales	" & vbNewLine

                If intCompanyID <> 0 And intProgramID <> 0 Then
                    .CommandText += _
                        "INNER JOIN mstBusinessPartnerAssign MBPA ON  	" & vbNewLine & _
                        "    A.ID=MBPA.BPID " & vbNewLine & _
                        "    AND MBPA.CompanyID=@CompanyID " & vbNewLine & _
                        "    AND MBPA.ProgramID=@ProgramID " & vbNewLine
                End If

                .CommandText += _
                    "WHERE 	" & vbNewLine & _
                    "	A.IsUsePurchaseLimit=0 OR (A.IsUsePurchaseLimit=1 AND A.MaxPurchaseLimit>=ISNULL(TR.TotalPrice1,0)+@OnAmount)	" & vbNewLine & _
                    "ORDER BY A.Code ASC "

                .Parameters.Add("@OnAmount", SqlDbType.Decimal).Value = decOnAmount
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = IIf(intCompanyID = 0, UI.usUserApp.CompanyID, intCompanyID)
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = IIf(intProgramID = 0, UI.usUserApp.ProgramID, intProgramID)
                .Parameters.Add("@DPTypePurchase", SqlDbType.Int).Value = VO.DownPayment.Type.Purchase
                .Parameters.Add("@DPTypeSales", SqlDbType.Int).Value = VO.DownPayment.Type.Sales
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListData(ByVal decOnAmount As Decimal, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "--TEMP TABLE" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPID, DPType, SUM(TotalAmount) AS TotalDownPayment 	" & vbNewLine & _
                    "INTO #T_DownPayment 	" & vbNewLine & _
                    "FROM traDownPayment 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   IsPostedGL=1 	" & vbNewLine & _
                    "   AND IsDeleted=0 	" & vbNewLine & _
                    "   AND ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND CompanyID=@CompanyID	" & vbNewLine & _
                    "GROUP BY BPID, DPType	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPID, SUM(TotalPrice1)-SUM(TotalReturn1)-SUM(TotalPayment) AS TotalReceive 	" & vbNewLine & _
                    "INTO #T_Receive 	" & vbNewLine & _
                    "FROM traReceive  	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   IsPostedGL=1 	" & vbNewLine & _
                    "   AND IsDeleted=0 	" & vbNewLine & _
                    "   AND ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND CompanyID=@CompanyID	" & vbNewLine & _
                    "GROUP BY BPID 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPID, SUM(TotalPrice)-SUM(TotalReturn)-SUM(TotalPayment) AS TotalSales 	" & vbNewLine & _
                    "INTO #T_Sales	" & vbNewLine & _
                    "FROM traSales	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   IsPostedGL=1 	" & vbNewLine & _
                    "   AND IsDeleted=0 	" & vbNewLine & _
                    "   AND ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND CompanyID=@CompanyID	" & vbNewLine & _
                    "GROUP BY BPID	" & vbNewLine & _
                    "" & vbNewLine & _
                    "SELECT  	" & vbNewLine & _
                    "   A.ID, A.Code, A.Name, A.Address, A.PICName, A.PICPhoneNumber, A.PaymentTermID, A.IsUsePurchaseLimit, A.MaxPurchaseLimit, 	" & vbNewLine & _
                    "   ISNULL(TR.TotalPrice1,0) AS TotalPurchase1, ISNULL(TR.TotalPrice2,0) AS TotalPurchase2, ISNULL(TRH.TotalReceive,0)-ISNULL(DPP.TotalDownPayment,0) AS APBalance, " & vbNewLine & _
                    "   ISNULL(TSH.TotalSales,0)-ISNULL(DPS.TotalDownPayment,0) AS ARBalance, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy, A.CreatedDate, A.LogBy, A.LogDate,   	" & vbNewLine & _
                    "   IsAssign=CAST(CASE WHEN ISNULL((SELECT TOP 1 ID FROM mstBusinessPartnerAssign WHERE BPID=A.ID),0)=0 THEN 0 ELSE 1 END AS BIT), " & vbNewLine & _
                    "   A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2 " & vbNewLine & _
                    "FROM mstBusinessPartner A  	" & vbNewLine & _
                    "INNER JOIN mstStatus B ON   	" & vbNewLine & _
                    "    A.IDStatus=B.ID 	" & vbNewLine & _
                    "LEFT JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		TR.BPID, SUM(TR.TotalPrice1) AS TotalPrice1, SUM(TR.TotalPrice2) AS TotalPrice2	" & vbNewLine & _
                    "	FROM traReceive TR 	" & vbNewLine & _
                    "	WHERE	" & vbNewLine & _
                    "		TR.IsDeleted=0 	" & vbNewLine & _
                    "		AND YEAR(TR.ReceiveDate)=YEAR(GETDATE())	" & vbNewLine & _
                    "	GROUP BY 	" & vbNewLine & _
                    "		TR.BPID 	" & vbNewLine & _
                    ") TR ON A.ID=TR.BPID 	" & vbNewLine & _
                    "LEFT JOIN #T_Receive TRH ON 	" & vbNewLine & _
                    "	A.ID=TRH.BPID 	" & vbNewLine & _
                    "LEFT JOIN #T_DownPayment DPP ON 	" & vbNewLine & _
                    "	A.ID=DPP.BPID 	" & vbNewLine & _
                    "	AND DPP.DPType=@DPTypePurchase 	" & vbNewLine & _
                    "LEFT JOIN #T_Sales TSH ON 	" & vbNewLine & _
                    "	A.ID=TSH.BPID 	" & vbNewLine & _
                    "LEFT JOIN #T_DownPayment DPS ON 	" & vbNewLine & _
                    "	A.ID=DPS.BPID 	" & vbNewLine & _
                    "	AND DPS.DPType=@DPTypeSales	" & vbNewLine

                If intCompanyID <> 0 And intProgramID <> 0 Then
                    .CommandText += _
                        "INNER JOIN mstBusinessPartnerAssign MBPA ON  	" & vbNewLine & _
                        "    A.ID=MBPA.BPID " & vbNewLine & _
                        "    AND MBPA.CompanyID=@CompanyID " & vbNewLine & _
                        "    AND MBPA.ProgramID=@ProgramID " & vbNewLine
                End If

                .CommandText += _
                    "WHERE 	" & vbNewLine & _
                    "	A.IsUsePurchaseLimit=0 OR (A.IsUsePurchaseLimit=1 AND A.MaxPurchaseLimit>=ISNULL(TR.TotalPrice1,0)+@OnAmount)	" & vbNewLine & _
                    "ORDER BY A.Code ASC "

                .Parameters.Add("@OnAmount", SqlDbType.Decimal).Value = decOnAmount
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = IIf(intCompanyID = 0, UI.usUserApp.CompanyID, intCompanyID)
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = IIf(intProgramID = 0, UI.usUserApp.ProgramID, intProgramID)
                .Parameters.Add("@DPTypePurchase", SqlDbType.Int).Value = VO.DownPayment.Type.Purchase
                .Parameters.Add("@DPTypeSales", SqlDbType.Int).Value = VO.DownPayment.Type.Sales
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataForFilter(ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT  	" & vbNewLine & _
                    "   CAST(0 AS BIT) AS Pick, A.ID, A.Code, A.Name, A.Address " & vbNewLine & _
                    "FROM mstBusinessPartner A  	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartnerAssign MBPA ON  	" & vbNewLine & _
                    "    A.ID=MBPA.BPID " & vbNewLine & _
                    "    AND MBPA.ProgramID=@ProgramID " & vbNewLine & _
                    "ORDER BY A.Code ASC " & vbNewLine

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = IIf(intProgramID = 0, UI.usUserApp.ProgramID, intProgramID)
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListData() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT  	" & vbNewLine & _
                    "   CAST(0 AS BIT) AS Pick, A.ID, A.Code, A.Name, A.Address " & vbNewLine & _
                    "FROM mstBusinessPartner A  	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    A.IDStatus=@IDStatus " & vbNewLine & _
                    "ORDER BY A.Code ASC " & vbNewLine

                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Active
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
                    "      ,[Address]	" & vbNewLine & _
                    "      ,[PICName]	" & vbNewLine & _
                    "      ,[PICPhoneNumber]	" & vbNewLine & _
                    "      ,[PaymentTermID]	" & vbNewLine & _
                    "      ,[IsUsePurchaseLimit]	" & vbNewLine & _
                    "      ,[MaxPurchaseLimit]	" & vbNewLine & _
                    "      ,[APBalance]	" & vbNewLine & _
                    "      ,[ARBalance]	" & vbNewLine & _
                    "      ,[SalesPrice]	" & vbNewLine & _
                    "      ,[PurchasePrice1]	" & vbNewLine & _
                    "      ,[PurchasePrice2]	" & vbNewLine & _
                    "      ,[IDStatus]	" & vbNewLine & _
                    "      ,[CreatedBy]	" & vbNewLine & _
                    "      ,[CreatedDate]	" & vbNewLine & _
                    "      ,[LogBy]	" & vbNewLine & _
                    "      ,[LogDate]	" & vbNewLine & _
                    "      ,[LogInc]	" & vbNewLine & _
                    "  FROM [dbo].[mstBusinessPartner]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.BusinessPartner)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstBusinessPartner " & vbNewLine & _
                       "    (ID, Code, Name, Address, PICName, PICPhoneNumber, PaymentTermID,   " & vbNewLine & _
                       "     IsUsePurchaseLimit, MaxPurchaseLimit, SalesPrice, PurchasePrice1, PurchasePrice2, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @Code, @Name, @Address, @PICName, @PICPhoneNumber, @PaymentTermID,   " & vbNewLine & _
                       "     @IsUsePurchaseLimit, @MaxPurchaseLimit, @SalesPrice, @PurchasePrice1, @PurchasePrice2, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE mstBusinessPartner SET " & vbNewLine & _
                    "    Code=@Code, " & vbNewLine & _
                    "    Name=@Name, " & vbNewLine & _
                    "    Address=@Address, " & vbNewLine & _
                    "    PICName=@PICName, " & vbNewLine & _
                    "    PICPhoneNumber=@PICPhoneNumber, " & vbNewLine & _
                    "    PaymentTermID=@PaymentTermID, " & vbNewLine & _
                    "    IsUsePurchaseLimit=@IsUsePurchaseLimit, " & vbNewLine & _
                    "    MaxPurchaseLimit=@MaxPurchaseLimit, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = clsData.Name
                .Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = clsData.Address
                .Parameters.Add("@PICName", SqlDbType.VarChar, 150).Value = clsData.PICName
                .Parameters.Add("@PICPhoneNumber", SqlDbType.VarChar, 100).Value = clsData.PICPhoneNumber
                .Parameters.Add("@PaymentTermID", SqlDbType.Int).Value = clsData.PaymentTermID
                .Parameters.Add("@IsUsePurchaseLimit", SqlDbType.Bit).Value = clsData.IsUsePurchaseLimit
                .Parameters.Add("@MaxPurchaseLimit", SqlDbType.Decimal).Value = clsData.MaxPurchaseLimit
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub SaveDataAll(ByVal clsData As VO.BusinessPartner)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstBusinessPartner " & vbNewLine & _
                   "    (ID, Code, Name, Address, PICName, PICPhoneNumber, PaymentTermID,   " & vbNewLine & _
                   "     IsUsePurchaseLimit, MaxPurchaseLimit, APBalance, ARBalance, SalesPrice, " & vbNewLine & _
                   "     PurchasePrice1, PurchasePrice2, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate, LogInc)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @Code, @Name, @Address, @PICName, @PICPhoneNumber, @PaymentTermID,   " & vbNewLine & _
                   "     @IsUsePurchaseLimit, @MaxPurchaseLimit, @APBalance, @ARBalance, @SalesPrice, " & vbNewLine & _
                   "     @PurchasePrice1, @PurchasePrice2, @IDStatus, @CreatedBy, @CreatedDate, @LogBy, @LogDate, @LogInc)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@Code", SqlDbType.VarChar, 10).Value = clsData.Code
                .Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = clsData.Name
                .Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = clsData.Address
                .Parameters.Add("@PICName", SqlDbType.VarChar, 150).Value = clsData.PICName
                .Parameters.Add("@PICPhoneNumber", SqlDbType.VarChar, 100).Value = clsData.PICPhoneNumber
                .Parameters.Add("@PaymentTermID", SqlDbType.Int).Value = clsData.PaymentTermID
                .Parameters.Add("@IsUsePurchaseLimit", SqlDbType.Bit).Value = clsData.IsUsePurchaseLimit
                .Parameters.Add("@MaxPurchaseLimit", SqlDbType.Decimal).Value = clsData.MaxPurchaseLimit
                .Parameters.Add("@APBalance", SqlDbType.Decimal).Value = clsData.APBalance
                .Parameters.Add("@ARBalance", SqlDbType.Decimal).Value = clsData.ARBalance
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
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

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.BusinessPartner
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.BusinessPartner
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.Code, A.Name, A.Address, A.PICName, A.PICPhoneNumber, A.PaymentTermID, " & vbNewLine & _
                       "    A.IsUsePurchaseLimit, A.MaxPurchaseLimit, A.APBalance, A.ARBalance, " & vbNewLine & _
                       "    A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2, A.IDStatus, A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstBusinessPartner A " & vbNewLine & _
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
                        voReturn.Address = .Item("Address")
                        voReturn.PICName = .Item("PICName")
                        voReturn.PICPhoneNumber = .Item("PICPhoneNumber")
                        voReturn.PaymentTermID = .Item("PaymentTermID")
                        voReturn.IsUsePurchaseLimit = .Item("IsUsePurchaseLimit")
                        voReturn.MaxPurchaseLimit = .Item("MaxPurchaseLimit")
                        voReturn.APBalance = .Item("APBalance")
                        voReturn.ARBalance = .Item("ARBalance")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
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

        Public Shared Function GetDetail(ByVal strCode As String) As VO.BusinessPartner
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.BusinessPartner
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.Code, A.Name, A.Address, A.PICName, A.PICPhoneNumber, A.PaymentTermID, " & vbNewLine & _
                       "    A.IsUsePurchaseLimit, A.MaxPurchaseLimit, A.APBalance, A.ARBalance, " & vbNewLine & _
                       "    A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2, A.IDStatus, A.LogBy, A.LogDate  " & vbNewLine & _
                       "FROM mstBusinessPartner A " & vbNewLine & _
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
                        voReturn.Code = .Item("Code")
                        voReturn.Name = .Item("Name")
                        voReturn.Address = .Item("Address")
                        voReturn.PICName = .Item("PICName")
                        voReturn.PICPhoneNumber = .Item("PICPhoneNumber")
                        voReturn.PaymentTermID = .Item("PaymentTermID")
                        voReturn.IsUsePurchaseLimit = .Item("IsUsePurchaseLimit")
                        voReturn.MaxPurchaseLimit = .Item("MaxPurchaseLimit")
                        voReturn.APBalance = .Item("APBalance")
                        voReturn.ARBalance = .Item("ARBalance")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
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
                    "UPDATE mstBusinessPartner " & vbNewLine & _
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
                    "DELETE mstBusinessPartner " & vbNewLine 

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
                        "FROM mstBusinessPartner " & vbNewLine

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
                        "FROM mstBusinessPartner " & vbNewLine & _
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
                        "FROM mstBusinessPartner " & vbNewLine & _
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

        Public Shared Sub UpdatePrice(ByVal clsData As VO.BusinessPartner)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE mstBusinessPartner SET " & vbNewLine & _
                    "    SalesPrice=CASE WHEN @SalesPrice=0 THEN SalesPrice ELSE @SalesPrice END, " & vbNewLine & _
                    "    PurchasePrice1=CASE WHEN @PurchasePrice1=0 THEN PurchasePrice1 ELSE @PurchasePrice1 END, " & vbNewLine & _
                    "    PurchasePrice2=CASE WHEN @PurchasePrice2=0 THEN PurchasePrice2 ELSE @PurchasePrice2 END, " & vbNewLine & _
                    "    LogInc=LogInc+1, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.BPID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM mstBusinessPartnerStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.BPID=@BPID " & vbNewLine

                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAllStatus() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ID]	" & vbNewLine & _
                    "      ,[BPID]	" & vbNewLine & _
                    "      ,[Status]	" & vbNewLine & _
                    "      ,[StatusBy]	" & vbNewLine & _
                    "      ,[StatusDate]	" & vbNewLine & _
                    "      ,[Remarks]	" & vbNewLine & _
                    "  FROM [dbo].[mstBusinessPartnerStatus]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.BusinessPartnerStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO mstBusinessPartnerStatus " & vbNewLine & _
                   "    (ID, BPID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @BPID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@Status", SqlDbType.VarChar, 100).Value = clsData.Status
                .Parameters.Add("@StatusBy", SqlDbType.VarChar, 20).Value = clsData.StatusBy
                .Parameters.Add("@StatusDate", SqlDbType.DateTime).Value = clsData.StatusDate
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDStatus() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstBusinessPartnerStatus " & vbNewLine

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

        Public Shared Sub DeleteDataAllStatus()
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "DELETE mstBusinessPartnerStatus " & vbNewLine

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

#End Region

#Region "Price"

        Public Shared Function ListDataPrice(ByVal intBPID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.BPID, A.DateFrom, A.DateTo, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2  " & vbNewLine & _
                   "FROM mstBusinessPartnerPrice A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.BPID=@BPID" & vbNewLine & _
                   "    AND A.DateFrom>=@DateFrom AND A.DateTo<=@DateTo " & vbNewLine & _
                   "ORDER BY A.DateFrom ASC " & vbNewLine

                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataAllPrice() As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT [ID]	" & vbNewLine & _
                    "      ,[BPID]	" & vbNewLine & _
                    "      ,[DateFrom]	" & vbNewLine & _
                    "      ,[DateTo]	" & vbNewLine & _
                    "      ,[SalesPrice]	" & vbNewLine & _
                    "      ,[PurchasePrice1]	" & vbNewLine & _
                    "      ,[PurchasePrice2]	" & vbNewLine & _
                    "  FROM [dbo].[mstBusinessPartnerPrice]	" & vbNewLine

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataPrice(ByVal bolNew As Boolean, ByVal clsData As VO.BusinessPartnerPrice)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO mstBusinessPartnerPrice " & vbNewLine & _
                       "    (ID, BPID, DateFrom, DateTo, SalesPrice, PurchasePrice1, PurchasePrice2)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ID, @BPID, @DateFrom, @DateTo, @SalesPrice, @PurchasePrice1, @PurchasePrice2)  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE mstBusinessPartnerPrice SET " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    DateFrom=@DateFrom, " & vbNewLine & _
                        "    DateTo=@DateTo, " & vbNewLine & _
                        "    SalesPrice=@SalesPrice, " & vbNewLine & _
                        "    PurchasePrice1=@PurchasePrice1, " & vbNewLine & _
                        "    PurchasePrice2=@PurchasePrice2 " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ID", SqlDbType.Int).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = clsData.DateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = clsData.DateTo
                .Parameters.Add("@SalesPrice", SqlDbType.Decimal).Value = clsData.SalesPrice
                .Parameters.Add("@PurchasePrice1", SqlDbType.Decimal).Value = clsData.PurchasePrice1
                .Parameters.Add("@PurchasePrice2", SqlDbType.Decimal).Value = clsData.PurchasePrice2
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetailPrice(ByVal intID As Integer) As VO.BusinessPartnerPrice
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.BusinessPartnerPrice
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.BPID, A.DateFrom, A.DateTo, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2  " & vbNewLine & _
                       "FROM mstBusinessPartnerPrice A " & vbNewLine & _
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
                        voReturn.BPID = .Item("BPID")
                        voReturn.DateFrom = .Item("DateFrom")
                        voReturn.DateTo = .Item("DateTo")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
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

        Public Shared Function GetDetailPrice(ByVal intBPID As Integer, ByVal dtmDate As DateTime) As VO.BusinessPartnerPrice
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.BusinessPartnerPrice
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.BPID, A.DateFrom, A.DateTo, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2  " & vbNewLine & _
                       "FROM mstBusinessPartnerPrice A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    A.BPID=@BPID " & vbNewLine & _
                       "    AND @Date>=A.DateFrom AND @Date<=A.DateTo " & vbNewLine

                    .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                    .Parameters.Add("@Date", SqlDbType.DateTime).Value = dtmDate
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.BPID = .Item("BPID")
                        voReturn.DateFrom = .Item("DateFrom")
                        voReturn.DateTo = .Item("DateTo")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
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

        Public Shared Function GetDetailLastPrice(ByVal intBPID As Integer, ByVal intID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As VO.BusinessPartnerPrice
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.BusinessPartnerPrice
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.ID, A.BPID, A.DateFrom, A.DateTo, A.SalesPrice, A.PurchasePrice1, A.PurchasePrice2  " & vbNewLine & _
                       "FROM mstBusinessPartnerPrice A " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    A.BPID=@BPID " & vbNewLine & _
                       "    AND A.ID<>@ID " & vbNewLine & _
                       "    AND A.DateTo='3000/01/01' AND A.DateFrom<@DateFrom " & vbNewLine & _
                       "ORDER BY ID DESC " & vbNewLine

                    .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
                    .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                    .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.ID = .Item("ID")
                        voReturn.BPID = .Item("BPID")
                        voReturn.DateFrom = .Item("DateFrom")
                        voReturn.DateTo = .Item("DateTo")
                        voReturn.SalesPrice = .Item("SalesPrice")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
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

        Public Shared Sub DeleteDataPrice(ByVal intID As Integer)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM mstBusinessPartnerPrice " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.Int).Value = intID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataAllPrice()
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM mstBusinessPartnerPrice " & vbNewLine 

            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDPrice() As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(MAX(ID),0) " & vbNewLine & _
                        "FROM mstBusinessPartnerPrice " & vbNewLine

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

        Public Shared Function IsExistDateFrom(ByVal dtmDateFrom As DateTime, ByVal intID As Integer, ByVal intBPID As Integer) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolReturn As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM mstBusinessPartnerPrice " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "   DateFrom=@DateFrom " & vbNewLine & _
                        "   AND BPID=@BPID " & vbNewLine & _
                        "   AND ID<>@ID " & vbNewLine


                    .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                    .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                    .Parameters.Add("@ID", SqlDbType.Int).Value = intID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        bolReturn = True
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return bolReturn
        End Function

#End Region

    End Class

End Namespace