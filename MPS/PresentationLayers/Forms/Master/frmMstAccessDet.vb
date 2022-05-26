Public Class frmMstAccessDet

#Region "Property"

    Private frmParent As frmMstAccess
    Private clsData As VO.Access
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
            Me.Text += " [new] "
        Else
            Me.Text += " [edit] "
        End If
    End Sub

    Private Sub prvFillForm()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.Access
                clsData = BL.Access.GetDetail(pubID)
                txtName.Text = clsData.Name
                ToolStripLogInc.Text = "Log Inc : " & clsData.LogInc
                ToolStripLogBy.Text = "Last Log : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If txtName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama belum diinput")
            txtName.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        clsData = New VO.Access
        clsData.ID = pubID
        clsData.Name = txtName.Text.Trim
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.Access.SaveData(pubIsNew, clsData)
            If pubIsNew Then
                UI.usForm.frmMessageBox("Save data success.")
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
        txtName.Text = ""
        txtRemarks.Text = ""
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmMstAccessDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Close this form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstAccessDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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