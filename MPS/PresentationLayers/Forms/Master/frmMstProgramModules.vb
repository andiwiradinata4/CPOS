Public Class frmMstProgramModules

#Region "Property"

    Private clsData As VO.ProgramModules
    Property pubFilterBy As VO.ProgramModules.FilterBy
    Property pubIsSave As Boolean = False
    Property pubRefID As Integer = 0
    Property pubRefName As String = ""
    Private dtData As New DataTable
    Private intPos As Integer

    Private Const _
       cSave = 0, cClose = 1,
       cAdd = 0, cDelete = 1

#End Region

    Private Sub prvSetTitleForm()
        If pubFilterBy = VO.ProgramModules.FilterBy.ProgramID Then
            Me.Text += "Program Detail"
            lblInfo.Text += "Program"
        ElseIf pubFilterBy = VO.ProgramModules.FilterBy.ModulesID Then
            Me.Text += "Modules Detail"
            lblInfo.Text += "Modules"
        End If
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ModulesID", "Modules ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramID", "Program ID", 100, UI.usDefGrid.gIntNum, False)
        If pubFilterBy = VO.ProgramModules.FilterBy.ProgramID Then
            UI.usForm.SetGrid(grdView, "ModulesName", "Modules", 250, UI.usDefGrid.gString)
        ElseIf pubFilterBy = VO.ProgramModules.FilterBy.ModulesID Then
            UI.usForm.SetGrid(grdView, "ProgramName", "Program", 250, UI.usDefGrid.gString)
        End If
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBarDet.Buttons
            .Item(cDelete).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            If pubFilterBy = VO.ProgramModules.FilterBy.ProgramID Then
                dtData = BL.ProgramModules.ListDataByProgramID(pubRefID)
            ElseIf pubFilterBy = VO.ProgramModules.FilterBy.ModulesID Then
                dtData = BL.ProgramModules.ListDataByModulesID(pubRefID)
            End If

            grdMain.DataSource = dtData
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Private Sub prvAdd()
        If pubFilterBy = VO.ProgramModules.FilterBy.ProgramID Then
            Dim frmDetail As New frmMstModules
            With frmDetail
                .pubIsLookUp = True
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                If .pubIsLookUpGet Then
                    Dim drNewRow As DataRow
                    For Each drRow As DataRow In .pubLUdtRow
                        drNewRow = dtData.NewRow
                        With drNewRow
                            .BeginEdit()
                            .Item("ID") = 0
                            .Item("ProgramID") = pubRefID
                            .Item("ModulesID") = drRow.Item("ID")
                            .Item("ModulesName") = drRow.Item("Name")
                            .EndEdit()
                        End With
                        dtData.Rows.Add(drNewRow)
                    Next
                    dtData.AcceptChanges()
                    prvSetButton()
                End If
            End With
        ElseIf pubFilterBy = VO.ProgramModules.FilterBy.ModulesID Then
            Dim frmDetail As New frmMstProgram
            With frmDetail
                .pubIsLookUp = True
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                If .pubIsLookUpGet Then
                    Dim drNewRow As DataRow
                    For Each drRow As DataRow In .pubLUdtRow
                        drNewRow = dtData.NewRow
                        With drNewRow
                            .BeginEdit()
                            .Item("ID") = 0
                            .Item("ProgramID") = drRow.Item("ID")
                            .Item("ModulesID") = pubRefID
                            .Item("ProgramName") = drRow.Item("Name")
                            .EndEdit()
                        End With
                        dtData.Rows.Add(drNewRow)
                    Next
                    dtData.AcceptChanges()
                    prvSetButton()
                End If
            End With
        End If
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If pubFilterBy = VO.ProgramModules.FilterBy.ProgramID Then
            Dim intModuleID As Integer = grdView.GetRowCellValue(intPos, "ModulesID")
            For Each drRow As DataRow In dtData.Rows
                If drRow.Item("ModulesID") = intModuleID Then
                    drRow.Delete()
                    Exit For
                End If
            Next
        ElseIf pubFilterBy = VO.ProgramModules.FilterBy.ModulesID Then
            Dim intProgramID As Integer = grdView.GetRowCellValue(intPos, "ProgramID")
            For Each drRow As DataRow In dtData.Rows
                If drRow.Item("ProgramID") = intProgramID Then
                    drRow.Delete()
                    Exit For
                End If
            Next
        End If

        dtData.AcceptChanges()
        grdMain.DataSource = dtData
        prvSetButton()
    End Sub

    Private Sub prvSave()
        If grdView.RowCount = 0 Then
            UI.usForm.frmMessageBox("Tambah item terlebih dahulu")
            grdView.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        Dim clsData As VO.ProgramModules
        Dim clsDataAll(grdView.RowCount - 1) As VO.ProgramModules
        With grdView
            For i As Integer = 0 To .RowCount - 1
                clsData = New VO.ProgramModules
                clsData.ModulesID = .GetRowCellValue(i, "ModulesID")
                clsData.ProgramID = .GetRowCellValue(i, "ProgramID")
                clsDataAll(i) = clsData
            Next
        End With

        Try
            If pubFilterBy = VO.ProgramModules.FilterBy.ProgramID Then
                BL.ProgramModules.SaveDataByProgramID(clsDataAll)
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                Me.Close()
            ElseIf pubFilterBy = VO.ProgramModules.FilterBy.ModulesID Then
                BL.ProgramModules.SaveDataByModulesID(clsDataAll)
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.NewAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmMstProgramModules_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstProgramModules_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDet.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        txtName.Text = pubRefName
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
            Case "Add" : prvAdd()
            Case "Delete" : prvDelete()
        End Select
    End Sub

#End Region
    
End Class