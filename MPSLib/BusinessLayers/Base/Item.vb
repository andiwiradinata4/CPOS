Namespace BL
    Public Class Item

        Public Shared Function ListData() As DataTable
            BL.Server.ServerDefault()
            Return DL.Item.ListData()
        End Function

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.Item.ListDataAll
        End Function

        Public Shared Function SaveData(ByVal bolNew As Boolean, ByVal clsData As VO.Item) As Integer
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If bolNew Then
                    clsData.ID = DL.Item.GetMaxID()
                    clsData.Code = Format(clsData.ID, "000")
                    If DL.Item.DataExists(clsData.ID) Then
                        Err.Raise(515, "", "ID sudah ada sebelumnya")
                    End If
                End If

                DL.Item.SaveData(bolNew, clsData)

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
                                           ByVal clsData As VO.Item)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.Item.SaveDataAll(clsData)
        End Sub

        Public Shared Function GetDetail(ByVal intID As Integer) As VO.Item
            BL.Server.ServerDefault()
            Return DL.Item.GetDetail(intID)
        End Function

        Public Shared Function GetDetail(ByVal strItemCode As String) As VO.Item
            BL.Server.ServerDefault()
            Return DL.Item.GetDetail(strItemCode)
        End Function

        Public Shared Sub DeleteData(ByVal intID As Integer)
            BL.Server.ServerDefault()
            Try
                DL.SQL.OpenConnection()
                DL.SQL.BeginTransaction()

                If DL.Item.GetIDStatus(intID) = VO.Status.Values.InActive Then
                    Err.Raise(515, "", "Data tidak dapat dihapus. Dikarenakan data telah tidak aktif")
                Else
                    DL.Item.DeleteData(intID)
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
            DL.Item.DeleteDataAll()
        End Sub

    End Class

End Namespace

