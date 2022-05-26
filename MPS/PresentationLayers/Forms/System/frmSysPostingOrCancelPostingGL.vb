Public Class frmSysPostingOrCancelPostingGL

    Private clsData As New VO.PostGL
    Private intCompanyID As Integer
    Private intPos As Integer = 0

    Private Const _
       cClose = 0, _
       cCancelPosting = 0, cRefresh = 1

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdItemView, "ProgramID", "ProgramID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "ID", "ID", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "DateFrom", "Dari Tanggal", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdItemView, "DateTo", "Sampai Tanggal", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdItemView, "PostedBy", "Diposting Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "PostedDate", "Tanggal Posting", 100, UI.usDefGrid.gFullDate)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdItemView.RowCount > 0, True, False)
        With ToolBarDetail.Buttons
            .Item(cCancelPosting).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvDefaultFilter()
        intCompanyID = MPSLib.UI.usUserApp.CompanyID
        txtCompanyName.Text = MPSLib.UI.usUserApp.CompanyName
    End Sub

    Private Sub prvGetLastPosted()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            clsData = New VO.PostGL
            clsData = BL.PostGL.GetDetailLast(intCompanyID, MPSLib.UI.usUserApp.ProgramID)

            If Not clsData.ID Is Nothing Then
                'dtpDateFrom.MinDate = clsData.DateTo.Date.AddDays(1)
                dtpDateFrom.Value = clsData.DateTo.Date.AddDays(1)
                'dtpDateFrom.Enabled = False
                dtpDateTo.Value = clsData.DateTo.Date.AddDays(1)
            Else
                dtpDateFrom.MinDate = "2000/01/01"
                dtpDateFrom.Value = VO.DefaultServer.StartFrom
                'dtpDateFrom.Enabled = False
                dtpDateTo.Value = Today.Date
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Private Function prvGetData() As VO.PostGL
        Dim clsReturn As New VO.PostGL
        clsReturn.ProgramID = grdItemView.GetRowCellValue(intPos, "ProgramID")
        clsReturn.CompanyID = grdItemView.GetRowCellValue(intPos, "CompanyID")
        clsReturn.ID = grdItemView.GetRowCellValue(intPos, "ID")
        clsReturn.DateFrom = grdItemView.GetRowCellValue(intPos, "DateFrom")
        clsReturn.DateTo = grdItemView.GetRowCellValue(intPos, "DateTo")
        clsReturn.PostedBy = grdItemView.GetRowCellValue(intPos, "PostedBy")
        clsReturn.PostedDate = grdItemView.GetRowCellValue(intPos, "PostedDate")
        Return clsReturn
    End Function

    Private Sub prvQuery()
        ToolBar.Focus()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdItem.DataSource = BL.PostGL.ListData(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom2.Value.Date, dtpDateTo2.Value.Date)
            grdItemView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvPost()
        ToolBar.Focus()
        If dtpDateFrom.Value.Date > dtpDateTo.Value.Date Then
            UI.usForm.frmMessageBox("Periode salah")
            dtpDateFrom.Focus()
            Exit Sub
        End If
        If Not UI.usForm.frmAskQuestion("Posting periode " & Format(dtpDateFrom.Value.Date, "dd MMMM yyyy") & " sampai " & Format(dtpDateTo.Value.Date, "dd MMMM yyyy") & "?") Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Me.Refresh()
        Try
            clsData = New VO.PostGL
            clsData.CompanyID = intCompanyID
            clsData.ProgramID = MPSLib.UI.usUserApp.ProgramID
            clsData.DateFrom = dtpDateFrom.Value.Date
            clsData.DateTo = dtpDateTo.Value.Date
            clsData.PostedBy = MPSLib.UI.usUserApp.UserID
            clsData.LogBy = MPSLib.UI.usUserApp.UserID
            If BL.PostGL.PostData(clsData) Then
                pgMain.Value = 100
                UI.usForm.frmMessageBox("Posting data berhasil")
                prvGetLastPosted()
                prvQuery()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvCancelPost()
        intPos = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        If Not UI.usForm.frmAskQuestion("Cancel Posting ID: " & clsData.ID & "?") Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            If BL.PostGL.UnpostData(clsData) Then
                pgMain.Value = 100
                UI.usForm.frmMessageBox("Cancel Posting data berhasil")
                prvGetLastPosted()
                prvQuery()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
            End If
        End With
    End Sub

    Private Sub prvUserAccess()
        With ToolBarDetail.Buttons
            .Item(cCancelPosting).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.SettingPostingTransaction, VO.Access.Values.DeleteAccess)
            btnPosting.Enabled = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.SettingPostingTransaction, VO.Access.Values.NewAccess)
        End With
    End Sub

    Private Sub prvSetProgressBar(ByVal intMax As Integer)
        pgMain.Value = 0
        pgMain.Maximum = intMax
    End Sub

    Private Sub prvRefreshProgressBar()
        pgMain.Value += 1
        Me.Refresh()
    End Sub

#Region "Form Handle"

    Private Sub frmSysPostingGL_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmSysPostingGL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDetail.SetIcon(Me)
        prvSetGrid()
        prvDefaultFilter()
        prvGetLastPosted()
        dtpDateFrom2.Value = VO.DefaultServer.StartFrom
        dtpDateTo2.Value = Today.Date
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        End If
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Cancel Posting" : prvCancelPost()
            Case "Refresh" : prvQuery()
        End Select
    End Sub

    Private Sub btnPosting_Click(sender As Object, e As EventArgs) Handles btnPosting.Click
        prvPost()
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

#End Region

End Class