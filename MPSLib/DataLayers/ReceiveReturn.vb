Namespace DL
    Public Class ReceiveReturn

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Name AS BPName, A.ReceiveReturnDate, A.PaymentTerm, A.DriverName, " & vbNewLine & _
                   "    A.PlatNumber, A.DONumber, A.SPBNumber, A.SegelNumber, A.Specification, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price1, A.Price2, A.TotalPrice1, A.TotalPrice2, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traReceiveReturn A " & vbNewLine & _
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
                   "    AND A.ReceiveReturnDate>=@DateFrom AND A.ReceiveReturnDate<=@DateTo " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                .CommandText += "ORDER BY A.ReceiveReturnDate, A.ID ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.ReceiveReturn)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traReceiveReturn " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, ReferencesID, BPID, ReceiveReturnDate, PaymentTerm, DriverName, PlatNumber, " & vbNewLine & _
                       "     DONumber, SPBNumber, SegelNumber, Specification, PPN, PPH, ItemID, ArrivalBrutto, ArrivalTarra, ArrivalNettoBefore, ArrivalDeduction, ArrivalNettoAfter, " & vbNewLine & _
                       "     Price1, TotalPrice1, Price2, TotalPrice2, Tolerance, Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @ReferencesID, @BPID, @ReceiveReturnDate, @PaymentTerm, @DriverName, @PlatNumber, " & vbNewLine & _
                       "     @DONumber, @SPBNumber, @SegelNumber, @Specification, @PPN, @PPH, @ItemID, @ArrivalBrutto, @ArrivalTarra, @ArrivalNettoBefore, @ArrivalDeduction, @ArrivalNettoAfter, " & vbNewLine & _
                       "     @Price1, @TotalPrice1, @Price2, @TotalPrice2, @Tolerance, @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE())  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE traReceiveReturn SET " & vbNewLine & _
                        "    CompanyID=@CompanyID, " & vbNewLine & _
                        "    ProgramID=@ProgramID, " & vbNewLine & _
                        "    ReferencesID=@ReferencesID, " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    ReceiveReturnDate=@ReceiveReturnDate, " & vbNewLine & _
                        "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                        "    PlatNumber=@PlatNumber, " & vbNewLine & _
                        "    DriverName=@DriverName, " & vbNewLine & _
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
                        "    LogDate=GETDATE() " & vbNewLine & _
                        "WHERE " & vbNewLine & _
                        "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = clsData.ReferencesID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@ReceiveReturnDate", SqlDbType.DateTime).Value = clsData.ReceiveReturnDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
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
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.ReceiveReturn
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.ReceiveReturn
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT 	" & vbNewLine & _
                        "   A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Name AS BPName, A.ReceiveReturnDate, A.PaymentTerm, A.DriverName, A.PlatNumber, 	" & vbNewLine & _
                        "   A.DONumber, A.SPBNumber, A.SegelNumber, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, 	" & vbNewLine & _
                        "   A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, TS.ArrivalReturn-A.ArrivalNettoAfter AS ArrivalUsage, (A.ArrivalNettoAfter+TS.ArrivalNettoAfter-TS.ArrivalReturn) AS MaxNetto, 	" & vbNewLine & _
                        "   A.Specification, A.Price1, A.TotalPrice1, A.Price2, A.TotalPrice2, A.Tolerance, A.IsPostedGL, TS.ArrivalNettoAfter AS ArrivalNettoAfterReceive, " & vbNewLine & _
                        "   A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy,   	" & vbNewLine & _
                        "   A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  	" & vbNewLine & _
                        "FROM traReceiveReturn A 	" & vbNewLine & _
                        "INNER JOIN traReceive TS ON 	" & vbNewLine & _
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
                        voReturn.BPName = .Item("BPName")
                        voReturn.ReceiveReturnDate = .Item("ReceiveReturnDate")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.DriverName = .Item("DriverName")
                        voReturn.PlatNumber = .Item("PlatNumber")
                        voReturn.DONumber = .Item("DONumber")
                        voReturn.SPBNumber = .Item("SPBNumber")
                        voReturn.SegelNumber = .Item("SegelNumber")
                        voReturn.Remarks = .Item("Remarks")
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
                        voReturn.ArrivalUsage = .Item("ArrivalUsage")
                        voReturn.Specification = .Item("Specification")
                        voReturn.Price1 = .Item("Price1")
                        voReturn.TotalPrice1 = .Item("TotalPrice1")
                        voReturn.Price2 = .Item("Price2")
                        voReturn.TotalPrice2 = .Item("TotalPrice2")
                        voReturn.Tolerance = .Item("Tolerance")
                        voReturn.ArrivalNettoAfterReceive = .Item("ArrivalNettoAfterReceive")
                        voReturn.ArrivalUsage = .Item("ArrivalUsage")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogInc = .Item("LogInc")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.JournalID = .Item("JournalID")
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
                    "UPDATE traReceiveReturn " & vbNewLine & _
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
                        "FROM traReceiveReturn " & vbNewLine & _
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
                        "FROM traReceiveReturn " & vbNewLine & _
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
                        "FROM traReceiveReturn " & vbNewLine & _
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

        Public Shared Function GetReturnID(ByVal strReferencesID As String) As String
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim strReturn As String = ""
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traReceiveReturn " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ReferencesID=@ReferencesID " & vbNewLine

                    .Parameters.Add("@ReferencesID", SqlDbType.VarChar, 30).Value = strReferencesID

                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        strReturn = .Item("ID")
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
                        "FROM traReceiveReturn " & vbNewLine & _
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
                    "UPDATE traReceiveReturn " & vbNewLine & _
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

        Public Shared Function ListDataOutstandingPostGL(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Name AS BPName, A.ReceiveReturnDate, A.PaymentTerm, A.DriverName, " & vbNewLine & _
                   "    A.PlatNumber, A.DONumber, A.SPBNumber, A.SegelNumber, A.Specification, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MU.Code AS UomCode, A.ArrivalBrutto, A.ArrivalTarra, " & vbNewLine & _
                   "    A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price1, A.Price2, A.TotalPrice1, A.TotalPrice2, A.IsPostedGL,   " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy,   " & vbNewLine & _
                   "    A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traReceiveReturn A " & vbNewLine & _
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
                   "    AND A.ReceiveReturnDate>=@DateFrom AND A.ReceiveReturnDate<=@DateTo " & vbNewLine & _
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
                    "UPDATE traReceiveReturn SET" & vbNewLine & _
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

        Public Shared Sub UnpostGL(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traReceiveReturn SET" & vbNewLine & _
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

        Public Shared Function ListDataHistoryBussinessPartners(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intItemID As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    RH.CompanyID, 'RETUR PEMBELIAN' AS Trans, RH.ID, RH.BPID, BP.Name AS BPName, RH.ReceiveReturnDate AS TransactionDate, RH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, RH.ArrivalBrutto, " & vbNewLine & _
                   "    RH.ArrivalTarra, RH.ArrivalNettoBefore, RH.ArrivalDeduction, RH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, RH.Price1 AS Price, " & vbNewLine & _
                   "    RH.TotalPrice1 AS TotalPrice, RH.Remarks, RH.IDStatus, MS.Name AS StatusInfo, RH.CreatedBy, RH.CreatedDate, RH.LogInc, RH.LogBy, RH.LogDate, RH.JournalID  " & vbNewLine & _
                   "FROM traReceiveReturn RH " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
                   "    RH.BPID=BP.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    RH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    RH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    RH.ReceiveReturnDate>=@DateFrom AND RH.ReceiveReturnDate<=@DateTo " & vbNewLine & _
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
                   "    RH.CompanyID, 'RETUR PEMBELIAN' AS Trans, RH.ID, RH.ReceiveReturnDate AS TransactionDate, RH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, RH.ArrivalBrutto, " & vbNewLine & _
                   "    RH.ArrivalTarra, RH.ArrivalNettoBefore, RH.ArrivalDeduction, RH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, RH.Price1 AS Price, " & vbNewLine & _
                   "    RH.TotalPrice1 AS TotalPrice, RH.Remarks, RH.IDStatus, MS.Name AS StatusInfo, RH.CreatedBy, RH.CreatedDate, RH.LogInc, RH.LogBy, RH.LogDate, RH.JournalID  " & vbNewLine & _
                   "FROM traReceiveReturn RH " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    RH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    RH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    RH.ReceiveReturnDate>=@DateFrom AND RH.ReceiveReturnDate<=@DateTo " & vbNewLine & _
                   "    AND RH.IsDeleted=0 " & vbNewLine & _
                   "    AND RH.BPID=@BPID " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
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
                   "     A.ID, A.ReceiveReturnID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traReceiveReturnStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ReceiveReturnID=@ReceiveReturnID " & vbNewLine

                .Parameters.Add("@ReceiveReturnID", SqlDbType.VarChar, 30).Value = strReceiveID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.ReceiveReturnStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traReceiveReturnStatus " & vbNewLine & _
                   "    (ID, ReceiveReturnID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @ReceiveReturnID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@ReceiveReturnID", SqlDbType.VarChar, 30).Value = clsData.ReceiveReturnID
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

        Public Shared Function GetMaxIDStatus(ByVal strReceiveReturnID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traReceiveReturnStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ReceiveReturnID=@ReceiveReturnID " & vbNewLine

                    .Parameters.Add("@ReceiveReturnID", SqlDbType.VarChar, 30).Value = strReceiveReturnID

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