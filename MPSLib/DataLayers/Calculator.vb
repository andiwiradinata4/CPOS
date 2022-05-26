Namespace DL
    Public Class Calculator

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                ByVal intIDStatus As Integer, Optional ByVal strBPCode As String = "") As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.TransactionNo, A.BPID, C.Name AS BPName, C.Address, " & vbNewLine & _
                   "     A.TransactionDate, A.PPN, A.PPH, A.TotalPrice, A.TotalPPN, A.TotalPPH, A.GrandTotal, A.TotalDownPayment,   " & vbNewLine & _
                   "     A.TotalPayment, A.TotalReturn, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted,   " & vbNewLine & _
                   "     A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, A.LogInc, A.LogBy,   " & vbNewLine & _
                   "     A.LogDate, A.JournalID, B.Name AS StatusInfo " & vbNewLine & _
                   "FROM traCalculator A " & vbNewLine & _
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
                   "    AND A.TransactionDate>=@DateFrom AND A.TransactionDate<=@DateTo " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                If strBPCode.Trim <> "" Then
                    .CommandText += "    AND C.Code IN (" & strBPCode & ") " & vbNewLine
                End If

                .CommandText += "ORDER BY CONVERT(DATE,A.TransactionDate), A.TransactionNo ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataStruk(ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.TransactionNo, A.BPID, C.Name AS BPName, C.Address AS BPAddress, " & vbNewLine & _
                   "     A.TransactionDate, B.Idx, B.Remarks, B.Amount, B.Symbol, A.TotalPrice " & vbNewLine & _
                   "FROM traCalculator A " & vbNewLine & _
                   "INNER JOIN traCalculatorDet B ON " & vbNewLine & _
                   "    A.ID=B.CalculatorID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ID=@ID " & vbNewLine & _
                   "ORDER BY B.Idx ASC " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = strID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Calculator)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traCalculator " & vbNewLine & _
                       "    (CompanyID, ProgramID, ID, TransactionNo, BPID, TransactionDate, PPN,   " & vbNewLine & _
                       "     PPH, TotalPrice, TotalPPN, TotalPPH, GrandTotal, TotalDownPayment,   " & vbNewLine & _
                       "     TotalPayment, Remarks, IDStatus, CreatedBy, LogBy)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@CompanyID, @ProgramID, @ID, @TransactionNo, @BPID, @TransactionDate, @PPN,   " & vbNewLine & _
                       "     @PPH, @TotalPrice, @TotalPPN, @TotalPPH, @GrandTotal, @TotalDownPayment,   " & vbNewLine & _
                       "     @TotalPayment, @Remarks, @IDStatus, @LogBy, @LogBy)  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traCalculator SET " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    ProgramID=@ProgramID, " & vbNewLine & _
                    "    TransactionNo=@TransactionNo, " & vbNewLine & _
                    "    BPID=@BPID, " & vbNewLine & _
                    "    TransactionDate=@TransactionDate, " & vbNewLine & _
                    "    PPN=@PPN, " & vbNewLine & _
                    "    PPH=@PPH, " & vbNewLine & _
                    "    TotalPrice=@TotalPrice, " & vbNewLine & _
                    "    TotalPPN=@TotalPPN, " & vbNewLine & _
                    "    TotalPPH=@TotalPPH, " & vbNewLine & _
                    "    GrandTotal=@GrandTotal, " & vbNewLine & _
                    "    TotalDownPayment=@TotalDownPayment, " & vbNewLine & _
                    "    TotalPayment=@TotalPayment, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = clsData.ID
                .Parameters.Add("@TransactionNo", SqlDbType.VarChar, 30).Value = clsData.TransactionNo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = clsData.TransactionDate
                .Parameters.Add("@PPN", SqlDbType.Decimal).Value = clsData.PPN
                .Parameters.Add("@PPH", SqlDbType.Decimal).Value = clsData.PPH
                .Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = clsData.TotalPrice
                .Parameters.Add("@TotalPPN", SqlDbType.Decimal).Value = clsData.TotalPPN
                .Parameters.Add("@TotalPPH", SqlDbType.Decimal).Value = clsData.TotalPPH
                .Parameters.Add("@GrandTotal", SqlDbType.Decimal).Value = clsData.GrandTotal
                .Parameters.Add("@TotalDownPayment", SqlDbType.Decimal).Value = clsData.TotalDownPayment
                .Parameters.Add("@TotalPayment", SqlDbType.Decimal).Value = clsData.TotalPayment
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

        Public Shared Function GetDetail(ByVal strID As String) As VO.Calculator
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.Calculator
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.TransactionNo, A.BPID, C.Name AS BPName, C.Address AS BPAddress, " & vbNewLine & _
                       "    A.TransactionDate, A.PPN, A.PPH, A.TotalPrice, A.TotalPPN, A.TotalPPH, A.GrandTotal, A.TotalDownPayment,   " & vbNewLine & _
                       "    A.TotalPayment, A.TotalReturn, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted,   " & vbNewLine & _
                       "    A.Remarks, A.IDStatus, A.LogBy, A.LogDate, A.LogInc, A.JournalID  " & vbNewLine & _
                       "FROM traCalculator A " & vbNewLine & _
                       "INNER JOIN mstStatus B ON " & vbNewLine & _
                       "    A.IDStatus=B.ID " & vbNewLine & _
                       "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                       "    A.BPID=C.ID " & vbNewLine & _
                       "INNER JOIN mstCompany MC ON " & vbNewLine & _
                       "    A.CompanyID=MC.ID " & vbNewLine & _
                       "INNER JOIN mstProgram MP ON " & vbNewLine & _
                       "    A.ProgramID=MP.ID " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "    A.ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = strID

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
                        voReturn.TransactionNo = .Item("TransactionNo")
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPName = .Item("BPName")
                        voReturn.BPAddress = .Item("BPAddress")
                        voReturn.TransactionDate = .Item("TransactionDate")
                        voReturn.PPN = .Item("PPN")
                        voReturn.PPH = .Item("PPH")
                        voReturn.TotalPrice = .Item("TotalPrice")
                        voReturn.TotalPPN = .Item("TotalPPN")
                        voReturn.TotalPPH = .Item("TotalPPH")
                        voReturn.GrandTotal = .Item("GrandTotal")
                        voReturn.TotalDownPayment = .Item("TotalDownPayment")
                        voReturn.TotalPayment = .Item("TotalPayment")
                        voReturn.TotalReturn = .Item("TotalReturn")
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
                    "UPDATE traCalculator " & vbNewLine & _
                    "SET IsDeleted=1, IDStatus=@IDStatus, TotalDownPayment=0 " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = strID
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
                        "FROM traCalculator " & vbNewLine & _
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

        Public Shared Function GetMaxTransactionNo(ByVal strID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 0
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(TransactionNo),3),0) " & vbNewLine & _
                        "FROM traCalculator " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(TransactionNo,16)=@TransactionNo " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    .Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = strID
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
                        "FROM traCalculator " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = strID

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

        Public Shared Function DataExistsTransactionNo(ByVal strTransactionNo As String, Optional ByVal strID As String = "") As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   TransactionNo " & vbNewLine & _
                        "FROM traCalculator " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   TransactionNo=@TransactionNo " & vbNewLine & _
                        "   AND IsDeleted=0 " & vbNewLine

                    If strID.Trim <> "" Then
                        .CommandText += "   AND ID<>@ID " & vbNewLine
                    End If

                    .Parameters.Add("@TransactionNo", SqlDbType.VarChar, 30).Value = strTransactionNo
                    .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = strID

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
                        "FROM traCalculator " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   ID=@ID " & vbNewLine & _
                        "   AND IsDeleted=1 " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = strID
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

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strCalculatorID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.CalculatorID, A.Idx, A.Remarks, A.Amount, A.Symbol  " & vbNewLine & _
                   "FROM traCalculatorDet A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CalculatorID=@CalculatorID" & vbNewLine

                .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 100).Value = strCalculatorID

            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.CalculatorDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traCalculatorDet " & vbNewLine & _
                   "    (ID, CalculatorID, Idx, Remarks, Amount, Symbol)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @CalculatorID, @Idx, @Remarks, @Amount, @Symbol)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = clsData.ID
                .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 100).Value = clsData.CalculatorID
                .Parameters.Add("@Idx", SqlDbType.Int).Value = clsData.Idx
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@Amount", SqlDbType.Decimal).Value = clsData.Amount
                .Parameters.Add("@Symbol", SqlDbType.VarChar, 10).Value = clsData.Symbol
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataDetail(ByVal strCalculatorID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traCalculatorDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CalculatorID=@CalculatorID " & vbNewLine

                .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 100).Value = strCalculatorID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strCalculatorID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traCalculatorDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(CalculatorID,19)=@CalculatorID " & vbNewLine

                    .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 19).Value = strCalculatorID

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

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strCalculatorID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.CalculatorID, A.Status, StatusInfo=CASE A.Status WHEN 0 THEN 'ACTIVE' ELSE 'IN-ACTIVE' END, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traCalculatorStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CalculatorID=@CalculatorID" & vbNewLine

                .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 100).Value = strCalculatorID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.CalculatorStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traCalculatorStatus " & vbNewLine & _
                   "    (ID, CalculatorID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @CalculatorID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 100).Value = clsData.ID
                .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 100).Value = clsData.CalculatorID
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

        Public Shared Function GetMaxIDStatus(ByVal strCalculatorID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traCalculatorStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   CalculatorID=@CalculatorID " & vbNewLine

                    .Parameters.Add("@CalculatorID", SqlDbType.VarChar, 19).Value = strCalculatorID

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

