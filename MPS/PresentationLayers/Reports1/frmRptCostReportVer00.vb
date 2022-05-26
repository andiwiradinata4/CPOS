Public Class frmRptCostReportVer00

    Private bolExport As Boolean = False
    Private intCompanyID As Integer = 0
    Private intProgramID As Integer = 0

    Private Sub prvPreview()
        ToolBar.Focus()
        If dtpDateFrom.Value.Date > dtpDateTo.Value.Date Then
            UI.usForm.frmMessageBox("Period salah")
            dtpDateFrom.Focus()
            Exit Sub
        End If

        Dim strFilterDate As String = Format(dtpDateFrom.Value, "dd MMMM yyyy") & " - " & Format(dtpDateTo.Value, "dd MMMM yyyy")

        Me.Cursor = Cursors.WaitCursor
        Try
            Dim crReport As New rptCostReportVer00
            crReport.DataSource = BL.Reports.CostReport(dtpDateFrom.Value.Date, dtpDateTo.Value.Date, intCompanyID, intProgramID)
            With crReport
                .Parameters.Item("FilterDate").Value = strFilterDate
                .CreateDocument(True)
                .ShowPreviewMarginLines = False
                .ShowPrintMarginsWarning = False
            End With

            Dim frmDetail As New frmReportPreview
            With frmDetail
                .docViewer.DocumentSource = crReport
                .pgExportButton.Enabled = bolExport
                .Text = Me.Text
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub prvChooseProgram()
        Dim frmDetail As New frmViewProgram
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intProgramID = .pubLUdtRow.Item("ProgramID")
                txtProgramName.Text = .pubLUdtRow.Item("ProgramName")
            End If
        End With
    End Sub

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
            End If
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmRptCostReportVer00_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmRptCostReportVer00_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        dtpDateFrom.Value = Today.Date.AddDays(-7)
        dtpDateTo.Value = Today.Date
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.ExportReportAccess)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text
            Case "Lihat Laporan" : prvPreview()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnProgram_Click(sender As Object, e As EventArgs) Handles btnProgram.Click
        prvChooseProgram()
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

    Private Sub txtProgramName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProgramName.KeyDown
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
            txtProgramName.Text = ""
            intProgramID = 0
        End If
    End Sub

    Private Sub txtCompanyName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCompanyName.KeyDown
        If e.KeyCode = Keys.Back Or e.KeyCode = Keys.Delete Then
            txtCompanyName.Text = ""
            intCompanyID = 0
        End If
    End Sub

#End Region

End Class