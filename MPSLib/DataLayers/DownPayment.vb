Namespace DL

    Public Class DownPayment

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, ByVal intDPType As VO.DownPayment.Type) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.DPType, A.DPDate, A.PaymentReferencesID,   " & vbNewLine & _
                   "     A.ReferencesNote, A.TotalAmount, A.TotalUsage, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, B.Name AS StatusInfo, A.CreatedBy, A.CreatedDate, " & vbNewLine & _
                   "     A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfActiva, A.BPID2, ISNULL(BP2.Name,'') AS BPName2  " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
                   "INNER JOIN mstStatus B ON " & vbNewLine & _
                   "    A.IDStatus=B.ID " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "LEFT JOIN mstBusinessPartner BP2 ON " & vbNewLine & _
                   "    A.BPID2=BP2.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.DPDate>=@DateFrom AND A.DPDate<=@DateTo " & vbNewLine & _
                   "    AND A.DPType=@DPType " & vbNewLine

                If intIDStatus <> VO.Status.Values.All Then
                    .CommandText += "    AND A.IDStatus=@IDStatus" & vbNewLine
                End If

                .CommandText += "ORDER BY A.DPDate, A.ID ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = intIDStatus
                .Parameters.Add("@DPType", SqlDbType.Int).Value = intDPType
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataSyncJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, A.ProgramID, A.ID, A.BPID, C.Name AS BPName, A.DPType " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
                   "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                   "    A.BPID=C.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine & _
                   "    AND A.DPDate>=@DateFrom AND A.DPDate<=@DateTo " & vbNewLine & _
                   "ORDER BY CONVERT(DATE,A.DPDate), A.ID ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataForLookup(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                 ByVal intBPID As Integer, ByVal intDPType As VO.DownPayment.Type, _
                                                 ByVal intBPID2 As Integer) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     CAST(0 AS BIT) AS Pick, A.ID, A.DPType, A.DPDate, A.TotalAmount-A.TotalUsage AS TotalAmount, A.TotalAmount-A.TotalUsage AS MaxTotalAmount " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND A.BPID=@BPID " & vbNewLine & _
                   "    AND A.DPType=@DPType " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.TotalAmount-A.TotalUsage>0 " & vbNewLine

                If intBPID2 > 0 Then
                    .CommandText += "    AND A.BPID2=@BPID2" & vbNewLine
                End If

                .CommandText += "ORDER BY A.DPDate ASC " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DPType", SqlDbType.Int).Value = intDPType
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@BPID2", SqlDbType.Int).Value = intBPID2
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.DownPayment)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                If bolNew Then
                    .CommandText = _
                       "INSERT INTO traDownPayment " & vbNewLine & _
                       "    (ProgramID, CompanyID, ID, BPID, DPType, DPDate, PaymentReferencesID, ReferencesNote, " & vbNewLine & _
                       "     TotalAmount, Remarks, IDStatus, CreatedBy, CreatedDate, LogBy, LogDate, CoAIDOfActiva, BPID2)   " & vbNewLine & _
                       "VALUES " & vbNewLine & _
                       "    (@ProgramID, @CompanyID, @ID, @BPID, @DPType, @DPDate, @PaymentReferencesID, @ReferencesNote, " & vbNewLine & _
                       "     @TotalAmount, @Remarks, @IDStatus, @LogBy, GETDATE(), @LogBy, GETDATE(), @CoAIDOfActiva, @BPID2)  " & vbNewLine
                Else
                    .CommandText = _
                    "UPDATE traDownPayment SET " & vbNewLine & _
                    "    ProgramID=@ProgramID, " & vbNewLine & _
                    "    CompanyID=@CompanyID, " & vbNewLine & _
                    "    BPID=@BPID, " & vbNewLine & _
                    "    DPDate=@DPDate, " & vbNewLine & _
                    "    PaymentReferencesID=@PaymentReferencesID, " & vbNewLine & _
                    "    ReferencesNote=@ReferencesNote, " & vbNewLine & _
                    "    TotalAmount=@TotalAmount, " & vbNewLine & _
                    "    Remarks=@Remarks, " & vbNewLine & _
                    "    IDStatus=@IDStatus, " & vbNewLine & _
                    "    LogBy=@LogBy, " & vbNewLine & _
                    "    LogDate=GETDATE(), " & vbNewLine & _
                    "    CoAIDOfActiva=@CoAIDOfActiva, " & vbNewLine & _
                    "    BPID2=@BPID2 " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "    ID=@ID " & vbNewLine
                End If

                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = clsData.ProgramID
                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = clsData.CompanyID
                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@BPID", SqlDbType.Int).Value = clsData.BPID
                .Parameters.Add("@DPType", SqlDbType.Int).Value = clsData.DPType
                .Parameters.Add("@DPDate", SqlDbType.DateTime).Value = clsData.DPDate
                .Parameters.Add("@PaymentReferencesID", SqlDbType.Int).Value = clsData.PaymentReferencesID
                .Parameters.Add("@ReferencesNote", SqlDbType.VarChar, 150).Value = clsData.ReferencesNote
                .Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = clsData.TotalAmount
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
                .Parameters.Add("@IDStatus", SqlDbType.Int).Value = clsData.IDStatus
                .Parameters.Add("@LogBy", SqlDbType.VarChar, 20).Value = clsData.LogBy
                .Parameters.Add("@CoAIDOfActiva", SqlDbType.Int).Value = clsData.CoAIDOfActiva
                .Parameters.Add("@BPID2", SqlDbType.Int).Value = clsData.BPID2
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetDetail(ByVal strID As String) As VO.DownPayment
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.DownPayment
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, C.Code AS BPCode, A.BPID, C.Name AS BPName, A.DPType, A.DPDate, A.PaymentReferencesID,   " & vbNewLine & _
                       "    A.ReferencesNote, A.TotalAmount, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfActiva, " & vbNewLine & _
                       "    COA.Code AS CoACodeOfActiva, COA.Name AS CoANameOfActiva, A.BPID2, ISNULL(BP2.Code,'') AS BPCode2, ISNULL(BP2.Name,'') AS BPName2 " & vbNewLine & _
                       "FROM traDownPayment A " & vbNewLine & _
                       "INNER JOIN mstChartOfAccount COA ON " & vbNewLine & _
                       "    A.CoAIDOfActiva=COA.ID " & vbNewLine & _
                       "INNER JOIN mstStatus B ON " & vbNewLine & _
                       "    A.IDStatus=B.ID " & vbNewLine & _
                       "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                       "    A.BPID=C.ID " & vbNewLine & _
                       "INNER JOIN mstCompany MC ON " & vbNewLine & _
                       "    A.CompanyID=MC.ID " & vbNewLine & _
                       "INNER JOIN mstProgram MP ON " & vbNewLine & _
                       "    A.ProgramID=MP.ID " & vbNewLine & _
                       "LEFT JOIN mstBusinessPartner BP2 ON " & vbNewLine & _
                       "    A.BPID2=BP2.ID " & vbNewLine & _
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
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPCode = .Item("BPCode")
                        voReturn.BPName = .Item("BPName")
                        voReturn.DPType = .Item("DPType")
                        voReturn.DPDate = .Item("DPDate")
                        voReturn.PaymentReferencesID = .Item("PaymentReferencesID")
                        voReturn.ReferencesNote = .Item("ReferencesNote")
                        voReturn.TotalAmount = .Item("TotalAmount")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.JournalID = .Item("JournalID")
                        voReturn.CoAIDOfActiva = .Item("CoAIDOfActiva")
                        voReturn.CoACodeOfActiva = .Item("CoACodeOfActiva")
                        voReturn.CoANameOfActiva = .Item("CoANameOfActiva")
                        voReturn.BPID2 = .Item("BPID2")
                        voReturn.BPCode2 = .Item("BPCode2")
                        voReturn.BPName2 = .Item("BPName2")
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

        Public Shared Function GetDetailOverTotalAmount(ByVal strReferenceID As String) As VO.DownPayment
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim voReturn As New VO.DownPayment
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                       "SELECT TOP 1 " & vbNewLine & _
                       "    DH.CompanyID, MC.Name AS CompanyName, DH.ProgramID, MP.Name AS ProgramName, DH.ID, DH.BPID, C.Code AS BPCode, C.Name AS BPName, DH.DPType, DH.DPDate, DH.PaymentReferencesID,   " & vbNewLine & _
                       "    DH.ReferencesNote, DH.TotalAmount, DH.IsPostedGL, DH.PostedBy, DH.PostedDate, DH.IsDeleted, DH.Remarks, DH.IDStatus, DH.LogBy, DH.LogDate, DH.JournalID, " & vbNewLine & _
                       "    DH.CoAIDOfActiva, COA.Code AS CoACodeOfActiva, COA.Name AS CoANameOfActiva, DH.BPID2, ISNULL(BP2.Code,'') AS BPCode2, ISNULL(BP2.Name,'') AS BPName2  " & vbNewLine & _
                       "FROM traDownPaymentDet DD " & vbNewLine & _
                       "INNER JOIN traDownPayment DH ON " & vbNewLine & _
                       "    DD.DPID=DH.ID " & vbNewLine & _
                       "INNER JOIN mstChartOfAccount COA ON " & vbNewLine & _
                       "    DH.CoAIDOfActiva=COA.ID " & vbNewLine & _
                       "INNER JOIN mstStatus B ON " & vbNewLine & _
                       "    DH.IDStatus=B.ID " & vbNewLine & _
                       "INNER JOIN mstBusinessPartner C ON " & vbNewLine & _
                       "    DH.BPID=C.ID " & vbNewLine & _
                       "INNER JOIN mstCompany MC ON " & vbNewLine & _
                       "    DH.CompanyID=MC.ID " & vbNewLine & _
                       "INNER JOIN mstProgram MP ON " & vbNewLine & _
                       "    DH.ProgramID=MP.ID " & vbNewLine & _
                       "LEFT JOIN mstBusinessPartner BP2 ON " & vbNewLine & _
                       "    DH.BPID2=BP2.ID " & vbNewLine & _
                       "WHERE " & vbNewLine & _
                       "   DD.ReferenceID=@ReferenceID " & vbNewLine & _
                       "   AND DH.TotalAmount-TotalUsage<0 " & vbNewLine

                    .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = strReferenceID
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
                        voReturn.BPID = .Item("BPID")
                        voReturn.BPCode = .Item("BPCode")
                        voReturn.BPName = .Item("BPName")
                        voReturn.DPType = .Item("DPType")
                        voReturn.DPDate = .Item("DPDate")
                        voReturn.PaymentReferencesID = .Item("PaymentReferencesID")
                        voReturn.ReferencesNote = .Item("ReferencesNote")
                        voReturn.TotalAmount = .Item("TotalAmount")
                        voReturn.IsPostedGL = .Item("IsPostedGL")
                        voReturn.PostedBy = .Item("PostedBy")
                        voReturn.PostedDate = .Item("PostedDate")
                        voReturn.IsDeleted = .Item("IsDeleted")
                        voReturn.Remarks = .Item("Remarks")
                        voReturn.IDStatus = .Item("IDStatus")
                        voReturn.LogBy = .Item("LogBy")
                        voReturn.LogDate = .Item("LogDate")
                        voReturn.JournalID = .Item("JournalID")
                        voReturn.CoAIDOfActiva = .Item("CoAIDOfActiva")
                        voReturn.CoACodeOfActiva = .Item("CoACodeOfActiva")
                        voReturn.CoANameOfActiva = .Item("CoANameOfActiva")
                        voReturn.BPID2 = .Item("BPID2")
                        voReturn.BPCode2 = .Item("BPCode2")
                        voReturn.BPName2 = .Item("BPName2")
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
                    "UPDATE traDownPayment " & vbNewLine & _
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
                        "FROM traDownPayment " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   LEFT(ID,19)=@ID " & vbNewLine

                    .Parameters.Add("@ID", SqlDbType.VarChar, 19).Value = strID
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
                        "FROM traDownPayment " & vbNewLine & _
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
                        "FROM traDownPayment " & vbNewLine & _
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
                        "FROM traDownPayment " & vbNewLine & _
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
                    "UPDATE traDownPayment " & vbNewLine & _
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

        Public Shared Sub PostGL(ByVal strID As String, ByVal strLogBy As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traDownPayment SET" & vbNewLine & _
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
                                 ByVal strLogBy As String, ByVal intDPType As VO.DownPayment.Type)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traDownPayment SET" & vbNewLine & _
                    "   IsPostedGL=1, " & vbNewLine & _
                    "   PostedBy=@LogBy, " & vbNewLine & _
                    "   PostedDate=GETDATE() " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
                    "   AND DPDate>=@DateFrom AND DPDate<=@DateTo " & vbNewLine & _
                    "   AND IsDeleted=0 " & vbNewLine

                If intDPType <> VO.DownPayment.Type.All Then
                    .CommandText += "    AND DPType=@DPType " & vbNewLine
                End If
                
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
                    "UPDATE traDownPayment SET" & vbNewLine & _
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
                                   ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                   ByVal intDPType As VO.DownPayment.Type)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traDownPayment SET" & vbNewLine & _
                    "   IsPostedGL=0, " & vbNewLine & _
                    "   PostedBy='' " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   CompanyID=@CompanyID " & vbNewLine & _
                    "   AND ProgramID=@ProgramID " & vbNewLine & _
                    "   AND DPDate>=@DateFrom AND DPDate<=@DateTo " & vbNewLine & _
                    "   AND IsDeleted=0 " & vbNewLine

                If intDPType <> VO.DownPayment.Type.All Then
                    .CommandText += "    AND DPType=@DPType " & vbNewLine
                End If

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

        Public Shared Function ListDataGenerateJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                       ByVal strReferencesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT DISTINCT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.DPType, A.DPDate, A.PaymentReferencesID,   " & vbNewLine & _
                   "    A.ReferencesNote, A.TotalAmount, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, " & vbNewLine & _
                   "    A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfActiva  " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
                   "INNER JOIN traDownPaymentDet DPD ON " & vbNewLine & _
                   "    A.ID=DPD.DPID " & vbNewLine & _
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
                   "    AND DPD.ReferenceID=@ReferenceID " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = strReferencesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataReCalculateJournal(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                          ByVal strID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT DISTINCT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.DPType, A.DPDate, A.PaymentReferencesID,   " & vbNewLine & _
                   "    A.ReferencesNote, A.TotalAmount, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, " & vbNewLine & _
                   "    A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfActiva  " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
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

        Public Shared Function ListDataUsed(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                            ByVal strReferencesID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT DISTINCT " & vbNewLine & _
                   "    A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.JournalID " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
                   "INNER JOIN traDownPaymentDet DPD ON " & vbNewLine & _
                   "    A.ID=DPD.DPID " & vbNewLine & _
                   "INNER JOIN mstCompany MC ON " & vbNewLine & _
                   "    A.CompanyID=MC.ID " & vbNewLine & _
                   "INNER JOIN mstProgram MP ON " & vbNewLine & _
                   "    A.ProgramID=MP.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.CompanyID=@CompanyID " & vbNewLine & _
                   "    AND A.ProgramID=@ProgramID " & vbNewLine & _
                   "    AND DPD.ReferenceID=@ReferenceID " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = strReferencesID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataOutstandingPostGL(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.CompanyID, MC.Name AS CompanyName, A.ProgramID, MP.Name AS ProgramName, A.ID, A.BPID, C.Name AS BPName, A.DPType, A.DPDate, A.PaymentReferencesID,   " & vbNewLine & _
                   "     A.ReferencesNote, A.TotalAmount, A.IsPostedGL, A.PostedBy, A.PostedDate, A.IsDeleted, A.Remarks, A.IDStatus, A.CreatedBy, A.CreatedDate, " & vbNewLine & _
                   "     A.LogInc, A.LogBy, A.LogDate, A.JournalID, A.CoAIDOfActiva  " & vbNewLine & _
                   "FROM traDownPayment A " & vbNewLine & _
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
                   "    AND A.DPDate>=@DateFrom AND A.DPDate<=@DateTo " & vbNewLine & _
                   "    AND A.IsDeleted=0 " & vbNewLine & _
                   "    AND A.IsPostedGL=0 " & vbNewLine

                .Parameters.Add("@CompanyID", SqlDbType.Int).Value = intCompanyID
                .Parameters.Add("@ProgramID", SqlDbType.Int).Value = intProgramID
                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub UpdateTotalUsage(ByVal strID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "UPDATE traDownPayment 	" & vbNewLine & _
                    "SET TotalUsage= 	" & vbNewLine & _
                    "	ISNULL(	" & vbNewLine & _
                    "		(SELECT 	" & vbNewLine & _
                    "			SUM(DD.TotalAmount) TotalAmount 	" & vbNewLine & _
                    "		FROM traDownPaymentDet DD	" & vbNewLine & _
                    "		WHERE 	" & vbNewLine & _
                    "			DD.DPID=@ID" & vbNewLine & _
                    "		)	" & vbNewLine & _
                    "	,0)	" & vbNewLine & _
                    "WHERE ID=@ID " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = strID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function ListDataHistoryItem(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intDPType As VO.DownPayment.Type) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "    DPH.CompanyID, CASE WHEN DPType=1 THEN 'PANJAR PEMBELIAN' ELSE 'PANJAR PENJUALAN' END AS Trans, DPH.ID, DPH.DPDate AS TransactionDate, 0 AS ItemID, '-' AS ItemCode, '-' AS ItemName, " & vbNewLine & _
                   "    CAST(0 AS DECIMAL(18,2)) AS ArrivalBrutto, CAST(0 AS DECIMAL(18,2)) AS ArrivalTarra, CAST(0 AS DECIMAL(18,2)) AS ArrivalNettoBefore, CAST(0 AS DECIMAL(18,2)) AS ArrivalDeduction, " & vbNewLine & _
                   "    CAST(0 AS DECIMAL(18,2)) AS ArrivalNettoAfter, '-' AS UomID, '-' AS UomCode, DPH.TotalAmount AS Price, " & vbNewLine & _
                   "    DPH.TotalAmount AS TotalPrice, DPH.Remarks, DPH.IDStatus, MS.Name AS StatusInfo, DPH.CreatedBy, DPH.CreatedDate, DPH.LogInc, DPH.LogBy, DPH.LogDate, DPH.JournalID  " & vbNewLine & _
                   "FROM traDownPayment DPH " & vbNewLine & _
                   "INNER JOIN mstStatus MS ON " & vbNewLine & _
                   "    DPH.IDStatus=MS.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    DPH.DPDate>=@DateFrom AND DPH.DPDate<=@DateTo " & vbNewLine & _
                   "    AND DPH.IsDeleted=0 " & vbNewLine & _
                   "    AND DPH.BPID=@BPID " & vbNewLine

                If intDPType <> VO.DownPayment.Type.All Then
                    .CommandText += _
                   "    AND DPH.DPType=@DPType " & vbNewLine
                End If

                .Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = dtmDateFrom
                .Parameters.Add("@DateTo", SqlDbType.DateTime).Value = dtmDateTo
                .Parameters.Add("@BPID", SqlDbType.Int).Value = intBPID
                .Parameters.Add("@DPType", SqlDbType.Int).Value = intDPType
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

#End Region

#Region "Detail"

        Public Shared Function ListDataDetailByDPID(ByVal strDPID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.DPID, A.ReferenceID, A.TotalAmount, A.Remarks  " & vbNewLine & _
                   "FROM traDownPaymentDet A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.DPID=@DPID" & vbNewLine

                .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataReferencesIDByDPID(ByVal strDPID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT DISTINCT " & vbNewLine & _
                   "    A.ReferenceID, B.DPType " & vbNewLine & _
                   "FROM traDownPaymentDet A " & vbNewLine & _
                   "INNER JOIN traDownPayment B ON " & vbNewLine & _
                   "    A.DPID=B.ID " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.DPID=@DPID" & vbNewLine

                .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Function ListDataDetailByReferenceID(ByVal strReferenceID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.DPID, A.ReferenceID, A.TotalAmount, A.Remarks  " & vbNewLine & _
                   "FROM traDownPaymentDet A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.ReferenceID=@ReferenceID" & vbNewLine

                .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = strReferenceID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataDetail(ByVal clsData As VO.DownPaymentDet)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traDownPaymentDet " & vbNewLine & _
                   "    (ID, DPID, ReferenceID, TotalAmount, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @DPID, @ReferenceID, @TotalAmount, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = clsData.DPID
                .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = clsData.ReferenceID
                .Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = clsData.TotalAmount
                .Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = clsData.Remarks
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataByDPID(ByVal strDPID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traDownPaymentDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   DPID=@DPID " & vbNewLine

                .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Sub DeleteDataByReferenceID(ByVal strReferenceID As String)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                    "DELETE FROM traDownPaymentDet " & vbNewLine & _
                    "WHERE " & vbNewLine & _
                    "   ReferenceID=@ReferenceID " & vbNewLine

                .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = strReferenceID
            End With
            Try
                SQL.ExecuteNonQuery(sqlcmdExecute)
            Catch ex As SqlException
                Throw ex
            End Try
        End Sub

        Public Shared Function GetMaxIDDetail(ByVal strDPID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traDownPaymentDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   DPID=@DPID " & vbNewLine

                    .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID

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

        Public Shared Function IsOverDownPayment(ByVal strReferenceID As String) As Boolean
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim bolExists As Boolean = False
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID " & vbNewLine & _
                        "FROM traDownPaymentDet DD " & vbNewLine & _
                        "INNER JOIN traDownPayment DH ON " & vbNewLine & _
                        "   DD.DPID=DH.ID " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   DD.ReferenceID=@ReferenceID " & vbNewLine & _
                        "   AND DH.TotalAmount-TotalUsage<0 " & vbNewLine

                    .Parameters.Add("@ReferenceID", SqlDbType.VarChar, 30).Value = strReferenceID
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

        Public Shared Function GetReferenceID(ByVal strDPID As String) As String
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim strReturn As String = ""
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ReferenceID=ISNULL(ReferenceID,'') " & vbNewLine & _
                        "FROM traDownPaymentDet " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   DPID=@DPID " & vbNewLine

                    .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID
                    If SQL.bolUseTrans Then .Transaction = SQL.sqlTrans
                End With
                sqlrdData = sqlcmdExecute.ExecuteReader(CommandBehavior.SingleRow)
                With sqlrdData
                    If .HasRows Then
                        .Read()
                        strReturn = .Item("ReferenceID")
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

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strDPID As String) As DataTable
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "SELECT " & vbNewLine & _
                   "     A.ID, A.DPID, A.Status, A.StatusBy, A.StatusDate, A.Remarks  " & vbNewLine & _
                   "FROM traDownPaymentStatus A " & vbNewLine & _
                   "WHERE  " & vbNewLine & _
                   "    A.DPID=@DPID " & vbNewLine

                .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID
            End With
            Return SQL.QueryDataTable(sqlcmdExecute)
        End Function

        Public Shared Sub SaveDataStatus(ByVal clsData As VO.DownPaymentStatus)
            Dim sqlcmdExecute As New SqlCommand
            With sqlcmdExecute
                .CommandText = _
                   "INSERT INTO traDownPaymentStatus " & vbNewLine & _
                   "    (ID, DPID, Status, StatusBy, StatusDate, Remarks)   " & vbNewLine & _
                   "VALUES " & vbNewLine & _
                   "    (@ID, @DPID, @Status, @StatusBy, @StatusDate, @Remarks)  " & vbNewLine

                .Parameters.Add("@ID", SqlDbType.VarChar, 30).Value = clsData.ID
                .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = clsData.DPID
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

        Public Shared Function GetMaxIDStatus(ByVal strDPID As String) As Integer
            Dim sqlcmdExecute As New SqlCommand, sqlrdData As SqlDataReader = Nothing
            Dim intReturn As Integer = 1
            Try
                If Not SQL.bolUseTrans Then SQL.OpenConnection()
                With sqlcmdExecute
                    .Connection = SQL.sqlConn
                    .CommandText = _
                        "SELECT TOP 1 " & vbNewLine & _
                        "   ID=ISNULL(RIGHT(MAX(ID),3),0) " & vbNewLine & _
                        "FROM traDownPaymentStatus " & vbNewLine & _
                        "WHERE  " & vbNewLine & _
                        "   DPID=@DPID " & vbNewLine

                    .Parameters.Add("@DPID", SqlDbType.VarChar, 30).Value = strDPID

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