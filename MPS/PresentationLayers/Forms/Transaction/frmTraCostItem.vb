Public Class frmTraCostItem

#Region "Property"

    Private frmParent As frmTraCostDet
    Private dtParent As New DataTable
    Private bolIsNew As Boolean = False
    Private intCoAID As Integer = 0
    Private drSelected As DataRow
    Private strID As String
    Property pubCS As New VO.CS

    Public WriteOnly Property pubTableParent As DataTable
        Set(value As DataTable)
            dtParent = value
        End Set
    End Property

    Public WriteOnly Property pubIsNew As Boolean
        Set(value As Boolean)
            bolIsNew = value
        End Set
    End Property

    Public WriteOnly Property pubDatRowSelected As DataRow
        Set(value As DataRow)
            drSelected = value
        End Set
    End Property

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

    Private Sub prvClear()
        txtCoACode.Focus()
        intCoAID = 0
        txtCoACode.Text = ""
        txtCoAName.Text = ""
        txtRemarks.Text = ""
        txtAmount.Value = 0
    End Sub

    Private Sub prvFillForm()
        If bolIsNew Then
            prvClear()
        Else
            strID = drSelected.Item("ID")
            intCoAID = drSelected.Item("CoAID")
            txtCoACode.Text = drSelected.Item("CoACode")
            txtCoAName.Text = drSelected.Item("CoAName")
            txtAmount.Value = drSelected.Item("Amount")
            txtRemarks.Text = drSelected.Item("Remarks")
        End If
    End Sub

    Private Sub prvSave()
        If txtCoACode.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih akun terlebih dahulu")
            txtCoACode.Focus()
            Exit Sub
        ElseIf txtAmount.Value <= 0 Then
            UI.usForm.frmMessageBox("Nilai harus lebih besar dari 0")
            txtAmount.Focus()
            Exit Sub
        End If

        If bolIsNew Then
            Dim dr As DataRow
            dr = dtParent.NewRow
            dr.BeginEdit()
            dr.Item("ID") = Guid.NewGuid
            dr.Item("CostID") = ""
            dr.Item("CoAID") = intCoAID
            dr.Item("CoACode") = txtCoACode.Text.Trim
            dr.Item("CoAName") = txtCoAName.Text.Trim
            dr.Item("Remarks") = txtRemarks.Text.Trim
            dr.Item("Amount") = txtAmount.Value
            dr.EndEdit()
            dtParent.Rows.Add(dr)
            prvClear()
            frmParent.grdItemView.BestFitColumns()
            frmParent.pubCalculate()
        Else
            For Each dr As DataRow In dtParent.Rows
                If dr.Item("ID") = strID Then
                    dr.BeginEdit()
                    dr.Item("ID") = strID
                    dr.Item("CostID") = ""
                    dr.Item("CoAID") = intCoAID
                    dr.Item("CoACode") = txtCoACode.Text.Trim
                    dr.Item("CoAName") = txtCoAName.Text.Trim
                    dr.Item("Remarks") = txtRemarks.Text.Trim
                    dr.Item("Amount") = txtAmount.Value
                    dr.EndEdit()
                    frmParent.grdItemView.BestFitColumns()
                    Exit For
                End If
            Next
            Me.Close()
        End If
    End Sub

    Private Sub prvChooseItem()
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .pubFilterGroup = VO.ChartOfAccount.FilterGroup.Expense
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAID = .pubLUdtRow.Item("ID")
                txtCoACode.Text = .pubLUdtRow.Item("Code")
                txtCoAName.Text = .pubLUdtRow.Item("Name")
                txtRemarks.Focus()
            End If
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmTraCostItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmTraCostItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillForm()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnCoA_Click(sender As Object, e As EventArgs) Handles btnCoA.Click
        prvChooseItem()
    End Sub

    Private Sub txtCoACode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCoACode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsCoA As VO.ChartOfAccount = BL.ChartOfAccount.GetDetail(txtCoACode.Text.Trim)
            If clsCoA.ID = 0 Then
                UI.usForm.frmMessageBox("Kode akun " & txtCoACode.Text.Trim & " tidak tersedia")
                intCoAID = 0
                txtCoACode.Text = ""
                txtCoAName.Text = ""
                Exit Sub
            End If
            intCoAID = clsCoA.ID
            txtCoACode.Text = clsCoA.Code
            txtCoAName.Text = clsCoA.Name
            txtRemarks.Focus()
        End If
    End Sub

#End Region

End Class