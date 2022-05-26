Public Class frmSysChooseCompany

    Private intPos As Integer = 0
    Private bolPick As Boolean = False

    Public ReadOnly Property pubIsPick As Boolean
        Get
            Return bolPick
        End Get
    End Property

    Private Const _
       cPick = 0, cClose = 1

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Name", "Nama", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Address", "Alamat", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PhoneNumber", "Nomor Telepon", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CompanyInitial", "Inisial Perusahaan", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cPick).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            grdMain.DataSource = BL.Company.ListDataForChooseDefault
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Private Sub prvPick()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        MPSLib.UI.usUserApp.CompanyID = grdView.GetRowCellValue(intPos, "ID")
        MPSLib.UI.usUserApp.CompanyName = grdView.GetRowCellValue(intPos, "Name")
        MPSLib.UI.usUserApp.CompanyAddress = grdView.GetRowCellValue(intPos, "Address")
        MPSLib.UI.usUserApp.CompanyPhoneNumber = grdView.GetRowCellValue(intPos, "PhoneNumber")
        MPSLib.UI.usUserApp.CompanyInitial = grdView.GetRowCellValue(intPos, "CompanyInitial")
        bolPick = True
        Me.Close()
    End Sub

#Region "Form Handle"

    Private Sub frmSysChooseCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cPick).Name Then
            prvPick()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Application.Exit()
        End If
    End Sub

#End Region

End Class