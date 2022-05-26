Public Class frmViewProgramCompany

    Private dtData As New DataTable
    Private intPos As Integer = 0
    Public pubLUdtRow As DataRow
    Public pubIsChoose As Boolean = False

    Private Const _
       cChoose = 0, cClose = 1

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ProgramID", "ProgramID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Nama Perusahaan", 350, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Address", "Alamat", 300, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CompanyInitial", "CompanyInitial", 100, UI.usDefGrid.gString, False)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cChoose).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            Dim dsHelper As New DataSetHelper
            dtData = dsHelper.SelectGroupByInto("Program", MPSLib.UI.usUserApp.AccessList, "ProgramID,ProgramName,CompanyID,CompanyName,Address,CompanyInitial", "", "ProgramID,ProgramName,CompanyID,CompanyName,Address,CompanyInitial")
            grdMain.DataSource = dtData
            grdView.Columns("ProgramName").GroupIndex = 0
            grdView.ExpandAllGroups()

            If dtData.Rows.Count = 1 Then
                pubLUdtRow = grdView.GetDataRow(0)
                pubIsChoose = True
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Private Sub prvChoose()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        pubLUdtRow = grdView.GetDataRow(grdView.FocusedRowHandle)
        pubIsChoose = True
        Me.Close()
    End Sub

#Region "Form Handle"

    Private Sub frmViewProgramCompany_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Application.Exit()
        End If
    End Sub

    Private Sub frmViewProgramCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cChoose).Name : prvChoose()
            Case ToolBar.Buttons(cClose).Name : Application.Exit()
        End Select
    End Sub

    Private Sub grdView_DoubleClick(sender As Object, e As EventArgs) Handles grdView.DoubleClick
        prvChoose()
    End Sub

    Private Sub grdMain_KeyDown(sender As Object, e As KeyEventArgs) Handles grdMain.KeyDown
        If e.KeyCode = Keys.Enter Then
            prvChoose()
        End If
    End Sub

#End Region

End Class