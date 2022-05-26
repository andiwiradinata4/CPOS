Namespace BL
    Public Class Calculator

#Region "Main"

        Public Shared Function ListData(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, _
                                        ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                        ByVal intIDStatus As Integer, ByVal strCustomerCode As String) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Calculator.ListData(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, intIDStatus, strCustomerCode)
        End Function

        Public Shared Function ListDataStruk(ByVal strID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Calculator.ListDataStruk(strID)
        End Function

        Private Shared Function GetNewID(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SO" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Calculator.GetMaxID(strReturn), "000")
            Return strReturn
        End Function

        Private Shared Function GetNewTransactionNo(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDate As DateTime)
            Dim clsCompany As VO.Company = DL.Company.GetDetail(intCompanyID)
            Dim strReturn As String = "SO" & Format(dtmDate, "yyMMdd") & "-" & clsCompany.CompanyInitial & "-" & Format(intProgramID, "00") & "-"
            strReturn = strReturn & Format(DL.Calculator.GetMaxTransactionNo(strReturn), "000")
            Return strReturn
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Calculator, ByVal clsDataDetail() As VO.CalculatorDet) As String
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = BL.Calculator.GetNewID(clsData.CompanyID, clsData.ProgramID, clsData.TransactionDate)
                    If clsData.TransactionNo.Trim = "" Then clsData.TransactionNo = GetNewTransactionNo(clsData.CompanyID, clsData.ProgramID, clsData.TransactionDate)
                    If DL.Calculator.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                Else
                    DL.Calculator.DeleteDataDetail(clsData.ID)
                End If

                DL.Calculator.SaveData(bolNew, clsData)

                '# Save Data Detail
                For Each clsDetail As VO.CalculatorDet In clsDataDetail
                    clsDetail.ID = clsData.ID & "-" & Format(DL.Calculator.GetMaxIDDetail(clsData.ID), "000")
                    clsDetail.CalculatorID = clsData.ID
                    DL.Calculator.SaveDataDetail(clsDetail)
                Next

                '# Save Data Status
                BL.Calculator.SaveDataStatus(clsData.ID, IIf(bolNew, "BARU", "EDIT"), clsData.LogBy, clsData.Remarks)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData.ID
        End Function

        Public Shared Function GetDetail(ByVal strID As String) As VO.Calculator
            BL.Server.ServerDefault()
            Return DL.Calculator.GetDetail(strID)
        End Function

        Public Shared Sub DeleteData(ByVal clsData As VO.Calculator)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.Calculator.IsDeleted(clsData.ID) Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah dihapus sebelumnya")
                Else
                    '# Delete Data
                    DL.Calculator.DeleteData(clsData.ID)

                    '# Save Data Status
                    SaveDataStatus(clsData.ID, "DIHAPUS", clsData.LogBy, clsData.Remarks)
                End If

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
        End Sub

#End Region

#Region "Detail"

        Public Shared Function ListDataDetail(ByVal strCalculatorID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Calculator.ListDataDetail(strCalculatorID)
        End Function

#End Region

#Region "Status"

        Public Shared Function ListDataStatus(ByVal strCalculatorID As String) As DataTable
            BL.Server.ServerDefault()
            Return DL.Calculator.ListDataStatus(strCalculatorID)
        End Function

        Public Shared Sub SaveDataStatus(ByVal strCalculatorID As String, ByVal strStatus As String, ByVal strStatusBy As String, _
                                         ByVal strRemarks As String)
            Dim clsData As New VO.CalculatorStatus
            clsData.ID = strCalculatorID & "-" & Format(DL.Calculator.GetMaxIDStatus(strCalculatorID), "000")
            clsData.CalculatorID = strCalculatorID
            clsData.Status = strStatus
            clsData.StatusBy = strStatusBy
            clsData.StatusDate = Now
            clsData.Remarks = strRemarks
            DL.Calculator.SaveDataStatus(clsData)
        End Sub

#End Region

    End Class

End Namespace

