Public Class frmMstChartOfAccountDet

#Region "Property"
    Private frmParent As frmMstChartOfAccount
    Private clsData As VO.ChartOfAccount
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
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.MasterChartOfAccount), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillGroup()
        Try
            UI.usForm.FillComboBox(cboChartOfAccountGroup, BL.ChartOfAccountGroup.ListDataForCombo, "ID", "Name")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillGroup()
    End Sub

    Private Sub prvFillForm()
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.ChartOfAccount
                clsData = BL.ChartOfAccount.GetDetail(pubID)
                txtCode.Text = clsData.Code
                cboChartOfAccountGroup.SelectedValue = clsData.AccountGroupID
                txtName.Text = clsData.Name
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If txtCode.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Kode akun belum diinput")
            txtCode.Focus()
            Exit Sub
        ElseIf txtName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama akun belum diinput")
            txtName.Focus()
            Exit Sub
        ElseIf cboChartOfAccountGroup.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Group kosong. Mohon untuk tutup form dan buka kembali")
            cboChartOfAccountGroup.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            cboStatus.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        clsData = New VO.ChartOfAccount
        clsData.ID = pubID
        clsData.Code = txtCode.Text.Trim
        clsData.Name = txtName.Text.Trim
        clsData.FirstBalance = 0
        clsData.FirstBalanceDate = "2000/01/01"
        clsData.AccountGroupID = cboChartOfAccountGroup.SelectedValue
        clsData.AccountGroupName = cboChartOfAccountGroup.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.ChartOfAccount.SaveData(pubIsNew, clsData)
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
        cboStatus.SelectedValue = VO.Status.Values.Active
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterChartOfAccount, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

#Region "Form Handle"

    Private Sub frmMstChartOfAccountDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstChartOfAccountDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvFillForm()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

#End Region
    
End Class