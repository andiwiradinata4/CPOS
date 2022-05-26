Namespace DL
    Public Class Reports

#Region "Accounting"

        Public Shared Function KartuPiutangLastBalance(ByVal dtmDateFrom As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As Decimal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim decReturn As Decimal = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT 	" & vbNewLine & _
                        "	CAST(0 AS DECIMAL) AS DebitAmount, DP.TotalAmount AS CreditAmount 	 	" & vbNewLine & _
                        "INTO #T_ARCard 	" & vbNewLine & _
                        "FROM traDownPayment DP 	 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   DP.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND DP.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND DP.DPDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND DP.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND DP.IsDeleted=0 	 	" & vbNewLine & _
                        "   AND DP.DPType=@DPType 	" & vbNewLine & _
                        "	 	" & vbNewLine & _
                        "UNION ALL 	 	" & vbNewLine & _
                        "SELECT 	 	" & vbNewLine & _
                        "	TS.TotalPrice AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount 	 	" & vbNewLine & _
                        "FROM traSales TS 	 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   TS.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND TS.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND TS.SalesDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND TS.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND TS.IsDeleted=0 	 	" & vbNewLine & _
                        "	" & vbNewLine & _
                        "UNION ALL 	 	" & vbNewLine & _
                        "SELECT 	 	" & vbNewLine & _
                        "	CAST(0 AS DECIMAL) AS DebitAmount, ARD.Amount AS CreditAmount 	 	" & vbNewLine & _
                        "FROM traAccountReceivable AR 	 	" & vbNewLine & _
                        "INNER JOIN traAccountReceivableDet ARD ON 	" & vbNewLine & _
                        "	AR.ID=ARD.ARID 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   AR.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND AR.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND AR.ARDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND AR.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND AR.IsDeleted=0 	 	" & vbNewLine & _
                        "	" & vbNewLine & _
                        "SELECT 	" & vbNewLine & _
                        "	ISNULL(SUM(DebitAmount)-SUM(CreditAmount),0) AS Balance  	" & vbNewLine & _
                        "FROM #T_ARCard	" & vbNewLine

                    .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                    .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                    .Parameters.Add("@DPType", SqlDbType.Int).Value = VO.DownPayment.Type.Sales
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        decReturn = .Item("Balance")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return decReturn
        End Function

        Public Shared Function KartuPiutangVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	BPS.Code, BPS.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,DP.DPDate,112) AS TransactionDate, 'PANJAR PENJUALAN KE ' + BPR.Name AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, CAST(0 AS DECIMAL) AS DebitAmount, DP.TotalAmount AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traDownPayment DP 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	 	" & vbNewLine & _
                    "	DP.BPID=BPS.ID 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPR ON 	 	" & vbNewLine & _
                    "	DP.BPID2=BPR.ID 	 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	DP.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND DP.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND DP.DPDate>=@DateFrom AND DP.DPDate<=@DateTo 	" & vbNewLine & _
                    "	AND DP.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND DP.IsDeleted=0 	 	" & vbNewLine & _
                    "   AND DP.DPType=@DPType 	" & vbNewLine & _
                    "	 	" & vbNewLine & _
                    "UNION ALL 	 	" & vbNewLine & _
                    "SELECT 	 	" & vbNewLine & _
                    "	BPS.Code, BPS.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,TS.SalesDate,112) AS TransactionDate, TS.SalesNo AS RemarksInfo, --'PENJUALAN ' + MI.Name + ' - ' + TS.ID AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, TS.TotalPrice AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traSales TS 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	 	" & vbNewLine & _
                    "	TS.BPID=BPS.ID 	 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	TS.ItemID=MI.ID 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	TS.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND TS.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND TS.SalesDate>=@DateFrom AND TS.SalesDate<=@DateTo 	" & vbNewLine & _
                    "	AND TS.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND TS.IsDeleted=0 	 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL 	 	" & vbNewLine & _
                    "SELECT 	 	" & vbNewLine & _
                    "	BPS.Code, BPS.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,AR.ARDate,112) AS TransactionDate, TS.SalesNo AS RemarksInfo, --'PELUNASAN ' + ARD.SalesID AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, CAST(0 AS DECIMAL) AS DebitAmount, ARD.Amount AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traAccountReceivable AR 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	 	" & vbNewLine & _
                    "	AR.BPID=BPS.ID 	 	" & vbNewLine & _
                    "INNER JOIN traAccountReceivableDet ARD ON 	" & vbNewLine & _
                    "	AR.ID=ARD.ARID 	" & vbNewLine & _
                    "INNER JOIN traSales TS ON 	" & vbNewLine & _
                    "	ARD.SalesID=TS.ID 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	AR.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND AR.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND AR.ARDate>=@DateFrom AND AR.ARDate<=@DateTo 	" & vbNewLine & _
                    "	AND AR.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND AR.IsDeleted=0	" & vbNewLine & _
                    "ORDER BY TransactionDate, RemarksInfo ASC " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@DPType", SqlDbType.Int).Value = VO.DownPayment.Type.Sales
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function KartuHutangLastBalance(ByVal dtmDateFrom As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As Decimal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim decReturn As Decimal = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT 	" & vbNewLine & _
                        "	CAST(0 AS DECIMAL) AS DebitAmount, DP.TotalAmount AS CreditAmount 	 	" & vbNewLine & _
                        "INTO #T_ARCard 	" & vbNewLine & _
                        "FROM traDownPayment DP 	 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   DP.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND DP.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND DP.DPDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND DP.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND DP.IsDeleted=0 	 	" & vbNewLine & _
                        "   AND DP.DPType=@DPType 	" & vbNewLine & _
                        "	 	" & vbNewLine & _
                        "UNION ALL 	 	" & vbNewLine & _
                        "SELECT 	 	" & vbNewLine & _
                        "	TR.TotalPrice1 AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount 	 	" & vbNewLine & _
                        "FROM traReceive TR 	 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   TR.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND TR.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND TR.ReceiveDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND TR.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND TR.IsDeleted=0 	 	" & vbNewLine & _
                        "	" & vbNewLine & _
                        "UNION ALL 	 	" & vbNewLine & _
                        "SELECT 	 	" & vbNewLine & _
                        "	CAST(0 AS DECIMAL) AS DebitAmount, APD.Amount AS CreditAmount 	 	" & vbNewLine & _
                        "FROM traAccountPayable AP 	 	" & vbNewLine & _
                        "INNER JOIN traAccountPayableDet APD ON 	" & vbNewLine & _
                        "	AP.ID=APD.APID 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   AP.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND AP.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND AP.APDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND AP.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND AP.IsDeleted=0 	 	" & vbNewLine & _
                        "	" & vbNewLine & _
                        "SELECT 	" & vbNewLine & _
                        "	ISNULL(SUM(DebitAmount)-SUM(CreditAmount),0) AS Balance  	" & vbNewLine & _
                        "FROM #T_ARCard	" & vbNewLine

                    .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                    .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                    .Parameters.Add("@DPType", SqlDbType.Int).Value = VO.DownPayment.Type.Purchase
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        decReturn = .Item("Balance")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return decReturn
        End Function

        Public Shared Function KartuHutangVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	BPS.Code, BPS.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,DP.DPDate,112) AS TransactionDate, 'PANJAR PEMBELIAN' AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, DP.TotalAmount AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traDownPayment DP 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	 	" & vbNewLine & _
                    "	DP.BPID=BPS.ID 	 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	DP.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND DP.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND DP.DPDate>=@DateFrom AND DP.DPDate<=@DateTo 	" & vbNewLine & _
                    "	AND DP.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND DP.IsDeleted=0 	 	" & vbNewLine & _
                    "	AND DP.DPType=@DPType 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL 	 	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPR.Code, BPR.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,TR.ReceiveDate,112) AS TransactionDate, TR.ReceiveNo AS RemarksInfo, --'PEMBELIAN ' + MI.Name + ' U/' + BPS.Name + ' - ' + TR.ID AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, CAST(0 AS DECIMAL) AS DebitAmount, TR.TotalPrice1 AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traReceive TR 	" & vbNewLine & _
                    "INNER JOIN traSales TS ON 	" & vbNewLine & _
                    "	TR.ReferencesID=TS.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPR ON 	" & vbNewLine & _
                    "	TR.BPID=BPR.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	" & vbNewLine & _
                    "	TS.BPID=BPS.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	TS.ItemID=MI.ID 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	TR.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND TR.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND TR.ReceiveDate>=@DateFrom AND TR.ReceiveDate<=@DateTo 	" & vbNewLine & _
                    "	AND TR.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND TR.IsDeleted=0 	 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL 	 	" & vbNewLine & _
                    "SELECT 	 	" & vbNewLine & _
                    "	BPR.Code, BPR.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,AP.APDate,112) AS TransactionDate, TR.ReceiveNo AS RemarksInfo, --'PEMBAYARAN ' + APD.ReceiveID AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, APD.Amount AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traAccountPayable AP 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPR ON 	 	" & vbNewLine & _
                    "	AP.BPID=BPR.ID 	 	" & vbNewLine & _
                    "INNER JOIN traAccountPayableDet APD ON 	" & vbNewLine & _
                    "	AP.ID=APD.APID 	" & vbNewLine & _
                    "INNER JOIN traReceive TR ON 	" & vbNewLine & _
                    "	APD.ReceiveID=TR.ID 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	AP.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND AP.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND AP.APDate>=@DateFrom AND AP.APDate<=@DateTo 	" & vbNewLine & _
                    "	AND AP.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND AP.IsDeleted=0 	 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "ORDER BY TransactionDate, RemarksInfo ASC	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@DPType", SqlDbType.Int).Value = VO.DownPayment.Type.Purchase
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function KartuHutangLastBalancePurchasePrice2(ByVal dtmDateFrom As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As Decimal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim decReturn As Decimal = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT 	" & vbNewLine & _
                        "	CAST(0 AS DECIMAL) AS DebitAmount, DP.TotalAmount AS CreditAmount 	 	" & vbNewLine & _
                        "INTO #T_ARCard 	" & vbNewLine & _
                        "FROM traDownPayment DP 	 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   DP.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND DP.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND DP.DPDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND DP.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND DP.IsDeleted=0 	 	" & vbNewLine & _
                        "   AND DP.DPType=@DPType 	" & vbNewLine & _
                        "	 	" & vbNewLine & _
                        "UNION ALL 	 	" & vbNewLine & _
                        "SELECT 	 	" & vbNewLine & _
                        "	TR.TotalPrice2 AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount 	 	" & vbNewLine & _
                        "FROM traReceive TR 	 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   TR.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND TR.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND TR.ReceiveDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND TR.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND TR.IsDeleted=0 	 	" & vbNewLine & _
                        "	" & vbNewLine & _
                        "UNION ALL 	 	" & vbNewLine & _
                        "SELECT 	 	" & vbNewLine & _
                        "	CAST(0 AS DECIMAL) AS DebitAmount, APD.Amount AS CreditAmount 	 	" & vbNewLine & _
                        "FROM traAccountPayable AP 	 	" & vbNewLine & _
                        "INNER JOIN traAccountPayableDet APD ON 	" & vbNewLine & _
                        "	AP.ID=APD.APID 	" & vbNewLine & _
                        "WHERE 	 	" & vbNewLine & _
                        "   AP.ProgramID=@ProgramID 	" & vbNewLine & _
                        "   AND AP.CompanyID=@CompanyID 	" & vbNewLine & _
                        "   AND AP.APDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "   AND AP.BPID=@BPID 	 	" & vbNewLine & _
                        "   AND AP.IsDeleted=0 	 	" & vbNewLine & _
                        "	" & vbNewLine & _
                        "SELECT 	" & vbNewLine & _
                        "	ISNULL(SUM(DebitAmount)-SUM(CreditAmount),0) AS Balance  	" & vbNewLine & _
                        "FROM #T_ARCard	" & vbNewLine

                    .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                    .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                    .Parameters.Add("@DPType", SqlDbType.Int).Value = VO.DownPayment.Type.Purchase
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        decReturn = .Item("Balance")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return decReturn
        End Function

        Public Shared Function KartuHutangVer00PurchasePrice2Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	BPS.Code, BPS.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,DP.DPDate,112) AS TransactionDate, 'PANJAR PEMBELIAN' AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, DP.TotalAmount AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traDownPayment DP 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	 	" & vbNewLine & _
                    "	DP.BPID=BPS.ID 	 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	DP.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND DP.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND DP.DPDate>=@DateFrom AND DP.DPDate<=@DateTo 	" & vbNewLine & _
                    "	AND DP.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND DP.IsDeleted=0 	 	" & vbNewLine & _
                    "	AND DP.DPType=@DPType 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL 	 	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	BPR.Code, BPR.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,TR.ReceiveDate,112) AS TransactionDate, TR.ID AS RemarksInfo, --'PEMBELIAN ' + MI.Name + ' U/' + BPS.Name + ' - ' + TR.ID AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, CAST(0 AS DECIMAL) AS DebitAmount, TR.TotalPrice2 AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traReceive TR 	" & vbNewLine & _
                    "INNER JOIN traSales TS ON 	" & vbNewLine & _
                    "	TR.ReferencesID=TS.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPR ON 	" & vbNewLine & _
                    "	TR.BPID=BPR.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPS ON 	" & vbNewLine & _
                    "	TS.BPID=BPS.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	TS.ItemID=MI.ID 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	TR.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND TR.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND TR.ReceiveDate>=@DateFrom AND TR.ReceiveDate<=@DateTo 	" & vbNewLine & _
                    "	AND TR.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND TR.IsDeleted=0 	 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL 	 	" & vbNewLine & _
                    "SELECT 	 	" & vbNewLine & _
                    "	BPR.Code, BPR.Name, 	" & vbNewLine & _
                    "	CONVERT(DATE,AP.APDate,112) AS TransactionDate, APD.ReceiveID AS RemarksInfo, --'PEMBAYARAN ' + APD.ReceiveID AS RemarksInfo, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL) AS FirstBalance, APD.Amount AS DebitAmount, CAST(0 AS DECIMAL) AS CreditAmount, CAST(0 AS DECIMAL) AS BalanceAmount	" & vbNewLine & _
                    "FROM traAccountPayable AP 	 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BPR ON 	 	" & vbNewLine & _
                    "	AP.BPID=BPR.ID 	 	" & vbNewLine & _
                    "INNER JOIN traAccountPayableDet APD ON 	" & vbNewLine & _
                    "	AP.ID=APD.APID 	" & vbNewLine & _
                    "WHERE 	 	" & vbNewLine & _
                    "	AP.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND AP.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND AP.APDate>=@DateFrom AND AP.APDate<=@DateTo 	" & vbNewLine & _
                    "	AND AP.BPID=@BPID 	 	" & vbNewLine & _
                    "	AND AP.IsDeleted=0 	 	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "ORDER BY TransactionDate, RemarksInfo ASC	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@DPType", SqlDbType.Int).Value = VO.DownPayment.Type.Purchase
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function BukuBesarLastBalance(ByVal dtmDateFrom As DateTime, ByVal intCoAID As Integer, ByVal intProgramID As Integer, ByVal intCompanyID As Integer) As Decimal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim decReturn As Decimal = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT 	" & vbNewLine & _
                        "	CASE WHEN COAG.COAType=1 OR COAG.COAType=2 OR COAG.COAType=6 THEN SUM(BB.DebitAmount)-SUM(BB.CreditAmount) ELSE SUM(BB.CreditAmount)-SUM(BB.DebitAmount) END AS Amount	" & vbNewLine & _
                        "FROM traBukuBesar BB 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount COAP ON 	" & vbNewLine & _
                        "	BB.COAIDParent=COAP.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccount COAC ON 	" & vbNewLine & _
                        "	BB.COAIDChild=COAC.ID 	" & vbNewLine & _
                        "INNER JOIN mstChartOfAccountGroup COAG ON 	" & vbNewLine & _
                        "	COAP.AccountGroupID=COAG.ID 	" & vbNewLine & _
                        "WHERE 	" & vbNewLine & _
                        "	BB.CompanyID=@CompanyID 	" & vbNewLine & _
                        "	AND BB.ProgramID=@ProgramID 	" & vbNewLine & _
                        "	AND BB.TransactionDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                        "	AND BB.COAIDParent=@COAID 	" & vbNewLine & _
                        "GROUP BY 	" & vbNewLine & _
                        "	COAG.COAType, COAP.Code, COAG.Name	" & vbNewLine

                    .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                    .Parameters.Add("@CoAID", SqlDbType.Int).Value = intCoAID
                    .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                    .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        decReturn = .Item("Amount")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return decReturn
        End Function

        Public Shared Function BukuBesarVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intCoAID As Integer, ByVal intProgramID As Integer, ByVal intCompanyID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	BB.TransactionDate AS JournalDate, BB.ReferencesID AS JournalNo, COAG.COAType, COAP.Code AS GroupCode, COAP.Name AS GroupName, 	" & vbNewLine & _
                    "	COAC.Code, COAC.Name, Remarks=CASE WHEN BB.Remarks='' THEN COAC.Name ELSE BB.Remarks END, BB.DebitAmount, BB.CreditAmount, 	" & vbNewLine & _
                    "	FirstBalance=CAST(0 AS DECIMAL(18,2)), BalanceAmount=CAST(0 AS DECIMAL(18,2)), LastBalance=CAST(0 AS DECIMAL(18,2))	" & vbNewLine & _
                    "FROM traBukuBesar BB 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COAP ON 	" & vbNewLine & _
                    "	BB.COAIDParent=COAP.ID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COAC ON 	" & vbNewLine & _
                    "	BB.COAIDChild=COAC.ID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccountGroup COAG ON 	" & vbNewLine & _
                    "	COAP.AccountGroupID=COAG.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	BB.CompanyID=@CompanyID 	" & vbNewLine & _
                    "	AND BB.ProgramID=@ProgramID 	" & vbNewLine & _
                    "	AND BB.TransactionDate>=@DateFrom AND BB.TransactionDate<=@DateTo	" & vbNewLine & _
                    "	AND BB.COAIDParent=@COAID 	" & vbNewLine & _
                    "ORDER BY BB.TransactionDate ASC	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@CoAID", SqlDbType.Int).Value = intCoAID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function NeracaSaldoVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	COA.Code, COA.Name, ISNULL(FB.Amount,0) AS FirstBalance, ISNULL(BB.TotalDebit,0) AS TotalDebit, ISNULL(BB.TotalCredit,0) AS TotalCredit,	" & vbNewLine & _
                    "	LastBalance=ISNULL(FB.Amount,0)+ISNULL(CASE WHEN COAG.COAType=1 OR COAG.COAType=2 OR COAG.COAType=6 THEN BB.TotalDebit-BB.TotalCredit ELSE BB.TotalCredit-BB.TotalDebit END,0)	" & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccountGroup COAG ON 	" & vbNewLine & _
                    "	COA.AccountGroupID=COAG.ID 	" & vbNewLine & _
                    "LEFT JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		BB.COAIDParent, SUM(BB.DebitAmount) AS TotalDebit, SUM(BB.CreditAmount) AS TotalCredit 	" & vbNewLine & _
                    "	FROM traBukuBesar BB 	" & vbNewLine & _
                    "	INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "		BB.COAIDParent=COA.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		BB.CompanyID=@CompanyID 	" & vbNewLine & _
                    "		AND BB.ProgramID=@ProgramID 	" & vbNewLine & _
                    "		AND BB.TransactionDate>=@DateFrom AND BB.TransactionDate<=@DateTo	" & vbNewLine & _
                    "	GROUP BY BB.COAIDParent 	" & vbNewLine & _
                    ") BB ON 	" & vbNewLine & _
                    "	COA.ID=BB.COAIDParent	" & vbNewLine & _
                    "LEFT JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		BB.COAIDParent, 	" & vbNewLine & _
                    "		CASE WHEN COAG.COAType=1 OR COAG.COAType=2 OR COAG.COAType=6 THEN SUM(BB.DebitAmount)-SUM(BB.CreditAmount) ELSE SUM(BB.CreditAmount)-SUM(BB.DebitAmount) END AS Amount	" & vbNewLine & _
                    "	FROM traBukuBesar BB 	" & vbNewLine & _
                    "	INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "		BB.COAIDParent=COA.ID 	" & vbNewLine & _
                    "	INNER JOIN mstChartOfAccountGroup COAG ON 	" & vbNewLine & _
                    "		COA.AccountGroupID=COAG.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		BB.CompanyID=@CompanyID 	" & vbNewLine & _
                    "		AND BB.ProgramID=@ProgramID 	" & vbNewLine & _
                    "		AND BB.TransactionDate<=DATEADD(DAY, -1, @DateFrom)+'23:59:59'	" & vbNewLine & _
                    "	GROUP BY BB.COAIDParent, COAG.COAType	" & vbNewLine & _
                    ") FB ON 	" & vbNewLine & _
                    "	COA.ID=FB.COAIDParent	" & vbNewLine & _
                    "ORDER BY COA.Code	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

        Public Shared Function SalesReport(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                           ByVal strCustomerCode As String, ByVal strSupplierCode As String, ByVal strRemarks As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	BP.Name AS CustomerName, BP.Address AS CustomerAddress, BP2.Name AS SupplierName, SH.SalesDate, SH.PlatNumber, SH.DriverName, " & vbNewLine & _
                    "	SH.ArrivalBrutto, SH.ArrivalTarra, SH.ArrivalNettoBefore, SH.ArrivalDeduction, SH.ArrivalNettoAfter, SH.Price, SH.TotalPrice, SH.Remarks " & vbNewLine & _
                    "FROM traSaleS SH 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	SH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP2 ON 	" & vbNewLine & _
                    "	SH.SupplierID=BP2.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SH.IsDeleted=0 " & vbNewLine & _
                    "	AND SH.CompanyID=@CompanyID " & vbNewLine & _
                    "	AND SH.ProgramID=@ProgramID " & vbNewLine & _
                    "	AND SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo	" & vbNewLine

                If strCustomerCode.Trim <> "" Then
                    .CommandText += "    AND BP.Code IN (" & strCustomerCode & ") " & vbNewLine
                End If

                If strSupplierCode.Trim <> "" Then
                    .CommandText += "    AND BP2.Code IN (" & strSupplierCode & ") " & vbNewLine
                End If

                If strRemarks.Trim <> "SEMUA" Then
                    .CommandText += "    AND SH.Remarks=@Remarks " & vbNewLine
                End If

                .CommandText += _
                    "ORDER BY 	" & vbNewLine & _
                    "	BP2.Code, SH.SalesDate " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = strRemarks
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function SalesServiceReport(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                                  ByVal strCustomerCode As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT " & vbNewLine & _
                    "	BP.Name AS CustomerName, BP.Address AS CustomerAddress, SH.SalesDate, SH.SPKNumber, " & vbNewLine & _
                    "	MI.Name AS ItemName, MO.Name AS UomName, SD.Price, SD.Qty, SD.TotalPrice, SH.Remarks, " & vbNewLine & _
                    "	SD.Remarks AS ItemRemarks " & vbNewLine & _
                    "FROM traSalesService SH " & vbNewLine & _
                    "INNER JOIN traSalesServiceDet SD ON " & vbNewLine & _
                    "    SH.ID = SD.SalesServiceID " & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
                    "   SH.BPID = BP.ID " & vbNewLine & _
                    "INNER JOIN mstItem MI ON " & vbNewLine & _
                    "   SD.ItemID = MI.ID " & vbNewLine & _
                    "INNER JOIN mstUOM MO ON " & vbNewLine & _
                    "   MI.UomID = MO.ID " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   SH.IsDeleted = 0 " & vbNewLine & _
                    "	AND SH.CompanyID=@CompanyID " & vbNewLine & _
                    "	AND SH.ProgramID=@ProgramID " & vbNewLine & _
                    "	AND SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo " & vbNewLine

                If strCustomerCode.Trim <> "" Then
                    .CommandText += "    AND BP.Code IN (" & strCustomerCode & ") " & vbNewLine
                End If

                .CommandText += _
                    "ORDER BY 	" & vbNewLine & _
                    "	BP.Code, SH.SalesDate " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function SalesReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SRH.ID AS SalesReturnNo, SRH.SalesReturnDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	SD.SalesID AS SalesNo, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, SRD.Qty AS OrderQty, SRD.Disc, SRD.Price, SRD.TotalPrice, 	" & vbNewLine & _
                    "	SRH.CreatedBy, SRH.Remarks 	" & vbNewLine & _
                    "FROM traSalesReturn SRH 	" & vbNewLine & _
                    "INNER JOIN traSalesReturnDet SRD ON 	" & vbNewLine & _
                    "	SRH.ID=SRD.SalesReturnID 	" & vbNewLine & _
                    "INNER JOIN traSalesDet SD ON 	" & vbNewLine & _
                    "	SRD.SalesDetID=SD.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	SRH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	SRD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SRH.IsDeleted=0 " & vbNewLine & _
                    "	AND SRH.SalesReturnDate>=@DateFrom AND SRH.SalesReturnDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND SRH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND SRD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PurchaseReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	RH.ID AS ReceiveNo, RH.ReceiveDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, RHD.Qty AS OrderQty, RHD.Disc, RHD.Price, RHD.TotalPrice, 	" & vbNewLine & _
                    "	RH.CreatedBy, RH.Remarks 	" & vbNewLine & _
                    "FROM traReceive RH 	" & vbNewLine & _
                    "INNER JOIN traReceiveDet RHD ON 	" & vbNewLine & _
                    "	RH.ID=RHD.ReceiveID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	RH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	RHD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	RH.IsDeleted=0	" & vbNewLine & _
                    "	AND RH.ReceiveDate>=@DateFrom AND RH.ReceiveDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND RH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND RHD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PurchaseReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	SRH.ID AS ReceiveReturnNo, SRH.ReceiveReturnDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	SD.ReceiveID AS ReceiveNo, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, SRD.Qty AS OrderQty, SRD.Disc, SRD.Price, SRD.TotalPrice, 	" & vbNewLine & _
                    "	SRH.CreatedBy, SRH.Remarks 	" & vbNewLine & _
                    "FROM traReceiveReturn SRH 	" & vbNewLine & _
                    "INNER JOIN traReceiveReturnDet SRD ON 	" & vbNewLine & _
                    "	SRH.ID=SRD.ReceiveReturnID 	" & vbNewLine & _
                    "INNER JOIN traReceiveDet SD ON 	" & vbNewLine & _
                    "	SRD.ReceiveDetID=SD.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	SRH.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	SRD.ItemID=MI.ID 	" & vbNewLine & _
                    "INNER JOIN mstUom MU ON 	" & vbNewLine & _
                    "	MI.UomID1=MU.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	SRH.IsDeleted=0 " & vbNewLine & _
                    "	AND SRH.ReceiveReturnDate>=@DateFrom AND SRH.ReceiveReturnDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND SRH.BPID=@BPID "
                End If

                If intItemID > 0 Then
                    .CommandText += "   AND SRD.ItemID=@ItemID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function AccountPayableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	AP.ID AS APNo, AP.APDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MPR.Name AS PaymentReference, AP.ReferencesNote, 	" & vbNewLine & _
                    "	APD.ReceiveID, APD.Amount, AP.CreatedBy, AP.Remarks	" & vbNewLine & _
                    "FROM traAccountPayable AP 	" & vbNewLine & _
                    "INNER JOIN traAccountPayableDet APD ON 	" & vbNewLine & _
                    "	AP.ID=APD.APID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	AP.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstPaymentReferences MPR ON 	" & vbNewLine & _
                    "	AP.PaymentReferencesID=MPR.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	AP.IsDeleted=0 	" & vbNewLine & _
                    "	AND AP.APDate>=@DateFrom AND AP.APDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND AP.BPID=@BPID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function AccountReceivableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	AR.ID AS ARNo, AR.ARDate, BP.Name AS BPName, 	" & vbNewLine & _
                    "	MPR.Name AS PaymentReference, AR.ReferencesNote, 	" & vbNewLine & _
                    "	ARD.SalesID, ARD.Amount, AR.CreatedBy, AR.Remarks	" & vbNewLine & _
                    "FROM traAccountReceivable AR 	" & vbNewLine & _
                    "INNER JOIN traAccountReceivableDet ARD ON 	" & vbNewLine & _
                    "	AR.ID=ARD.ARID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner BP ON 	" & vbNewLine & _
                    "	AR.BPID=BP.ID 	" & vbNewLine & _
                    "INNER JOIN mstPaymentReferences MPR ON 	" & vbNewLine & _
                    "	AR.PaymentReferencesID=MPR.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	AR.IsDeleted=0 	" & vbNewLine & _
                    "	AND AR.ARDate>=@DateFrom AND AR.ARDate<=@DateTo	" & vbNewLine

                If intBPID > 0 Then
                    .CommandText += "   AND AR.BPID=@BPID "
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function CostReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	CC.Code AS COACode, CC.Name AS COAName, TC.ID, TC.CostNo, CD .Code AS COACodeCredit, CD.Name AS COANameCredit, TCD.Remarks AS RemarksDetail, TCD.Amount, 	" & vbNewLine & _
                    "	TC.TotalAmount, TC.CreatedBy, TC.Remarks 	" & vbNewLine & _
                    "FROM traCost TC 	" & vbNewLine & _
                    "INNER JOIN traCostDet TCD ON 	" & vbNewLine & _
                    "	TC.ID=TCD.CostID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount CC ON 	" & vbNewLine & _
                    "	TC.CoAID=CC.ID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount CD ON 	" & vbNewLine & _
                    "	TCD.CoAID=CD.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	TC.IsDeleted=0	" & vbNewLine & _
                    "	AND TC.CostDate>=@DateFrom AND TC.CostDate<=@DateTo	" & vbNewLine

                If intProgramID > 0 Then
                    .CommandText += "   AND TC.ProgramID=@ProgramID " & vbNewLine
                End If

                If intCompanyID > 0 Then
                    .CommandText += "   AND TC.CompanyID=@CompanyID " & vbNewLine
                End If

                .CommandText += "ORDER BY CC.Code ASC " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function JournalReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	JH.ID, JH.JournalNo, JH.JournalDate, JH.TotalAmount, COA.Code AS AccountCode, COA.Name AS AccountName, JD.DebitAmount, JD.CreditAmount, 	" & vbNewLine & _
                    "	JH.CreatedBy, JH.Remarks 	" & vbNewLine & _
                    "FROM traJournal JH 	" & vbNewLine & _
                    "INNER JOIN traJournalDet JD ON 	" & vbNewLine & _
                    "	JH.ID=JD.JournalID 	" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "	JH.IsDeleted=0 	" & vbNewLine & _
                    "	AND JH.IsAutoGenerate=0 	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo	" & vbNewLine

                If intProgramID > 0 Then
                    .CommandText += "   AND JH.ProgramID=@ProgramID " & vbNewLine
                End If

                If intCompanyID > 0 Then
                    .CommandText += "   AND JH.CompanyID=@CompanyID " & vbNewLine
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#Region "Profit and Loss"

        Public Shared Function ListDataPerCOATypeBaseOnBukuBesarReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intCOAType As Integer, ByVal intCOAGroupID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT COAG.COAType, " & vbNewLine & _
                    "	COAG.Name AS COAGroupName, BB.COAIDParent, COA.Code AS COACode, COA.Name AS COAName,  " & vbNewLine & _
                    "   TotalAmount = " & vbNewLine & _
                    "	CASE WHEN COAG.COAType=1 OR COAG.COAType=2 OR COAG.COAType=6 THEN  " & vbNewLine & _
                    "		SUM(BB.DebitAmount)-SUM(BB.CreditAmount)  " & vbNewLine & _
                    "	ELSE  " & vbNewLine & _
                    "		SUM(BB.CreditAmount)-SUM(BB.DebitAmount)  " & vbNewLine & _
                    "   END " & vbNewLine & _
                    "FROM traBukuBesar BB  " & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON  " & vbNewLine & _
                    "   BB.COAIDParent = COA.ID " & vbNewLine & _
                    "INNER JOIN mstChartOfAccountGroup COAG ON  " & vbNewLine & _
                    "   COA.AccountGroupID = COAG.ID " & vbNewLine & _
                    "WHERE 	" & vbNewLine & _
                    "   BB.CompanyID=@CompanyID 	" & vbNewLine & _
                    "   AND BB.ProgramID=@ProgramID 	" & vbNewLine & _
                    "   AND COAG.COAType=@COAType " & vbNewLine & _
                    "   AND BB.TransactionDate>=@DateFrom AND BB.TransactionDate<=@DateTo" & vbNewLine

                If intCOAGroupID > 0 Then
                    .CommandText += "   AND COA.AccountGroupID=@AccountGroupID " & vbNewLine
                End If

                .CommandText += _
                    "GROUP BY " & vbNewLine & _
                    "	COAG.Name, BB.COAIDParent, COA.Code, COA.Name, COAG.COAType " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@COAType", SqlDbType.Int).Value = intCOAType
                .Parameters.Add("@AccountGroupID", SqlDbType.Int).Value = intCOAGroupID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataPerCOATypeBaseOnChartOfAccountReport(ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intCOAType As Integer, ByVal intCOAGroupID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT " & vbNewLine & _
                    "	COAT.ID AS TypeID, COAT.Name AS TypeName, COAG.ID AS GroupID, COAG.Name AS GroupName, COA.ID AS COAID, COA.Code AS COACode, " & vbNewLine & _
                    "	COA.Name AS COAName, TotalAmount=ISNULL(BB.TotalAmount,0) " & vbNewLine & _
                    "FROM mstChartOfAccount COA " & vbNewLine & _
                    "INNER JOIN mstChartOfAccountGroup COAG ON " & vbNewLine & _
                    "   COA.AccountGroupID = COAG.ID " & vbNewLine & _
                    "INNER JOIN mstChartOfAccountType COAT ON " & vbNewLine & _
                    "   COAG.COAType = COAT.ID " & vbNewLine & _
                    "LEFT JOIN " & vbNewLine & _
                    "( " & vbNewLine & _
                    "	SELECT " & vbNewLine & _
                    "		COA.ID AS COAID, " & vbNewLine & _
                    "		TotalAmount= " & vbNewLine & _
                    "		CASE WHEN COAG.COAType=1 OR COAG.COAType=2 OR COAG.COAType=6 THEN " & vbNewLine & _
                    "            SUM(BB.DebitAmount) - SUM(BB.CreditAmount) " & vbNewLine & _
                    "		ELSE " & vbNewLine & _
                    "            SUM(BB.CreditAmount) - SUM(BB.DebitAmount) " & vbNewLine & _
                    "       END " & vbNewLine & _
                    "	FROM traBukuBesar BB " & vbNewLine & _
                    "	INNER JOIN mstChartOfAccount COA ON " & vbNewLine & _
                    "       BB.COAIDParent = COA.ID " & vbNewLine & _
                    "	INNER JOIN mstChartOfAccountGroup COAG ON " & vbNewLine & _
                    "       COA.AccountGroupID = COAG.ID " & vbNewLine & _
                    "   WHERE " & vbNewLine & _
                    "       BB.CompanyID=@CompanyID 	" & vbNewLine & _
                    "       AND BB.ProgramID=@ProgramID 	" & vbNewLine & _
                    "		AND COAG.COAType=@COAType " & vbNewLine & _
                    "       AND BB.TransactionDate<=@DateTo" & vbNewLine

                If intCOAGroupID > 0 Then
                    .CommandText += "		AND COA.AccountGroupID=@AccountGroupID " & vbNewLine
                End If

                .CommandText += _
                    "	GROUP BY COA.ID, COAG.COAType " & vbNewLine & _
                    ") BB ON " & vbNewLine & _
                    "   COA.ID = BB.COAID " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "	COAG.COAType=@COAType " & vbNewLine

                If intCOAGroupID > 0 Then
                    .CommandText += "   AND COA.AccountGroupID=@AccountGroupID " & vbNewLine
                End If

                .CommandText += "ORDER BY COAT.ID, COA.Code ASC " & vbNewLine

                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@COAType", SqlDbType.Int).Value = intCOAType
                .Parameters.Add("@AccountGroupID", SqlDbType.Int).Value = intCOAGroupID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function DiscountRevenueReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 		" & vbNewLine & _
                    "	COA.Name, SUM(JD.CreditAmount)-SUM(JD.DebitAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM traJournalDet JD 		" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 		" & vbNewLine & _
                    "	JD.JournalID=JH.ID 		" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "	AND COA.AccountGroupID=17	" & vbNewLine & _
                    "WHERE 		" & vbNewLine & _
                    "	JH.IsDeleted=0 	" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "GROUP BY COA.Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function FirstStockReport(ByVal dtmDateFrom As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AWAL' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "INTO #TFirstStock 	" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=3		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate<@DateFrom 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL	" & vbNewLine & _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AWAL' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=18		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate<@DateFrom 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	Name, SUM(TotalAmount) AS TotalAmount  	" & vbNewLine & _
                    "FROM #TFirstStock 	" & vbNewLine & _
                    "GROUP BY Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom.AddDays(-1)
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PurchaseStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 			" & vbNewLine & _
                    "	'PEMBELIAN' AS Name, ISNULL(SUM(RH.GrandTotal),0) AS TotalAmount		" & vbNewLine & _
                    "INTO #T_PurchaseStock	" & vbNewLine & _
                    "FROM traReceive RH 			" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	RH.IsDeleted=0 		" & vbNewLine & _
                    "	AND RH.IsPostedGL=1		" & vbNewLine & _
                    "	AND RH.ReceiveDate>=@DateFrom AND RH.ReceiveDate<=@DateTo		" & vbNewLine & _
                    "		" & vbNewLine & _
                    "UNION ALL 	" & vbNewLine & _
                    "SELECT 			" & vbNewLine & _
                    "	'PEMBELIAN' AS Name, ISNULL(SUM(RH.GrandTotal),0)*-1 AS TotalAmount		" & vbNewLine & _
                    "FROM traReceiveReturn RH 			" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	RH.IsDeleted=0 		" & vbNewLine & _
                    "	AND RH.IsPostedGL=1		" & vbNewLine & _
                    "	AND RH.ReceiveReturnDate>=@DateFrom AND RH.ReceiveReturnDate<=@DateTo		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	Name, SUM(TotalAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM #T_PurchaseStock	" & vbNewLine & _
                    "GROUP BY Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function LastStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AKHIR' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "INTO #TLastStock 	" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=3		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "	" & vbNewLine & _
                    "UNION ALL	" & vbNewLine & _
                    "SELECT 			" & vbNewLine & _
                    "	'PERSEDIAAN AKHIR' AS Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount		" & vbNewLine & _
                    "FROM traJournalDet JD 			" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 			" & vbNewLine & _
                    "	JD.JournalID=JH.ID 			" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 		" & vbNewLine & _
                    "	JD.CoAID=COA.ID 		" & vbNewLine & _
                    "	AND COA.AccountGroupID=18		" & vbNewLine & _
                    "WHERE 			" & vbNewLine & _
                    "	JH.IsDeleted=0 		" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 		" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 		" & vbNewLine & _
                    "GROUP BY COA.Name		" & vbNewLine & _
                    "	" & vbNewLine & _
                    "SELECT 	" & vbNewLine & _
                    "	Name, SUM(TotalAmount) AS TotalAmount  	" & vbNewLine & _
                    "FROM #TLastStock 	" & vbNewLine & _
                    "GROUP BY Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ExpensesReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 		" & vbNewLine & _
                    "	COA.Name, SUM(JD.DebitAmount)-SUM(JD.CreditAmount) AS TotalAmount	" & vbNewLine & _
                    "FROM traJournalDet JD 		" & vbNewLine & _
                    "INNER JOIN traJournal JH ON 		" & vbNewLine & _
                    "	JD.JournalID=JH.ID 		" & vbNewLine & _
                    "INNER JOIN mstChartOfAccount COA ON 	" & vbNewLine & _
                    "	JD.CoAID=COA.ID 	" & vbNewLine & _
                    "	AND COA.AccountGroupID IN (14,15)	" & vbNewLine & _
                    "WHERE 		" & vbNewLine & _
                    "	JH.IsDeleted=0 	" & vbNewLine & _
                    "	AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	AND JH.JournalDate>=@DateFrom AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "GROUP BY COA.Name	" & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Balance Sheet"

        Public Shared Function BalanceSheetDebitReport(ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	COA.Code AS CoACode, COA.Name AS CoAName, 	" & vbNewLine & _
                    "	DebitAmount=ISNULL(	" & vbNewLine & _
                    "	COA.FirstBalance+	" & vbNewLine & _
                    "	CASE WHEN 	" & vbNewLine & _
                    "		COA.AccountGroupID=1 OR COA.AccountGroupID=2 OR COA.AccountGroupID=3 OR COA.AccountGroupID=4 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=5 OR COA.AccountGroupID=6 OR COA.AccountGroupID=7 OR COA.AccountGroupID=17 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=18 OR COA.AccountGroupID=13 OR COA.AccountGroupID=14 OR COA.AccountGroupID=15 OR COA.AccountGroupID=16 	" & vbNewLine & _
                    "	THEN ISNULL(JH.TotalDebit,0)-ISNULL(JH.TotalCredit,0) " & vbNewLine & _
                    "	ELSE ISNULL(JH.TotalCredit,0)-ISNULL(JH.TotalDebit,0) " & vbNewLine & _
                    "	END,0),	" & vbNewLine & _
                    "	CreditAmount=CAST(0 AS DECIMAL(18,2))" & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		JD.CoAID, SUM(JD.DebitAmount) AS TotalDebit, SUM(JD.CreditAmount) AS TotalCredit	" & vbNewLine & _
                    "	FROM traJournalDet JD 	" & vbNewLine & _
                    "	INNER JOIN traJournal JH ON 	" & vbNewLine & _
                    "		JD.JournalID=JH.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		JH.IsDeleted=0 	" & vbNewLine & _
                    "	    AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	    AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "	GROUP BY JD.CoAID 	" & vbNewLine & _
                    ") JH ON COA.ID=JH.CoAID	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    COA.AccountGroupID IN (1,2,4,5,6,7)" & vbNewLine & _
                    "ORDER BY " & vbNewLine & _
                    "    COA.Code ASC " & vbNewLine

                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function BalanceSheetCreditReport(ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	COA.Code AS CoACode, COA.Name AS CoAName, 	" & vbNewLine & _
                    "	DebitAmount=CAST(0 AS DECIMAL(18,2))," & vbNewLine & _
                    "	CreditAmount=ISNULL(	" & vbNewLine & _
                    "	COA.FirstBalance+	" & vbNewLine & _
                    "	CASE WHEN 	" & vbNewLine & _
                    "		COA.AccountGroupID=1 OR COA.AccountGroupID=2 OR COA.AccountGroupID=3 OR COA.AccountGroupID=4 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=5 OR COA.AccountGroupID=6 OR COA.AccountGroupID=7 OR COA.AccountGroupID=17 	" & vbNewLine & _
                    "		OR COA.AccountGroupID=18 OR COA.AccountGroupID=13 OR COA.AccountGroupID=14 OR COA.AccountGroupID=15 OR COA.AccountGroupID=16 	" & vbNewLine & _
                    "	THEN ISNULL(JH.TotalDebit,0)-ISNULL(JH.TotalCredit,0) " & vbNewLine & _
                    "	ELSE ISNULL(JH.TotalCredit,0)-ISNULL(JH.TotalDebit,0) " & vbNewLine & _
                    "	END,0) " & vbNewLine & _
                    "FROM mstChartOfAccount COA 	" & vbNewLine & _
                    "INNER JOIN 	" & vbNewLine & _
                    "(	" & vbNewLine & _
                    "	SELECT 	" & vbNewLine & _
                    "		JD.CoAID, SUM(JD.DebitAmount) AS TotalDebit, SUM(JD.CreditAmount) AS TotalCredit	" & vbNewLine & _
                    "	FROM traJournalDet JD 	" & vbNewLine & _
                    "	INNER JOIN traJournal JH ON 	" & vbNewLine & _
                    "		JD.JournalID=JH.ID 	" & vbNewLine & _
                    "	WHERE 	" & vbNewLine & _
                    "		JH.IsDeleted=0 	" & vbNewLine & _
                    "	    AND ((JH.IsAutoGenerate=0 AND JH.IsPostedGL=1) OR (JH.IsAutoGenerate=1)) 	" & vbNewLine & _
                    "	    AND JH.JournalDate<=@DateTo 	" & vbNewLine & _
                    "	GROUP BY JD.CoAID 	" & vbNewLine & _
                    ") JH ON COA.ID=JH.CoAID	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    COA.AccountGroupID IN (8,9,10,11)" & vbNewLine & _
                    "ORDER BY " & vbNewLine & _
                    "    COA.Code ASC " & vbNewLine

                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function


#End Region

    End Class
End Namespace

