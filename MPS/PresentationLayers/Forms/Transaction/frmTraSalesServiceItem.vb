Public Class frmTraSalesServiceItem

#Region "Property"

    Private frmParent As frmTraSalesServiceDet
    Private dtParent As New DataTable
    Private bolIsNew As Boolean = False
    Private intItemID As Integer = 0
    Private drSelected As DataRow
    Private strID As String

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
        Try
            UI.usForm.FillComboBox(cboUOMID, BL.UOM.ListData, "ID", "Code")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvClear()
        txtItemCode.Focus()
        intItemID = 0
        txtItemCode.Text = ""
        txtItemName.Text = ""
        cboUOMID.SelectedIndex = -1
        txtQty.Value = 0
        txtPrice.Value = 0
        txtTotalPrice.Value = 0
        txtRemarks.Text = ""
    End Sub

    Private Sub prvFillForm()
        prvFillCombo()
        If bolIsNew Then
            prvClear()
        Else
            strID = drSelected.Item("ID")
            intItemID = drSelected.Item("ItemID")
            txtItemCode.Text = drSelected.Item("ItemCode")
            txtItemName.Text = drSelected.Item("ItemName")
            cboUOMID.SelectedValue = drSelected.Item("UomID")
            txtQty.Value = drSelected.Item("Qty")
            txtPrice.Value = drSelected.Item("Price")
            txtTotalPrice.Value = drSelected.Item("TotalPrice")
            txtRemarks.Text = drSelected.Item("Remarks")
        End If
    End Sub

    Private Sub prvSave()
        If txtItemCode.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih item terlebih dahulu")
            txtItemCode.Focus()
            Exit Sub
        ElseIf cboUOMID.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Satuan tidak valid")
            cboUOMID.Focus()
            Exit Sub
        ElseIf txtQty.Value <= 0 Then
            UI.usForm.frmMessageBox("Jumlah harus lebih besar dari 0")
            txtQty.Focus()
            Exit Sub
        ElseIf txtPrice.Value <= 0 Then
            UI.usForm.frmMessageBox("Harga harus lebih besar dari 0")
            txtPrice.Focus()
            Exit Sub
        End If

        If bolIsNew Then
            Dim dr As DataRow
            dr = dtParent.NewRow
            dr.BeginEdit()
            dr.Item("ID") = Guid.NewGuid
            dr.Item("ItemID") = intItemID
            dr.Item("ItemCode") = txtItemCode.Text.Trim
            dr.Item("ItemName") = txtItemName.Text.Trim
            dr.Item("Qty") = txtQty.Value
            dr.Item("UomID") = cboUOMID.SelectedValue
            dr.Item("UomCode") = cboUOMID.Text.Trim
            dr.Item("ReturnQty") = 0
            dr.Item("Price") = txtPrice.Value
            dr.Item("Disc") = 0
            dr.Item("Tax") = 0
            dr.Item("TotalPrice") = txtTotalPrice.Value
            dr.Item("Remarks") = txtRemarks.Text.Trim
            dr.EndEdit()
            dtParent.Rows.Add(dr)
            frmParent.grdItemView.BestFitColumns()
            prvClear()
        Else
            For Each dr As DataRow In dtParent.Rows
                If dr.Item("ID") = strID Then
                    dr.BeginEdit()
                    dr.Item("ID") = strID
                    dr.Item("ItemID") = intItemID
                    dr.Item("ItemCode") = txtItemCode.Text.Trim
                    dr.Item("ItemName") = txtItemName.Text.Trim
                    dr.Item("Qty") = txtQty.Value
                    dr.Item("UomID") = cboUOMID.SelectedValue
                    dr.Item("UomCode") = cboUOMID.Text.Trim
                    dr.Item("ReturnQty") = 0
                    dr.Item("Price") = txtPrice.Value
                    dr.Item("Disc") = 0
                    dr.Item("Tax") = 0
                    dr.Item("TotalPrice") = txtTotalPrice.Value
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
        Dim frmDetail As New frmMstItem
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intItemID = .pubLUdtRow.Item("ID")
                txtItemCode.Text = .pubLUdtRow.Item("Code")
                txtItemName.Text = .pubLUdtRow.Item("Name")
                cboUOMID.SelectedValue = .pubLUdtRow.Item("UomID")
                txtPrice.Value = .pubLUdtRow.Item("SalesPrice")
                txtPrice.Focus()
            End If
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmTraSalesServiceItem_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmTraSalesServiceItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub btnItem_Click(sender As Object, e As EventArgs) Handles btnItem.Click
        prvChooseItem()
    End Sub

    Private Sub txtItemCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItemCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsItem As VO.Item = BL.Item.GetDetail(txtItemCode.Text.Trim)
            If clsItem.ID = 0 Then
                UI.usForm.frmMessageBox("Kode barang " & txtItemCode.Text.Trim & " tidak tersedia")
                intItemID = 0
                txtItemCode.Text = ""
                txtItemName.Text = ""
                cboUOMID.SelectedValue = 0
                txtPrice.Value = 0
                Exit Sub
            End If
            intItemID = clsItem.ID
            txtItemCode.Text = clsItem.Code
            txtItemName.Text = clsItem.Name
            cboUOMID.SelectedValue = clsItem.UomID
            txtPrice.Value = clsItem.SalesPrice
            txtPrice.Focus()
        End If
    End Sub

    Private Sub txtValue_ValueChanged(sender As Object, e As EventArgs) Handles txtQty.ValueChanged, txtPrice.ValueChanged
        txtTotalPrice.Value = txtQty.Value * txtPrice.Value
    End Sub

#End Region

End Class