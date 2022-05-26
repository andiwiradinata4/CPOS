Namespace BL
    Public Class PaymentReferences
        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.PaymentReferences.ListData
        End Function

        Public Shared Function ListDataForCombo() As DataTable
            BL.Server.ServerDefault()
            Return DL.PaymentReferences.ListDataForCombo
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.PaymentReferences.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal bolNew as Boolean, ByVal clsData As VO.PaymentReferences) As Integer
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 

                If bolNew Then 
                    clsData.ID = DL.PaymentReferences.GetMaxID
                    If DL.PaymentReferences.DataExists(clsData.ID) Then 
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If 
                End If 

                DL.PaymentReferences.SaveData(bolNew, clsData) 

                DL.SQL.CommitTransaction() 
            Catch ex As Exception
                DL.SQL.RollBackTransaction() 
                Throw ex 
            Finally 
                DL.SQL.CloseConnection() 
            End Try
            Return clsData.ID
        End Function

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.PaymentReferences)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.PaymentReferences.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.PaymentReferences
            BL.Server.ServerDefault() 
            Return DL.PaymentReferences.GetDetail(intID)
        End Function 

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 

                If DL.PaymentReferences.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.PaymentReferences.DeleteData(intID)
                End If

                DL.SQL.CommitTransaction() 
            Catch ex As Exception
                DL.SQL.RollBackTransaction() 
                Throw ex 
            Finally 
                DL.SQL.CloseConnection() 
            End Try
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.PaymentReferences.DeleteDataAll()
        End Sub

    End Class 

End Namespace

