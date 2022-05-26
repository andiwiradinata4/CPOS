Public Class frmMstModules

    Public pubLUdtRow() As DataRow
    Public pubIsLookUp As Boolean = False
    Public pubIsLookUpGet As Boolean = False
    Private dtData As New DataTable
    Private intPos As Integer = 0

    Private Const _
       cGet = 0, cSep1 = 1, cNew = 2, cDetail = 3, cDelete = 4, cStatus = 5, cAccess = 6, cSep2 = 7, cRefresh = 8, cClose = 9

    Private Sub prvSetTitleForm()
        If pubIsLookUp Then
            Me.Text += " [Lookup] "
        End If
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "Pick", "Pilih", 100, UI.usDefGrid.gBoolean, pubIsLookUp, False)
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Name", "Nama", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "IsDeleted", "IsDeleted", 100, UI.usDefGrid.gBoolean, False)
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
            .Item(cStatus).Enabled = bolEnable
            .Item(cAccess).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            dtData = BL.Modules.ListData()
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
        ToolBar.Focus()
        Dim drPick() As DataRow = dtData.Select("Pick=true")

        If drPick.Count = 0 Then
            UI.usForm.frmMessageBox("Pilih item terlebih dahulu")
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Ambil semua item yang terpilih?") Then Exit Sub

        pubLUdtRow = drPick
        pubIsLookUpGet = True
        Me.Close()
    End Sub

    Private Sub prvNew()
        Dim frmDetail As New frmMstModulesDet
        With frmDetail
            .pubIsNew = True
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvDetail()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstModulesDet
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
        If Not UI.usForm.frmAskQuestion("Hapus status dengan ID " & grdView.GetRowCellValue(intPos, "ID") & "?") Then Exit Sub
        Try
            BL.Modules.DeleteData(grdView.GetRowCellValue(intPos, "ID"))
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "Code"))
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvStatus()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmmstStatusModules
        With frmDetail
            .pubRefID = grdView.GetRowCellValue(intPos, "ID")
            .pubRefName = grdView.GetRowCellValue(intPos, "Name")
            .pubFilterBy = VO.StatusModules.FilterBy.ModulesID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub prvAccess()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstModulesAccess
        With frmDetail
            .pubRefID = grdView.GetRowCellValue(intPos, "ID")
            .pubRefName = grdView.GetRowCellValue(intPos, "Name")
            .pubFilterBy = VO.ModulesAccess.FilterBy.ModulesID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
        End With
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cNew).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.NewAccess)
            .Item(cDelete).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.DeleteAccess)
            .Item(cAccess).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.SetupUserAccessAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmMstModules_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstModules_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                Case ToolBar.Buttons(cStatus).Name : prvStatus()
                Case ToolBar.Buttons(cAccess).Name : prvAccess()
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
            Dim IsDeleted As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IsDeleted"))
            If IsDeleted = "Checked" Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        End If
    End Sub

#End Region

End Class


