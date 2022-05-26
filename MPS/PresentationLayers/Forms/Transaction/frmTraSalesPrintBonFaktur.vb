Public Class frmTraSalesPrintBonFaktur

    Public Property pubData As VO.Sales
    Public Property pubIsPrint As Boolean = False

    Private Sub prvPrint()
        UI.usForm.PrintBonFaktur(Me, pubData.ID)
        Try
            BL.Sales.PrintFakturPenjualan(pubData)
            pubIsPrint = True
            Me.Close()
        Catch ex As Exception
            pubIsPrint = False
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmTraSalesPrintBonFaktur_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraSalesPrintBonFaktur_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        prvPrint()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region

End Class