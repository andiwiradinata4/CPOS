Public Class frmTraCostDet

#Region "Property"

    Private frmParent As frmTraCost
    Private clsData As VO.Cost
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private intCoAID As Integer = 0
    Private strJournalID As String = ""
    Private strID As String = ""
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Property pubCS As New VO.CS
    Private bolExport As Boolean = False

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1, cPrint = 2,
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
        UI.usForm.SetGrid(grdItemView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "CostID", "CostID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "CoAID", "CoAID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "CoACode", "Kode Akun", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "CoAName", "Nama Akun", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "Remarks", "Keterangan", 180, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "Amount", "Nilai", 180, UI.usDefGrid.gReal2Num)

        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "CostID", "CostID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillCombo()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionCost), "IDStatus", "StatusName")
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
                clsData = New VO.Cost
                clsData = BL.Cost.GetDetail(pubID)
                strID = clsData.ID
                txtID.Text = clsData.ID
                txtCostNo.Text = clsData.CostNo
                dtpCostDate.Value = clsData.CostDate
                intCoAID = clsData.CoAID
                txtCoACode.Text = clsData.CoACode
                txtCoAName.Text = clsData.CoAName
                txtPaymentTo.Text = clsData.PaymentTo
                txtTotalAmount.Value = clsData.TotalAmount
                cboStatus.SelectedValue = clsData.IDStatus
                txtRemarks.Text = clsData.Remarks
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
                strJournalID = clsData.JournalID
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
        ElseIf txtCoACode.Text.Trim = "" Or txtCoAName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Kas / Bank belum dipilih")
            txtCoACode.Focus()
            Exit Sub
        ElseIf txtTotalAmount.Value <= 0 Then
            UI.usForm.frmMessageBox("Total biaya harus lebih besar dari 0")
            txtTotalAmount.Focus()
            Exit Sub
        ElseIf grdItemView.RowCount = 0 Then
            UI.usForm.frmMessageBox("Item belum diinput")
            grdItemView.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            cboStatus.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data biaya?") Then Exit Sub

        clsData = New VO.Cost
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.CostDate = dtpCostDate.Value
        clsData.CoAID = intCoAID
        clsData.CoACode = txtCoACode.Text.Trim
        clsData.CoAName = txtCoAName.Text.Trim
        clsData.TotalAmount = txtTotalAmount.Value
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID
        clsData.CostNo = txtCostNo.Text.Trim
        clsData.PaymentTo = txtPaymentTo.Text.Trim

        Dim clsDataDetail As New VO.CostDet
        Dim clsDataDetailAll(dtItem.Rows.Count - 1) As VO.CostDet
        With dtItem
            For i As Integer = 0 To .Rows.Count - 1
                clsDataDetail = New VO.CostDet
                clsDataDetail.CostID = txtID.Text.Trim
                clsDataDetail.CoAID = .Rows(i).Item("CoAID")
                clsDataDetail.CoACode = .Rows(i).Item("CoACode")
                clsDataDetail.CoAName = .Rows(i).Item("CoAName")
                clsDataDetail.Remarks = .Rows(i).Item("Remarks")
                clsDataDetail.Amount = .Rows(i).Item("Amount")
                clsDataDetailAll(i) = clsDataDetail
            Next
        End With

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            strID = BL.Cost.SaveData(pubIsNew, clsData, clsDataDetailAll)
            pgMain.Value = 50
            If strID.Trim <> "" Then
                prvPrintFakturBiaya()
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor biaya: " & clsData.CostNo)
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
        txtCostNo.Focus()
        Dim frmDetail As New frmRptFakturBiayaVer00
        With frmDetail
            .pubID = strID
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
        'Try
        '    Dim crReport As New rptFakturBiayaPembayaranVer00
        '    With crReport
        '        .DataSource = BL.Cost.PrintFakturBiaya(IIf(strID.Trim = "", txtID.Text.Trim, strID))
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
        txtCostNo.Text = ""
        dtpCostDate.Value = Now
        intCoAID = 0
        txtCoACode.Text = ""
        txtCoAName.Text = ""
        txtPaymentTo.Text = ""
        txtTotalAmount.Value = 0
        cboStatus.SelectedValue = VO.Status.Values.Draft
        txtRemarks.Text = ""
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)

        ToolBar.Buttons(cPrint).Enabled = False
    End Sub

    Private Sub prvChooseItem()
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .pubFilterGroup = VO.ChartOfAccount.FilterGroup.CashOrBank
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAID = .pubLUdtRow.Item("ID")
                txtCoACode.Text = .pubLUdtRow.Item("Code")
                txtCoAName.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Public Sub pubCalculate()
        Dim decTotalCost As Decimal = 0
        For Each dr As DataRow In dtItem.Rows
            decTotalCost += dr.Item("Amount")
        Next
        txtTotalAmount.Value = decTotalCost
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionCost, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
        ToolBar.Buttons(cPrint).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.PrintReportAccess)
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.ExportReportAccess)
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
            dtItem = BL.Cost.ListDataDetail(txtID.Text.Trim)
            grdItem.DataSource = dtItem
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
        Dim frmDetail As New frmTraCostItem
        With frmDetail
            .pubIsNew = True
            .pubTableParent = dtItem
            .pubCS = pubCS
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            prvSetButton()
        End With
    End Sub

    Private Sub prvEdit()
        intPos = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmTraCostItem
        With frmDetail
            .pubIsNew = False
            .pubCS = pubCS
            .pubTableParent = dtItem
            .pubDatRowSelected = grdItemView.GetDataRow(intPos)
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            prvSetButton()
            pubCalculate()
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
        pubCalculate()
    End Sub

#End Region

#Region "History Handle"

    Private Sub prvQueryHistory()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdStatus.DataSource = BL.Cost.ListDataStatus(txtID.Text.Trim)
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

    Private Sub frmTraCostDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub frmTraCostDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub btnCoA_Click(sender As Object, e As EventArgs) Handles btnCoA.Click
        prvChooseItem()
    End Sub

    Private Sub txtCoACode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCoACode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsCoA As VO.ChartOfAccount = BL.ChartOfAccount.GetDetail(txtCoACode.Text.Trim)
            If clsCoA.ID = 0 Then
                UI.usForm.frmMessageBox("Kode akun " & txtCoACode.Text.Trim & " tidak tersedia")
                intCoAID = 0
                txtCoACode.Text = ""
                txtCoAName.Text = ""
                Exit Sub
            End If
            intCoAID = clsCoA.ID
            txtCoACode.Text = clsCoA.Code
            txtCoAName.Text = clsCoA.Name
        End If
    End Sub

#End Region

End Class