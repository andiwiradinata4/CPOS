Namespace DL
    Public Class Sales

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, Optional ByVal strCustomerCode As String = "", _
                                        Optional ByVal strSupplierCode As String = "") As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    CAST(0 AS BIT) AS Pick, A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalReturn, A.IsSplitReceive, A.TotalDownPayment, A.TotalReturn, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID, TR.ID AS ReceiveID, BP2.ID AS SupplierID, BP2.Name AS SupplierName, A.SalesNo  " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner BP2 ON " & vbNewLine & _
                   "    A.SupplierID=BP2.ID " & vbNewLine & _
                   "INNER JOIN traReceive TR ON " & vbNewLine & _
                   "    A.ID=TR.ReferencesID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.SalesDate>=@DateFrom AND A.SalesDate<=@DateTo " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                If strCustomerCode.Trim <> "" Then
                    .CommandText += "    AND C.Code IN (" & strCustomerCode & ") " & vbNewLine
                End If

                If strSupplierCode.Trim <> "" Then
                    .CommandText += "    AND BP2.Code IN (" & strSupplierCode & ") " & vbNewLine
                End If

                .CommandText += "ORDER BY CONVERT(DATE,A.SalesDate), A.SalesNo ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataRemarks(ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.Remarks " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.Remarks<>'' " & vbNewLine

                .CommandText += "GROUP BY A.Remarks " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, A.ProgramID, A.ID, A.BPID, C.Name AS BPName " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine & _
                   "    AND A.SalesDate>=@DateFrom AND A.SalesDate<=@DateTo " & vbNewLine & _
                   "ORDER BY CONVERT(DATE,A.SalesDate), A.SalesNo ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Sales)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traSales " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, BPID, SupplierID, SalesDate, PaymentTerm, DueDate, DriverName, PlatNumber, " & vbNewLine & _
                       "     PPN, PPH, ItemID, ArrivalBrutto, ArrivalTarra, ArrivalNettoBefore, ArrivalDeduction, ArrivalNettoAfter, " & vbNewLine & _
                       "     Price, TotalPrice, Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate, SalesNo)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @BPID, @SupplierID, @SalesDate, @PaymentTerm, @DueDate, @DriverName, @PlatNumber, " & vbNewLine & _
                       "     @PPN, @PPH, @ItemID, @ArrivalBrutto, @ArrivalTarra, @ArrivalNettoBefore, @ArrivalDeduction, @ArrivalNettoAfter, " & vbNewLine & _
                       "     @Price, @TotalPrice, @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE(), @SalesNo)  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE traSales SET " & vbNewLine & _
                        "    CompanyID=@CompanyID, " & vbNewLine & _
                        "    ProgramID=@ProgramID, " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    SupplierID=@SupplierID, " & vbNewLine & _
                        "    SalesDate=@SalesDate, " & vbNewLine & _
                        "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                        "    PlatNumber=@PlatNumber, " & vbNewLine & _
                        "    DriverName=@DriverName, " & vbNewLine & _
                        "    DueDate=@DueDate, " & vbNewLine & _
                        "    PPN=@PPN, " & vbNewLine & _
                        "    PPH=@PPH, " & vbNewLine & _
                        "    ItemID=@ItemID, " & vbNewLine & _
                        "    ArrivalBrutto=@ArrivalBrutto, " & vbNewLine & _
                        "    ArrivalTarra=@ArrivalTarra, " & vbNewLine & _
                        "    ArrivalNettoBefore=@ArrivalNettoBefore, " & vbNewLine & _
                        "    ArrivalDeduction=@ArrivalDeduction, " & vbNewLine & _
                        "    ArrivalNettoAfter=@ArrivalNettoAfter, " & vbNewLine & _
                        "    Price=@Price, " & vbNewLine & _
                        "    TotalPrice=@TotalPrice, " & vbNewLine & _
                        "    Remarks=@Remarks, " & vbNewLine & _
                        "    IDStatus=@IDStatus, " & vbNewLine & _
                        "    LogInc=LogInc+1, " & vbNewLine & _
                        "    LogBy=@LogBy, " & vbNewLine & _
                        "    LogDate=GETDATE(), " & vbNewLine & _
                        "    SalesNo=@SalesNo " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@SupplierID", SqlDbType.Int).Value = clsData.SupplierID
                .Parameters.Add("@SalesDate", SqlDbType.DateTime).Value = clsData.SalesDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
                .Parameters.Add("@DueDate", SqlDbType.DateTime).Value = clsData.DueDate
                .Parameters.Add("@DriverName", SqlDbType.VarChar, 100).Value = clsData.DriverName
                .Parameters.Add("@PlatNumber", SqlDbType.VarChar, 10).Value = clsData.PlatNumber
                .Parameters.Add("@PPN", SqlDbType.Decimal).Value = clsData.PPN
                .Parameters.Add("@PPH", SqlDbType.Decimal).Value = clsData.PPH
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ArrivalBrutto", SqlDbType.Decimal).Value = clsData.ArrivalBrutto
                .Parameters.Add("@ArrivalTarra", SqlDbType.Decimal).Value = clsData.ArrivalTarra
                .Parameters.Add("@ArrivalNettoBefore", SqlDbType.Decimal).Value = clsData.ArrivalNettoBefore
                .Parameters.Add("@ArrivalDeduction", SqlDbType.Decimal).Value = clsData.ArrivalDeduction
                .Parameters.Add("@ArrivalNettoAfter", SqlDbType.Decimal).Value = clsData.ArrivalNettoAfter
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@SalesNo", SqlDbType.VarChar, 30).Value = clsData.SalesNo
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.Sales
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Sales
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT " & vbNewLine & _
                        "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Code AS CustomerCode, C.Name AS BPName, A.SupplierID, D.Code AS SupplierCode, D.Name AS SupplierName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                        "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, " & vbNewLine & _
                        "    A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalReturn, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                        "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy,   " & vbNewLine & _
                        "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID, TR.JournalID AS JournalIDReceive, MI.Tolerance, A.ArrivalUsage, TR.ID AS ReceiveID, TR.Price1 AS PurchasePrice1, TR.Price2 AS PurchasePrice2, " & vbNewLine & _
                        "    A.SalesNo, TR.ReceiveNo " & vbNewLine & _
                        "FROM traSales A " & vbNewLine & _
                        "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                        "    A.BPID=C.ID " & vbNewLine & _
                        "INNER JOIN mstBusinessPartner D ON " & vbNewLine & _
                        "    A.SupplierID=D.ID " & vbNewLine & _
                        "INNER JOIN traReceive TR ON " & vbNewLine & _
                        "    A.ID=TR.ReferencesID " & vbNewLine & _
                        "INNER JOIN mstItem MI ON " & vbNewLine & _
                        "    A.ItemID=MI.ID " & vbNewLine & _
                        "INNER JOIN mstUOM MU ON " & vbNewLine & _
                        "    MI.UomID=MU.ID " & vbNewLine & _
                        "INNER JOIN mstCompany MC ON " & vbNewLine & _
                        "   A.CompanyID=MC.ID " & vbNewLine & _
                        "INNER JOIN mstProgram MP ON " & vbNewLine & _
                        "   A.ProgramID=MP.ID " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "    A.ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        voReturn.CompanyID = .Item("CompanyID")
                        voReturn.CompanyName = .Item("CompanyName")
                        voReturn.ProgramID = .Item("ProgramID")
                        voReturn.ProgramName = .Item("ProgramName")
                        voReturn.ID = .Item("ID")
                        voReturn.CustomerCode = .Item("CustomerCode")
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPName = .Item("BPName")
                        voReturn.SupplierID = .Item("SupplierID")
                        voReturn.SupplierCode = .Item("SupplierCode")
                        voReturn.SupplierName = .Item("SupplierName")
                        voReturn.SalesDate = .Item("SalesDate")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.DueDate = .Item("DueDate")
                        voReturn.DriverName = .Item("DriverName")
                        voReturn.PlatNumber = .Item("PlatNumber")
                        voReturn.PPN = .Item("PPN")
                        voReturn.PPH = .Item("PPH")
                        voReturn.ItemID = .Item("ItemID")
                        voReturn.ItemCode = .Item("ItemCode")
                        voReturn.ItemName = .Item("ItemName")
                        voReturn.UOMID = .Item("UOMID")
                        voReturn.ArrivalBrutto = .Item("ArrivalBrutto")
                        voReturn.ArrivalTarra = .Item("ArrivalTarra")
                        voReturn.ArrivalNettoBefore = .Item("ArrivalNettoBefore")
                        voReturn.ArrivalDeduction = .Item("ArrivalDeduction")
                        voReturn.ArrivalNettoAfter = .Item("ArrivalNettoAfter")
                        voReturn.Price = .Item("Price")
                        voReturn.TotalPrice = .Item("TotalPrice")
                        voReturn.ArrivalReturn = .Item("ArrivalReturn")
                        voReturn.TotalPayment = .Item("TotalPayment")
                        voReturn.TotalDownPayment = .Item("TotalDownPayment")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogInc = .Item("LogInc")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.JournalID = .Item("JournalID")
                        voReturn.JournalIDReceive = .Item("JournalIDReceive")
                        voReturn.Tolerance = .Item("Tolerance")
                        voReturn.ArrivalUsage = .Item("ArrivalUsage")
                        voReturn.ReceiveID = .Item("ReceiveID")
                        voReturn.PurchasePrice1 = .Item("PurchasePrice1")
                        voReturn.PurchasePrice2 = .Item("PurchasePrice2")
                        voReturn.SalesNo = .Item("SalesNo")
                        voReturn.ReceiveNo = .Item("ReceiveNo")
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

        Public Shared Sub DeleteData(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales " & vbNewLine & _
                    "SET IsDeleted=1, IDStatus=@IDStatus, TotalDownPayment=0 " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Deleted
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
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
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

        Public Shared Function GetMaxSalesNo(ByVal strSalesNo As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(SalesNo),3),0) " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(SalesNo,16)=@SalesNo " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    .Parameters.Add("@SalesNo", SqlDbType.VarChar, 16).Value = strSalesNo

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

        Public Shared Function DataExists(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID

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

        Public Shared Function DataExistsSalesNo(ByVal strSalesNo As String, Optional ByVal strID As String = "") As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   SalesNo " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   SalesNo=@SalesNo " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    If strID.Trim <> "" Then
                        .CommandText += "   AND ID<>@ID " & vbNewLine
                    End If

                    .Parameters.Add("@SalesNo", SqlDbType.VarChar, 30).Value = strSalesNo
                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID

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

        Public Shared Function IsDeleted(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsDeleted=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
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

        Public Shared Function ListDataSplitReceive(ByVal strSalesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	TS.ProgramID, MP.Name AS ProgramName, TS.CompanyID, MC.Name AS CompanyName, TSS.ID, TSS.SalesID AS ReferencesID, TSS.BPID, MBP.Name AS BPName, GETDATE() AS ReceiveDate, 	" & vbNewLine & _
                    "	MBP.PaymentTermID AS PaymentTerm, GETDATE() AS DueDate, TS.DriverName, TS.PlatNumber, TS.PPN, TS.PPH, CAST(0 AS DECIMAL(18,2)) AS ArrivalBrutto, CAST(0 AS DECIMAL(18,2)) AS ArrivalTarra,	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL(18,2)) AS ArrivalNettoBefore, CAST(0 AS DECIMAL(18,2)) AS ArrivalDeduction, CAST(0 AS DECIMAL(18,2)) AS ArrivalNettoAfter, MI.PurchasePrice1 AS Price1, MI.PurchasePrice2 AS Price2, 	" & vbNewLine & _
                    "	CAST(0 AS DECIMAL(18,2)) AS TotalPrice1, CAST(0 AS DECIMAL(18,2)) AS TotalPrice2, CAST('' AS VARCHAR(250)) AS Remarks, CAST('' AS VARCHAR(250)) AS DONumber, CAST('' AS VARCHAR(250)) AS SPBNumber,	" & vbNewLine & _
                    "	CAST('' AS VARCHAR(100)) AS SegelNumber, CAST('' AS VARCHAR(250)) AS Specification " & vbNewLine & _
                    "FROM traSales TS 	" & vbNewLine & _
                    "INNER JOIN traSalesSupplier TSS ON 	" & vbNewLine & _
                    "	TS.ID=TSS.SalesID 	" & vbNewLine & _
                    "INNER JOIN mstProgram MP ON 	" & vbNewLine & _
                    "	TS.ProgramID=MP.ID 	" & vbNewLine & _
                    "INNER JOIN mstCompany MC ON 	" & vbNewLine & _
                    "	TS.CompanyID=MC.ID 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner MBP ON 	" & vbNewLine & _
                    "	TSS.BPID=MBP.ID		" & vbNewLine & _
                    "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                    "	TS.ItemID=MI.ID 	" & vbNewLine & _
                    "WHERE TS.ID=@SalesID	" & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function IsSplitReceive(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsSplitReceive=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
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

        Public Shared Sub SetIsSplitReceive(ByVal strID As String, ByVal bolIsSplitReceive As Boolean)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales " & vbNewLine & _
                    "SET IsSplitReceive=@IsSplitReceive " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Deleted
                .Parameters.Add("@IsSplitReceive", SqlDbType.Bit).Value = bolIsSplitReceive
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub CalculateArrivalUsage(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales 	" & vbNewLine & _
                    "SET ArrivalUsage=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT SUM(A.ArrivalNettoAfter) 	" & vbNewLine & _
                    "		FROM traReceive A 	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			A.ReferencesID=@ReferencesID " & vbNewLine & _
                    "			AND A.IsDeleted=0 	" & vbNewLine & _
                    "	),0)	" & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function OutstandingReceive(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traReceive " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ReferencesID=@ID " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
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

        Public Shared Function ListDataFakturPenjualan(ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    MC.Name AS CompanyName, A.SalesNo AS ID, A.SalesDate, C.Name AS BPName, C.Address AS BPAddress, A.PlatNumber, A.DriverName, " & vbNewLine & _
                   "    MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto AS Brutto, A.ArrivalTarra AS Tarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore AS NettoBefore, A.ArrivalDeduction AS Deduction, A.ArrivalNettoAfter AS NettoAfter, A.Price, A.TotalPrice, " & vbNewLine & _
                   "    A.Remarks, A.CreatedBy " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "WHERE A.ID=@ID	" & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub PrintFakturPenjualan(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET " & vbNewLine & _
                    "   IDStatus=@IDStatus " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = VO.Status.Values.Printed
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function ListDataOutstandingReturn(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT  	" & vbNewLine & _
                    "   A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.PlatNumber, A.DriverName,  	" & vbNewLine & _
                    "	A.DueDate, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.ID AS UOMID, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra,  	" & vbNewLine & _
                    "   A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalNettoAfter AS ArrivalNettoAfterSales, 	" & vbNewLine & _
                    "	A.ArrivalNettoAfter-A.ArrivalReturn AS MaxNetto, A.ArrivalReturn AS ArrivalNettoUsage, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,    	" & vbNewLine & _
                    "   A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID   	" & vbNewLine & _
                    "FROM traSales A  	" & vbNewLine & _
                    "INNER JOIN mstStatus B ON  	" & vbNewLine & _
                    "    A.IDStatus=B.ID  	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner C ON  	" & vbNewLine & _
                    "    A.BPID=C.ID  	" & vbNewLine & _
                    "INNER JOIN mstItem MI ON  	" & vbNewLine & _
                    "    A.ItemID=MI.ID  	" & vbNewLine & _
                    "INNER JOIN mstUOM MU ON  	" & vbNewLine & _
                    "    MI.UomID=MU.ID  	" & vbNewLine & _
                    "INNER JOIN mstCompany MC ON  	" & vbNewLine & _
                    "    A.CompanyID=MC.ID  	" & vbNewLine & _
                    "INNER JOIN mstProgram MP ON  	" & vbNewLine & _
                    "    A.ProgramID=MP.ID  	" & vbNewLine & _
                    "WHERE   	" & vbNewLine & _
                    "   A.CompanyID=@CompanyID  	" & vbNewLine & _
                    "   AND A.ProgramID=@ProgramID  	" & vbNewLine & _
                    "   AND A.BPID=@BPID  	" & vbNewLine & _
                    "   AND A.SalesDate>=@DateFrom AND A.SalesDate<=@DateTo 	" & vbNewLine & _
                    "	AND A.IsDeleted=0	" & vbNewLine & _
                    "	AND A.ArrivalNettoAfter-A.ArrivalReturn>0	" & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub CalculateReturnValue(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET " & vbNewLine & _
                    "ArrivalReturn=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT ISNULL(SUM(A.ArrivalNettoAfter),0) AS Total " & vbNewLine & _
                    "		FROM traSalesReturn A 	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			A.ReferencesID=@ReferencesID " & vbNewLine & _
                    "			AND A.IsDeleted=0 	" & vbNewLine & _
                    "	),0), " & vbNewLine & _
                    "TotalReturn=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT ISNULL(SUM(A.TotalPrice),0) AS Total" & vbNewLine & _
                    "		FROM traSalesReturn A 	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			A.ReferencesID=@ReferencesID " & vbNewLine & _
                    "			AND A.IsDeleted=0 	" & vbNewLine & _
                    "	),0) " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ReferencesID " & vbNewLine

                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub CalculateTotalPayment(ByVal strSalesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales " & vbNewLine & _
                    "SET TotalPayment=" & vbNewLine & _
                    "	(" & vbNewLine & _
                    "		SELECT " & vbNewLine & _
                    "			ISNULL(SUM(ARD.Amount),0) AS Total" & vbNewLine & _
                    "		FROM traAccountReceivableDet ARD " & vbNewLine & _
                    "		INNER JOIN traAccountReceivable AR ON " & vbNewLine & _
                    "            ARD.ARID=AR.ID" & vbNewLine & _
                    "        WHERE" & vbNewLine & _
                    "            AR.IsDeleted=0" & vbNewLine & _
                    "			AND ARD.SalesID=@SalesID" & vbNewLine & _
                    "	)" & vbNewLine & _
                    "WHERE ID=@SalesID " & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function IsPostedGL(ByVal strID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traSales " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsPostedGL=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
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

        Public Shared Sub UpdateJournalID(ByVal strID As String, ByVal strJournalID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales " & vbNewLine & _
                    "SET JournalID=@JournalID " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@JournalID", SqlDbType.VarChar, 30).Value = strJournalID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function ListDataGenerateJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                       ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalReturn, A.IsSplitReceive, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.ID=@ID " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataOutstandingPostGL(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.ArrivalReturn, A.IsSplitReceive, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstUOM MU ON " & vbNewLine & _
                   "    MI.UomID=MU.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.SalesDate>=@DateFrom AND A.SalesDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub PostGL(ByVal strID As String, ByVal strLogBy As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET" & vbNewLine & _
                    "   IsPostedGL=1, " & vbNewLine & _
                    "   PostedBy=@LogBy, " & vbNewLine & _
                    "   PostedDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = strLogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub PostGL(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                 ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                 ByVal strLogBy As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET" & vbNewLine & _
                    "   IsPostedGL=1, " & vbNewLine & _
                    "   PostedBy=@LogBy, " & vbNewLine & _
                    "   PostedDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
                    "   AND SalesDate>=@DateFrom AND SalesDate<=@DateTo " & vbNewLine & _
                    "   AND IsDeleted=0 " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = strLogBy
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub UnpostGL(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET" & vbNewLine & _
                    "   IsPostedGL=0, " & vbNewLine & _
                    "   PostedBy='' " & vbNewLine & _
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

        Public Shared Sub UnpostGL(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales SET" & vbNewLine & _
                    "   IsPostedGL=0, " & vbNewLine & _
                    "   PostedBy='' " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
                    "   AND SalesDate>=@DateFrom AND SalesDate<=@DateTo " & vbNewLine & _
                    "   AND IsDeleted=0 " & vbNewLine

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

        Public Shared Function ListDataHistoryBussinessPartners(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    SH.CompanyID, 'PENJUALAN' AS Trans, SH.ID, SH.BPID, BP.Name AS BPName, SH.SalesDate AS TransactionDate, SH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, SH.ArrivalBrutto, " & vbNewLine & _
                   "    SH.ArrivalTarra, SH.ArrivalNettoBefore, SH.ArrivalDeduction, SH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, SH.Price, " & vbNewLine & _
                   "    SH.TotalPrice, SH.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
                   "FROM traSales SH " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
                   "    SH.BPID=BP.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    SH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    SH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo " & vbNewLine & _
                   "    AND SH.IsDeleted=0 " & vbNewLine & _
                   "    AND SH.ItemID=@ItemID " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = intItemID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataHistoryItem(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    SH.CompanyID, 'PENJUALAN' AS Trans, SH.ID, SH.SalesDate AS TransactionDate, SH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, SH.ArrivalBrutto, " & vbNewLine & _
                   "    SH.ArrivalTarra, SH.ArrivalNettoBefore, SH.ArrivalDeduction, SH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, SH.Price, " & vbNewLine & _
                   "    SH.TotalPrice, SH.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
                   "FROM traSales SH " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    SH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    SH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    SH.SalesDate>=@DateFrom AND SH.SalesDate<=@DateTo " & vbNewLine & _
                   "    AND SH.IsDeleted=0 " & vbNewLine & _
                   "    AND SH.BPID=@BPID " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub UpdateTotalDownPayment(ByVal strID As String, ByVal decTotalDownPayment As Decimal)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales " & vbNewLine & _
                    "SET TotalDownPayment=@TotalDownPayment " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                .Parameters.Add("@TotalDownPayment", SqlDbType.Decimal).Value = decTotalDownPayment
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub RecalculateTotalDownPayment(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSales " & vbNewLine & _
                    "SET TotalDownPayment= " & vbNewLine & _
                    "(" & vbNewLine & _
                    "   SELECT " & vbNewLine & _
                    "       ISNULL(SUM(DPD.TotalAmount),0) Total " & vbNewLine & _
                    "   FROM traDownPaymentDet DPD " & vbNewLine & _
                    "   INNER JOIN traDownPayment DP ON " & vbNewLine & _
                    "       DPD.DPID=DP.ID " & vbNewLine & _
                    "   WHERE DP.IsDeleted=0 AND DPD.ReferenceID=@ID " & vbNewLine & _
                    ")" & vbNewLine & _
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

        Public Shared Function ListDataOutstandingPayment(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                          ByVal intBPID As Integer, ByVal intSupplierID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, A.ProgramID, A.ID, A.BPID, A.SalesDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, " & vbNewLine & _
                   "    A.Price, A.TotalPrice, A.ArrivalReturn, A.IsSplitReceive, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, " & vbNewLine & _
                   "    A.LogDate, A.JournalID, A.TotalPrice-A.TotalReturn-A.TotalPayment-A.TotalDownPayment AS OutstandingPayment  " & vbNewLine & _
                   "FROM traSales A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.BPID=@BPID " & vbNewLine & _
                   "    AND A.SupplierID=@SupplierID " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine & _
                   "    AND A.TotalPrice-A.TotalReturn-A.TotalPayment-A.TotalDownPayment>0" & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@SupplierID", SqlDbType.Int).Value = intSupplierID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Sales Supplier"

        Public Shared Function ListDataSupplier(ByVal strSalesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT 	" & vbNewLine & _
                    "	TSS.BPID, MB.Name AS BPName, MB.Address 	" & vbNewLine & _
                    "FROM traSalesSupplier TSS 	" & vbNewLine & _
                    "INNER JOIN mstBusinessPartner MB ON 	" & vbNewLine & _
                    "	TSS.BPID=MB.ID 	" & vbNewLine & _
                    "WHERE	" & vbNewLine & _
                    "	TSS.SalesID=@SalesID	" & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataSupplier(ByVal clsData As VO.SalesSupplier)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traSalesSupplier " & vbNewLine & _
                   "    (ID, SalesID, BPID)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @SalesID, @BPID)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = clsData.SalesID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDSupplier(ByVal strSalesID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesSupplier " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   SalesID=@SalesID " & vbNewLine

                    .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID

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

        Public Shared Sub DeleteDataSupplier(ByVal strSalesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traSalesSupplier " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   SalesID=@SalesID " & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.SalesID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traSalesStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesID=@SalesID " & vbNewLine

                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.SalesStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traSalesStatus " & vbNewLine & _
                   "    (ID, SalesID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @SalesID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@SalesID", SqlDbType.VarChar, 30).Value = clsData.SalesID
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

        Public Shared Function GetMaxIDStatus(ByVal strSalesID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   SalesID=@SalesID " & vbNewLine

                    .Parameters.Add("@SalesID", SqlDbType.VarChar, 19).Value = strSalesID

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

#End Region

    End Class

End Namespace