Imports DevExpress.XtraGrid
Public Class frmTraSalesServiceDet

#Region "Property"

    Private frmParent As frmTraSalesService
    Private clsData As VO.SalesService
    Private intBPID As Integer = 0
    Private dtItem As New DataTable, dtDownPayment As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = ""
    Private intItemID As Integer = 0
    Private intServiceType As Integer = VO.SalesService.Type.RentalAlatBerat
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Property pubCS As New VO.CS
    Private bolExport As Boolean = False

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1, cPrintFaktur = 2, _
       cAddItem = 0, cEditItem = 1, cDeleteItem = 2, _
       cAddDP = 0, cDeleteDP = 1

    Private Sub prvSetTitleForm()
        If pubIsNew Then
            Me.Text += " [baru] "
        Else
            Me.Text += " [edit] "
        End If
    End Sub

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdItemView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "SalesID", "SalesID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdItemView, "ItemID", "ItemID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "ItemCode", "Kode Barang", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "ItemName", "Nama Barang", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "Qty", "Jumlah", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdItemView, "UomID", "UomID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdItemView, "UomCode", "Satuan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdItemView, "ReturnQty", "Jumlah Retur", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdItemView, "Price", "Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdItemView, "Disc", "Disc", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdItemView, "Tax", "Tax", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdItemView, "TotalPrice", "Total Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdItemView, "Remarks", "Keterangan", 100, UI.usDefGrid.gString)

        UI.usForm.SetGrid(grdDownPaymentView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdDownPaymentView, "DPID", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdDownPaymentView, "ReferenceID", "ReferenceID", 200, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdDownPaymentView, "TotalAmount", "Total Panjar", 200, UI.usDefGrid.gReal2Num)
        grdDownPaymentView.Columns("TotalAmount").ColumnEdit = rpiAmount

        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "SalesID", "SalesID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionSales), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillPaymentTerm()
        Try
            UI.usForm.FillComboBox(cboPaymentTerm, BL.PaymentTerm.ListDataForCombo, "ID", "Name")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillPaymentTerm()
    End Sub

    Private Sub prvFillForm()
        pgMain.Value = 30
        txtPPNPercentage.Minimum = 0
        txtPPNPercentage.Maximum = 100
        Me.Cursor = Cursors.WaitCursor
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.SalesService
                clsData = BL.SalesService.GetDetail(pubID)
                txtID.Text = clsData.ID
                txtSalesNo.Text = clsData.SalesNo
                intServiceType = clsData.ServiceType
                intBPID = clsData.BPID
                txtCustomerCode.Text = clsData.CustomerCode
                txtCustomerName.Text = clsData.BPName
                dtpSalesDate.Value = clsData.SalesDate
                dtpDueDate.Value = clsData.DueDate
                cboPaymentTerm.SelectedValue = clsData.PaymentTerm
                txtPPNPercentage.Value = clsData.PPNPercentage
                txtSPKNumber.Text = clsData.SPKNumber
                txtBillNumber.Text = clsData.BillNumber
                cboStatus.SelectedValue = clsData.IDStatus
                txtRemarks.Text = clsData.Remarks
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
                strJournalID = clsData.JournalID
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvSave()
        ToolBar.Focus()
        If txtCustomerName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih pelanggan terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtCustomerCode.Focus()
            Exit Sub
        ElseIf cboPaymentTerm.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih jenis pembayaran terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            cboPaymentTerm.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            tcHeader.SelectedTab = tpMain
            cboStatus.Focus()
            Exit Sub
        ElseIf grdItemView.RowCount = 0 Then
            UI.usForm.frmMessageBox("Item kosong. Mohon untuk diinput item terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            grdItemView.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data penjualan jasa?") Then Exit Sub

        '# Sales Service
        clsData = New VO.SalesService
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.SalesNo = txtSalesNo.Text.Trim
        clsData.ServiceType = intServiceType
        clsData.BPID = intBPID
        clsData.BPName = txtCustomerName.Text.Trim
        clsData.SalesDate = dtpSalesDate.Value
        clsData.PaymentTerm = cboPaymentTerm.SelectedValue
        clsData.DueDate = dtpDueDate.Value
        clsData.PPNPercentage = txtPPNPercentage.Value
        clsData.SPKNumber = txtSPKNumber.Text.Trim
        clsData.BillNumber = txtBillNumber.Text.Trim
        clsData.TotalPrice = grdItemView.Columns("TotalPrice").SummaryItem.SummaryValue
        clsData.TotalPPN = grdItemView.Columns("TotalPrice").SummaryItem.SummaryValue / 100 * txtPPNPercentage.Value
        clsData.GrandTotal = clsData.TotalPrice + clsData.TotalPPN + clsData.TotalPPH
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID

        '# Sales Service Detail
        Dim clsDetailAll(grdItemView.RowCount - 1) As VO.SalesServiceDet
        With grdItemView
            Dim clsDetail As VO.SalesServiceDet
            For i As Integer = 0 To .RowCount - 1
                clsDetail = New VO.SalesServiceDet
                clsDetail.SalesServiceID = txtID.Text.Trim
                clsDetail.ItemID = .GetRowCellValue(i, "ItemID")
                clsDetail.UomID = .GetRowCellValue(i, "UomID")
                clsDetail.Qty = .GetRowCellValue(i, "Qty")
                clsDetail.Price = .GetRowCellValue(i, "Price")
                clsDetail.Disc = .GetRowCellValue(i, "Disc")
                clsDetail.Tax = txtPPNPercentage.Value
                clsDetail.TotalPrice = .GetRowCellValue(i, "TotalPrice")
                clsDetail.Remarks = .GetRowCellValue(i, "Remarks")
                clsDetailAll(i) = clsDetail
            Next
        End With

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim strID As String = BL.SalesService.SaveData(pubIsNew, clsData, clsDetailAll)
            pgMain.Value = 80
            If strID.Trim <> "" Then
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor penjualan: " & strID)
                    frmParent.pubRefresh(clsData.ID)
                    prvClear()
                    prvQueryItem()
                    prvQueryDownPayment()
                    prvQueryHistory()
                Else
                    pubIsSave = True
                    Me.Close()
                End If
            Else
                pgMain.Value = 100
                UI.usForm.frmMessageBox("Proses simpan data tidak berhasil")
                Exit Sub
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvClear()
        tcHeader.SelectedTab = tpMain
        txtID.Text = ""
        txtSalesNo.Text = ""
        intBPID = 0
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        dtpSalesDate.Value = Now
        dtpDueDate.Value = Now.Date
        cboPaymentTerm.SelectedIndex = -1
        txtSPKNumber.Text = ""
        txtBillNumber.Text = ""
        txtRemarks.Text = ""
        cboStatus.SelectedValue = VO.Status.Values.Draft
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)

        ToolBar.Buttons(cPrintFaktur).Enabled = False
    End Sub

    Private Sub prvChooseCustomer()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intBPID = .pubLUdtRow.Item("ID")
                txtCustomerCode.Text = .pubLUdtRow.Item("Code")
                txtCustomerName.Text = .pubLUdtRow.Item("Name")
                cboPaymentTerm.SelectedValue = .pubLUdtRow.Item("PaymentTermID")
            End If
        End With
    End Sub

    Private Sub prvSumGrid()
        '# Down Payment
        Dim SumTotalAmount As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "Total Pembayaran: {0:#,##0.00}")

        If grdDownPaymentView.Columns("TotalAmount").SummaryText.Trim = "" Then
            grdDownPaymentView.Columns("TotalAmount").Summary.Add(SumTotalAmount)
        End If

        '# Sales Service Detail
        Dim SumTotalPrice As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "Total Price: {0:#,##0.00}")

        If grdItemView.Columns("TotalPrice").SummaryText.Trim = "" Then
            grdItemView.Columns("TotalPrice").Summary.Add(SumTotalPrice)
        End If
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionSalesService, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionSalesService, VO.Access.Values.ExportReportAccess)
    End Sub

    Private Sub prvPrintBonFaktur()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim crReport As New rptBonFakturSalesServiceVer00
            With crReport
                .DataSource = BL.SalesService.PrintBonFaktur(txtID.Text.Trim)
                .DisplayName = Me.Text
                .CreateDocument(True)
                .ShowPreviewMarginLines = False
                .ShowPrintMarginsWarning = False
            End With

            Dim frmDetail As New frmReportPreview
            With frmDetail
                .docViewer.DocumentSource = crReport
                .pgExportButton.Enabled = bolExport
                .Text = Me.Text & " - " & VO.Reports.PrintOut
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
        End Try
    End Sub

#Region "Item Handle"

    Private Sub prvSetButtonItem()
        Dim bolEnabled As Boolean = IIf(grdItemView.RowCount = 0, False, True)
        With ToolBarDetail
            .Buttons(cEditItem).Enabled = bolEnabled
            .Buttons(cDeleteItem).Enabled = bolEnabled
        End With
    End Sub

    Private Sub prvQueryItem()
        Try
            dtItem = BL.SalesService.ListDataDetail(txtID.Text.Trim)
            grdItem.DataSource = dtItem
            prvSumGrid()
            grdItemView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            prvSetButtonItem()
        End Try
    End Sub

    Private Sub prvAddItem()
        Dim frmDetail As New frmTraSalesServiceItem
        With frmDetail
            .pubIsNew = True
            .pubTableParent = dtItem
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            prvSetButtonItem()
        End With
    End Sub

    Private Sub prvEditItem()
        intPos = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim frmDetail As New frmTraSalesServiceItem
        With frmDetail
            .pubIsNew = False
            .pubTableParent = dtItem
            .pubDatRowSelected = grdItemView.GetDataRow(intPos)
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            prvSetButtonItem()
        End With
    End Sub

    Private Sub prvDeleteItem()
        intPos = grdItemView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim strID As String = grdItemView.GetRowCellValue(intPos, "ID")
        For i As Integer = 0 To dtItem.Rows.Count - 1
            If dtItem.Rows(i).Item("ID") = strID Then
                dtItem.Rows(i).Delete()
                Exit For
            End If
        Next
        dtItem.AcceptChanges()
        grdItem.DataSource = dtItem
        prvSetButtonItem()
    End Sub

#End Region

#Region "Down Payment Handle"

    Private Sub prvSetButtonDP()
        Dim bolEnabled As Boolean = IIf(grdDownPaymentView.RowCount = 0, False, True)
        With ToolBarDP
            .Buttons(cDeleteDP).Enabled = bolEnabled
        End With
    End Sub

    Private Sub prvQueryDownPayment()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            dtDownPayment = BL.DownPayment.ListDataDetailByReferenceID(txtID.Text.Trim)
            grdDownPayment.DataSource = dtDownPayment
            prvSumGrid()
            grdDownPaymentView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            prvSetButtonDP()
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvAddDP()
        If intBPID = 0 Then
            UI.usForm.frmMessageBox("Pilih pelanggan terlebih dahulu")
            txtCustomerCode.Focus()
            Exit Sub
        End If

        Dim frmDetail As New frmViewOutstandingDownPayment
        With frmDetail
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .pubBPID = intBPID
            .pubBPID2 = 0
            .pubDPType = VO.DownPayment.Type.SalesService
            .pubTableParent = dtDownPayment
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                Dim drNew As DataRow
                For Each dr As DataRow In .pubLURow
                    drNew = dtDownPayment.NewRow
                    drNew.BeginEdit()
                    drNew.Item("ID") = Guid.NewGuid
                    drNew.Item("DPID") = dr.Item("ID")
                    drNew.Item("ReferenceID") = txtID.Text.Trim
                    If grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue = 0 Then
                        drNew.Item("TotalAmount") = IIf(grdItemView.Columns("TotalPrice").SummaryItem.SummaryValue > dr.Item("TotalAmount"), dr.Item("TotalAmount"), grdItemView.Columns("TotalPrice").SummaryItem.SummaryValue)
                    Else
                        drNew.Item("TotalAmount") = IIf(grdItemView.Columns("TotalPrice").SummaryItem.SummaryValue > grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue + dr.Item("TotalAmount"), _
                                                        dr.Item("TotalAmount"), _
                                                        grdItemView.Columns("TotalPrice").SummaryItem.SummaryValue - grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue)
                    End If
                    drNew.EndEdit()
                    dtDownPayment.Rows.Add(drNew)
                Next
                dtDownPayment.AcceptChanges()
                grdDownPayment.DataSource = dtDownPayment
                grdDownPaymentView.BestFitColumns()
                prvSetButtonDP()
            End If
        End With
    End Sub

    Private Sub prvDeleteDP()
        intPos = grdDownPaymentView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim strID As String = grdDownPaymentView.GetRowCellValue(intPos, "ID")
        For i As Integer = 0 To dtDownPayment.Rows.Count - 1
            If dtDownPayment.Rows(i).Item("ID") = strID Then
                dtDownPayment.Rows(i).Delete()
                dtDownPayment.AcceptChanges()
                Exit For
            End If
        Next
        grdDownPayment.DataSource = dtDownPayment
        grdDownPaymentView.BestFitColumns()
        prvSetButtonDP()
    End Sub

#End Region

#Region "History Handle"

    Private Sub prvQueryHistory()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdStatus.DataSource = BL.Sales.ListDataStatus(txtID.Text.Trim)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

#End Region

#Region "Form Handle"

    Private Sub frmTraSalesServiceDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            tcHeader.SelectedTab = tpMain
        ElseIf e.KeyCode = Keys.F2 Then
            tcHeader.SelectedTab = tpDownPayment
        ElseIf e.KeyCode = Keys.F3 Then
            tcHeader.SelectedTab = tpHistory
        ElseIf e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmTraSalesServiceDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDetail.SetIcon(Me)
        ToolBarDP.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQueryItem()
        prvQueryDownPayment()
        prvQueryHistory()
        prvUserAccess()

        If pubCS.ProgramID = VO.Program.Values.RentalAlatBerat Then
            intServiceType = VO.SalesService.Type.RentalAlatBerat
        ElseIf pubCS.ProgramID = VO.Program.Values.RentalTruk Then
            intServiceType = VO.SalesService.Type.RentalTruk
        End If

        AddHandler cboPaymentTerm.SelectedIndexChanged, AddressOf cboPaymentTerm_SelectedIndexChanged
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
            Case "Cetak Bon" : prvPrintBonFaktur()
        End Select
    End Sub

    Private Sub btnBP_Click(sender As Object, e As EventArgs) Handles btnBP.Click
        prvChooseCustomer()
    End Sub

    Private Sub ToolBarDetail_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDetail.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Tambah" : prvAddItem()
            Case "Edit" : prvEditItem()
            Case "Hapus" : prvDeleteItem()
        End Select
    End Sub

    Private Sub ToolBarDP_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDP.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Tambah" : prvAddDP()
            Case "Hapus" : prvDeleteDP()
        End Select
    End Sub

    Private Sub cboPaymentTerm_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cboPaymentTerm.Text.Trim = "CASH" Then dtpDueDate.Value = dtpSalesDate.Value
    End Sub

    Private Sub txtCustomerCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsBP As VO.BusinessPartner = BL.BusinessPartner.GetDetail(txtCustomerCode.Text.Trim)
            If clsBP.ID = 0 Then
                UI.usForm.frmMessageBox("Kode Pelanggan " & txtCustomerCode.Text.Trim & " tidak tersedia")
                intBPID = 0
                txtCustomerName.Text = ""
                cboPaymentTerm.SelectedValue = 0
                Exit Sub
            End If
            intBPID = clsBP.ID
            txtCustomerName.Text = clsBP.Name
            cboPaymentTerm.SelectedValue = clsBP.PaymentTermID
        End If
    End Sub

    Private Sub txtCustomer_LostFocus(sender As Object, e As EventArgs) Handles txtCustomerCode.LostFocus, txtCustomerName.LostFocus
        Dim clsBP As VO.BusinessPartner = BL.BusinessPartner.GetDetail(txtCustomerCode.Text.Trim)
        If clsBP.ID = 0 Then
            UI.usForm.frmMessageBox("Kode Pelanggan " & txtCustomerCode.Text.Trim & " tidak tersedia")
            intBPID = 0
            txtCustomerName.Text = ""
            cboPaymentTerm.SelectedValue = 0
            Exit Sub
        End If
        intBPID = clsBP.ID
        txtCustomerName.Text = clsBP.Name
        cboPaymentTerm.SelectedValue = clsBP.PaymentTermID
    End Sub

#End Region

End Class