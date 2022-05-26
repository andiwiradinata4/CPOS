Public Class frmTraJournalItem

#Region "Property"

    Private frmParent As frmTraJournalDet
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

    Private Sub prvFillCombo()
        cboPosition.Items.Add("DEBIT")
        cboPosition.Items.Add("KREDIT")
    End Sub

    Private Sub prvClear()
        txtCoACode.Focus()
        intCoAID = 0
        txtCoACode.Text = ""
        txtCoAName.Text = ""
        cboPosition.SelectedIndex = -1
        txtAmount.Value = 0
        txtRemarks.Text = ""
    End Sub

    Private Sub prvFillForm()
        prvFillCombo()
        If bolIsNew Then
            prvClear()
        Else
            strID = drSelected.Item("ID")
            intCoAID = drSelected.Item("CoAID")
            txtCoACode.Text = drSelected.Item("CoACode")
            txtCoAName.Text = drSelected.Item("CoAName")
            cboPosition.SelectedIndex = IIf(drSelected.Item("DebitAmount") > 0, 0, 1)
            txtAmount.Value = IIf(drSelected.Item("DebitAmount") > 0, drSelected.Item("DebitAmount"), drSelected.Item("CreditAmount"))
        End If
    End Sub

    Private Sub prvSave()
        If txtCoACode.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih akun terlebih dahulu")
            txtCoACode.Focus()
            Exit Sub
        ElseIf cboPosition.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih posisi terlebih dahulu")
            cboPosition.Focus()
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
            dr.Item("JournalID") = ""
            dr.Item("CoAID") = intCoAID
            dr.Item("CoACode") = txtCoACode.Text.Trim
            dr.Item("CoAName") = txtCoAName.Text.Trim
            If cboPosition.SelectedIndex = 0 Then
                dr.Item("DebitAmount") = txtAmount.Value
                dr.Item("CreditAmount") = 0
            Else
                dr.Item("DebitAmount") = 0
                dr.Item("CreditAmount") = txtAmount.Value
            End If
            dr.Item("Remarks") = txtRemarks.Text.Trim
            dr.EndEdit()
            dtParent.Rows.Add(dr)
            prvClear()
            frmParent.grdItemView.BestFitColumns()
        Else
            For Each dr As DataRow In dtParent.Rows
                If dr.Item("ID") = strID Then
                    dr.BeginEdit()
                    dr.Item("ID") = strID
                    dr.Item("JournalID") = ""
                    dr.Item("CoAID") = intCoAID
                    dr.Item("CoACode") = txtCoACode.Text.Trim
                    dr.Item("CoAName") = txtCoAName.Text.Trim
                    If cboPosition.SelectedIndex = 0 Then
                        dr.Item("DebitAmount") = txtAmount.Value
                        dr.Item("CreditAmount") = 0
                    Else
                        dr.Item("DebitAmount") = 0
                        dr.Item("CreditAmount") = txtAmount.Value
                    End If
                    dr.Item("Remarks") = txtRemarks.Text.Trim
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
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAID = .pubLUdtRow.Item("ID")
                txtCoACode.Text = .pubLUdtRow.Item("Code")
                txtCoAName.Text = .pubLUdtRow.Item("Name")
                cboPosition.Focus()
            End If
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmTraJournalItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmTraJournalItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            cboPosition.Focus()
        End If
    End Sub

#End Region

End Class