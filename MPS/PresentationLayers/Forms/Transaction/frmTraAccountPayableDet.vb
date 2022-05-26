Imports DevExpress.XtraGrid.Columns

Public Class frmTraAccountPayableDet

#Region "Property"

    Private frmParent As frmTraAccountPayable
    Private clsData As VO.AccountPayable
    Private intBPID As Integer
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = ""
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Private bolValid As Boolean = True
    Private intCoAIDOfOutgoingPayment As Integer = 0
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
        UI.usForm.SetGrid(grdItemView, "APID", "APID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "ReceiveID", "Nomor Pembelian", 250, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "Amount", "Total Bayar", 250, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdItemView, "MaxAmount", "Total Maksimal Pembayaran", 250, UI.usDefGrid.gReal2Num)
        grdItemView.Columns("Amount").ColumnEdit = rpiValue

        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "ReceiveID", "ReceiveID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionAccountReceivable), "IDStatus", "StatusName")
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
                clsData = New VO.AccountPayable
                clsData = BL.AccountPayable.GetDetail(pubID)
                txtID.Text = clsData.ID
                intBPID = clsData.BPID
                txtBPCode.Text = clsData.BPCode
                txtBPName.Text = clsData.BPName
                dtpAPDate.Value = clsData.APDate
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
                intCoAIDOfOutgoingPayment = clsData.CoAIDOfOutgoingPayment
                txtCoACodeOfOutgoingPayment.Text = clsData.CoACodeOfOutgoingPayment
                txtCoANameOfOutgoingPayment.Text = clsData.CoANameOfOutgoingPayment
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
            UI.usForm.frmMessageBox("Pilih pemasok terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtBPName.Focus()
            Exit Sub
        ElseIf cboPaymentReferences.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih kolom pembayaran melalui terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            cboPaymentReferences.Focus()
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

        If drPick.Count = 0 Then UI.usForm.frmMessageBox("Mohon pilih pembayaran pada item terlebih dahulu") : Exit Sub

        With grdItemView
            For i As Integer = 0 To grdItemView.RowCount - 1
                If .GetRowCellValue(i, "Pick") And .GetRowCellValue(i, "Amount") > .GetRowCellValue(i, "MaxAmount") Then
                    UI.usForm.frmMessageBox("Nilai total pembayaran tidak boleh lebih besar dari nilai total maksimal pembayaran")
                    .FocusedRowHandle = i
                    .FocusedColumn = .Columns("Amount")
                    Exit Sub
                ElseIf .GetRowCellValue(i, "Pick") And .GetRowCellValue(i, "Amount") = 0 Then
                    UI.usForm.frmMessageBox("Kolom total pembayaran harus lebih besar dari 0 jika sudah dipilih")
                    .FocusedRowHandle = i
                    .FocusedColumn = .Columns("Amount")
                    Exit Sub
                End If
            Next
        End With

        If Not UI.usForm.frmAskQuestion("Simpan data pembayaran?") Then Exit Sub

        clsData = New VO.AccountPayable
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.BPID = intBPID
        clsData.BPName = txtBPName.Text.Trim
        clsData.APDate = dtpAPDate.Value
        clsData.TotalAmount = txtTotalAmount.Value
        clsData.PaymentReferencesID = cboPaymentReferences.SelectedValue
        clsData.ReferencesNote = txtReferencesNote.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID
        clsData.CoAIDOfOutgoingPayment = intCoAIDOfOutgoingPayment
        clsData.CoACodeOfOutgoingPayment = txtCoACodeOfOutgoingPayment.Text.Trim
        clsData.CoANameOfOutgoingPayment = txtCoANameOfOutgoingPayment.Text.Trim

        Dim clsDataDetail As New VO.AccountPayableDet
        Dim clsDataDetailAll(drPick.Count - 1) As VO.AccountPayableDet
        For i As Integer = 0 To drPick.Count - 1
            clsDataDetail = New VO.AccountPayableDet
            clsDataDetail.APID = txtID.Text.Trim
            clsDataDetail.ReceiveID = drPick(i).Item("ReceiveID")
            clsDataDetail.Amount = drPick(i).Item("Amount")
            clsDataDetailAll(i) = clsDataDetail
        Next

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim strID As String = BL.AccountPayable.SaveData(pubIsNew, clsData, clsDataDetailAll)
            pgMain.Value = 50
            If strID.Trim <> "" Then
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor Pembayaran: " & strID)
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
        dtpAPDate.Value = Now
        txtTotalAmount.Value = 0
        cboPaymentReferences.SelectedIndex = -1
        txtReferencesNote.Text = ""
        cboStatus.SelectedValue = VO.Status.Values.Draft
        txtRemarks.Text = ""
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
        intCoAIDOfOutgoingPayment = MPSLib.UI.usUserApp.JournalPost.CoAofCash
        txtCoACodeOfOutgoingPayment.Text = MPSLib.UI.usUserApp.JournalPost.CoACodeofCash
        txtCoANameOfOutgoingPayment.Text = MPSLib.UI.usUserApp.JournalPost.CoANameofCash
    End Sub

    Private Sub prvChooseBP()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
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
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionAccountPayable, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
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
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            If pubIsNew Then
                dtItem = BL.AccountPayable.ListDataDetailOutstanding(intBPID)
            Else
                If clsData.IsDeleted Then
                    dtItem = BL.AccountPayable.ListDataDetail(txtID.Text.Trim)
                Else
                    dtItem = BL.AccountPayable.ListDataDetailWithOutstanding(txtID.Text.Trim, intBPID)
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
            grdStatus.DataSource = BL.AccountPayable.ListDataStatus(txtID.Text.Trim)
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

    Private Sub frmTraAccountPayableDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub frmTraAccountPayableDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                    strErrorMessage = "Total bayar tidak boleh lebih besar dari total maksimal pembayaran"
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

    Private Sub btnCoAOfOutgoingPayment_Click(sender As Object, e As EventArgs) Handles btnCoAOfOutgoingPayment.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .pubFilterGroup = VO.ChartOfAccount.FilterGroup.CashOrBank
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDOfOutgoingPayment = .pubLUdtRow.Item("ID")
                txtCoACodeOfOutgoingPayment.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfOutgoingPayment.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub txtBPCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBPCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsBP As VO.BusinessPartner = BL.BusinessPartner.GetDetail(txtBPCode.Text.Trim)
            If clsBP.ID = 0 Then
                UI.usForm.frmMessageBox("Kode Pemasok " & txtBPCode.Text.Trim & " tidak tersedia")
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

    Private Sub txtCoACodeOfOutgoingPayment_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCoACodeOfOutgoingPayment.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsCoA As VO.ChartOfAccount = BL.ChartOfAccount.GetDetail(txtCoACodeOfOutgoingPayment.Text.Trim)
            If clsCoA.ID = 0 Then
                UI.usForm.frmMessageBox("Kode akun " & txtCoACodeOfOutgoingPayment.Text.Trim & " tidak tersedia")
                intCoAIDOfOutgoingPayment = 0
                txtCoACodeOfOutgoingPayment.Text = ""
                txtCoANameOfOutgoingPayment.Text = ""
                Exit Sub
            End If
            intCoAIDOfOutgoingPayment = clsCoA.ID
            txtCoACodeOfOutgoingPayment.Text = clsCoA.Code
            txtCoANameOfOutgoingPayment.Text = clsCoA.Name
        End If
    End Sub

#End Region

End Class