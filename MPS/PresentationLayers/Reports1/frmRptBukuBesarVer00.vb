Public Class frmRptBukuBesarVer00

#Region "Property"

    Private bolExport As Boolean = False
    Private dtCOA As New DataTable
    Private Const _
       cPreview = 0, cClose = 1

#End Region

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "Pick", "Pilih", 100, UI.usDefGrid.gIntNum, True, False)
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Code", "Kode Akun", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Name", "Nama Akun", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvQuery()
        Try
            dtCOA = BL.ChartOfAccount.ListData(VO.ChartOfAccount.FilterGroup.All, MPSLib.UI.usUserApp.CompanyID, MPSLib.UI.usUserApp.ProgramID)
            grdMain.DataSource = dtCOA
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSetProgressBar(ByVal intMax As Integer)
        pgMain.Value = 0
        pgMain.Maximum = intMax
    End Sub

    Private Sub prvRefreshProgressBar()
        pgMain.Value += 1
        Me.Refresh()
    End Sub

    Private Function prvCollectDataVer00(ByVal drPick() As DataRow) As DataTable
        Dim dtCurrent As New DataTable, dtReturn As New DataTable
        Try
            '# Loop All Selected Chart Of Account
            For Each dr As DataRow In drPick
                '# Get Last Balance
                Dim decFirstBalance As Decimal = BL.Reports.BukuBesarLastBalance(dtpDateFrom.Value.Date, dr.Item("ID"), MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID)

                '# Calculate Table Return
                dtCurrent = BL.Reports.BukuBesarVer00Report(dtpDateFrom.Value.Date, dtpDateTo.Value.Date, dr.Item("ID"), MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID)
                With dtCurrent
                    prvSetProgressBar(.Rows.Count)
                    For i As Integer = 0 To .Rows.Count - 1
                        .Rows(i).BeginEdit()
                        .Rows(i).Item("FirstBalance") = decFirstBalance
                        If i = 0 Then
                            If .Rows(i).Item("COAType") = VO.ChartOfAccountType.Values.AktivaLancar Or _
                                .Rows(i).Item("COAType") = VO.ChartOfAccountType.Values.AktivaTetap Or _
                                .Rows(i).Item("COAType") = VO.ChartOfAccountType.Values.Beban Then
                                .Rows(i).Item("BalanceAmount") = decFirstBalance + .Rows(i).Item("DebitAmount") - .Rows(i).Item("CreditAmount")
                            Else
                                .Rows(i).Item("BalanceAmount") = decFirstBalance + .Rows(i).Item("CreditAmount") - .Rows(i).Item("DebitAmount")
                            End If
                        Else
                            If .Rows(i).Item("COAType") = VO.ChartOfAccountType.Values.AktivaLancar Or _
                                .Rows(i).Item("COAType") = VO.ChartOfAccountType.Values.AktivaTetap Or _
                                .Rows(i).Item("COAType") = VO.ChartOfAccountType.Values.Beban Then
                                .Rows(i).Item("BalanceAmount") = .Rows(i - 1).Item("BalanceAmount") + .Rows(i).Item("DebitAmount") - .Rows(i).Item("CreditAmount")
                            Else
                                .Rows(i).Item("BalanceAmount") = .Rows(i - 1).Item("BalanceAmount") + .Rows(i).Item("CreditAmount") - .Rows(i).Item("DebitAmount")
                            End If
                        End If
                        .Rows(i).EndEdit()
                        prvRefreshProgressBar()
                    Next

                    '# Update Last Balance
                    prvSetProgressBar(.Rows.Count)
                    For i As Integer = 0 To .Rows.Count - 1
                        .Rows(i).BeginEdit()
                        .Rows(i).Item("LastBalance") = .Rows(.Rows.Count - 1).Item("BalanceAmount")
                        .Rows(i).EndEdit()
                        prvRefreshProgressBar()
                    Next
                    .AcceptChanges()
                End With

                dtReturn.Merge(dtCurrent)
            Next

        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
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

        Dim drPick() As DataRow = dtCOA.Select("Pick=True")
        If drPick.Count = 0 Then
            UI.usForm.frmMessageBox("Pilih salah satu akun terlebih dahulu")
            Exit Sub
        End If

        Dim strFilterDate As String = Format(dtpDateFrom.Value, "dd-MMM-yyyy") & " - " & Format(dtpDateTo.Value, "dd-MMM-yyyy")

        Me.Cursor = Cursors.WaitCursor
        Try
            Dim crReport As New rptBukuBesarVer00
            crReport.DataSource = prvCollectDataVer00(drPick)
            With crReport
                .Parameters.Item("FilterPeriod").Value = strFilterDate
                .Parameters.Item("CompanyName").Value = MPSLib.UI.usUserApp.CompanyName
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

    Private Sub prvCheckUnchecked(ByVal bolValue As Boolean)
        For Each dr As DataRow In dtCOA.Rows
            dr.BeginEdit()
            dr.Item("Pick") = bolValue
            dr.EndEdit()
        Next
        dtCOA.AcceptChanges()
        grdMain.DataSource = dtCOA
    End Sub

#Region "Form Handle"

    Private Sub frmRptBukuBesarVer00_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDetail.SetIcon(Me)
        prvSetGrid()
        dtpDateFrom.Value = Today.Date.AddDays(-14)
        dtpDateTo.Value = Today.Date
        prvQuery()
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBukuBesar, VO.Access.Values.ExportReportAccess)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Lihat Laporan" : prvPreview()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Centang Semua" : prvCheckUnchecked(True)
            Case "Tidak Centang Semua" : prvCheckUnchecked(False)
        End Select
    End Sub

#End Region
  

End Class