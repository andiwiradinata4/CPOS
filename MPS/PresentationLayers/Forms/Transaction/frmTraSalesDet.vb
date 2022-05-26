Imports DevExpress.XtraGrid
Public Class frmTraSalesDet

#Region "Property"

    Private frmParent As frmTraSales
    Private clsData As VO.Sales
    Private intBPID As Integer = 0
    Private intSupplierID As Integer = 0
    Private decPurchasePrice1 As Decimal = 0, decPurchasePrice2 As Decimal = 0
    Private dtItem As New DataTable, dtDownPayment As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = "", strJournalIDReceive As String = "", strReceiveID As String = "", strReceiveNo As String = ""
    Private intItemID As Integer = 0
    Private decTolerance As Decimal = 0
    Private dtSupplier As New DataTable
    Private clsSupplier As VO.SalesSupplier
    Property pubID As String
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Property pubCS As New VO.CS

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1, cPrintFaktur = 2, _
       cAdd = 0, cDelete = 1

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

    Private Sub prvFillUom()
        Try
            UI.usForm.FillComboBox(cboUOMID, BL.UOM.ListData, "ID", "Code")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub prvFillCombo()
        prvFillStatus()
        prvFillPaymentTerm()
        prvFillUom()
    End Sub

    Private Sub prvFillForm()
        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        prvFillCombo()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.Sales
                clsData = BL.Sales.GetDetail(pubID)
                txtID.Text = clsData.ID
                txtSalesNo.Text = clsData.SalesNo
                intBPID = clsData.BPID
                txtCustomerCode.Text = clsData.CustomerCode
                txtCustomerName.Text = clsData.BPName
                intSupplierID = clsData.SupplierID
                txtSupplierCode.Text = clsData.SupplierCode
                txtSupplierName.Text = clsData.SupplierName
                dtpSalesDate.Value = clsData.SalesDate
                cboPaymentTerm.SelectedValue = clsData.PaymentTerm
                dtpDueDate.Value = clsData.DueDate
                txtPlatNumber.Text = clsData.PlatNumber
                txtDriverName.Text = clsData.DriverName
                txtRemarks.Text = clsData.Remarks
                intItemID = clsData.ItemID
                txtItemCode.Text = clsData.ItemCode
                txtItemName.Text = clsData.ItemName
                cboUOMID.SelectedValue = clsData.UOMID
                txtBrutto.Value = clsData.ArrivalBrutto
                txtTarra.Value = clsData.ArrivalTarra
                txtNettoBefore.Value = clsData.ArrivalNettoBefore
                txtDeduction.Value = clsData.ArrivalDeduction
                txtNettoAfter.Value = clsData.ArrivalNettoAfter
                txtPrice.Value = clsData.Price
                txtTotalPrice.Value = clsData.TotalPrice
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
                strJournalID = clsData.JournalID
                strJournalIDReceive = clsData.JournalIDReceive
                strReceiveID = clsData.ReceiveID
                strReceiveNo = clsData.ReceiveNo
                decPurchasePrice1 = clsData.PurchasePrice1
                decPurchasePrice2 = clsData.PurchasePrice2
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
        ElseIf txtSupplierName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih pemasok terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtSupplierCode.Focus()
            Exit Sub
        ElseIf cboPaymentTerm.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih jenis pembayaran terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            cboPaymentTerm.Focus()
            Exit Sub
        ElseIf txtPlatNumber.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nomor polisi harus diiisi terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtPlatNumber.Focus()
            Exit Sub
        ElseIf txtDriverName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Nama supir harus diiisi terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtDriverName.Focus()
            Exit Sub
        ElseIf txtItemCode.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih kode barang terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtItemCode.Focus()
            Exit Sub
        ElseIf txtBrutto.Value <= 0 Then
            UI.usForm.frmMessageBox("Brutto harus lebih besar dari 0")
            tcHeader.SelectedTab = tpMain
            txtBrutto.Focus()
            Exit Sub
            'ElseIf txtPrice.Value <= 0 Then '# 20220210 Request untuk penyesuaian harga jual dan harga beli
            '    UI.usForm.frmMessageBox("Harga harus lebih besar dari 0")
            '    tcHeader.SelectedTab = tpMain
            '    txtPrice.Focus()
            '    Exit Sub
            'ElseIf decPurchasePrice1 <= 0 Then
            '    UI.usForm.frmMessageBox("Harga beli 1 tidak tersedia dari pemasok " & txtSupplierName.Text.Trim)
            '    tcHeader.SelectedTab = tpMain
            '    Exit Sub
            'ElseIf decPurchasePrice2 <= 0 Then
            '    UI.usForm.frmMessageBox("Harga beli 2 tidak tersedia dari pemasok " & txtSupplierName.Text.Trim)
            '    tcHeader.SelectedTab = tpMain
            '    Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            tcHeader.SelectedTab = tpMain
            cboStatus.Focus()
            Exit Sub
        ElseIf cboUOMID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Satuan kosong. Mohon untuk tutup form dan buka kembali")
            tcHeader.SelectedTab = tpMain
            cboUOMID.Focus()
            Exit Sub
            'ElseIf txtTotalPrice.Value < grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue Then
            '    UI.usForm.frmMessageBox("Total Panjar tidak boleh lebih besar dari total harga penjualan")
            '    tcHeader.SelectedTab = tpDownPayment
            '    Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data penjualan?") Then Exit Sub

        '# Sales
        clsData = New VO.Sales
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.SalesNo = txtSalesNo.Text.Trim
        clsData.BPID = intBPID
        clsData.BPName = txtCustomerName.Text.Trim
        clsData.SupplierID = intSupplierID
        clsData.SupplierName = txtSupplierName.Text.Trim
        clsData.SalesDate = dtpSalesDate.Value
        clsData.PaymentTerm = cboPaymentTerm.SelectedValue
        clsData.DueDate = dtpDueDate.Value
        clsData.DriverName = txtDriverName.Text.Trim
        clsData.PlatNumber = txtPlatNumber.Text.Trim
        clsData.Remarks = txtRemarks.Text.Trim
        clsData.ItemID = intItemID
        clsData.ItemCode = txtItemCode.Text.Trim
        clsData.ItemName = txtItemName.Text.Trim
        clsData.UOMID = cboUOMID.SelectedValue
        clsData.ArrivalBrutto = txtBrutto.Value
        clsData.ArrivalTarra = txtTarra.Value
        clsData.ArrivalNettoBefore = txtNettoBefore.Value
        clsData.ArrivalDeduction = txtDeduction.Value
        clsData.ArrivalNettoAfter = txtNettoAfter.Value
        clsData.Price = txtPrice.Value
        clsData.TotalPrice = txtTotalPrice.Value
        'clsData.TotalDownPayment = grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID

        '# Receive
        Dim clsReceive As New VO.Receive
        clsReceive.ProgramID = pubCS.ProgramID
        clsReceive.CompanyID = pubCS.CompanyID
        clsReceive.ID = strReceiveID
        clsReceive.ReferencesID = clsData.ID
        clsReceive.ReceiveNo = strReceiveNo
        clsReceive.BPID = intSupplierID
        clsReceive.BPName = txtSupplierName.Text.Trim
        clsReceive.ReceiveDate = dtpSalesDate.Value
        clsReceive.PaymentTerm = cboPaymentTerm.SelectedValue
        clsReceive.DueDate = dtpDueDate.Value
        clsReceive.DriverName = txtDriverName.Text.Trim
        clsReceive.PlatNumber = txtPlatNumber.Text.Trim
        clsReceive.Remarks = txtRemarks.Text.Trim
        clsReceive.ItemID = intItemID
        clsReceive.ItemCode = txtItemCode.Text.Trim
        clsReceive.ItemName = txtItemName.Text.Trim
        clsReceive.UOMID = cboUOMID.SelectedValue
        clsReceive.ArrivalBrutto = txtBrutto.Value
        clsReceive.ArrivalTarra = txtTarra.Value
        clsReceive.ArrivalNettoBefore = txtNettoBefore.Value
        clsReceive.ArrivalDeduction = txtDeduction.Value
        clsReceive.ArrivalNettoAfter = txtNettoAfter.Value
        clsReceive.Price1 = decPurchasePrice1
        clsReceive.TotalPrice1 = decPurchasePrice1 * txtNettoAfter.Value
        clsReceive.Price2 = decPurchasePrice2
        clsReceive.TotalPrice2 = decPurchasePrice2 * txtNettoAfter.Value
        clsReceive.Tolerance = decTolerance
        clsReceive.DONumber = ""
        clsReceive.SPBNumber = ""
        clsReceive.SegelNumber = ""
        clsReceive.Specification = ""
        clsReceive.IDStatus = VO.Status.Values.Draft
        clsReceive.LogBy = MPSLib.UI.usUserApp.UserID
        clsReceive.JournalID = strJournalIDReceive

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim strID As String = BL.Sales.SaveDataDefault(pubIsNew, clsData, clsReceive)
            pgMain.Value = 80
            If strID.Trim <> "" Then
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor penjualan: " & strID)
                    frmParent.pubRefresh(clsData.ID)
                    prvClear()
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

    Private Sub prvPrintBonFaktur()
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.Remarks = ""
        Dim frmDetail As New frmTraSalesPrintBonFaktur
        With frmDetail
            .pubData = clsData
            .ShowDialog()
            If .pubIsPrint Then frmParent.pubRefresh(clsData.ID)
        End With
    End Sub

    Private Sub prvClear()
        tcHeader.SelectedTab = tpMain
        txtID.Text = ""
        txtSalesNo.Text = ""
        intBPID = 0
        txtCustomerCode.Text = ""
        txtCustomerName.Text = ""
        intSupplierID = 0
        txtSupplierCode.Text = ""
        txtSupplierName.Text = ""
        dtpSalesDate.Value = Now
        cboPaymentTerm.SelectedIndex = -1
        dtpDueDate.Value = Now.Date
        txtPlatNumber.Text = ""
        txtDriverName.Text = ""
        txtRemarks.Text = ""
        txtItemCode.Text = ""
        txtItemName.Text = ""
        cboUOMID.SelectedIndex = -1
        txtBrutto.Value = 0
        txtTarra.Value = 0
        txtNettoBefore.Value = 0
        txtDeduction.Value = 0
        txtNettoAfter.Value = 0
        txtPrice.Value = 0
        txtTotalPrice.Value = 0
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
                prvGetPrice()
            End If
        End With
    End Sub

    Private Sub prvChooseSupplier()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intSupplierID = .pubLUdtRow.Item("ID")
                txtSupplierCode.Text = .pubLUdtRow.Item("Code")
                txtSupplierName.Text = .pubLUdtRow.Item("Name")
                prvGetPrice()
            End If
        End With
    End Sub

    Private Sub prvGetPrice()
        Try
            Dim clsPrice As VO.BusinessPartnerPrice = BL.BusinessPartner.GetDetailPrice(intSupplierID, dtpSalesDate.Value.Date)
            If clsPrice.ID = 0 Then
                txtPrice.Value = 0
                decPurchasePrice1 = 0
                decPurchasePrice2 = 0
                Exit Sub
            End If

            txtPrice.Value = clsPrice.SalesPrice
            decPurchasePrice1 = clsPrice.PurchasePrice1
            decPurchasePrice2 = clsPrice.PurchasePrice2
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionSales, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

#Region "Item Handle"

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
                decTolerance = .pubLUdtRow.Item("Tolerance")
                txtBrutto.Focus()
            End If
        End With
    End Sub

    Private Sub prvCalculate()
        txtNettoBefore.Value = txtBrutto.Value - txtTarra.Value
        txtNettoAfter.Value = txtNettoBefore.Value - txtDeduction.Value
        txtTotalPrice.Value = txtNettoAfter.Value * txtPrice.Value
    End Sub

#End Region

#Region "Down Payment Handle"

    Private Sub prvSetButton()
        Dim bolEnabled As Boolean = IIf(grdDownPaymentView.RowCount = 0, False, True)
        With ToolBarDP
            .Buttons(cDelete).Enabled = bolEnabled
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
            prvSetButton()
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
        ElseIf intSupplierID = 0 Then
            UI.usForm.frmMessageBox("Pilih pemasok terlebih dahulu")
            txtSupplierCode.Focus()
            Exit Sub
        End If

        Dim frmDetail As New frmViewOutstandingDownPayment
        With frmDetail
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .pubBPID = intBPID
            .pubBPID2 = intSupplierID
            .pubDPType = VO.DownPayment.Type.Sales
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
                        drNew.Item("TotalAmount") = IIf(txtTotalPrice.Value > dr.Item("TotalAmount"), dr.Item("TotalAmount"), txtTotalPrice.Value)
                    Else
                        drNew.Item("TotalAmount") = IIf(txtTotalPrice.Value > grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue + dr.Item("TotalAmount"), _
                                                        dr.Item("TotalAmount"), _
                                                        txtTotalPrice.Value - grdDownPaymentView.Columns("TotalAmount").SummaryItem.SummaryValue)
                    End If
                    drNew.EndEdit()
                    dtDownPayment.Rows.Add(drNew)
                Next
                dtDownPayment.AcceptChanges()
                grdDownPayment.DataSource = dtDownPayment
                grdDownPaymentView.BestFitColumns()
                prvSetButton()
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
        prvSetButton()
    End Sub

    Private Sub prvSumGrid()
        Dim SumTotalAmount As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "Total Pembayaran: {0:#,##0.00}")

        If grdDownPaymentView.Columns("TotalAmount").SummaryText.Trim = "" Then
            grdDownPaymentView.Columns("TotalAmount").Summary.Add(SumTotalAmount)
        End If
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

    Private Sub frmTraSalesDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub frmTraSalesDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        ToolBarDP.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQueryDownPayment()
        prvQueryHistory()
        prvUserAccess()

        AddHandler cboPaymentTerm.SelectedIndexChanged, AddressOf cboPaymentTerm_SelectedIndexChanged
    End Sub

    Private Sub btnBP_Click(sender As Object, e As EventArgs) Handles btnBP.Click
        prvChooseCustomer()
    End Sub

    Private Sub btnSupplier_Click(sender As Object, e As EventArgs) Handles btnSupplier.Click
        prvChooseSupplier()
    End Sub

    Private Sub btnItem_Click(sender As Object, e As EventArgs) Handles btnItem.Click
        prvChooseItem()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
            Case "Cetak Bon" : prvPrintBonFaktur()
        End Select
    End Sub

    Private Sub ToolBarDP_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBarDP.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Tambah" : prvAddDP()
            Case "Hapus" : prvDeleteDP()
        End Select
    End Sub

    Private Sub txtValue_ValueChanged(sender As Object, e As EventArgs) Handles txtBrutto.ValueChanged, txtTarra.ValueChanged, _
        txtDeduction.ValueChanged, txtPrice.ValueChanged
        txtNettoBefore.Value = txtBrutto.Value - txtTarra.Value
        txtNettoAfter.Value = txtNettoBefore.Value - txtDeduction.Value
        txtTotalPrice.Value = txtNettoAfter.Value * txtPrice.Value
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
                decTolerance = 0
                Exit Sub
            End If
            intItemID = clsItem.ID
            txtItemCode.Text = clsItem.Code
            txtItemName.Text = clsItem.Name
            cboUOMID.SelectedValue = clsItem.UomID
            decTolerance = clsItem.Tolerance
            txtBrutto.Focus()
        End If
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
            txtSupplierCode.Focus()
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

    Private Sub txtSupplierCode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSupplierCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim clsBP As VO.BusinessPartner = BL.BusinessPartner.GetDetail(txtSupplierCode.Text.Trim)
            If clsBP.ID = 0 Then
                UI.usForm.frmMessageBox("Kode Pemasok " & txtSupplierCode.Text.Trim & " tidak tersedia")
                intSupplierID = 0
                txtSupplierName.Text = ""
                txtPrice.Value = 0
                decPurchasePrice1 = 0
                decPurchasePrice2 = 0
                Exit Sub
            End If
            intSupplierID = clsBP.ID
            txtSupplierName.Text = clsBP.Name
            prvGetPrice()
            dtpSalesDate.Focus()
        End If
    End Sub

    Private Sub txtSupplier_LostFocus(sender As Object, e As EventArgs) Handles txtSupplierCode.LostFocus, txtSupplierName.LostFocus
        If txtSupplierCode.Text.Trim = "" Or txtSupplierName.Text.Trim = "" Then
            intSupplierID = 0
            txtSupplierName.Text = ""
            txtPrice.Value = 0
            decPurchasePrice1 = 0
            decPurchasePrice2 = 0
        Else
            Dim clsBP As VO.BusinessPartner = BL.BusinessPartner.GetDetail(txtSupplierCode.Text.Trim)
            If clsBP.ID = 0 Then
                intSupplierID = 0
                txtSupplierName.Text = ""
                txtPrice.Value = 0
                decPurchasePrice1 = 0
                decPurchasePrice2 = 0
            End If
        End If
    End Sub

    Private Sub dtpSalesDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSalesDate.ValueChanged
        If intSupplierID > 0 Then
            prvGetPrice()
        End If
        If cboPaymentTerm.Text.Trim = "CASH" Then dtpDueDate.Value = dtpSalesDate.Value
    End Sub

    Private Sub cboPaymentTerm_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cboPaymentTerm.Text.Trim = "CASH" Then dtpDueDate.Value = dtpSalesDate.Value
    End Sub

#End Region

End Class