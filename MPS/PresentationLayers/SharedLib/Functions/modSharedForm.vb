Module modSharedForm

    Public Function ShowSplitReceive(ByVal strID As String, ByVal clsCS As VO.CS) As Boolean
        Dim bolReturn As Boolean = False
        Dim frmDetail As New frmTraSalesSplitReceive
        With frmDetail
            .pubCS = clsCS
            .pubSalesID = strID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            bolReturn = .pubIsSave
        End With
        Return bolReturn
    End Function

End Module
