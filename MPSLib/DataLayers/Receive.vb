Namespace DL

    Public Class Receive

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, Optional ByVal strSupplierCode As String = "") As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, TS.SalesNo, A.BPID, C.Name AS BPName, A.ReceiveDate, A.PaymentTerm, A.DriverName, " & vbNewLine & _
                   "    A.PlatNumber, A.DONumber, A.SPBNumber, A.SegelNumber, A.Specification, A.DueDate, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price1, A.Price2, A.TotalPrice1, A.TotalPrice2, A.TotalDownPayment, A.ArrivalReturn, A.TotalReturn1, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.ReceiveNo " & vbNewLine & _
                   "FROM traReceive A " & vbNewLine & _
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
                   "INNER JOIN traSales TS ON " & vbNewLine & _
                   "    A.ReferencesID=TS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.ReceiveDate>=@DateFrom AND A.ReceiveDate<=@DateTo " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                If strSupplierCode.Trim <> "" Then
                    .CommandText += "    AND C.Code IN (" & strSupplierCode & ") " & vbNewLine
                End If

                .CommandText += "ORDER BY CONVERT(DATE,A.ReceiveDate), A.ReceiveNo ASC " & vbNewLine

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
                   "FROM traReceive A " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine & _
                   "    AND A.ReceiveDate>=@DateFrom AND A.ReceiveDate<=@DateTo " & vbNewLine & _
                   "ORDER BY CONVERT(DATE,A.ReceiveDate), A.ReceiveNo ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Receive)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traReceive " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, ReferencesID, BPID, ReceiveDate, PaymentTerm, DueDate, DriverName, PlatNumber, " & vbNewLine & _
                       "     DONumber, SPBNumber, SegelNumber, Specification, PPN, PPH, ItemID, ArrivalBrutto, ArrivalTarra, ArrivalNettoBefore, ArrivalDeduction, ArrivalNettoAfter, " & vbNewLine & _
                       "     Price1, TotalPrice1, Price2, TotalPrice2, Tolerance, Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate, ReceiveNo)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @ReferencesID, @BPID, @ReceiveDate, @PaymentTerm, @DueDate, @DriverName, @PlatNumber, " & vbNewLine & _
                       "     @DONumber, @SPBNumber, @SegelNumber, @Specification, @PPN, @PPH, @ItemID, @ArrivalBrutto, @ArrivalTarra, @ArrivalNettoBefore, @ArrivalDeduction, @ArrivalNettoAfter, " & vbNewLine & _
                       "     @Price1, @TotalPrice1, @Price2, @TotalPrice2, @Tolerance, @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE(), @ReceiveNo)  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE traReceive SET " & vbNewLine & _
                        "    CompanyID=@CompanyID, " & vbNewLine & _
                        "    ProgramID=@ProgramID, " & vbNewLine & _
                        "    ReferencesID=@ReferencesID, " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    ReceiveDate=@ReceiveDate, " & vbNewLine & _
                        "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                        "    PlatNumber=@PlatNumber, " & vbNewLine & _
                        "    DriverName=@DriverName, " & vbNewLine & _
                        "    DueDate=@DueDate, " & vbNewLine & _
                        "    DONumber=@DONumber, " & vbNewLine & _
                        "    SPBNumber=@SPBNumber, " & vbNewLine & _
                        "    SegelNumber=@SegelNumber, " & vbNewLine & _
                        "    Specification=@Specification, " & vbNewLine & _
                        "    PPN=@PPN, " & vbNewLine & _
                        "    PPH=@PPH, " & vbNewLine & _
                        "    ItemID=@ItemID, " & vbNewLine & _
                        "    ArrivalBrutto=@ArrivalBrutto, " & vbNewLine & _
                        "    ArrivalTarra=@ArrivalTarra, " & vbNewLine & _
                        "    ArrivalNettoBefore=@ArrivalNettoBefore, " & vbNewLine & _
                        "    ArrivalDeduction=@ArrivalDeduction, " & vbNewLine & _
                        "    ArrivalNettoAfter=@ArrivalNettoAfter, " & vbNewLine & _
                        "    Price1=@Price1, " & vbNewLine & _
                        "    Price2=@Price2, " & vbNewLine & _
                        "    TotalPrice1=@TotalPrice1, " & vbNewLine & _
                        "    TotalPrice2=@TotalPrice2, " & vbNewLine & _
                        "    Tolerance=@Tolerance, " & vbNewLine & _
                        "    Remarks=@Remarks, " & vbNewLine & _
                        "    IDStatus=@IDStatus, " & vbNewLine & _
                        "    LogInc=LogInc+1, " & vbNewLine & _
                        "    LogBy=@LogBy, " & vbNewLine & _
                        "    LogDate=GETDATE(), " & vbNewLine & _
                        "    ReceiveNo=@ReceiveNo " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@ReceiveDate", SqlDbType.DateTime).Value = clsData.ReceiveDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
                .Parameters.Add("@DueDate", SqlDbType.DateTime).Value = clsData.DueDate
                .Parameters.Add("@DriverName", SqlDbType.VarChar, 100).Value = clsData.DriverName
                .Parameters.Add("@PlatNumber", SqlDbType.VarChar, 10).Value = clsData.PlatNumber
                .Parameters.Add("@DONumber", SqlDbType.VarChar, 250).Value = clsData.DONumber
                .Parameters.Add("@SPBNumber", SqlDbType.VarChar, 250).Value = clsData.SPBNumber
                .Parameters.Add("@SegelNumber", SqlDbType.VarChar, 100).Value = clsData.SegelNumber
                .Parameters.Add("@Specification", SqlDbType.VarChar, 250).Value = clsData.Specification
                .Parameters.Add("@PPN", SqlDbType.Decimal).Value = clsData.PPN
                .Parameters.Add("@PPH", SqlDbType.Decimal).Value = clsData.PPH
                .Parameters.Add("@ItemID", SqlDbType.Int).Value = clsData.ItemID
                .Parameters.Add("@ArrivalBrutto", SqlDbType.Decimal).Value = clsData.ArrivalBrutto
                .Parameters.Add("@ArrivalTarra", SqlDbType.Decimal).Value = clsData.ArrivalTarra
                .Parameters.Add("@ArrivalNettoBefore", SqlDbType.Decimal).Value = clsData.ArrivalNettoBefore
                .Parameters.Add("@ArrivalDeduction", SqlDbType.Decimal).Value = clsData.ArrivalDeduction
                .Parameters.Add("@ArrivalNettoAfter", SqlDbType.Decimal).Value = clsData.ArrivalNettoAfter
                .Parameters.Add("@Price1", SqlDbType.Decimal).Value = clsData.Price1
                .Parameters.Add("@Price2", SqlDbType.Decimal).Value = clsData.Price2
                .Parameters.Add("@TotalPrice1", SqlDbType.Decimal).Value = clsData.TotalPrice1
                .Parameters.Add("@TotalPrice2", SqlDbType.Decimal).Value = clsData.TotalPrice2
                .Parameters.Add("@Tolerance", SqlDbType.Decimal).Value = clsData.Tolerance
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@ReceiveNo", SqlDbType.VarChar, 30).Value = clsData.ReceiveNo
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.Receive
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Receive
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT 	" & vbNewLine & _
                        "   A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Code AS BPCode, C.Name AS BPName, A.ReceiveDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, 	" & vbNewLine & _
                        "   A.DONumber, A.SPBNumber, A.SegelNumber, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, 	" & vbNewLine & _
                        "   A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, TS.ArrivalUsage, (A.ArrivalNettoAfter+TS.ArrivalNettoAfter-TS.ArrivalUsage-TS.ArrivalReturn) AS MaxNetto, 	" & vbNewLine & _
                        "   A.Specification, A.Price1, A.TotalPrice1, A.Price2, A.TotalPrice2, A.TotalDownPayment, A.ArrivalReturn, A.TotalPayment, A.Tolerance, A.IsPostedGL, TS.ArrivalNettoAfter AS ArrivalNettoAfterSales, " & vbNewLine & _
                        "   A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy,   	" & vbNewLine & _
                        "   A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.ReceiveNo, TS.SalesNo " & vbNewLine & _
                        "FROM traReceive A 	" & vbNewLine & _
                        "INNER JOIN traSales TS ON 	" & vbNewLine & _
                        "	A.ReferencesID=TS.ID 	" & vbNewLine & _
                        "INNER JOIN mstBusinessPartner C ON 	" & vbNewLine & _
                        "   A.BPID=C.ID 	" & vbNewLine & _
                        "INNER JOIN mstItem MI ON 	" & vbNewLine & _
                        "   A.ItemID=MI.ID 	" & vbNewLine & _
                        "INNER JOIN mstUOM MU ON 	" & vbNewLine & _
                        "   MI.UomID=MU.ID	" & vbNewLine & _
                        "INNER JOIN mstCompany MC ON " & vbNewLine & _
                        "   A.CompanyID=MC.ID " & vbNewLine & _
                        "INNER JOIN mstProgram MP ON " & vbNewLine & _
                        "   A.ProgramID=MP.ID " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   A.ID=@ID " & vbNewLine

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
                        voReturn.ReferencesID = .Item("ReferencesID")
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPCode = .Item("BPCode")
                        voReturn.BPName = .Item("BPName")
                        voReturn.ReceiveDate = .Item("ReceiveDate")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.DueDate = .Item("DueDate")
                        voReturn.DriverName = .Item("DriverName")
                        voReturn.PlatNumber = .Item("PlatNumber")
                        voReturn.DONumber = .Item("DONumber")
                        voReturn.SPBNumber = .Item("SPBNumber")
                        voReturn.SegelNumber = .Item("SegelNumber")
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
                        voReturn.MaxNetto = .Item("MaxNetto")
                        voReturn.Price1 = .Item("Price1")
                        voReturn.Price2 = .Item("Price2")
                        voReturn.TotalPrice1 = .Item("TotalPrice1")
                        voReturn.TotalPrice2 = .Item("TotalPrice2")
                        voReturn.TotalDownPayment = .Item("TotalDownPayment")
                        voReturn.ArrivalNettoAfterSales = .Item("ArrivalNettoAfterSales")
                        voReturn.Specification = .Item("Specification")
                        voReturn.ArrivalUsage = .Item("ArrivalUsage")
                        voReturn.ArrivalReturn = .Item("ArrivalReturn")
                        voReturn.TotalPayment = .Item("TotalPayment")
                        voReturn.Tolerance = .Item("Tolerance")
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
                        voReturn.ReceiveNo = .Item("ReceiveNo")
                        voReturn.SalesNo = .Item("SalesNo")
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
                    "UPDATE traReceive " & vbNewLine & _
                    "SET IsDeleted=1, IDStatus=@IDStatus " & vbNewLine & _
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
                        "FROM traReceive " & vbNewLine & _
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

        Public Shared Function GetMaxReceiveNo(ByVal strReceiveNo As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ReceiveNo),3),0) " & vbNewLine & _
                        "FROM traReceive " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ReceiveNo,16)=@ReceiveNo " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    .Parameters.Add("@ReceiveNo", SqlDbType.VarChar, 16).Value = strReceiveNo

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
                        "FROM traReceive " & vbNewLine & _
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

        Public Shared Function DataExistsReceiveNo(ByVal strReceiveNo As String, Optional ByVal strID As String = "") As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ReceiveNo " & vbNewLine & _
                        "FROM traReceive " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ReceiveNo=@ReceiveNo " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    If strID.Trim <> "" Then
                        .CommandText += "   AND ID<>@ID " & vbNewLine
                    End If

                    .Parameters.Add("@ReceiveNo", SqlDbType.VarChar, 30).Value = strReceiveNo
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
                        "FROM traReceive " & vbNewLine & _
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

        Public Shared Function GetReceiveNo(ByVal strID As String) As String
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim strReturn As String = ""
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 ReceiveNo " & vbNewLine & _
                        "FROM traReceive " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        strReturn = .Item("ReceiveNo")
                    End If
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            Finally
                If Not sqlrdData Is Nothing Then sqlrdData.Close()
            End Try
            Return strReturn
        End Function

        Public Shared Function ListDataSlipTimbang(ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    MC.Name AS CompanyName, A.ReceiveNo AS ID, A.ReceiveDate, C.Name AS BPName, C.Address AS BPAddress, A.DONumber, A.SPBNumber, A.ArrivalBrutto AS Brutto, A.ArrivalTarra AS Tarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore AS NettoBefore, A.ArrivalDeduction AS Deduction, A.ArrivalNettoAfter AS NettoAfter, A.Remarks, MI.Name AS ItemName, A.SegelNumber, A.DriverName, A.PlatNumber, " & vbNewLine & _
                   "    A.Specification, A.Price1 AS Price, A.TotalPrice1 AS TotalPrice, " & vbNewLine & _
                   "    A.Remarks, A.CreatedBy " & vbNewLine & _
                   "FROM traReceive A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstItem MI ON " & vbNewLine & _
                   "    A.ItemID=MI.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "WHERE A.ID=@ID	" & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 20).Value = strID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub PrintSlipTimbang(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traReceive SET " & vbNewLine & _
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
                    "   A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.ReceiveDate, A.PaymentTerm, A.PlatNumber, A.DriverName,  	" & vbNewLine & _
                    "	A.DONumber, A.SPBNumber, A.SegelNumber, A.DueDate, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.ID AS UOMID, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra,  	" & vbNewLine & _
                    "   A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price1, A.Price2, A.TotalPrice1, A.TotalPrice2, A.Tolerance, A.ArrivalNettoAfter AS ArrivalNettoAfterReceive, 	" & vbNewLine & _
                    "	A.ArrivalNettoAfter-A.ArrivalReturn AS MaxNetto, A.ArrivalReturn AS ArrivalNettoUsage, A.Specification, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,    	" & vbNewLine & _
                    "   A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID   	" & vbNewLine & _
                    "FROM traReceive A  	" & vbNewLine & _
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
                    "   AND A.ReceiveDate>=@DateFrom AND A.ReceiveDate<=@DateTo 	" & vbNewLine & _
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
                    "UPDATE traReceive SET " & vbNewLine & _
                    "ArrivalReturn=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT ISNULL(SUM(A.ArrivalNettoAfter),0) AS Total " & vbNewLine & _
                    "		FROM traReceiveReturn A 	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			A.ReferencesID=@ReferencesID " & vbNewLine & _
                    "			AND A.IsDeleted=0 	" & vbNewLine & _
                    "	),0), " & vbNewLine & _
                    "TotalReturn1=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT ISNULL(SUM(A.TotalPrice1),0) AS Total" & vbNewLine & _
                    "		FROM traReceiveReturn A 	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			A.ReferencesID=@ReferencesID " & vbNewLine & _
                    "			AND A.IsDeleted=0 	" & vbNewLine & _
                    "	),0), " & vbNewLine & _
                    "TotalReturn2=	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "	(	" & vbNewLine & _
                    "		SELECT ISNULL(SUM(A.TotalPrice2),0) AS Total" & vbNewLine & _
                    "		FROM traReceiveReturn A 	" & vbNewLine & _
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

        Public Shared Sub CalculateTotalPayment(ByVal strReceiveID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traReceive " & vbNewLine & _
                    "SET TotalPayment=" & vbNewLine & _
                    "	(" & vbNewLine & _
                    "		SELECT " & vbNewLine & _
                    "			ISNULL(SUM(APD.Amount),0) AS Total" & vbNewLine & _
                    "		FROM traAccountPayableDet APD " & vbNewLine & _
                    "		INNER JOIN traAccountPayable AP ON " & vbNewLine & _
                    "            APD.APID=AP.ID" & vbNewLine & _
                    "        WHERE" & vbNewLine & _
                    "           AP.IsDeleted=0" & vbNewLine & _
                    "			AND APD.ReceiveID=@ReceiveID" & vbNewLine & _
                    "	)" & vbNewLine & _
                    "WHERE ID=@ReceiveID " & vbNewLine

                .Parameters.Add("@ReceiveID", SqlDbType.VarChar, 30).Value = strReceiveID
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
                        "FROM traReceive " & vbNewLine & _
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
                    .Close()
                End With
                If Not SQL.bolUseTrans Then SQL.CloseConnection()
            Catch ex As Exception
                Throw ex
            End Try
            Return bolExists
        End Function

        Public Shared Sub UpdateJournalID(ByVal strID As String, ByVal strJournalID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traReceive " & vbNewLine & _
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
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Name AS BPName, A.ReceiveDate, A.PaymentTerm, A.DriverName, " & vbNewLine & _
                   "    A.PlatNumber, A.DONumber, A.SPBNumber, A.SegelNumber, A.Specification, A.DueDate, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price1, A.Price2, A.TotalPrice1, A.TotalPrice2, A.ArrivalReturn, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traReceive A " & vbNewLine & _
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
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Name AS BPName, A.ReceiveDate, A.PaymentTerm, A.DriverName, " & vbNewLine & _
                   "    A.PlatNumber, A.DONumber, A.SPBNumber, A.SegelNumber, A.Specification, A.DueDate, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price1, A.Price2, A.TotalPrice1, A.TotalPrice2, A.ArrivalReturn, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traReceive A " & vbNewLine & _
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
                   "    AND A.ReceiveDate>=@DateFrom AND A.ReceiveDate<=@DateTo " & vbNewLine & _
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
                    "UPDATE traReceive SET" & vbNewLine & _
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
                    "UPDATE traReceive SET" & vbNewLine & _
                    "   IsPostedGL=1, " & vbNewLine & _
                    "   PostedBy=@LogBy, " & vbNewLine & _
                    "   PostedDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
                    "   AND ReceiveDate>=@DateFrom AND ReceiveDate<=@DateTo " & vbNewLine & _
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
                    "UPDATE traReceive SET" & vbNewLine & _
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
                    "UPDATE traReceive SET" & vbNewLine & _
                    "   IsPostedGL=0, " & vbNewLine & _
                    "   PostedBy='' " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
                    "   AND ReceiveDate>=@DateFrom AND ReceiveDate<=@DateTo " & vbNewLine & _
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
                   "    RH.CompanyID, 'PEMBELIAN' AS Trans, RH.ID, RH.BPID, BP.Name AS BPName, RH.ReceiveDate AS TransactionDate, RH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, RH.ArrivalBrutto, " & vbNewLine & _
                   "    RH.ArrivalTarra, RH.ArrivalNettoBefore, RH.ArrivalDeduction, RH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, RH.Price1 AS Price, " & vbNewLine & _
                   "    RH.TotalPrice1 AS TotalPrice, RH.Remarks, RH.IDStatus, MS.Name AS StatusInfo, RH.CreatedBy, RH.CreatedDate, RH.LogInc, RH.LogBy, RH.LogDate, RH.JournalID  " & vbNewLine & _
                   "FROM traReceive RH " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
                   "    RH.BPID=BP.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    RH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    RH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    RH.ReceiveDate>=@DateFrom AND RH.ReceiveDate<=@DateTo " & vbNewLine & _
                   "    AND RH.IsDeleted=0 " & vbNewLine & _
                   "    AND RH.ItemID=@ItemID " & vbNewLine

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
                   "    RH.CompanyID, 'PEMBELIAN' AS Trans, RH.ID, RH.ReceiveDate AS TransactionDate, RH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, RH.ArrivalBrutto, " & vbNewLine & _
                   "    RH.ArrivalTarra, RH.ArrivalNettoBefore, RH.ArrivalDeduction, RH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, RH.Price1 AS Price, " & vbNewLine & _
                   "    RH.TotalPrice1 AS TotalPrice, RH.Remarks, RH.IDStatus, MS.Name AS StatusInfo, RH.CreatedBy, RH.CreatedDate, RH.LogInc, RH.LogBy, RH.LogDate, RH.JournalID  " & vbNewLine & _
                   "FROM traReceive RH " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    RH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    RH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    RH.ReceiveDate>=@DateFrom AND RH.ReceiveDate<=@DateTo " & vbNewLine & _
                   "    AND RH.IsDeleted=0 " & vbNewLine & _
                   "    AND RH.BPID=@BPID " & vbNewLine

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
                    "UPDATE traReceive " & vbNewLine & _
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
                    "UPDATE traReceive " & vbNewLine & _
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
                   "    A.CompanyID, A.ProgramID, A.ID, A.BPID, A.ReceiveDate, A.PaymentTerm, A.DriverName, A.PlatNumber, A.DueDate, " & vbNewLine & _
                   "    A.PPN, A.PPH, A.ItemID, A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, " & vbNewLine & _
                   "    A.Price1, A.TotalPrice1, A.ArrivalReturn, A.TotalDownPayment, A.TotalPayment, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, " & vbNewLine & _
                   "    A.LogDate, A.JournalID, A.TotalPrice1-A.TotalReturn1-A.TotalPayment-A.TotalDownPayment AS OutstandingPayment  " & vbNewLine & _
                   "FROM traReceive A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.BPID=@BPID " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine & _
                   "    AND A.TotalPrice1-A.TotalReturn1-A.TotalPayment-A.TotalDownPayment>0" & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strReceiveID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.ReceiveID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traReceiveStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ReceiveID=@ReceiveID " & vbNewLine

                .Parameters.Add("@ReceiveID", SqlDbType.VarChar, 30).Value = strReceiveID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.ReceiveStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traReceiveStatus " & vbNewLine & _
                   "    (ID, ReceiveID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @ReceiveID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@ReceiveID", SqlDbType.VarChar, 30).Value = clsData.ReceiveID
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

        Public Shared Function GetMaxIDStatus(ByVal strReceiveID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traReceiveStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ReceiveID=@ReceiveID " & vbNewLine

                    .Parameters.Add("@ReceiveID", SqlDbType.VarChar, 21).Value = strReceiveID

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