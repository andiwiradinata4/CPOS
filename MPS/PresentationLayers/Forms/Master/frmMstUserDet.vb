Public Class frmMstUserDet

#Region "Property"

    Private frmParent As frmMstUser
    Private clsData As VO.User
    Private dtCompany As New DataTable
    Property pubID As String = ""
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1, _
       cAdd = 0, cDelete = 1

    Private Sub prvSetTitleForm()
        If pubIsNew Then
            Me.Text += " [baru] "
        Else
            Me.Text += " [edit] "
        End If
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdItemView.RowCount > 0, True, False)
        With ToolBarDetail.Buttons
            .Item(cDelete).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdItemView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "CompanyName", "Nama", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "Address", "Alamat", 300, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "CompanyInitial", "CompanyInitial", 100, UI.usDefGrid.gString, False)
    End Sub

    Private Sub prvFillCombo()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.MasterUOM), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillForm()
        txtPassword.CharacterCasing = CharacterCasing.Normal
        txtConfirmPassword.CharacterCasing = CharacterCasing.Normal
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
                txtID.ReadOnly = False
                txtID.BackColor = Color.White
            Else
                clsData = New VO.User
                clsData = BL.User.GetDetail(pubID)
                txtID.Text = clsData.ID
                txtStaffID.Text = clsData.StaffID
                txtName.Text = clsData.Name
                txtPassword.Text = clsData.Password
                txtConfirmPassword.Text = clsData.Password
                txtPosition.Text = clsData.Position
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)

                If cboStatus.SelectedValue = VO.Status.Values.InActive Then cboStatus.Enabled = True
                txtPassword.ReadOnly = True
                txtConfirmPassword.ReadOnly = True
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If pubIsNew And txtID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("User ID belum diinput")
            txtID.Focus()
            Exit Sub
        ElseIf txtName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama belum diinput")
            txtName.Focus()
            Exit Sub
        ElseIf txtPassword.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Password belum diinput")
            txtPassword.Focus()
            Exit Sub
        ElseIf txtConfirmPassword.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Konfirmasi password belum diinput")
            txtConfirmPassword.Focus()
            Exit Sub
        ElseIf txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
            UI.usForm.frmMessageBox("Password dan konfirmasi password tidak sama")
            txtConfirmPassword.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            cboStatus.Focus()
            Exit Sub
        End If

        clsData = New VO.User
        clsData.ID = txtID.Text.Trim
        clsData.StaffID = txtStaffID.Text.Trim
        clsData.Name = txtName.Text.Trim
        clsData.Password = Encryption.Encrypt(txtPassword.Text.Trim)
        clsData.Position = txtPosition.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Dim clsCompany As New VO.UserCompany
        Dim clsCompanyAll(dtCompany.Rows.Count - 1) As VO.UserCompany

        For i As Integer = 0 To dtCompany.Rows.Count - 1
            clsCompany = New VO.UserCompany
            clsCompany.CompanyID = dtCompany.Rows(i).Item("CompanyID")
            clsCompanyAll(i) = clsCompany
        Next

        Try
            BL.User.SaveData(pubIsNew, clsData, clsCompanyAll)
            If pubIsNew Then
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                frmParent.pubRefresh(clsData.ID)
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
        txtID.Text = ""
        txtStaffID.Text = ""
        txtName.Text = ""
        txtPassword.Text = ""
        txtConfirmPassword.Text = ""
        txtPosition.Text = ""
        cboStatus.SelectedValue = VO.Status.Values.Active
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

    Private Sub prvQuery()
        Try
            dtCompany = BL.UserCompany.ListDataByUserID(pubID)
            grdItem.DataSource = dtCompany
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            prvSetButton()
        End Try
    End Sub

    Private Sub prvAdd()
        Dim frmDetail As New frmMstCompany
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                Dim drExists() = dtCompany.Select("CompanyID=" & .pubLUdtRow.Item("CompanyID"))
                If drExists.Count > 0 Then
                    UI.usForm.frmMessageBox("Nama " & .pubLUdtRow.Item("CompanyName") & " telah ada sebelumnya")
                    Exit Sub
                End If

                Dim drNew As DataRow
                drNew = dtCompany.NewRow
                With drNew
                    .BeginEdit()
                    .Item("CompanyID") = frmDetail.pubLUdtRow.Item("CompanyID")
                    .Item("CompanyName") = frmDetail.pubLUdtRow.Item("CompanyName")
                    .Item("Address") = frmDetail.pubLUdtRow.Item("Address")
                    .Item("CompanyInitial") = frmDetail.pubLUdtRow.Item("CompanyInitial")
                    .EndEdit()
                End With
                dtCompany.Rows.Add(drNew)
            End If
        End With
    End Sub

    Private Sub prvDelete()
        Dim intPos As Integer = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim strCompanyID As String = grdItemView.GetRowCellValue(intPos, "CompanyID")
        For i As Integer = 0 To dtCompany.Rows.Count - 1
            If dtCompany.Rows(i).Item("CompanyID") = strCompanyID Then
                dtCompany.Rows(i).Delete()
                Exit For
            End If
        Next
        dtCompany.AcceptChanges()
    End Sub

#Region "Form Handle"

    Private Sub frmMstUserDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Close this form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstUserDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDetail.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQuery()
        prvUserAccess()
    End Sub
    
    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Name
            Case ToolBarDetail.Buttons(cAdd).Name : prvAdd()
            Case ToolBarDetail.Buttons(cDelete).Name : prvDelete()
        End Select
    End Sub

#End Region

End Class


