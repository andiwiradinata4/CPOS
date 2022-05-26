Public Class frmRptNeracaVer00

#Region "Property"

    Private bolExport As Boolean = False
    Private Const _
       cPreview = 0, cClose = 1

#End Region


    Private Sub prvSetProgressBar(ByVal intMax As Integer)
        pgMain.Value = 0
        pgMain.Maximum = intMax
    End Sub

    Private Sub prvRefreshProgressBar()
        pgMain.Value += 1
        Me.Refresh()
    End Sub
    
    Private Function prvCollectDataCOAPerTypeVer00(ByVal intCOAType As Integer) As DataTable
        Dim dtReturn As New DataTable
        Try
            dtReturn = BL.Reports.ListDataPerCOATypeBaseOnChartOfAccountReport(dtpDateTo.Value.Date, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, intCOAType)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, "Collect COA Type" & intCOAType)
        End Try
        Return dtReturn
    End Function

    Private Sub prvPreview()
        ToolBar.Focus()

        prvSetProgressBar(5)
        Me.Cursor = Cursors.WaitCursor
        Dim dtAktivaLancar As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.AktivaLancar)
        prvRefreshProgressBar()

        Dim dtAktivaTetap As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.AktivaTetap)
        prvRefreshProgressBar()

        Dim dtPasiva As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Pasiva)
        prvRefreshProgressBar()

        Dim dtEquity As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Modal)
        prvRefreshProgressBar()

        Dim decProfitAndLoss As Decimal = BL.Reports.CalculateProfitAndLoss("1900/01/01", dtpDateTo.Value.Date)
        prvRefreshProgressBar()

        Dim decTotalAktiva As Decimal = 0, decTotalPasivaDanEquity As Decimal = 0, decTotalLabaRugi As Decimal = 0

        '# Calculate Aktiva
        If dtAktivaLancar.Rows.Count > 0 Then prvSetProgressBar(dtAktivaLancar.Rows.Count)
        For Each dr As DataRow In dtAktivaLancar.Rows
            decTotalAktiva += dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next

        If dtAktivaTetap.Rows.Count > 0 Then prvSetProgressBar(dtAktivaTetap.Rows.Count)
        For Each dr As DataRow In dtAktivaTetap.Rows
            decTotalAktiva += dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next
        '# End of Calculate Aktiva

        '# Calculate Pasiva dan Equity
        If dtPasiva.Rows.Count > 0 Then prvSetProgressBar(dtPasiva.Rows.Count)
        For Each dr As DataRow In dtPasiva.Rows
            decTotalPasivaDanEquity += dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next

        If dtEquity.Rows.Count > 0 Then prvSetProgressBar(dtEquity.Rows.Count)
        For Each dr As DataRow In dtEquity.Rows
            If dr.Item("COAID") = 50 Then
                decTotalPasivaDanEquity += decProfitAndLoss
                dr.BeginEdit()
                dr.Item("TotalAmount") = decProfitAndLoss
                dr.EndEdit()
            Else
                decTotalPasivaDanEquity += dr.Item("TotalAmount")
            End If
            prvRefreshProgressBar()
        Next
        dtEquity.AcceptChanges()
        '# End of Pasiva dan Equity

        Try
            Dim crReport As New rptNeracaVer00
            With crReport
                .DataSource = New DataTable
                .Parameters.Item("FilterPeriod").Value = dtpDateTo.Value.Date
                .Parameters.Item("CompanyName").Value = MPSLib.UI.usUserApp.CompanyName
                .Parameters.Item("paramTotalAktiva").Value = decTotalAktiva
                .Parameters.Item("paramTotalPasivaDanEquity").Value = decTotalPasivaDanEquity
                .srAktivaLancar.ReportSource.DataSource = dtAktivaLancar
                .srAktivaTetap.ReportSource.DataSource = dtAktivaTetap
                .srPasiva.ReportSource.DataSource = dtPasiva
                .srEquity.ReportSource.DataSource = dtEquity
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

    Private Sub frmRptNeracaVer00_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        dtpDateTo.Value = Today.Date
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBalanceSheet, VO.Access.Values.ExportReportAccess)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Lihat Laporan" : prvPreview()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

#End Region

End Class