Namespace BL
    Public Class StockIN

        Public Shared Function ListDataAll() As DataTable
            BL.Server.ServerDefault()
            Return DL.StockIN.ListDataAll
        End Function

        Public Shared Sub SaveDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String, _
                                      ByVal clsData As VO.StockIN)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.StockIN.SaveDataAll(clsData)
        End Sub

        Public Shared Sub DeleteDataAll(ByVal strServer As String, ByVal strDBMS As String, ByVal strUserID As String, ByVal strPassword As String)
            BL.Server.SetServer(strServer, strDBMS, strUserID, strPassword)
            DL.StockIN.DeleteDataAll()
        End Sub

    End Class
End Namespace

