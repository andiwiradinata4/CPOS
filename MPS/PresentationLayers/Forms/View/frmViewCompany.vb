Public Class frmViewCompany

    Private dtData As New DataTable
    Private intPos As Integer = 0
    Public pubLUdtRow As DataRow
    Public pubIsLookUpGet As Boolean = False

    Private Const _
       cGet = 0, cClose = 1

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Nama Perusahaan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Address", "Alamat", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cGet).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            If MPSLib.UI.usUserApp.IsSuperUser Then
                dtData = BL.Company.ListData
            Else
                Dim dsHelper As New DataSetHelper
                dtData = dsHelper.SelectGroupByInto("Company", MPSLib.UI.usUserApp.AccessList, "CompanyID,CompanyName,Address", "", "CompanyID,CompanyName,Address")
            End If
            grdMain.DataSource = dtData
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Private Sub prvGet()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        pubLUdtRow = grdView.GetDataRow(intPos)
        pubIsLookUpGet = True
        Me.Close()
    End Sub

#Region "Form Handle"

    Private Sub frmViewCompany_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmViewCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cGet).Name : prvGet()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub grdView_DoubleClick(sender As Object, e As EventArgs) Handles grdView.DoubleClick
        prvGet()
    End Sub

#End Region

End Class