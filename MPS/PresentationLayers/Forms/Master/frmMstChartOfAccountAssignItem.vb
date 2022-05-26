Public Class frmMstChartOfAccountAssignItem

#Region "Property"

    Private frmParent As frmMstChartOfAccountAssign
    Private intProgramID As Integer
    Private intCompanyID As Integer
    Private intPos As Integer = 0
    Private strID As String
    Property pubDtItem As New DataTable
    Property pubSelectedRow As DataRow
    Property pubIsNew As Boolean = False

    Public WriteOnly Property pubID As String
        Set(value As String)
            strID = value
        End Set
    End Property

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvFillForm()
        If pubIsNew Then
            prvClear()
        Else
            ToolBar.Buttons(cSave).Text = "Edit"
            intProgramID = pubSelectedRow.Item("ProgramID")
            txtProgramName.Text = pubSelectedRow.Item("ProgramName")
            intCompanyID = pubSelectedRow.Item("CompanyID")
            txtCompanyName.Text = pubSelectedRow.Item("CompanyName")
            txtFirstBalance.Value = pubSelectedRow.Item("FirstBalance")
            dtpFirstBalanceDate.Value = pubSelectedRow.Item("FirstBalanceDate")
        End If
    End Sub

    Private Sub prvChooseProgram()
        Dim frmDetail As New frmViewProgram
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intProgramID = .pubLUdtRow.Item("ProgramID")
                txtProgramName.Text = .pubLUdtRow.Item("ProgramName")
                txtCompanyName.Focus()
            End If
        End With
    End Sub

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
                txtFirstBalance.Focus()
            End If
        End With
    End Sub

    Private Sub prvSave()
        txtProgramName.Focus()
        If intProgramID = 0 Then
            UI.usForm.frmMessageBox("Pilih program terlebih dahulu")
            Exit Sub
        ElseIf txtProgramName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih program terlebih dahulu")
            txtProgramName.Focus()
            Exit Sub
        ElseIf txtCompanyName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih perusahaan terlebih dahulu")
            txtCompanyName.Focus()
            Exit Sub
        End If

        If pubIsNew Then
            Dim drFind() As DataRow = pubDtItem.Select("ProgramID=" & intProgramID & " AND CompanyID=" & intCompanyID)
            If drFind.Count > 0 Then
                UI.usForm.frmMessageBox("Program " & txtProgramName.Text.Trim & " dengan perusahaan " & txtCompanyName.Text.Trim & " telah ada sebelumnya")
                txtProgramName.Focus()
                Exit Sub
            End If

            Dim drNew As DataRow
            drNew = pubDtItem.NewRow
            With drNew
                .BeginEdit()
                .Item("ID") = Guid.NewGuid
                .Item("COAID") = 0
                .Item("ProgramID") = intProgramID
                .Item("ProgramName") = txtProgramName.Text.Trim
                .Item("CompanyID") = intCompanyID
                .Item("CompanyName") = txtCompanyName.Text.Trim
                .Item("FirstBalance") = txtFirstBalance.Value
                .Item("FirstBalanceDate") = dtpFirstBalanceDate.Value
                .EndEdit()
            End With
            pubDtItem.Rows.Add(drNew)
            prvClear()
            frmParent.grdView.BestFitColumns()
            frmParent.prvSetButton()
        Else
            Dim drFind() As DataRow = pubDtItem.Select("ProgramID=" & intProgramID & " AND CompanyID=" & intCompanyID & " AND ID<>'" & strID & "'")
            If drFind.Count > 0 Then
                UI.usForm.frmMessageBox("Program " & txtProgramName.Text.Trim & " dengan perusahaan " & txtCompanyName.Text.Trim & " telah ada sebelumnya")
                txtProgramName.Focus()
                Exit Sub
            End If
            For Each dr As DataRow In pubDtItem.Rows
                If dr.Item("ID") = strID Then
                    With dr
                        .BeginEdit()
                        .Item("ID") = strID
                        .Item("COAID") = 0
                        .Item("ProgramID") = intProgramID
                        .Item("ProgramName") = txtProgramName.Text.Trim
                        .Item("CompanyID") = intCompanyID
                        .Item("CompanyName") = txtCompanyName.Text.Trim
                        .Item("FirstBalance") = txtFirstBalance.Value
                        .Item("FirstBalanceDate") = dtpFirstBalanceDate.Value
                        .EndEdit()
                    End With
                    Exit For
                End If
            Next
        End If
        Me.Close()
    End Sub

    Private Sub prvClear()
        intProgramID = 0
        txtProgramName.Text = ""
        intCompanyID = 0
        txtCompanyName.Text = ""
        txtFirstBalance.Value = 0
        dtpFirstBalanceDate.Value = "2000/01/01"
    End Sub

#Region "Form Handle"

    Private Sub frmMstChartOfAccountAssignItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmMstChartOfAccountAssignItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillForm()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub btnProgram_Click(sender As Object, e As EventArgs) Handles btnProgram.Click
        prvChooseProgram()
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

#End Region

End Class