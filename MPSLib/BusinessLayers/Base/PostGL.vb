Namespace BL
    Public Class PostGL

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.PostGL.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.PostGL.ListDataAll
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                         ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Return clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-" & Format(dtmDateFrom, "yyMMdd") & "-" & Format(dtmDateTo, "yyMMdd")
        End Function

        Public Shared Function PostData(ByVal clsData As VO.PostGL) As Boolean
            Dim bolReturn As Boolean = False
            clsData.DateTo = clsData.DateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If MPSLib.UI.usUserApp.JournalPost.LogBy Is Nothing Then
                    Err.Raise(515, "", "Mohon agar Setup Posting Jurnal Transaksi terlebih dahulu melalui menu Setting -> Setup Posting Jurnal Transaksi")
                End If

                If Format(clsData.DateTo, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Periode posting harus lebih besar dari tanggal terakhir posting")
                End If

                clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                If DL.PostGL.DataExists(clsData.ID) Then
                    Err.Raise(515, "", "ID telah ada sebelumnya")
                End If

                DL.DownPayment.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy, VO.DownPayment.Type.All)
                DL.Receive.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)
                DL.AccountPayable.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)

                DL.Sales.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)
                DL.SalesService.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)
                DL.AccountReceivable.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)

                DL.Cost.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)
                DL.Journal.PostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, clsData.LogBy)

                DL.PostGL.SaveData(clsData)

                DL.SQL.CommitTransaction()
                bolReturn = True
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolReturn
        End Function

        Public Shared Function UnpostData(ByVal clsData As VO.PostGL) As Boolean
            Dim bolReturn As Boolean = False
            clsData.DateTo = clsData.DateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If Format(clsData.DateTo, "yyyyMMdd") < DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                    Err.Raise(515, "", "Periode posting harus mulai dari tanggal terakhir posting")
                End If

                DL.DownPayment.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo, VO.DownPayment.Type.All)
                DL.Receive.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                DL.AccountPayable.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)

                DL.Sales.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                DL.SalesService.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                DL.AccountReceivable.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)

                DL.Cost.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)
                DL.Journal.UnpostGL(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)

                DL.BukuBesar.DeleteData(clsData.CompanyID, clsData.ProgramID, clsData.DateFrom, clsData.DateTo)

                DL.PostGL.DeleteData(clsData.ID)

                DL.SQL.CommitTransaction()
                bolReturn = True
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return bolReturn
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.PostGL
            BL.Server.ServerDefault()
            Return DL.PostGL.GetDetail(strID)
        End Function

        Public Shared Function GetDetailLast(ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As VO.PostGL
            BL.Server.ServerDefault()
            Return DL.PostGL.GetDetailLast(intCompanyID, intProgramID)
        End Function

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.PostGL)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.PostGL.SaveDataAll(clsData)
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.PostGL.DeleteDataAll()
        End Sub

        Public Shared Function GetAllTransactionForGenerateJournal() As DataTable
            BL.Server.ServerDefault()
            Return DL.PostGL.ListDataAll
        End Function

    End Class

End Namespace

