Namespace DL

    Public Class SalesService

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                ByVal intIDStatus As Integer, Optional ByVal strCustomerCode As String = "") As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(0 AS BIT) AS Pick, A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ServiceType, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DueDate,   " & vbNewLine & _
                   "     A.SPKNumber, A.TotalPPN, A.TotalPPH, A.TotalPrice, A.TotalDisc, A.TotalDownPayment,   " & vbNewLine & _
                   "     A.TotalPayment, A.TotalReturn, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted,   " & vbNewLine & _
                   "     A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy,   " & vbNewLine & _
                   "     A.LogDate, A.JournalID, A.SalesNo, A.GrandTotal, A.BillNumber, A.PPNPercentage, B.Name AS StatusInfo  " & vbNewLine & _
                   "FROM traSalesService A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
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

                .CommandText += "ORDER BY CONVERT(DATE,A.SalesDate), A.SalesNo ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
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
                   "FROM traSalesService A " & vbNewLine & _
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

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.SalesService)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traSalesService " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, ServiceType, BPID, SalesDate, PaymentTerm, DueDate,   " & vbNewLine & _
                       "     SPKNumber, TotalPPN, TotalPPH, TotalPrice, TotalDisc, TotalDownPayment,   " & vbNewLine & _
                       "     GrandTotal, Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate,   " & vbNewLine & _
                       "     SalesNo, BillNumber, PPNPercentage)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @ServiceType, @BPID, @SalesDate, @PaymentTerm, @DueDate,   " & vbNewLine & _
                       "     @SPKNumber, @TotalPPN, @TotalPPH, @TotalPrice, @TotalDisc, @TotalDownPayment,   " & vbNewLine & _
                       "     @GrandTotal, @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE(),   " & vbNewLine & _
                       "     @SalesNo, @BillNumber, @PPNPercentage)  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traSalesService SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    ProgramID=@ProgramID, " & vbNewLine & _
                    "    ServiceType=@ServiceType, " & vbNewLine & _
                    "    BPID=@BPID, " & vbNewLine & _
                    "    SalesDate=@SalesDate, " & vbNewLine & _
                    "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                    "    DueDate=@DueDate, " & vbNewLine & _
                    "    SPKNumber=@SPKNumber, " & vbNewLine & _
                    "    TotalPPN=@TotalPPN, " & vbNewLine & _
                    "    TotalPPH=@TotalPPH, " & vbNewLine & _
                    "    TotalPrice=@TotalPrice, " & vbNewLine & _
                    "    TotalDisc=@TotalDisc, " & vbNewLine & _
                    "    TotalDownPayment=@TotalDownPayment, " & vbNewLine & _
                    "    GrandTotal=@GrandTotal, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE(), " & vbNewLine & _
                    "    SalesNo=@SalesNo, " & vbNewLine & _
                    "    BillNumber=@BillNumber, " & vbNewLine & _
                    "    PPNPercentage=@PPNPercentage " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@ServiceType", SqlDbType.Int).Value = clsData.ServiceType
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@SalesDate", SqlDbType.DateTime).Value = clsData.SalesDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
                .Parameters.Add("@DueDate", SqlDbType.DateTime).Value = clsData.DueDate
                .Parameters.Add("@SPKNumber", SqlDbType.VarChar, 250).Value = clsData.SPKNumber
                .Parameters.Add("@TotalPPN", SqlDbType.Decimal).Value = clsData.TotalPPN
                .Parameters.Add("@TotalPPH", SqlDbType.Decimal).Value = clsData.TotalPPH
                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
                .Parameters.Add("@TotalDisc", SqlDbType.Decimal).Value = clsData.TotalDisc
                .Parameters.Add("@TotalDownPayment", SqlDbType.Decimal).Value = clsData.TotalDownPayment
                .Parameters.Add("@GrandTotal", SqlDbType.Decimal).Value = clsData.GrandTotal
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@SalesNo", SqlDbType.VarChar, 30).Value = clsData.SalesNo
                .Parameters.Add("@BillNumber", SqlDbType.VarChar, 250).Value = clsData.BillNumber
                .Parameters.Add("@PPNPercentage", SqlDbType.Decimal).Value = clsData.PPNPercentage
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.SalesService
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.SalesService
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ServiceType, A.BPID, C.Code AS CustomerCode, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DueDate,   " & vbNewLine & _
                        "    A.SPKNumber, A.TotalPPN, A.TotalPPH, A.TotalPrice, A.TotalDisc, A.TotalDownPayment,   " & vbNewLine & _
                        "    A.TotalPayment, A.TotalReturn, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted,   " & vbNewLine & _
                        "    A.Remarks, A.IDStatus, A.LogBy, A.LogDate, A.JournalID, A.SalesNo, A.GrandTotal, A.BillNumber, A.PPNPercentage  " & vbNewLine & _
                        "FROM traSalesService A " & vbNewLine & _
                        "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                        "    A.BPID=C.ID " & vbNewLine & _
                        "INNER JOIN mstCompany MC ON " & vbNewLine & _
                        "   A.CompanyID=MC.ID " & vbNewLine & _
                        "INNER JOIN mstProgram MP ON " & vbNewLine & _
                        "   A.ProgramID=MP.ID " & vbNewLine & _
                        "WHERE " & vbNewLine & _
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
                        voReturn.ServiceType = .Item("ServiceType")
                        voReturn.CustomerCode = .Item("CustomerCode")
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPName = .Item("BPName")
                        voReturn.SalesDate = .Item("SalesDate")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.DueDate = .Item("DueDate")
                        voReturn.SPKNumber = .Item("SPKNumber")
                        voReturn.TotalPPN = .Item("TotalPPN")
                        voReturn.TotalPPH = .Item("TotalPPH")
                        voReturn.TotalPrice = .Item("TotalPrice")
                        voReturn.TotalDisc = .Item("TotalDisc")
                        voReturn.TotalDownPayment = .Item("TotalDownPayment")
                        voReturn.GrandTotal = .Item("GrandTotal")
                        voReturn.TotalPayment = .Item("TotalPayment")
                        voReturn.TotalReturn = .Item("TotalReturn")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.JournalID = .Item("JournalID")
                        voReturn.SalesNo = .Item("SalesNo")
                        voReturn.BillNumber = .Item("BillNumber")
                        voReturn.PPNPercentage = .Item("PPNPercentage")
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
                    "UPDATE traSalesService " & vbNewLine & _
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
                        "FROM traSalesService " & vbNewLine & _
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
                        "FROM traSalesService " & vbNewLine & _
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
                        "FROM traSalesService " & vbNewLine & _
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
                        "FROM traSalesService " & vbNewLine & _
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
                        "FROM traSalesService " & vbNewLine & _
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

        Public Shared Sub CalculateTotalPayment(ByVal strSalesID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traSalesService " & vbNewLine & _
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
                        "FROM traSalesService " & vbNewLine & _
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
                    "UPDATE traSalesService " & vbNewLine & _
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
                   "     A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ServiceType, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DueDate,   " & vbNewLine & _
                   "     A.SPKNumber, A.TotalPPN, A.TotalPPH, A.TotalPrice, A.TotalDisc, A.TotalDownPayment,   " & vbNewLine & _
                   "     A.TotalPayment, A.TotalReturn, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted,   " & vbNewLine & _
                   "     A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy,   " & vbNewLine & _
                   "     A.LogDate, A.JournalID, A.SalesNo, A.GrandTotal  " & vbNewLine & _
                   "FROM traSalesService A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
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
                   "     A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ServiceType, A.BPID, C.Name AS BPName, A.SalesDate, A.PaymentTerm, A.DueDate,   " & vbNewLine & _
                   "     A.SPKNumber, A.TotalPPN, A.TotalPPH, A.TotalPrice, A.TotalDisc, A.TotalDownPayment,   " & vbNewLine & _
                   "     A.TotalPayment, A.TotalReturn, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted,   " & vbNewLine & _
                   "     A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy,   " & vbNewLine & _
                   "     A.LogDate, A.JournalID, A.SalesNo, A.GrandTotal  " & vbNewLine & _
                   "FROM traSalesService A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
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
                    "UPDATE traSalesService SET" & vbNewLine & _
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
                    "UPDATE traSalesService SET" & vbNewLine & _
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
                    "UPDATE traSalesService SET" & vbNewLine & _
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
                    "UPDATE traSalesService SET" & vbNewLine & _
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
                    "UPDATE traSalesService " & vbNewLine & _
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
                    "UPDATE traSalesService " & vbNewLine & _
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
                                                          ByVal intBPID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, A.ProgramID, A.ID, A.BPID, A.SalesDate, A.PaymentTerm, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, " & vbNewLine & _
                   "    A.LogDate, A.JournalID, A.GrandTotal-A.TotalReturn-A.TotalPayment-A.TotalDownPayment AS OutstandingPayment  " & vbNewLine & _
                   "FROM traSalesService A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.BPID=@BPID " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine & _
                   "    AND A.GrandTotal-A.TotalReturn-A.TotalPayment-A.TotalDownPayment>0" & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function PrintBonFaktur(ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "SELECT " & vbNewLine & _
                    "	BP.Name AS CustomerName, BP.Address AS CustomerAddress, SH.SalesDate, SH.SPKNumber, SH.BillNumber, " & vbNewLine & _
                    "	MI.Name AS ItemName, MO.Name AS UomName, SD.Price, SD.Qty, SD.TotalPrice, SH.Remarks, " & vbNewLine & _
                    "	SD.Remarks AS ItemRemarks, SH.PPNPercentage, SH.TotalPPN, SH.GrandTotal " & vbNewLine & _
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
                    "   SH.ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strSalesServiceID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.ID, A.SalesServiceID, A.ItemID, B.Code AS ItemCode, B.Name AS ItemName, A.Qty, A.UomID, C.Code AS UomCode, A.ReturnQty, A.Price, A.Disc,   " & vbNewLine & _
                   "    A.Tax, A.TotalPrice, A.Remarks  " & vbNewLine & _
                   "FROM traSalesServiceDet A " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    A.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    A.UomID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesServiceID=@SalesServiceID" & vbNewLine

                .Parameters.Add("@SalesServiceID", SqlDbType.VarChar, 30).Value = strSalesServiceID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.SalesServiceDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "INSERT INTO traSalesServiceDet " & vbNewLine & _
                    "    (ID, SalesServiceID, ItemID, UomID, Qty, Price, Disc,   " & vbNewLine & _
                    "     Tax, TotalPrice, Remarks)   " & vbNewLine & _
                    "VALUES " & vbNewLine & _
                    "    (@ID, @SalesServiceID, @ItemID, @UomID, @Qty, @Price, @Disc,   " & vbNewLine & _
                    "     @Tax, @TotalPrice, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@SalesServiceID", SqlDbType.VarChar, 30).Value = clsData.SalesServiceID
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@UomID", SqlDbType.Int).Value = clsData.UomID
                .Parameters.Add("@Qty", SqlDbType.Decimal).Value = clsData.Qty
                .Parameters.Add("@Price", SqlDbType.Decimal).Value = clsData.Price
                .Parameters.Add("@Disc", SqlDbType.Decimal).Value = clsData.Disc
                .Parameters.Add("@Tax", SqlDbType.Decimal).Value = clsData.Tax
                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strSalesServiceID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traSalesServiceDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   SalesServiceID=@SalesServiceID " & vbNewLine

                .Parameters.Add("@SalesServiceID", SqlDbType.VarChar, 20).Value = strSalesServiceID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strSalesServiceID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesServiceDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(SalesServiceID,19)=@SalesServiceID " & vbNewLine

                    .Parameters.Add("@SalesServiceID", SqlDbType.VarChar, 19).Value = strSalesServiceID
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

        Public Shared Function OutstandingAmount(ByVal strSalesServiceID As String) As Decimal
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim decReturn As Decimal = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   Amount=GrandTotal-TotalReturn-TotalPayment " & vbNewLine & _
                        "FROM traSalesService " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@SalesServiceID " & vbNewLine

                    .Parameters.Add("@SalesServiceID", SqlDbType.VarChar, 30).Value = strSalesServiceID
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

#End Region

    End Class


End Namespace

