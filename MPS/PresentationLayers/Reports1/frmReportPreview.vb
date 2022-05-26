Public Class frmReportPreview

    Private Sub frmReportPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        Me.WindowState = FormWindowState.Maximized
    End Sub

End Class