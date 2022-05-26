Public Class frmMstBusinessPartnerAssign

#Region "Property"

    Private frmParent As frmMstBusinessPartner
    Private clsData As VO.BusinessPartner
    Private dtData As New DataTable
    Private intPos As Integer
    Property pubClsData As VO.BusinessPartner

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
        UI.usForm.SetGrid(grdView, "BPID", "BPID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramID", "Program ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 250, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CompanyID", "Company ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Perusahaan", 250, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "APBalance", "Saldo Hutang", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "ARBalance", "Saldo Piutang", 100, UI.usDefGrid.gReal2Num, False)
    End Sub

    Private Sub prvFillForm()
        Try
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
            dtData = BL.BusinessPartnerAssign.ListData(pubClsData.ID)
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

        Dim clsData As New VO.BusinessPartnerAssign
        Dim clsDataAll(grdView.RowCount - 1) As VO.BusinessPartnerAssign
        With grdView
            For i As Integer = 0 To .RowCount - 1
                clsData = New VO.BusinessPartnerAssign
                clsData.BPID = pubClsData.ID
                clsData.ProgramID = .GetRowCellValue(i, "ProgramID")
                clsData.CompanyID = .GetRowCellValue(i, "CompanyID")
                clsData.APBalance = .GetRowCellValue(i, "APBalance ")
                clsData.ARBalance = .GetRowCellValue(i, "ARBalance")
                clsDataAll(i) = clsData
            Next
        End With

        Try
            If BL.BusinessPartnerAssign.SaveData(pubClsData.ID, clsDataAll) Then
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvAdd()
        Dim frmDetail As New frmMstBusinessPartnerAssignItem
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
        Dim frmDetail As New frmMstBusinessPartnerAssignItem
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
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.AssignAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmMstBusinessPartnerAssign_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstBusinessPartnerAssign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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