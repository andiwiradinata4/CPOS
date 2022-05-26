Namespace BL
    Public Class StockOut

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.StockOut.ListDataAll
        End Function

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.StockOut)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.StockOut.SaveDataAll(clsData)
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.StockOut.DeleteDataAll()
        End Sub

    End Class
End Namespace