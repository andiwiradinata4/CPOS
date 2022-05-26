Namespace BL
    Public Class BukuBesar
        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        Optional ByVal intCOAIDParent As Integer = 0) As DataTable
            BL.Server.ServerDefault()
            Return DL.BukuBesar.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intCOAIDParent)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "BB" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.BukuBesar.GetMaxID(strReturn), "00000")
            Return strReturn
        End Function

        'Public Shared Function SaveDataDefault(ByVal clsData As VO.BukuBesar) As String
        '    BL.Server.ServerDefault()
        '    Try
        '        DL.SQL.OpenConnection()
        '        DL.SQL.BeginTransaction()

        '        SaveData(clsData)

        '        DL.SQL.CommitTransaction()
        '    Catch ex As Exception
        '        DL.SQL.RollBackTransaction()
        '        Throw ex
        '    Finally
        '        DL.SQL.CloseConnection()
        '    End Try
        '    Return clsData.ID
        'End Function

        Public Shared Function SaveData(ByVal clsData As VO.BukuBesar) As String
            clsData.ID = GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.TransactionDate)
            If Format(clsData.TransactionDate, "yyyyMMdd") <= DL.PostGL.LastPostedDate(clsData.CompanyID, clsData.ProgramID) Then
                Err.Raise(515, "", "Data tidak dapat disimpan. Dikarenakan tanggal transaksi lebih kecil atau sama dengan tanggal Posting Transaksi")
            End If

            DL.BukuBesar.SaveData(clsData)

            Return clsData.ID
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.BukuBesar)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                DL.BukuBesar.DeleteData(clsData.ID)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

    End Class

End Namespace

