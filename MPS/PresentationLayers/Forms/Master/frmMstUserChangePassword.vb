Public Class frmMstUserChangePassword

#Region "Property"

    Property pubID As String = MPSLib.UI.usUserApp.UserID
    Private clsData As VO.User

#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvSave()
        If txtID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("User ID kosong")
            txtID.Focus()
            Exit Sub
        ElseIf txtOldPassword.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Password lama belum diinput")
            txtOldPassword.Focus()
            Exit Sub
        ElseIf txtConfirmPassword.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Konfirmasi password belum diinput")
            txtConfirmPassword.Focus()
            Exit Sub
        ElseIf txtNewPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
            UI.usForm.frmMessageBox("Password baru dan konfirmasi password tidak sama")
            txtConfirmPassword.Focus()
            Exit Sub
        End If

        clsData = New VO.User
        clsData.ID = txtID.Text.Trim
        clsData.Password = txtNewPassword.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.User.ChangePassword(clsData, txtOldPassword.Text.Trim)
            UI.usForm.frmMessageBox("Ubah password berhasil.")
            Me.Close()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmMstUserChangePassword_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstUserChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        txtID.Text = pubID
        txtOldPassword.CharacterCasing = CharacterCasing.Normal
        txtNewPassword.CharacterCasing = CharacterCasing.Normal
        txtConfirmPassword.CharacterCasing = CharacterCasing.Normal
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub chkViewOldPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkViewOldPassword.CheckedChanged
        txtOldPassword.UseSystemPasswordChar = Not chkViewOldPassword.Checked
    End Sub

    Private Sub chkViewNewPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkViewNewPassword.CheckedChanged
        txtNewPassword.UseSystemPasswordChar = Not chkViewNewPassword.Checked
    End Sub

    Private Sub chkViewConfirmPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkViewConfirmPassword.CheckedChanged
        txtConfirmPassword.UseSystemPasswordChar = Not chkViewConfirmPassword.Checked
    End Sub

#End Region

End Class