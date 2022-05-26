Public Class frmMstBusinessPartnerDet

#Region "Property"
    Private frmParent As frmMstBusinessPartner
    Private clsData As VO.BusinessPartner
    Property pubID As Integer
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub
#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvSetTitleForm()
        If pubIsNew Then
            Me.Text += " [baru] "
        Else
            Me.Text += " [edit] "
        End If
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "BPID", "BPID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.MasterBusinessPartners), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillPaymentTerm()
        Try
            UI.usForm.FillComboBox(cboPaymentTerm, BL.PaymentTerm.ListDataForCombo, "ID", "Name")
            cboPaymentTerm.SelectedIndex = 0
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillPaymentTerm()
    End Sub

    Private Sub prvFillForm()
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.BusinessPartner
                clsData = BL.BusinessPartner.GetDetail(pubID)
                txtCode.Text = clsData.Code
                txtName.Text = clsData.Name
                txtAddress.Text = clsData.Address
                txtPICName.Text = clsData.PICName
                txtPICPhoneNumber.Text = clsData.PICPhoneNumber
                cboPaymentTerm.SelectedValue = clsData.PaymentTermID
                chkIsUsePurchaseLimit.Checked = clsData.IsUsePurchaseLimit
                txtMaxPurchaseLimit.Value = clsData.MaxPurchaseLimit
                txtSalesPrice.Value = clsData.SalesPrice
                txtPurchasePrice1.Value = clsData.PurchasePrice1
                txtPurchasePrice2.Value = clsData.PurchasePrice2
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                If cboStatus.SelectedValue = VO.Status.Values.InActive Then cboStatus.Enabled = True
                If clsData.IsUsePurchaseLimit Then txtMaxPurchaseLimit.Enabled = True
                txtSalesPrice.Enabled = False
                txtPurchasePrice1.Enabled = False
                txtPurchasePrice2.Enabled = False
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If txtName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama belum diinput")
            tcBP.SelectedTab = tpMain
            txtName.Focus()
            Exit Sub
        ElseIf cboPaymentTerm.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Jenis pembayaran harus dipilih agar dapat dijadikan nilai default saat transaksi")
            tcBP.SelectedTab = tpMain
            cboPaymentTerm.Focus()
            Exit Sub
        ElseIf chkIsUsePurchaseLimit.Checked AndAlso txtMaxPurchaseLimit.Value <= 0 Then
            UI.usForm.frmMessageBox(chkIsUsePurchaseLimit.Text & " harus lebih besar dari 0 jika tercentang")
            tcBP.SelectedTab = tpMain
            txtMaxPurchaseLimit.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            tcBP.SelectedTab = tpMain
            cboStatus.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        clsData = New VO.BusinessPartner
        clsData.ID = pubID
        clsData.Code = txtCode.Text.Trim
        clsData.Name = txtName.Text.Trim
        clsData.Address = txtAddress.Text.Trim
        clsData.PICName = txtPICName.Text.Trim
        clsData.PICPhoneNumber = txtPICPhoneNumber.Text.Trim
        clsData.PaymentTermID = cboPaymentTerm.SelectedValue
        clsData.IsUsePurchaseLimit = chkIsUsePurchaseLimit.Checked
        clsData.MaxPurchaseLimit = txtMaxPurchaseLimit.Value
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.BusinessPartner.SaveData(pubIsNew, clsData)
            If pubIsNew Then
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                frmParent.pubRefresh(clsData.Code)
                prvClear()
            Else
                pubIsSave = True
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvClear()
        txtName.Focus()
        txtName.Text = ""
        txtAddress.Text = ""
        txtPICName.Text = ""
        txtPICPhoneNumber.Text = ""
        cboPaymentTerm.SelectedIndex = 0
        chkIsUsePurchaseLimit.Checked = False
        txtMaxPurchaseLimit.Value = 0
        txtSalesPrice.Value = 0
        txtPurchasePrice1.Value = 0
        txtPurchasePrice2.Value = 0
        cboStatus.SelectedValue = VO.Status.Values.Active
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
        End With
    End Sub

#Region "History Handle"

    Private Sub prvQueryHistory()
        Me.Cursor = Cursors.WaitCursor
        Try
            grdStatus.DataSource = BL.BusinessPartner.ListDataStatus(pubID)
            grdStatusView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region

#Region "Form Handle"

    Private Sub frmMstBusinessPartnerDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        ElseIf e.KeyCode = Keys.F1 Then
            tcBP.SelectedTab = tpMain
        ElseIf e.KeyCode = Keys.F2 Then
            tcBP.SelectedTab = tpStatus
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstBusinessPartnerDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQueryHistory()
        prvUserAccess()

        AddHandler chkIsUsePurchaseLimit.CheckedChanged, AddressOf chkIsUsePurchaseLimit_CheckedChanged
    End Sub

    Private Sub ToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub chkIsUsePurchaseLimit_CheckedChanged(sender As Object, e As EventArgs)
        txtMaxPurchaseLimit.Enabled = chkIsUsePurchaseLimit.Checked
    End Sub

#End Region

End Class


