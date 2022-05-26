Namespace DL
    Public Class SalesReturn

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intIDStatus As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, A.SalesReturnDate, A.PaymentTerm,   " & vbNewLine & _
                   "    A.DriverName, A.PlatNumber, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, " & vbNewLine & _
                   "    A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.IsPostedGL, " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSalesReturn A " & vbNewLine & _
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
                   "    AND A.SalesReturnDate>=@DateFrom AND A.SalesReturnDate<=@DateTo " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                .CommandText += "ORDER BY A.SalesReturnDate, A.ID ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.SalesReturn)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traSalesReturn " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, ReferencesID, BPID, SalesReturnDate, PaymentTerm,   " & vbNewLine & _
                       "     DriverName, PlatNumber, PPN, PPH, ItemID, ArrivalBrutto,   " & vbNewLine & _
                       "     ArrivalTarra, ArrivalNettoBefore, ArrivalDeduction, ArrivalNettoAfter, Price, TotalPrice,   " & vbNewLine & _
                       "     Remarks, IDStatus, CreatedBy, LogBy)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @ReferencesID, @BPID, @SalesReturnDate, @PaymentTerm,   " & vbNewLine & _
                       "     @DriverName, @PlatNumber, @PPN, @PPH, @ItemID, @ArrivalBrutto,   " & vbNewLine & _
                       "     @ArrivalTarra, @ArrivalNettoBefore, @ArrivalDeduction, @ArrivalNettoAfter, @Price, @TotalPrice,   " & vbNewLine & _
                       "     @Remarks, @IDStatus, @LogBy, @LogBy)  " & vbNewLine
                Else
                    .CommandText = _
                        "UPDATE traSalesReturn SET " & vbNewLine & _
                        "    CompanyID=@CompanyID, " & vbNewLine & _
                        "    ProgramID=@ProgramID, " & vbNewLine & _
                        "    ReferencesID=@ReferencesID, " & vbNewLine & _
                        "    BPID=@BPID, " & vbNewLine & _
                        "    SalesReturnDate=@SalesReturnDate, " & vbNewLine & _
                        "    PaymentTerm=@PaymentTerm, " & vbNewLine & _
                        "    DriverName=@DriverName, " & vbNewLine & _
                        "    PlatNumber=@PlatNumber, " & vbNewLine & _
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
                .Parameters.Add("@SalesReturnDate", SqlDbType.DateTime).Value = clsData.SalesReturnDate
                .Parameters.Add("@PaymentTerm", SqlDbType.Int).Value = clsData.PaymentTerm
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
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.SalesReturn
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.SalesReturn
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, C.Name AS BPName, A.SalesReturnDate, A.PaymentTerm,   " & vbNewLine & _
                        "   A.DriverName, A.PlatNumber, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, A.ArrivalBrutto,   " & vbNewLine & _
                        "   A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, TS.ArrivalReturn-A.ArrivalNettoAfter AS ArrivalUsage, " & vbNewLine & _
                        "   (A.ArrivalNettoAfter+TS.ArrivalNettoAfter-TS.ArrivalReturn) AS MaxNetto, TS.ArrivalNettoAfter AS ArrivalNettoAfterSales, A.Price, A.TotalPrice, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, " & vbNewLine & _
                        "   A.Remarks, A.IDStatus, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                        "FROM traSalesReturn A " & vbNewLine & _
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
                        "WHERE " & vbNewLine & _
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
                        voReturn.SalesReturnDate = .Item("SalesReturnDate")
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPName = .Item("BPName")
                        voReturn.PaymentTerm = .Item("PaymentTerm")
                        voReturn.ReferencesID = .Item("ReferencesID")
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
                        voReturn.MaxNetto = .Item("MaxNetto")
                        voReturn.ArrivalUsage = .Item("ArrivalUsage")
                        voReturn.ArrivalNettoAfterSales = .Item("ArrivalNettoAfterSales")
                        voReturn.Price = .Item("Price")
                        voReturn.TotalPrice = .Item("TotalPrice")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
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
                    "UPDATE traSalesReturn " & vbNewLine & _
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
            Dim intReturn As Integer = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesReturn " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ID,16)=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID

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
                        "FROM traSalesReturn " & vbNewLine & _
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
                        "FROM traSalesReturn " & vbNewLine & _
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
                        "FROM traSalesReturn " & vbNewLine & _
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
                        "FROM traSalesReturn " & vbNewLine & _
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
                    "UPDATE traSalesReturn " & vbNewLine & _
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
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.ReferencesID, A.BPID, A.SalesReturnDate, A.PaymentTerm,   " & vbNewLine & _
                   "    A.DriverName, A.PlatNumber, A.PPN, A.PPH, A.ItemID, MI.Code AS ItemCode, MI.Name AS ItemName, MI.UomID AS UOMID, MU.Code AS UomCode, " & vbNewLine & _
                   "    A.ArrivalBrutto, A.ArrivalTarra, A.ArrivalNettoBefore, A.ArrivalDeduction, A.ArrivalNettoAfter, A.Price, A.TotalPrice, A.IsPostedGL, " & vbNewLine & _
                   "    A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy, A.LogDate, A.JournalID  " & vbNewLine & _
                   "FROM traSalesReturn A " & vbNewLine & _
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
                   "    AND A.SalesReturnDate>=@DateFrom AND A.SalesReturnDate<=@DateTo " & vbNewLine & _
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
                    "UPDATE traSalesReturn SET" & vbNewLine & _
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
                    "UPDATE traSalesReturn SET" & vbNewLine & _
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
                   "    SH.CompanyID, 'RETUR PENJUALAN' AS Trans, SH.ID, SH.BPID, BP.Name AS BPName, SH.SalesReturnDate AS TransactionDate, SH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, SH.ArrivalBrutto, " & vbNewLine & _
                   "    SH.ArrivalTarra, SH.ArrivalNettoBefore, SH.ArrivalDeduction, SH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, SH.Price, " & vbNewLine & _
                   "    SH.TotalPrice, SH.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
                   "FROM traSalesReturn SH " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner BP ON " & vbNewLine & _
                   "    SH.BPID=BP.ID " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    SH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    SH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    SH.SalesReturnDate>=@DateFrom AND SH.SalesReturnDate<=@DateTo " & vbNewLine & _
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
                   "    SH.CompanyID, 'RETUR PENJUALAN' AS Trans, SH.ID, SH.SalesReturnDate AS TransactionDate, SH.ItemID, B.Code AS ItemCode, B.Name AS ItemName, SH.ArrivalBrutto, " & vbNewLine & _
                   "    SH.ArrivalTarra, SH.ArrivalNettoBefore, SH.ArrivalDeduction, SH.ArrivalNettoAfter, B.UomID, C.Code AS UomCode, SH.Price, " & vbNewLine & _
                   "    SH.TotalPrice, SH.Remarks, SH.IDStatus, MS.Name AS StatusInfo, SH.CreatedBy, SH.CreatedDate, SH.LogInc, SH.LogBy, SH.LogDate, SH.JournalID  " & vbNewLine & _
                   "FROM traSalesReturn SH " & vbNewLine & _
                   "INNER JOIN mstItem B ON " & vbNewLine & _
                   "    SH.ItemID=B.ID " & vbNewLine & _
                   "INNER JOIN mstUOM C ON " & vbNewLine & _
                   "    B.UomID=C.ID " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    SH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    SH.SalesReturnDate>=@DateFrom AND SH.SalesReturnDate<=@DateTo " & vbNewLine & _
                   "    AND SH.IsDeleted=0 " & vbNewLine & _
                   "    AND SH.BPID=@BPID " & vbNewLine

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strSalesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.SalesReturnID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traSalesReturnStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.SalesReturnID=@SalesReturnID " & vbNewLine

                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 30).Value = strSalesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.SalesReturnStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traSalesReturnStatus " & vbNewLine & _
                   "    (ID, SalesReturnID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @SalesReturnID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 30).Value = clsData.SalesReturnID
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

        Public Shared Function GetMaxIDStatus(ByVal strSalesReturnID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traSalesReturnStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   SalesReturnID=@SalesReturnID " & vbNewLine

                    .Parameters.Add("@SalesReturnID", SqlDbType.VarChar, 30).Value = strSalesReturnID

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