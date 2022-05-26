Public Class frmMstBusinessPartner

    Public pubLUdtRow As DataRow
    Public pubIsLookUp As Boolean = False
    Public pubIsLookUpGet As Boolean = False
    Public pubOnAmount As Decimal = 0
    Public pubCompanyID As Integer = 0
    Public pubProgramID As Integer = 0
    Private intPos As Integer = 0

    Private Const _
       cGet = 0, cSep1 = 1, cNew = 2, cDetail = 3, cDelete = 4, cHistory = 5, cAssign = 6, cPrice = 7, cSep2 = 8, cRefresh = 9, cClose = 10

    Private Sub prvSetTitleForm()
        If pubIsLookUp Then
            Me.Text += " [Lookup] "
        End If
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Code", "Kode", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "Name", "Nama", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Address", "Alamat", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PICName", "Nama PIC", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PICPhoneNumber", "No. HP PIC", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PaymentTermID", "PaymentTermID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "IsUsePurchaseLimit", "Set Limit Pembelian", 100, UI.usDefGrid.gBoolean, False)
        UI.usForm.SetGrid(grdView, "MaxPurchaseLimit", "Maks. Limit Pembelian", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "IsAssign", "Assign", 100, UI.usDefGrid.gBoolean, False)
        UI.usForm.SetGrid(grdView, "APBalance", "Saldo Hutang", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "ARBalance", "Saldo Piutang", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "SalesPrice", "Harga Jual", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "PurchasePrice1", "Harga Beli 1", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "PurchasePrice2", "Harga Beli 2", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "IDStatus", "IDStatus", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CreatedBy", "Dibuat Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CreatedDate", "Tanggal Buat", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogBy", "Diedit Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "LogDate", "Tanggal Edit", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "StatusInfo", "Status", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cGet).Visible = pubIsLookUp
            .Item(cSep1).Visible = pubIsLookUp
            .Item(cGet).Enabled = bolEnable
            .Item(cDetail).Enabled = bolEnable
            .Item(cDelete).Enabled = bolEnable
            .Item(cHistory).Enabled = bolEnable
            .Item(cAssign).Enabled = bolEnable
            .Item(cPrice).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Try
            grdMain.DataSource = BL.BusinessPartner.ListData(pubOnAmount, pubCompanyID, pubProgramID)
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
        prvSetButton()
    End Sub

    Public Sub pubRefresh(Optional ByVal strSearch As String = "")
        With grdView
            If Not grdView.FocusedValue Is Nothing And strSearch = "" Then
                strSearch = grdView.GetDataRow(grdView.FocusedRowHandle).Item("Code")
            End If
            prvQuery()
            If grdView.RowCount > 0 Then UI.usForm.GridMoveRow(grdView, "Code", strSearch)
        End With
    End Sub

    Private Function prvGetData() As VO.BusinessPartner
        Dim clsReturn As New VO.BusinessPartner
        clsReturn.ID = grdView.GetRowCellValue(intPos, "ID")
        clsReturn.Name = grdView.GetRowCellValue(intPos, "Name")
        clsReturn.Address = grdView.GetRowCellValue(intPos, "Address")
        clsReturn.PICName = grdView.GetRowCellValue(intPos, "PICName")
        clsReturn.PICPhoneNumber = grdView.GetRowCellValue(intPos, "PICPhoneNumber")
        clsReturn.PaymentTermID = grdView.GetRowCellValue(intPos, "PaymentTermID")
        clsReturn.IsUsePurchaseLimit = grdView.GetRowCellValue(intPos, "IsUsePurchaseLimit")
        clsReturn.MaxPurchaseLimit = grdView.GetRowCellValue(intPos, "MaxPurchaseLimit")
        Return clsReturn
    End Function

    Private Sub prvGet()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If Not pubIsLookUp Then Exit Sub
        If grdView.GetRowCellValue(intPos, "IDStatus") = VO.Status.Values.InActive Then
            UI.usForm.frmMessageBox("Tidak dapat pilih rekan bisnis " & grdView.GetRowCellValue(intPos, "Name") & ". Dikarenakan barang tersebut sudah tidak aktif")
            Exit Sub
        Else
            pubLUdtRow = grdView.GetDataRow(grdView.FocusedRowHandle)
            pubIsLookUpGet = True
            Me.Close()
        End If
    End Sub

    Private Sub prvNew()
        Dim frmDetail As New frmMstBusinessPartnerDet
        With frmDetail
            .pubIsNew = True
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvDetail()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstBusinessPartnerDet
        With frmDetail
            .pubIsNew = False
            .pubID = grdView.GetRowCellValue(intPos, "ID")
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            If .pubIsSave Then pubRefresh()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        If Not UI.usForm.frmAskQuestion("Hapus nama " & grdView.GetRowCellValue(intPos, "Name") & "?") Then Exit Sub
        Try
            BL.BusinessPartner.DeleteData(grdView.GetRowCellValue(intPos, "ID"))
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "ID"))
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvHistory()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstBusinessPartnerHistory
        With frmDetail
            .pubBPID = grdView.GetRowCellValue(intPos, "ID")
            .ShowDialog()
        End With
    End Sub

    Private Sub prvAssign()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstBusinessPartnerAssign
        With frmDetail
            .pubClsData = prvGetData()
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvPrice()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmMstBusinessPartnerPrice
        With frmDetail
            .pubClsBP = prvGetData()
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            '.Item(cNew).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.NewAccess)
            '.Item(cDelete).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.DeleteAccess)
            .Item(cAssign).Visible = False
            .Item(cHistory).Visible = False 'BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.HistoryAccess)
            .Item(cPrice).Visible = False 'BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.ChangePriceAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmMstBusinessPartner_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            If pubIsLookUp Then prvGet()
        End If
    End Sub

    Private Sub frmMstBusinessPartner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvQuery()
        prvUserAccess()
        If Not pubIsLookUp Then Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        ElseIf e.Button.Name = ToolBar.Buttons(cNew).Name Then
            prvNew()
        ElseIf e.Button.Name = ToolBar.Buttons(cRefresh).Name Then
            pubRefresh()
        ElseIf grdView.FocusedRowHandle >= 0 Then
            Select Case e.Button.Name
                Case ToolBar.Buttons(cGet).Name : prvGet()
                Case ToolBar.Buttons(cAssign).Name : prvAssign()
                Case ToolBar.Buttons(cDetail).Name : prvDetail()
                Case ToolBar.Buttons(cDelete).Name : prvDelete()
                Case ToolBar.Buttons(cHistory).Name : prvHistory()
                Case ToolBar.Buttons(cPrice).Name : prvPrice()
            End Select
        End If
    End Sub

    Private Sub grdView_DoubleClick(sender As Object, e As EventArgs) Handles grdView.DoubleClick
        If pubIsLookUp Then
            prvGet()
        End If
    End Sub

    Private Sub grdView_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdView.RowStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If (e.RowHandle >= 0) Then
            Dim intStatusID As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IDStatus"))
            If intStatusID = VO.Status.Values.InActive And e.Appearance.BackColor <> Color.Salmon Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        End If
    End Sub

#End Region

End Class


