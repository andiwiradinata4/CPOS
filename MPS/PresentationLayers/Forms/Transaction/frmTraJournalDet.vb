Imports DevExpress.XtraGrid

Public Class frmTraJournalDet

#Region "Property"

    Private frmParent As frmTraJournal
    Private clsData As VO.Journal
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private bolExport As Boolean = False
    Private strID As String = ""
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Property pubCS As New VO.CS

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1, cPrint = 2, _
       cAdd = 0, cEdit = 1, cDelete = 2

    Private Sub prvSetTitleForm()
        If pubIsNew Then
            Me.Text += " [baru] "
        Else
            Me.Text += " [edit] "
        End If
    End Sub

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdItemView, "JournalID", "JournalID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "CoAID", "CoAID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "CoACode", "Kode Akun", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "CoAName", "Nama Akun", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "DebitAmount", "Debit", 180, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdItemView, "CreditAmount", "Kredit", 180, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdItemView, "Remarks", "Keterangan", 200, UI.usDefGrid.gString)

        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "JournalID", "JournalID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillCombo()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionJournal), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillForm()
        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.Journal
                clsData = BL.Journal.GetDetail(pubID)
                strID = clsData.ID
                txtID.Text = clsData.ID
                txtJournalNo.Text = clsData.JournalNo
                txtPaymentTo.Text = clsData.PaymentTo
                txtCashOrBankInfo.Text = clsData.CashOrBankInfo
                dtpJournalDate.Value = clsData.JournalDate
                cboStatus.SelectedValue = clsData.IDStatus
                txtRemarks.Text = clsData.Remarks
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                If clsData.IsAutoGenerate Then ToolBar.Buttons(cSave).Enabled = False
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvSave()
        If grdItemView.RowCount = 0 Then
            UI.usForm.frmMessageBox("Item belum diinput")
            grdItemView.Focus()
            Exit Sub
        ElseIf prvIsBalance() = False Then
            UI.usForm.frmMessageBox("Nilai debit dengan nilai kredit tidak balance")
            grdItemView.Focus()
            Exit Sub
        ElseIf grdItemView.Columns("DebitAmount").SummaryItem.SummaryValue = 0 Or grdItemView.Columns("CreditAmount").SummaryItem.SummaryValue = 0 Then
            UI.usForm.frmMessageBox("Nilai debit / kredit tidak boleh 0")
            grdItemView.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            cboStatus.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data jurnal?") Then Exit Sub

        Dim drDebit() As DataRow = dtItem.Select("DebitAmount>0")
        Dim drCredit() As DataRow = dtItem.Select("CreditAmount>0")

        If drDebit.Count > 1 And drCredit.Count > 1 Then
            UI.usForm.frmMessageBox("Akun Debit dan Akun Credit tidak boleh sama-sama lebih dari 1 baris")
            Exit Sub
        End If

        clsData = New VO.Journal
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.JournalNo = txtJournalNo.Text.Trim
        clsData.PaymentTo = txtPaymentTo.Text.Trim
        clsData.CashOrBankInfo = txtCashOrBankInfo.Text.Trim
        clsData.ReferencesID = ""
        clsData.JournalDate = dtpJournalDate.Value
        clsData.TotalAmount = grdItemView.Columns("DebitAmount").SummaryItem.SummaryValue
        clsData.IsAutoGenerate = False
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Dim clsDataDetail As New VO.JournalDet
        Dim clsDataDetailAll(dtItem.Rows.Count - 1) As VO.JournalDet
        With dtItem
            For i As Integer = 0 To .Rows.Count - 1
                clsDataDetail = New VO.JournalDet
                clsDataDetail.JournalID = txtID.Text.Trim
                clsDataDetail.CoAID = .Rows(i).Item("CoAID")
                clsDataDetail.CoAName = .Rows(i).Item("CoAName")
                clsDataDetail.DebitAmount = .Rows(i).Item("DebitAmount")
                clsDataDetail.CreditAmount = .Rows(i).Item("CreditAmount")
                clsDataDetail.Remarks = .Rows(i).Item("Remarks")
                clsDataDetailAll(i) = clsDataDetail
            Next
        End With

        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        Try
            strID = BL.Journal.SaveDataDefault(pubIsNew, clsData, clsDataDetailAll)
            If strID.Trim <> "" Then
                prvPrintFakturBiaya()
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor jurnal: " & clsData.JournalNo)
                    frmParent.pubRefresh(clsData.ID)
                    prvClear()
                    prvQueryItem()
                    prvQueryHistory()
                Else
                    pgMain.Value = 100
                    pubIsSave = True
                    Me.Close()
                End If
            Else
                UI.usForm.frmMessageBox("Proses simpan data tidak berhasil")
                Exit Sub
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvPrintFakturBiaya()
        txtJournalNo.Focus()
        Dim frmDetail As New frmRptFakturJurnalVer00
        With frmDetail
            .pubID = strID
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
        'Try
        '    Dim crReport As New rptFakturBiayaPembayaranVer00
        '    With crReport
        '        .DataSource = BL.Journal.PrintFakturBiaya(IIf(strID.Trim = "", txtID.Text.Trim, strID))
        '        .CreateDocument(True)
        '        .ShowPreviewMarginLines = False
        '        .ShowPrintMarginsWarning = False
        '    End With

        '    Dim frmDetail As New frmReportPreview
        '    With frmDetail
        '        .docViewer.DocumentSource = crReport
        '        .pgExportButton.Enabled = bolExport
        '        .Text = Me.Text & " - " & VO.Reports.PrintOut
        '        .WindowState = FormWindowState.Maximized
        '        .ShowDialog()
        '    End With
        'Catch ex As Exception
        '    UI.usForm.frmMessageBox(ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        'End Try
    End Sub

    Private Sub prvClear()
        txtID.Text = ""
        dtpJournalDate.Value = Now
        cboStatus.SelectedValue = VO.Status.Values.Draft
        txtRemarks.Text = ""
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)

        ToolBar.Buttons(cPrint).Enabled = False
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionJournal, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
        ToolBar.Buttons(cPrint).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.PrintReportAccess)
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ExportReportAccess)
    End Sub

#Region "Item Handle"

    Private Sub prvSetButton()
        Dim bolEnabled As Boolean = IIf(grdItemView.RowCount = 0, False, True)
        With ToolBarDetail
            .Buttons(cEdit).Enabled = bolEnabled
            .Buttons(cDelete).Enabled = bolEnabled
        End With
    End Sub

    Private Sub prvQueryItem()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            dtItem = BL.Journal.ListDataDetail(txtID.Text.Trim)
            grdItem.DataSource = dtItem
            prvSumGrid()
            grdItemView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
            prvSetButton()
        End Try
    End Sub

    Private Sub prvAdd()
        Dim frmDetail As New frmTraJournalItem
        With frmDetail
            .pubIsNew = True
            .pubCS = pubCS
            .pubTableParent = dtItem
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            prvSetButton()
        End With
    End Sub

    Private Sub prvEdit()
        intPos = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmTraJournalItem
        With frmDetail
            .pubIsNew = False
            .pubCS = pubCS
            .pubTableParent = dtItem
            .pubDatRowSelected = grdItemView.GetDataRow(intPos)
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            prvSetButton()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim strID As String = grdItemView.GetRowCellValue(intPos, "ID")
        For i As Integer = 0 To dtItem.Rows.Count - 1
            If dtItem.Rows(i).Item("ID") = strID Then
                dtItem.Rows(i).Delete()
                Exit For
            End If
        Next
        dtItem.AcceptChanges()
        grdItem.DataSource = dtItem
        prvSetButton()
    End Sub

    Private Function prvIsBalance() As Boolean
        Dim bolReturn As Boolean = False
        If grdItemView.Columns("DebitAmount").SummaryItem.SummaryValue - grdItemView.Columns("CreditAmount").SummaryItem.SummaryValue = 0 Then
            bolReturn = True
        End If
        Return bolReturn
    End Function

    Private Sub prvSumGrid()
        Dim SumTotalDebit As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DebitAmount", "Total Debit: {0:#,##0.00}")
        Dim SumTotalCredit As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CreditAmount", "Total Kredit: {0:#,##0.00}")

        If grdItemView.Columns("DebitAmount").SummaryText.Trim = "" Then
            grdItemView.Columns("DebitAmount").Summary.Add(SumTotalDebit)
        End If

        If grdItemView.Columns("CreditAmount").SummaryText.Trim = "" Then
            grdItemView.Columns("CreditAmount").Summary.Add(SumTotalCredit)
        End If
    End Sub

#End Region

#Region "History Handle"

    Private Sub prvQueryHistory()
        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        Try
            grdStatus.DataSource = BL.Journal.ListDataStatus(txtID.Text.Trim)
            grdStatusView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

#End Region

#Region "Form Handle"

    Private Sub frmTraJournalDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            tcHeader.SelectedTab = tpMain
        ElseIf e.KeyCode = Keys.F2 Then
            tcHeader.SelectedTab = tpHistory
        ElseIf e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmTraJournalDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDetail.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQueryItem()
        prvQueryHistory()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
            Case "Cetak Bon" : prvPrintFakturBiaya()
        End Select
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Tambah" : prvAdd()
            Case "Edit" : prvEdit()
            Case "Hapus" : prvDelete()
        End Select
    End Sub

#End Region

End Class