Public Class frmMstItemDet

#Region "Property"

    Private frmParent As frmMstItem
    Private clsData As VO.Item
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

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.MasterItem), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillUOM()
        Try
            UI.usForm.FillComboBox(cboUOMID, BL.UOM.ListDataForCombo, "ID", "Code")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillUOM()
    End Sub

    Private Sub prvFillForm()
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.Item
                clsData = BL.Item.GetDetail(pubID)

                txtCode.Text = clsData.Code
                txtName.Text = clsData.Name
                cboUOMID.SelectedValue = clsData.UomID
                txtBalanceQty.Value = clsData.BalanceQty
                txtTolerance.Value = clsData.Tolerance
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                If cboStatus.SelectedValue = VO.Status.Values.InActive Then cboStatus.Enabled = True
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        txtCode.Focus()
        If txtName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama belum diinput")
            txtName.Focus()
            Exit Sub
        ElseIf cboUOMID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Satuan belum dipilih")
            cboUOMID.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            cboStatus.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        clsData = New VO.Item
        clsData.ID = pubID
        clsData.Code = txtCode.Text.Trim
        clsData.Name = txtName.Text.Trim
        clsData.UomID = cboUOMID.SelectedValue
        clsData.SalesPrice = txtSalesPrice.Value
        clsData.PurchasePrice1 = 0
        clsData.PurchasePrice2 = 0
        clsData.Tolerance = txtTolerance.Value
        clsData.MinQty = 0
        clsData.BalanceQty = txtBalanceQty.Value
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.Item.SaveData(pubIsNew, clsData)
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
        txtCode.Text = ""
        txtName.Text = ""
        cboUOMID.SelectedIndex = -1
        txtBalanceQty.Value = 0
        cboStatus.SelectedValue = VO.Status.Values.Active
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvUserAccess()
        Toolbar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterItem, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

#Region "Form Handle"

    Private Sub frmMstItemDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstItemDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        Toolbar.SetIcon(Me)
        prvSetTitleForm()
        prvFillForm()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles Toolbar.ButtonClick
        Select Case e.Button.Name
            Case Toolbar.Buttons(cSave).Name : prvSave()
            Case Toolbar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

#End Region

End Class


