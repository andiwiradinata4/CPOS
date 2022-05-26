Public Class frmRptFakturBiayaVer00

#Region "Property"

    Private frmParent As frmTraCostDet
    Private bolExport As Boolean = False
    Property pubID As String

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Sub prvPrint()
        btnPrint.Focus()
        Try
            Dim crReport As Object
            If rdFakturPenerimaan.Checked Then
                crReport = New rptFakturBiayaPenerimaanVer00
            Else
                crReport = New rptFakturBiayaPembayaranVer00
            End If
            With crReport
                .DataSource = BL.Cost.PrintFakturBiaya(pubID)
                .CreateDocument(True)
                .ShowPreviewMarginLines = False
                .ShowPrintMarginsWarning = False
            End With

            Dim frmDetail As New frmReportPreview
            With frmDetail
                .docViewer.DocumentSource = crReport
                .pgExportButton.Enabled = bolExport
                .Text = Me.Text & " - " & VO.Reports.PrintOut
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#Region "Form Handle"

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        prvPrint()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

#End Region
    
End Class