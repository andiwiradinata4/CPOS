Imports DevExpress.XtraGrid
Public Class frmTraJournal

    Private intPos As Integer = 0
    Private clsData As New VO.Journal
    Private intCompanyID As Integer

    Private Const _
       cNew = 0, cDetail = 1, cDelete = 2, cSep1 = 3, cRefresh = 4, cClose = 5

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "Pick", "Pilih", 100, UI.usDefGrid.gIntNum, True, False)
        UI.usForm.SetGrid(grdView, "ProgramID", "ProgramID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Perusahaan", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "ID", "Nomor", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "JournalNo", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CashOrBankInfo", "Cash / Bank", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PaymentTo", "Kepada", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "JournalDate", "Tanggal", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdView, "TotalAmount", "Total Jurnal", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ReferencesID", "No. Referensi", 100, UI.usDefGrid.gString, MPSLib.UI.usUserApp.IsSuperUser)
        UI.usForm.SetGrid(grdView, "IsPostedGL", "Posted GL", 100, UI.usDefGrid.gBoolean)
        UI.usForm.SetGrid(grdView, "PostedBy", "Posted By", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PostedDate", "Posted Date", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "IsAutoGenerate", "IsGenerate", 100, UI.usDefGrid.gBoolean)
        UI.usForm.SetGrid(grdView, "IsDeleted", "IsDeleted", 100, UI.usDefGrid.gBoolean, False)
        UI.usForm.SetGrid(grdView, "Remarks", "Keterangan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "IDStatus", "IDStatus", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CreatedBy", "Dibuat Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CreatedDate", "Tanggal Buat", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogBy", "Diedit Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "LogDate", "Tanggal Edit", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogInc", "LogInc", 100, UI.usDefGrid.gIntNum)
        UI.usForm.SetGrid(grdView, "StatusInfo", "Status", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cDetail).Enabled = bolEnable
            .Item(cDelete).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvFillCombo()
        Try
            Dim dtData As DataTable = BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionJournal)
            Dim dr As DataRow
            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("IDStatus") = VO.Status.Values.All
                .Item("StatusName") = "SEMUA"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()
            dtData.DefaultView.Sort = "IDStatus ASC"
            dtData = dtData.DefaultView.ToTable

            UI.usForm.FillComboBox(cboStatus, dtData, "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvDefaultFilter()
        intCompanyID = MPSLib.UI.usUserApp.CompanyID
        txtCompanyName.Text = MPSLib.UI.usUserApp.CompanyName
    End Sub

    Private Sub prvQuery()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdMain.DataSource = BL.Journal.ListData(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date, cboStatus.SelectedValue)
            prvSumGrid()
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Public Sub pubRefresh(Optional ByVal strSearch As String = "")
        With grdView
            If Not grdView.FocusedValue Is Nothing And strSearch = "" Then
                strSearch = grdView.GetDataRow(grdView.FocusedRowHandle).Item("ID")
            End If
            prvQuery()
            If grdView.RowCount > 0 Then UI.usForm.GridMoveRow(grdView, "ID", strSearch)
        End With
    End Sub

    Private Function prvGetCS() As VO.CS
        Dim clsCS As New VO.CS
        clsCS.ProgramID = MPSLib.UI.usUserApp.ProgramID
        clsCS.ProgramName = MPSLib.UI.usUserApp.ProgramName
        clsCS.CompanyID = intCompanyID
        clsCS.CompanyName = txtCompanyName.Text.Trim
        Return clsCS
    End Function

    Private Function prvGetData() As VO.Journal
        Dim clsReturn As New VO.Journal
        clsReturn.ProgramID = grdView.GetRowCellValue(intPos, "ProgramID")
        clsReturn.ProgramName = grdView.GetRowCellValue(intPos, "ProgramName")
        clsReturn.CompanyID = grdView.GetRowCellValue(intPos, "CompanyID")
        clsReturn.CompanyName = grdView.GetRowCellValue(intPos, "CompanyName")
        clsReturn.ID = grdView.GetRowCellValue(intPos, "ID")
        clsReturn.JournalNo = grdView.GetRowCellValue(intPos, "JournalNo")
        clsReturn.PaymentTo = grdView.GetRowCellValue(intPos, "PaymentTo")
        clsReturn.CashOrBankInfo = grdView.GetRowCellValue(intPos, "CashOrBankInfo")
        clsReturn.ReferencesID = grdView.GetRowCellValue(intPos, "ReferencesID")
        clsReturn.JournalDate = grdView.GetRowCellValue(intPos, "JournalDate")
        clsReturn.TotalAmount = grdView.GetRowCellValue(intPos, "TotalAmount")
        clsReturn.IDStatus = grdView.GetRowCellValue(intPos, "IDStatus")
        clsReturn.IsAutoGenerate = grdView.GetRowCellValue(intPos, "IsAutoGenerate")
        Return clsReturn
    End Function

    Private Sub prvNew()
        prvResetProgressBar()
        Dim frmDetail As New frmTraJournalDet
        With frmDetail
            .pubIsNew = True
            .pubCS = prvGetCS()
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvDetail()
        prvResetProgressBar()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        Dim frmDetail As New frmTraJournalDet
        With frmDetail
            .pubIsNew = False
            .pubCS = prvGetCS()
            .pubID = clsData.ID
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            If .pubIsSave Then pubRefresh()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        If clsData.IsAutoGenerate Then
            UI.usForm.frmMessageBox("Jurnal yang di auto generate oleh sistem tidak dapat dihapus.")
            Exit Sub
        End If
        If Not UI.usForm.frmAskQuestion("Hapus Nomor " & clsData.ID & "?") Then Exit Sub

        Dim frmDetail As New usFormRemarks
        With frmDetail
            .ShowDialog()
            If .pubIsSave Then
                clsData.Remarks = .pubValue
            Else
                Exit Sub
            End If
        End With

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 40
        Try
            BL.Journal.DeleteDataDefault(clsData)
            pgMain.Value = 100
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "ID"))
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvClear()
        grdMain.DataSource = Nothing
        grdView.Columns.Clear()
        prvSetGrid()
        prvSetButton()
    End Sub

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
                prvClear()
                btnExecute.Focus()
            End If
        End With
    End Sub

    Private Sub prvSumGrid()
        Dim SumTotalAmount As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "Total Jurnal: {0:#,##0.00}")

        If grdView.Columns("TotalAmount").SummaryText.Trim = "" Then
            grdView.Columns("TotalAmount").Summary.Add(SumTotalAmount)
        End If
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cNew).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.NewAccess)
            .Item(cDelete).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.DeleteAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmTraJournal_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraJournal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillCombo()
        prvSetGrid()
        cboStatus.SelectedValue = VO.Status.Values.All
        dtpDateFrom.Value = Today.Date.AddDays(-7)
        dtpDateTo.Value = Today.Date
        prvDefaultFilter()
        prvQuery()
        prvUserAccess()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cNew).Name Then
            prvNew()
        ElseIf e.Button.Name = ToolBar.Buttons(cRefresh).Name Then
            pubRefresh()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        ElseIf grdView.FocusedRowHandle >= 0 Then
            Select Case e.Button.Name
                Case ToolBar.Buttons(cDetail).Name : prvDetail()
                Case ToolBar.Buttons(cDelete).Name : prvDelete()
            End Select
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        prvQuery()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        prvClear()
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

    Private Sub grdView_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdView.RowStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If (e.RowHandle >= 0) Then
            Dim intStatusID As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IDStatus"))
            If intStatusID = VO.Status.Values.Deleted And e.Appearance.BackColor <> Color.Salmon Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        End If
    End Sub

#End Region

End Class