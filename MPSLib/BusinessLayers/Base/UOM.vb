Namespace BL
    Public Class UOM
        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.UOM.ListData
        End Function

        Public Shared Function ListDataForCombo() As DataTable
            BL.Server.ServerDefault()
            Return DL.UOM.ListDataForCombo
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.UOM.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.UOM) As VO.UOM
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.UOM.GetMaxID
                    If DL.UOM.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                DL.UOM.SaveData(bolNew, clsData)

                DL.SQL.CommitTransaction()
            Catch ex As Exception
                DL.SQL.RollBackTransaction()
                Throw ex
            Finally
                DL.SQL.CloseConnection()
            End Try
            Return clsData
        End Function

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.UOM)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.UOM.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.UOM
            BL.Server.ServerDefault() 
            Return DL.UOM.GetDetail(intID)
        End Function 

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault() 
            Try
                DL.SQL.OpenConnection() 
                DL.SQL.BeginTransaction() 
                
                If DL.UOM.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.UOM.DeleteData(intID)
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
            DL.UOM.DeleteDataAll()
        End Sub

    End Class 

End Namespace

