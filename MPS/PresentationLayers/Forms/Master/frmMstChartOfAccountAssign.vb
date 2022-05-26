Public Class frmMstChartOfAccountAssign

#Region "Property"

    Private frmParent As frmMstChartOfAccount
    Private clsData As VO.ChartOfAccount
    Private dtData As New DataTable
    Private intPos As Integer
    Property pubClsData As VO.ChartOfAccount

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1, _
       cAdd = 0, cEdit = 1, cDelete = 2

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "COAID", "COAID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramID", "Program ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 250, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CompanyID", "Company ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Perusahaan", 250, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "FirstBalance", "Saldo Awal", 250, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "FirstBalanceDate", "Pertanggal", 250, UI.usDefGrid.gSmallDate, True, False)
    End Sub

    Private Sub prvFillForm()
        Try
            txtCode.Text = pubClsData.Code
            txtName.Text = pubClsData.Name
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Public Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBarDet.Buttons
            .Item(cEdit).Enabled = bolEnable
            .Item(cDelete).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            dtData = BL.ChartOfAccountAssign.ListData(pubClsData.ID)
            grdMain.DataSource = dtData
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            prvSetButton()
        End Try
    End Sub

    Private Sub prvSave()
        If grdView.RowCount = 0 Then
            UI.usForm.frmMessageBox("Tambah item terlebih dahulu")
            grdView.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        Dim clsData As New VO.ChartOfAccountAssign
        Dim clsDataAll(grdView.RowCount - 1) As VO.ChartOfAccountAssign
        With grdView
            For i As Integer = 0 To .RowCount - 1
                clsData = New VO.ChartOfAccountAssign
                clsData.COAID = pubClsData.ID
                clsData.ProgramID = .GetRowCellValue(i, "ProgramID")
                clsData.CompanyID = .GetRowCellValue(i, "CompanyID")
                clsData.FirstBalance = .GetRowCellValue(i, "FirstBalance")
                clsData.FirstBalanceDate = .GetRowCellValue(i, "FirstBalanceDate")
                clsDataAll(i) = clsData
            Next
        End With

        Try
            If BL.ChartOfAccountAssign.SaveData(pubClsData.ID, clsDataAll) Then
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvAdd()
        Dim frmDetail As New frmMstChartOfAccountAssignItem
        With frmDetail
            .pubIsNew = True
            .pubDtItem = dtData
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvEdit()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstChartOfAccountAssignItem
        With frmDetail
            .pubIsNew = False
            .pubID = grdView.GetRowCellValue(intPos, "ID")
            .pubSelectedRow = grdView.GetDataRow(intPos)
            .pubDtItem = dtData
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            grdView.BestFitColumns()
            prvSetButton()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim strID As String = grdView.GetRowCellValue(intPos, "ID")
        For i As Integer = 0 To dtData.Rows.Count - 1
            If dtData.Rows(i).Item("ID") = strID Then
                dtData.Rows(i).Delete()
                Exit For
            End If
        Next
        dtData.AcceptChanges()
        grdView.BestFitColumns()
        prvSetButton()
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterChartOfAccount, VO.Access.Values.AssignAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmMstChartOfAccountAssign_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstChartOfAccountAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDet.SetIcon(Me)
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

    Private Sub ToolBarDet_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDet.ButtonClick
        Select Case e.Button.Text
            Case "Tambah" : prvAdd()
            Case "Edit" : prvEdit()
            Case "Hapus" : prvDelete()
        End Select
    End Sub

#End Region

End Class