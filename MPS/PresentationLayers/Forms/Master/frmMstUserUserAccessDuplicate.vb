Public Class frmMstUserUserAccessDuplicate

    Public Property pubNewUserID As String = ""

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvChooseUser()
        Dim frmDetail As New frmMstUser
        With frmDetail
            .pubIsLookUp = True
            .ShowDialog()
            If .pubIsLookUpGet Then
                txtBaseUserID.Text = .pubLUdtRow.Item("ID")
            End If
        End With
    End Sub

    Private Sub prvSave()
        txtNewUserID.Focus()
        If txtBaseUserID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih User ID yang ingin dijadikan pedoman")
            txtBaseUserID.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Salin user akses dari " & txtBaseUserID.Text.Trim & " ke user " & txtNewUserID.Text.Trim & "?") Then Exit Sub
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value += 30

        Try
            If BL.UserAccess.DuplicateUserAccess(txtBaseUserID.Text.Trim, txtNewUserID.Text.Trim) Then
                pgMain.Value = 100
                Me.Cursor = Cursors.Default
                UI.usForm.frmMessageBox("Proses salin user akses berhasil")
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            pgMain.Value = 100
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmMstUserUserAccessDuplicate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Close this form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstUserUserAccessDuplicate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        txtNewUserID.Text = pubNewUserID
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub btnBaseUserID_Click(sender As Object, e As EventArgs) Handles btnBaseUserID.Click
        prvChooseUser()
    End Sub

#End Region

End Class