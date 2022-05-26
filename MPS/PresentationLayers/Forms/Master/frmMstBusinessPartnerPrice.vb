Imports DevExpress.XtraGrid.Columns

Public Class frmMstBusinessPartnerPrice

#Region "Property"

    Private frmParent As frmMstBusinessPartner
    Private clsBP As VO.BusinessPartner
    Private dtData As New DataTable
    Private bolValid As Boolean = True
    Private intPos As Integer = 0

    Public WriteOnly Property pubClsBP As VO.BusinessPartner
        Set(value As VO.BusinessPartner)
            clsBP = value
        End Set
    End Property

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cRefresh = 0, cClose = 1, _
       cAdd = 0, cEdit = 1, cDelete = 2

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "DateFrom", "Dari Tanggal", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdView, "DateTo", "Sampai Tanggal", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdView, "SalesPrice", "Harga Jual", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "PurchasePrice1", "Harga Beli 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "PurchasePrice2", "Harga Beli 2", 100, UI.usDefGrid.gReal2Num)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBarDetail.Buttons
            .Item(cEdit).Enabled = bolEnable
            .Item(cDelete).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            grdMain.DataSource = BL.BusinessPartner.ListDataPrice(clsBP.ID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Public Sub pubRefresh(Optional ByVal strSearch As String = "")
        With grdView
            If Not grdView.FocusedValue Is Nothing And strSearch = "" Then
                strSearch = grdView.GetDataRow(grdView.FocusedRowHandle).Item("ID")
            End If
            prvQuery()
            If grdView.RowCount > 0 Then UI.usForm.GridMoveRow(grdView, "ID", strSearch)
        End With
    End Sub

    Private Sub prvClear()
        grdMain.DataSource = Nothing
        grdView.Columns.Clear()
        prvSetGrid()
        prvSetButton()
    End Sub

    Private Function prvGetData() As VO.BusinessPartnerPrice
        Dim clsReturn As New VO.BusinessPartnerPrice
        clsReturn.ID = grdView.GetRowCellValue(intPos, "ID")
        clsReturn.DateFrom = grdView.GetRowCellValue(intPos, "DateFrom")
        clsReturn.DateTo = grdView.GetRowCellValue(intPos, "DateTo")
        clsReturn.SalesPrice = grdView.GetRowCellValue(intPos, "SalesPrice")
        clsReturn.PurchasePrice1 = grdView.GetRowCellValue(intPos, "PurchasePrice1")
        clsReturn.PurchasePrice2 = grdView.GetRowCellValue(intPos, "PurchasePrice2")
        Return clsReturn
    End Function

    Private Sub prvAdd()
        Dim frmDetail As New frmMstBusinessPartnerPriceDet
        With frmDetail
            .pubIsNew = True
            .pubClsBP = clsBP
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvEdit()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstBusinessPartnerPriceDet
        With frmDetail
            .pubIsNew = False
            .pubClsBP = clsBP
            .pubID = grdView.GetRowCellValue(intPos, "ID")
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            If .pubIsSave Then pubRefresh()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If Not UI.usForm.frmAskQuestion("Hapus harga periode " & grdView.GetRowCellValue(intPos, "DateFrom") & " - " & grdView.GetRowCellValue(intPos, "DateTo") & "?") Then Exit Sub
        Try
            BL.BusinessPartner.DeleteDataPrice(grdView.GetRowCellValue(intPos, "ID"))
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "ID"))
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmMstBusinessPartnerPrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDetail.SetIcon(Me)
        prvSetGrid()
        dtpDateFrom.Value = Today.AddDays(-14)
        prvQuery()
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Text
            Case "Tambah" : prvAdd()
            Case "Edit" : prvEdit()
            Case "Hapus" : prvDelete()
        End Select
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cRefresh).Name : prvQuery()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        prvQuery()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        prvClear()
    End Sub

#End Region

End Class