Public Class frmRptLabaRugiVer00

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

    Private Function prvCollectDataCOAPerTypeVer00(ByVal intCOAType As Integer, Optional ByVal intCOAGroupID As Integer = 0) As DataTable
        Dim dtReturn As New DataTable
        Try
            dtReturn = BL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtpDateFrom.Value.Date, dtpDateTo.Value.Date, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, intCOAType, intCOAGroupID)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, "Collect COA Type" & intCOAType)
        End Try
        Return dtReturn
    End Function

    Private Sub prvPreview()
        ToolBar.Focus()
        If dtpDateFrom.Value.Date > dtpDateTo.Value.Date Then
            UI.usForm.frmMessageBox("Period salah")
            dtpDateFrom.Focus()
            Exit Sub
        End If

        prvSetProgressBar(6)

        Dim strFilterDate As String = Format(dtpDateFrom.Value, "dd-MMM-yyyy") & " - " & Format(dtpDateTo.Value, "dd-MMM-yyyy")

        Me.Cursor = Cursors.WaitCursor
        Dim dtSalesAndRevenue As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Pendapatan, VO.ChartOfAccountGroup.Values.PendapatanDanPenjualan)
        prvRefreshProgressBar()

        Dim dtCOGS As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Beban, VO.ChartOfAccountGroup.Values.BebanHPP)
        prvRefreshProgressBar()

        Dim dtOperationalExpenses As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Beban, VO.ChartOfAccountGroup.Values.BebanOperasional)
        prvRefreshProgressBar()

        Dim dtOthersRevenue As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Pendapatan, VO.ChartOfAccountGroup.Values.PendapatanLainLain)
        prvRefreshProgressBar()

        Dim dtOthersExpenses As DataTable = prvCollectDataCOAPerTypeVer00(VO.ChartOfAccountType.Values.Beban, VO.ChartOfAccountGroup.Values.BebanLainnya)
        prvRefreshProgressBar()

        Dim decLabaKotor As Decimal = 0, decLabaUsaha As Decimal = 0, decLabaRugiBersih As Decimal = 0

        '# Calculate Laba Kotor
        If dtSalesAndRevenue.Rows.Count > 0 Then prvSetProgressBar(dtSalesAndRevenue.Rows.Count)
        For Each dr As DataRow In dtSalesAndRevenue.Rows
            decLabaKotor += dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next

        If dtCOGS.Rows.Count > 0 Then prvSetProgressBar(dtCOGS.Rows.Count)
        For Each dr As DataRow In dtCOGS.Rows
            decLabaKotor -= dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next
        '# End of Calculate Laba Kotor

        '# Calculate Laba Usaha
        decLabaUsaha = decLabaKotor
        If dtOperationalExpenses.Rows.Count > 0 Then prvSetProgressBar(dtOperationalExpenses.Rows.Count)
        For Each dr As DataRow In dtOperationalExpenses.Rows
            decLabaUsaha -= dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next
        '# End of Calculate Laba Usaha

        '# Calculate Laba / Rugi Bersih
        decLabaRugiBersih = decLabaUsaha
        If dtOthersRevenue.Rows.Count > 0 Then prvSetProgressBar(dtOthersRevenue.Rows.Count)
        For Each dr As DataRow In dtOthersRevenue.Rows
            decLabaRugiBersih += dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next

        If dtOthersExpenses.Rows.Count > 0 Then prvSetProgressBar(dtOthersExpenses.Rows.Count)
        For Each dr As DataRow In dtOthersExpenses.Rows
            decLabaRugiBersih -= dr.Item("TotalAmount")
            prvRefreshProgressBar()
        Next
        '# End of Calculate Laba / Rugi Bersih

        Try
            Dim crReport As New rptLabaRugiVer00
            With crReport
                .DataSource = New DataTable
                .Parameters.Item("FilterPeriod").Value = strFilterDate
                .Parameters.Item("CompanyName").Value = MPSLib.UI.usUserApp.CompanyName
                .Parameters.Item("paramTotalLabaKotor").Value = decLabaKotor
                .Parameters.Item("paramTotalLabaUsaha").Value = decLabaUsaha
                .Parameters.Item("paramTotalLabaRugiBersih").Value = decLabaRugiBersih
                .srRevenueAndSales.ReportSource.DataSource = dtSalesAndRevenue
                .srCOGS.ReportSource.DataSource = dtCOGS
                .srOperationalExpenses.ReportSource.DataSource = dtOperationalExpenses
                .srOthersRevenue.ReportSource.DataSource = dtOthersRevenue
                .srOthersExpenses.ReportSource.DataSource = dtOthersExpenses

                '# Handle Sub Report Visible
                If dtCOGS.Rows.Count = 0 Then .banCOGS.Visible = False
                If dtOperationalExpenses.Rows.Count = 0 Then .banOperationalExpense.Visible = False
                If dtOthersRevenue.Rows.Count = 0 Then .banOtherRevenue.Visible = False
                If dtOthersExpenses.Rows.Count = 0 Then .banOtherExpenses.Visible = False
                '# End of Handle Sub Report Visible

                '# Handle Label Section Visible
                If .banOperationalExpense.Visible = False Then .banLabaKotor.Visible = False
                If .banOtherExpenses.Visible = False And .banOtherRevenue.Visible = False Then .banTotalLabaUsaha.Visible = False
                '# End of Handle Label Section Visible

                If decLabaRugiBersih < 0 Then
                    .xrTotalLabaRugiBersih.Text = "TOTAL RUGI BERSIH"
                Else
                    .xrTotalLabaRugiBersih.Text = "TOTAL LABA BERSIH"
                End If

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

    Private Sub frmRptLabaRugiVer00_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        dtpDateFrom.Value = Today.Date.AddDays(-14)
        dtpDateTo.Value = Today.Date
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportProfitAndLoss, VO.Access.Values.ExportReportAccess)
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