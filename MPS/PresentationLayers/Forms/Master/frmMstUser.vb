Public Class frmMstUser

    Public pubLUdtRow As DataRow
    Public pubIsLookUp As Boolean = False
    Public pubIsLookUpGet As Boolean = False
    Private intPos As Integer = 0
    Private clsData As New VO.User

    Private Const _
       cGet = 0, cSep1 = 1, cNew = 2, cDetail = 3, cDelete = 4, cSep2 = 5, cResetPassword = 6, cChangePassword = 7, cUserAccess = 8, cDuplicateUserAccess = 9, cSep3 = 10, cRefresh = 11, cClose = 12

    Private Sub prvSetTitleForm()
        If pubIsLookUp Then
            Me.Text += " [Lookup] "
        End If
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "User ID", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "StaffID", "Staff ID", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Name", "Nama", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Password", "Password", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "Position", "Posisi", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "IDStatus", "IDStatus", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CreatedBy", "Dibuat Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CreatedDate", "Tanggal Buat", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogBy", "Diedit Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "LogDate", "Tanggal Edit", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "StatusInfo", "Status", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cGet).Visible = pubIsLookUp
            .Item(cSep1).Visible = pubIsLookUp
            .Item(cGet).Enabled = bolEnable
            .Item(cDetail).Enabled = bolEnable
            .Item(cDelete).Enabled = bolEnable
            .Item(cResetPassword).Enabled = bolEnable
            .Item(cChangePassword).Enabled = bolEnable
            .Item(cUserAccess).Enabled = bolEnable
        End With
    End Sub

    Private Function prvGetData() As VO.User
        Dim voReturn As New VO.User
        voReturn.ID = grdView.GetRowCellValue(intPos, "ID")
        voReturn.StaffID = grdView.GetRowCellValue(intPos, "StaffID")
        voReturn.Name = grdView.GetRowCellValue(intPos, "Name")
        voReturn.Position = grdView.GetRowCellValue(intPos, "Position")
        Return voReturn
    End Function

    Private Sub prvQuery()
        Try
            Dim dtData As DataTable = BL.User.ListData
            If MPSLib.UI.usUserApp.IsSuperUser = False Then
                For Each dr As DataRow In dtData.Rows
                    If dr.Item("ID") = "ANDI.WIRADINATA" Then
                        dr.Delete()
                    End If
                Next
            End If
            dtData.AcceptChanges()
            grdMain.DataSource = dtData
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Public Sub pubRefresh(Optional ByVal strSearch As String = "")
        With grdView
            If Not grdView.FocusedValue Is Nothing And strSearch = "" Then
                strSearch = grdView.GetDataRow(grdView.FocusedRowHandle).Item("ID")
            End If
            prvQuery()
            If grdView.RowCount > 0 Then UI.usForm.GridMoveRow(grdView, "ID", strSearch)
        End With
    End Sub

    Private Sub prvGet()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If Not pubIsLookUp Then Exit Sub
        If grdView.GetRowCellValue(intPos, "IDStatus") = VO.Status.Values.InActive Then
            UI.usForm.frmMessageBox("Tidak dapat pilih user " & grdView.GetRowCellValue(intPos, "ID") & ". Dikarenakan user tersebut sudah tidak aktif")
            Exit Sub
        Else
            pubLUdtRow = grdView.GetDataRow(grdView.FocusedRowHandle)
            pubIsLookUpGet = True
            Me.Close()
        End If
    End Sub

    Private Sub prvNew()
        Dim frmDetail As New frmMstUserDet
        With frmDetail
            .pubIsNew = True
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvDetail()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstUserDet
        With frmDetail
            .pubIsNew = False
            .pubID = grdView.GetRowCellValue(intPos, "ID")
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            If .pubIsSave Then pubRefresh()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If Not UI.usForm.frmAskQuestion("Hapus nama  " & grdView.GetRowCellValue(intPos, "Name") & "?") Then Exit Sub
        Try
            BL.User.DeleteData(grdView.GetRowCellValue(intPos, "ID"))
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "ID"))
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvResetPassword()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If Not UI.usForm.frmAskQuestion("Reset password " & grdView.GetRowCellValue(intPos, "Name") & "?") Then Exit Sub
        clsData = prvGetData()
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.Password = "123456"
        Try
            BL.User.ResetPassword(clsData)
            UI.usForm.frmMessageBox("Reset password berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "ID"))
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvChangePassword()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()

        Dim frmDetail As New frmMstUserChangePassword
        With frmDetail
            .pubID = clsData.ID
            .ShowDialog()
        End With
    End Sub

    Private Sub prvSetUserAccess()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        Dim frmDetail As New frmMstUserUserAccess
        With frmDetail
            .pubUserID = clsData.ID
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvDuplicateUserAccess()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        Dim frmDetail As New frmMstUserUserAccessDuplicate
        With frmDetail
            .pubNewUserID = clsData.ID
            .ShowDialog()
        End With
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cNew).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.NewAccess)
            .Item(cDelete).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.DeleteAccess)
            .Item(cResetPassword).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.ResetPasswordAccess)
            .Item(cChangePassword).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.ChangePasswordAccess)
            .Item(cUserAccess).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.SetupUserAccessAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmMstUser_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Close this form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvQuery()
        prvUserAccess()
        If Not pubIsLookUp Then Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        ElseIf e.Button.Name = ToolBar.Buttons(cNew).Name Then
            prvNew()
        ElseIf e.Button.Name = ToolBar.Buttons(cRefresh).Name Then
            pubRefresh()
        ElseIf grdView.FocusedRowHandle >= 0 Then
            Select Case e.Button.Name
                Case ToolBar.Buttons(cGet).Name : prvGet()
                Case ToolBar.Buttons(cDetail).Name : prvDetail()
                Case ToolBar.Buttons(cDelete).Name : prvDelete()
                Case ToolBar.Buttons(cResetPassword).Name : prvResetPassword()
                Case ToolBar.Buttons(cChangePassword).Name : prvChangePassword()
                Case ToolBar.Buttons(cUserAccess).Name : prvSetUserAccess()
                Case ToolBar.Buttons(cDuplicateUserAccess).Name : prvDuplicateUserAccess()
            End Select
        End If
    End Sub

    Private Sub grdView_DoubleClick(sender As Object, e As EventArgs) Handles grdView.DoubleClick
        If pubIsLookUp Then
            prvGet()
        End If
    End Sub

    Private Sub grdView_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdView.RowStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If (e.RowHandle >= 0) Then
            Dim intStatusID As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IDStatus"))
            If intStatusID = VO.Status.Values.InActive And e.Appearance.BackColor <> Color.Salmon Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        End If
    End Sub

#End Region

End Class


