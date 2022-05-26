Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns

Public Class frmTraSalesSplitReceive

#Region "Property"

    Private intBPID As Integer
    Private intItemID As Integer = 0
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private clsData As VO.Sales
    Private bolValid As Boolean = True
    Property pubIsSave As Boolean = False
    Property pubCS As New VO.CS
    Property pubSalesID As String

#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ProgramID", "ProgramID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Perusahaan", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "ID", "Nomor", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "ReferencesID", "Nomor Referensi", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "BPID", "BPID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "BPName", "Pemasok", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ReceiveDate", "Tanggal", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "PaymentTerm", "PaymentTerm", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "DueDate", "Jatuh Tempo", 100, UI.usDefGrid.gFullDate, False)
        UI.usForm.SetGrid(grdView, "DriverName", "Nama Supir", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PlatNumber", "Nomor Plat", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PPN", " PPN", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "PPH", " PPH", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "ArrivalBrutto", "Brutto", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "ArrivalTarra", "Tarra", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "ArrivalNettoBefore", "Netto 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalDeduction", "Potongan", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "ArrivalNettoAfter", "Netto 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Price1", "Price 1", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "Price2", "Price 2", 100, UI.usDefGrid.gReal2Num, True, False)
        UI.usForm.SetGrid(grdView, "TotalPrice1", "Total Price 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPrice2", "Total Price 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "DONumber", "Nomor DO", 100, UI.usDefGrid.gString, True, False)
        UI.usForm.SetGrid(grdView, "SPBNumber", "Nomor SPB", 100, UI.usDefGrid.gString, True, False)
        UI.usForm.SetGrid(grdView, "SegelNumber", "Nomor Segel", 100, UI.usDefGrid.gString, True, False)
        UI.usForm.SetGrid(grdView, "Specification", "Spesifikasi", 100, UI.usDefGrid.gString, True, False)
        UI.usForm.SetGrid(grdView, "Remarks", "Remarks", 100, UI.usDefGrid.gString, True, False)

        grdView.Columns("ArrivalBrutto").ColumnEdit = rpiValue
        grdView.Columns("ArrivalTarra").ColumnEdit = rpiValue
        grdView.Columns("ArrivalDeduction").ColumnEdit = rpiValue
        grdView.Columns("Price1").ColumnEdit = rpiValue
        grdView.Columns("Price2").ColumnEdit = rpiValue
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionSales), "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillPaymentTerm()
        Try
            UI.usForm.FillComboBox(cboPaymentTerm, BL.PaymentTerm.ListDataForCombo, "ID", "Name")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillUom()
        Try
            UI.usForm.FillComboBox(cboUOMID, BL.UOM.ListData, "ID", "Code")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
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
            clsData = New VO.Sales
            clsData = BL.Sales.GetDetail(pubSalesID)
            txtID.Text = clsData.ID
            intBPID = clsData.BPID
            txtBPName.Text = clsData.BPName
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
            txtTolerance.Value = clsData.Tolerance
            txtMaxNetto.Value = clsData.ArrivalNettoAfter + (clsData.ArrivalNettoAfter * (clsData.Tolerance / 100))
            txtArrivalNettoUsage.Value = clsData.ArrivalUsage
            cboStatus.SelectedValue = clsData.IDStatus
            ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
            ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
            ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvQuery()
        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor

        Dim decArrivalBrutto As Decimal = 0, _
            decArrivalTarra As Decimal = 0, _
            decArrivalNettoBefore As Decimal = 0, _
            decArrivalDeduction As Decimal = 0, _
            decArrivalNettoAfter As Decimal = 0, _
            decRoundAmount As Decimal = 0

        Try
            dtItem = BL.Sales.ListDataSplitReceive(pubSalesID)
            With dtItem
                For i As Integer = 0 To .Rows.Count - 1
                    If i < .Rows.Count - 1 Then
                        .Rows(i).BeginEdit()

                        '# Arrival Brutto
                        decRoundAmount = IIf(clsData.ArrivalBrutto <= 0, 0, Math.Round((clsData.ArrivalBrutto / .Rows.Count) / 10, 0) * 10)
                        .Rows(i).Item("ArrivalBrutto") = decRoundAmount
                        decArrivalBrutto += decRoundAmount

                        '# Arrival Tarra
                        decRoundAmount = IIf(clsData.ArrivalTarra <= 0, 0, Math.Round((clsData.ArrivalTarra / .Rows.Count) / 10, 0) * 10)
                        .Rows(i).Item("ArrivalTarra") = decRoundAmount
                        decArrivalTarra += decRoundAmount

                        '# Arrival Netto Before
                        .Rows(i).Item("ArrivalNettoBefore") = .Rows(i).Item("ArrivalBrutto") - .Rows(i).Item("ArrivalTarra")
                        decArrivalNettoBefore += .Rows(i).Item("ArrivalBrutto") - .Rows(i).Item("ArrivalTarra")

                        '# Arrival Deduction
                        decRoundAmount = IIf(clsData.ArrivalDeduction <= 0, 0, Math.Round((clsData.ArrivalDeduction / .Rows.Count) / 10, 0) * 10)
                        .Rows(i).Item("ArrivalDeduction") = decRoundAmount
                        decArrivalDeduction += decRoundAmount

                        '# Arrival Netto After
                        .Rows(i).Item("ArrivalNettoAfter") = .Rows(i).Item("ArrivalNettoBefore") - .Rows(i).Item("ArrivalDeduction")
                        decArrivalNettoAfter += decArrivalNettoBefore - decArrivalDeduction

                        '# Total Price 1
                        .Rows(i).Item("TotalPrice1") = .Rows(i).Item("ArrivalNettoAfter") * .Rows(i).Item("Price1")

                        '# Total Price 2
                        .Rows(i).Item("TotalPrice2") = .Rows(i).Item("ArrivalNettoAfter") * .Rows(i).Item("Price2")

                        .Rows(i).EndEdit()
                    Else
                        .Rows(i).BeginEdit()

                        '# Arrival Brutto
                        .Rows(i).Item("ArrivalBrutto") = clsData.ArrivalBrutto - decArrivalBrutto

                        '# Arrival Tarra
                        .Rows(i).Item("ArrivalTarra") = clsData.ArrivalTarra - decArrivalTarra

                        '# Arrival Netto Before
                        .Rows(i).Item("ArrivalNettoBefore") = .Rows(i).Item("ArrivalBrutto") - .Rows(i).Item("ArrivalTarra")

                        '# Arrival Deduction
                        .Rows(i).Item("ArrivalDeduction") = clsData.ArrivalDeduction - decArrivalDeduction

                        '# Arrival Netto After
                        .Rows(i).Item("ArrivalNettoAfter") = .Rows(i).Item("ArrivalNettoBefore") - .Rows(i).Item("ArrivalDeduction")

                        '# Total Price 1
                        .Rows(i).Item("TotalPrice1") = .Rows(i).Item("ArrivalNettoAfter") * .Rows(i).Item("Price1")

                        '# Total Price 2
                        .Rows(i).Item("TotalPrice2") = .Rows(i).Item("ArrivalNettoAfter") * .Rows(i).Item("Price2")

                        .Rows(i).EndEdit()
                    End If
                Next
            End With
            dtItem.AcceptChanges()
            grdMain.DataSource = dtItem
            prvSumGrid()
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
            Me.Close()
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvSumGrid()
        Dim SumTotalBrutto As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalBrutto", "Total Brutto: {0:#,##0.00}")
        Dim SumTotalTarra As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalTarra", "Total Tarra: {0:#,##0.00}")
        Dim SumTotalNetto1 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalNettoBefore", "Total Netto 1: {0:#,##0.00}")
        Dim SumTotalPotongan As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalDeduction", "Total Potongan: {0:#,##0.00}")
        Dim SumTotalNetto2 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalNettoAfter", "Total Netto 2: {0:#,##0.00}")
        Dim SumTotalPrice1 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice1", "Total Price 1: {0:#,##0.00}")
        Dim SumTotalPrice2 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice2", "Total Price 2: {0:#,##0.00}")

        If grdView.Columns("ArrivalBrutto").SummaryText.Trim = "" Then
            grdView.Columns("ArrivalBrutto").Summary.Add(SumTotalBrutto)
        End If

        If grdView.Columns("ArrivalTarra").SummaryText.Trim = "" Then
            grdView.Columns("ArrivalTarra").Summary.Add(SumTotalTarra)
        End If

        If grdView.Columns("ArrivalNettoBefore").SummaryText.Trim = "" Then
            grdView.Columns("ArrivalNettoBefore").Summary.Add(SumTotalNetto1)
        End If

        If grdView.Columns("ArrivalDeduction").SummaryText.Trim = "" Then
            grdView.Columns("ArrivalDeduction").Summary.Add(SumTotalPotongan)
        End If

        If grdView.Columns("ArrivalNettoAfter").SummaryText.Trim = "" Then
            grdView.Columns("ArrivalNettoAfter").Summary.Add(SumTotalNetto2)
        End If

        If grdView.Columns("TotalPrice1").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice1").Summary.Add(SumTotalPrice1)
        End If

        If grdView.Columns("TotalPrice2").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice2").Summary.Add(SumTotalPrice2)
        End If

    End Sub

    Private Sub prvSave()
        txtID.Focus()
        grdView.UpdateCurrentRow()
        If bolValid = False Then Exit Sub

        If grdView.Columns("ArrivalNettoAfter").SummaryItem.SummaryValue > txtMaxNetto.Value Then
            UI.usForm.frmMessageBox("Total netto 2 tidak boleh lebih besar dari " & lblMaxNetto.Text)
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data split pembelian?") Then Exit Sub
        Dim clsReceive As VO.Receive
        Dim clsReceiveAll(dtItem.Rows.Count - 1) As VO.Receive

        With dtItem
            For i As Integer = 0 To .Rows.Count - 1
                clsReceive = New VO.Receive
                clsReceive.ProgramID = pubCS.ProgramID
                clsReceive.CompanyID = pubCS.CompanyID
                clsReceive.ID = txtID.Text.Trim
                clsReceive.BPID = .Rows(i).Item("BPID")
                clsReceive.BPName = .Rows(i).Item("BPName")
                clsReceive.ReceiveDate = Now
                clsReceive.PaymentTerm = cboPaymentTerm.SelectedValue
                clsReceive.DueDate = .Rows(i).Item("DueDate")
                clsReceive.DriverName = txtDriverName.Text.Trim
                clsReceive.PlatNumber = txtPlatNumber.Text.Trim
                clsReceive.ReferencesID = clsData.ID
                clsReceive.Remarks = UCase(.Rows(i).Item("Remarks"))
                clsReceive.ItemID = intItemID
                clsReceive.ItemCode = txtItemCode.Text.Trim
                clsReceive.ItemName = txtItemName.Text.Trim
                clsReceive.UOMID = cboUOMID.SelectedValue
                clsReceive.ArrivalBrutto = .Rows(i).Item("ArrivalBrutto")
                clsReceive.ArrivalTarra = .Rows(i).Item("ArrivalTarra")
                clsReceive.ArrivalNettoBefore = .Rows(i).Item("ArrivalNettoBefore")
                clsReceive.ArrivalDeduction = .Rows(i).Item("ArrivalDeduction")
                clsReceive.ArrivalNettoAfter = .Rows(i).Item("ArrivalNettoAfter")
                clsReceive.Price1 = .Rows(i).Item("Price1")
                clsReceive.TotalPrice1 = .Rows(i).Item("TotalPrice1")
                clsReceive.Price2 = .Rows(i).Item("Price2")
                clsReceive.TotalPrice2 = .Rows(i).Item("TotalPrice2")
                clsReceive.Tolerance = txtTolerance.Value
                clsReceive.DONumber = UCase(.Rows(i).Item("DONumber"))
                clsReceive.SPBNumber = UCase(.Rows(i).Item("SPBNumber"))
                clsReceive.SegelNumber = UCase(.Rows(i).Item("SegelNumber"))
                clsReceive.Specification = UCase(.Rows(i).Item("Specification"))
                clsReceive.IDStatus = VO.Status.Values.Draft
                clsReceive.LogBy = MPSLib.UI.usUserApp.UserID
                clsReceive.JournalID = ""
                clsReceiveAll(i) = clsReceive
            Next
        End With

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim bolValid As Boolean = BL.Sales.SplitDataReceive(clsData, clsReceiveAll)
            If bolValid Then
                pgMain.Value = 100
                UI.usForm.frmMessageBox("Split data pembelian berhasil.")
                pubIsSave = bolValid
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.NewAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmTraSalesSplitReceive_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraSalesSplitReceive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvFillForm()
        prvQuery()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub grdView_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles grdView.ValidatingEditor
        With grdView
            bolValid = True
            Dim intFocus As Integer = .FocusedRowHandle
            Dim col As GridColumn = .FocusedColumn
            If (col.Name = "ArrivalBrutto") Or (col.Name = "ArrivalTarra") Or (col.Name = "ArrivalDeduction") Or _
               (col.Name = "Price1") Or (col.Name = "Price2") Then
                Dim oldValue As Decimal = IIf(.GetFocusedRowCellValue(col).Equals(DBNull.Value), 0, .GetFocusedRowCellValue(col))
                Dim newValue As Decimal = IIf((e.Value = "") Or (e.Value.Equals(DBNull.Value) Or (e.Value = ".")), 0, e.Value)
                Dim strErrorMessage As String = ""

                If newValue < 0 Then
                    bolValid = False
                    strErrorMessage = "Nilai harus lebih besar dari 0"
                End If

                If bolValid = False Then
                    e.Value = oldValue
                    e.Valid = False
                    e.ErrorText = strErrorMessage
                    .FocusedRowHandle = intFocus
                    .FocusedColumn = .Columns(col.Name)
                    .ShowEditor()
                    Exit Sub
                Else
                    .SetRowCellValue(intFocus, col.Name, newValue)
                    .SetRowCellValue(intFocus, "ArrivalNettoBefore", .GetRowCellValue(intFocus, "ArrivalBrutto") - .GetRowCellValue(intFocus, "ArrivalTarra"))
                    .SetRowCellValue(intFocus, "ArrivalNettoAfter", .GetRowCellValue(intFocus, "ArrivalNettoBefore") - .GetRowCellValue(intFocus, "ArrivalDeduction"))
                    .SetRowCellValue(intFocus, "TotalPrice1", .GetRowCellValue(intFocus, "ArrivalNettoAfter") * .GetRowCellValue(intFocus, "Price1"))
                    .SetRowCellValue(intFocus, "TotalPrice2", .GetRowCellValue(intFocus, "ArrivalNettoAfter") * .GetRowCellValue(intFocus, "Price2"))
                    .UpdateCurrentRow()
                    .BestFitColumns()
                End If
            ElseIf col.Name = "DONumber" Or col.Name = "SPBNumber" Or col.Name = "SegelNumber" Or col.Name = "Specification" Then
                .SetRowCellValue(intFocus, col.Name, e.Value)
                .UpdateCurrentRow()
                .BestFitColumns()
            End If

        End With
    End Sub

#End Region

End Class