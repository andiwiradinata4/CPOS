Namespace BL
    Public Class SalesDiscount
        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesDiscount.ListData()
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.SalesDiscount.ListDataAll()
        End Function

        Public Shared Function SaveData(ByVal bolNew as Boolean, ByVal clsData As VO.SalesDiscount) As Integer
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 

                If bolNew Then 
                    clsData.ID = DL.SalesDiscount.GetMaxID()
                    If DL.SalesDiscount.DataExists(clsData.ID) Then 
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If 
                End If 

                DL.SalesDiscount.SaveData(bolNew, clsData) 

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
                                      ByVal clsData As VO.SalesDiscount)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.SalesDiscount.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.SalesDiscount
            BL.Server.ServerDefault() 
            Return DL.SalesDiscount.GetDetail(intID)
        End Function 

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 

                If DL.SalesDiscount.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.SalesDiscount.DeleteData(intID)
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
            DL.SalesDiscount.DeleteDataAll()
        End Sub

    End Class 

End Namespace

