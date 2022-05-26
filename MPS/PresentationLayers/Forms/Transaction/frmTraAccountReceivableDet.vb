Imports DevExpress.XtraGrid.Columns

Public Class frmTraAccountReceivableDet

#Region "Property"

    Private frmParent As frmTraAccountReceivable
    Private clsData As VO.AccountReceivable
    Private intBPID As Integer
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = ""
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Private bolValid As Boolean = True
    Private intCoAIDOfIncomePayment As Integer = 0
    Property pubCS As New VO.CS

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1,
       cCheckAll = 0, cUncheckAll = 1

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
        UI.usForm.SetGrid(grdItemView, "Pick", "Pilih", 80, UI.usDefGrid.gBoolean, True, False)
        UI.usForm.SetGrid(grdItemView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "ARID", "ARID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "SalesID", "Nomor Penjualan", 250, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "SupplierID", "Kode Pemasok", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "SupplierName", "Pemasok", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "Amount", "Total Tagihan", 250, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdItemView, "MaxAmount", "Total Maksimal Tagihan", 250, UI.usDefGrid.gReal2Num)
        grdItemView.Columns("Amount").ColumnEdit = rpiValue

        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "ARID", "ARID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionReceive), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillPaymentTerm()
        Try
            UI.usForm.FillComboBox(cboPaymentReferences, BL.PaymentReferences.ListDataForCombo, "ID", "Name")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillPaymentTerm()
    End Sub

    Private Sub prvFillForm()
        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.AccountReceivable
                clsData = BL.AccountReceivable.GetDetail(pubID)
                txtID.Text = clsData.ID
                intBPID = clsData.BPID
                txtBPCode.Text = clsData.BPCode
                txtBPName.Text = clsData.BPName
                dtpARDate.Value = clsData.ARDate
                txtTotalAmount.Value = clsData.TotalAmount
                cboPaymentReferences.SelectedValue = clsData.PaymentReferencesID
                txtReferencesNote.Text = clsData.ReferencesNote
                cboStatus.SelectedValue = clsData.IDStatus
                txtRemarks.Text = clsData.Remarks
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                ToolBar.Buttons(cSave).Enabled = Not clsData.IsDeleted
                strJournalID = clsData.JournalID
                intCoAIDOfIncomePayment = clsData.CoAIDOfIncomePayment
                txtCoACodeOfIncomePayment.Text = clsData.CoACodeOfIncomePayment
                txtCoANameOfIncomePayment.Text = clsData.CoANameOfIncomePayment
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
        ToolBar.Focus()
        If bolValid = False Then Exit Sub
        If txtBPName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih pelanggan terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtBPName.Focus()
            Exit Sub
        ElseIf cboPaymentReferences.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih kolom pembayaran melalui terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            cboPaymentReferences.Focus()
            Exit Sub
        ElseIf txtCoACodeOfIncomePayment.Text.Trim = "" Or txtCoANameOfIncomePayment.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih kolom kas / bank terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtCoACodeOfIncomePayment.Focus()
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

        Dim drPick() As DataRow = dtItem.Select("Pick=True")

        If drPick.Count = 0 Then UI.usForm.frmMessageBox("Mohon pilih tagihan pada item terlebih dahulu") : Exit Sub

        With grdItemView
            For i As Integer = 0 To grdItemView.RowCount - 1
                If .GetRowCellValue(i, "Pick") And .GetRowCellValue(i, "Amount") > .GetRowCellValue(i, "MaxAmount") Then
                    UI.usForm.frmMessageBox("Nilai total tagihan tidak boleh lebih besar dari nilai total maksimal tagihan")
                    .FocusedRowHandle = i
                    .FocusedColumn = .Columns("Amount")
                    Exit Sub
                ElseIf .GetRowCellValue(i, "Pick") And .GetRowCellValue(i, "Amount") = 0 Then
                    UI.usForm.frmMessageBox("Kolom total tagihan harus lebih besar dari 0 jika sudah dipilih")
                    .FocusedRowHandle = i
                    .FocusedColumn = .Columns("Amount")
                    Exit Sub
                End If
            Next
        End With

        If Not UI.usForm.frmAskQuestion("Simpan data tagihan?") Then Exit Sub

        clsData = New VO.AccountReceivable
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.BPID = intBPID
        clsData.BPName = txtBPName.Text.Trim
        clsData.ARDate = dtpARDate.Value
        clsData.TotalAmount = txtTotalAmount.Value
        clsData.PaymentReferencesID = cboPaymentReferences.SelectedValue
        clsData.ReferencesNote = txtReferencesNote.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID
        clsData.CoAIDOfIncomePayment = intCoAIDOfIncomePayment
        clsData.CoACodeOfIncomePayment = txtCoACodeOfIncomePayment.Text.Trim
        clsData.CoANameOfIncomePayment = txtCoANameOfIncomePayment.Text.Trim

        Dim clsDataDetail As New VO.AccountReceivableDet
        Dim clsDataDetailAll(drPick.Count - 1) As VO.AccountReceivableDet
        For i As Integer = 0 To drPick.Count - 1
            clsDataDetail = New VO.AccountReceivableDet
            clsDataDetail.ARID = txtID.Text.Trim
            clsDataDetail.SalesID = drPick(i).Item("SalesID")
            clsDataDetail.Amount = drPick(i).Item("Amount")
            clsDataDetailAll(i) = clsDataDetail
        Next

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim strID As String = BL.AccountReceivable.SaveData(pubIsNew, clsData, clsDataDetailAll)
            If strID.Trim <> "" Then
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor Tagihan: " & strID)
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

    Private Sub prvClear()
        txtID.Text = ""
        intBPID = 0
        txtBPCode.Text = ""
        txtBPName.Text = ""
        dtpARDate.Value = Now
        txtTotalAmount.Value = 0
        cboPaymentReferences.SelectedIndex = -1
        txtReferencesNote.Text = ""
        cboStatus.SelectedValue = VO.Status.Values.Draft
        txtRemarks.Text = ""
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
        intCoAIDOfIncomePayment = MPSLib.UI.usUserApp.JournalPost.CoAofCash
        txtCoACodeOfIncomePayment.Text = MPSLib.UI.usUserApp.JournalPost.CoACodeofCash
        txtCoANameOfIncomePayment.Text = MPSLib.UI.usUserApp.JournalPost.CoANameofCash
    End Sub

    Private Sub prvChooseBP()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intBPID = .pubLUdtRow.Item("ID")
                txtBPCode.Text = .pubLUdtRow.Item("Code")
                txtBPName.Text = .pubLUdtRow.Item("Name")
                prvQueryItem()
            End If
        End With
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionAccountReceivable, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

#Region "Item Handle"

    Private Sub prvSetButton()
        Dim bolEnabled As Boolean = IIf(grdItemView.RowCount = 0, False, True)
        With ToolBarDetail
            .Buttons(cCheckAll).Enabled = bolEnabled
            .Buttons(cUncheckAll).Enabled = bolEnabled
        End With
    End Sub

    Private Sub prvQueryItem()
        Try
            If pubIsNew Then
                dtItem = BL.AccountReceivable.ListDataDetailOutstanding(pubCS.CompanyID, pubCS.ProgramID, intBPID)
            Else
                If clsData.IsDeleted Then
                    dtItem = BL.AccountReceivable.ListDataDetail(pubCS.CompanyID, pubCS.ProgramID, txtID.Text.Trim)
                Else
                    dtItem = BL.AccountReceivable.ListDataDetailWithOutstanding(pubCS.CompanyID, pubCS.ProgramID, txtID.Text.Trim, intBPID)
                End If
            End If
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

    Private Sub prvCalculate()
        Dim decAmount As Decimal = 0

        For Each dr As DataRow In dtItem.Rows
            decAmount += dr.Item("Amount")
        Next

        txtTotalAmount.Value = decAmount
    End Sub

    Private Sub prvChangeCheckedValue(ByVal bolValue As Boolean)
        With grdItemView
            For i As Integer = 0 To .RowCount - 1
                .SetRowCellValue(i, "Pick", bolValue)
                If bolValue Then
                    .SetRowCellValue(i, "Amount", .GetRowCellValue(i, "MaxAmount"))
                Else
                    .SetRowCellValue(i, "Amount", 0)
                End If
                .UpdateCurrentRow()
            Next
            ToolBarDetail.Focus()
            prvCalculate()
        End With
    End Sub

#End Region

#Region "History Handle"

    Private Sub prvQueryHistory()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdStatus.DataSource = BL.AccountReceivable.ListDataStatus(txtID.Text.Trim)
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

    Private Sub frmTraAccountReceivableDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub frmTraAccountReceivableDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub btnBP_Click(sender As Object, e As EventArgs) Handles btnBP.Click
        prvChooseBP()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Centang Semua" : prvChangeCheckedValue(True)
            Case "Hapus Semua Centangan" : prvChangeCheckedValue(False)
        End Select
    End Sub

    Private Sub rpiValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rpiValue.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        If (e.KeyChar = ChrW(Asc(","))) Or (e.KeyChar = ChrW(Asc("."))) Then
            e.Handled = False
        End If
    End Sub

    Private Sub grdItemView_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles grdItemView.ValidatingEditor
        With grdItemView
            bolValid = True
            Dim col As GridColumn = .FocusedColumn
            Dim intFocus As Integer = .FocusedRowHandle
            If col.Name = "Amount" Then
                Dim oldValue As Decimal = IIf(.GetFocusedRowCellValue(col).Equals(DBNull.Value), 0, .GetFocusedRowCellValue(col))
                Dim newValue As Decimal = IIf((e.Value = "") Or (e.Value.Equals(DBNull.Value) Or (e.Value = ".")), 0, e.Value)
                Dim strErrorMessage As String = ""
                If newValue > 0 And newValue > grdItemView.GetFocusedRowCellValue("MaxAmount") Then
                    bolValid = False
                    strErrorMessage = "Total tagihan tidak boleh lebih besar dari total maksimal tagihan"
                    UI.usForm.frmMessageBox(strErrorMessage)
                End If

                If bolValid = False Then
                    e.Value = oldValue
                    e.Valid = False
                    e.ErrorText = strErrorMessage
                    .FocusedRowHandle = intFocus
                    .FocusedColumn = .Columns(col.Name)
                    .ShowEditor()
                    Exit Sub
                Else
                    .SetRowCellValue(intFocus, col.Name, newValue)
                    .UpdateCurrentRow()
                    prvCalculate()
                End If
            ElseIf col.Name = "Pick" Then
                If e.Value = True Then
                    grdItemView.SetRowCellValue(intFocus, col.Name, e.Value)
                    grdItemView.SetRowCellValue(intFocus, "Amount", grdItemView.GetRowCellValue(intFocus, "MaxAmount"))
                ElseIf e.Value = False Then
                    grdItemView.SetRowCellValue(intFocus, col.Name, e.Value)
                    grdItemView.SetRowCellValue(intFocus, "Amount", 0)
                End If
                .UpdateCurrentRow()
                prvCalculate()
            End If
        End With
    End Sub

    Private Sub btnCoAOfIncomePayment_Click(sender As Object, e As EventArgs) Handles btnCoAOfIncomePayment.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .pubFilterGroup = VO.ChartOfAccount.FilterGroup.CashOrBank
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDOfIncomePayment = .pubLUdtRow.Item("ID")
                txtCoACodeOfIncomePayment.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfIncomePayment.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub txtBPCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBPCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsBP As VO.BusinessPartner = BL.BusinessPartner.GetDetail(txtBPCode.Text.Trim)
            If clsBP.ID = 0 Then
                UI.usForm.frmMessageBox("Kode Pelanggan " & txtBPCode.Text.Trim & " tidak tersedia")
                intBPID = 0
                txtBPName.Text = ""
                prvQueryItem()
                Exit Sub
            End If
            intBPID = clsBP.ID
            txtBPName.Text = clsBP.Name
            prvQueryItem()
        End If
    End Sub

    Private Sub txtCoACodeOfIncomePayment_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCoACodeOfIncomePayment.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsCoA As VO.ChartOfAccount = BL.ChartOfAccount.GetDetail(txtCoACodeOfIncomePayment.Text.Trim)
            If clsCoA.ID = 0 Then
                UI.usForm.frmMessageBox("Kode akun " & txtCoACodeOfIncomePayment.Text.Trim & " tidak tersedia")
                intCoAIDOfIncomePayment = 0
                txtCoACodeOfIncomePayment.Text = ""
                txtCoANameOfIncomePayment.Text = ""
                Exit Sub
            End If
            intCoAIDOfIncomePayment = clsCoA.ID
            txtCoACodeOfIncomePayment.Text = clsCoA.Code
            txtCoANameOfIncomePayment.Text = clsCoA.Name
        End If
    End Sub

#End Region

End Class