Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraGrid

Public Class frmTraSalesService

    Private intPos As Integer = 0
    Private clsData As New VO.SalesService
    Private intCompanyID As Integer

    Private Const _
       cNew = 0, cDetail = 1, cDelete = 2, cSep1 = 3, cExportExcel = 4, cSep2 = 5, cRefresh = 6, cClose = 7

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "Pick", "Pilih", 100, UI.usDefGrid.gIntNum, True, False)
        UI.usForm.SetGrid(grdView, "ProgramID", "ProgramID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Perusahaan", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "SalesNo", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ServiceType", "ServiceType", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "BPID", "BPID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "BPName", "Pelanggan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "SalesDate", "Tanggal", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdView, "PaymentTerm", "PaymentTerm", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "DueDate", "Jatuh Tempo", 100, UI.usDefGrid.gSmallDate)
        UI.usForm.SetGrid(grdView, "SPKNumber", "Nomor SPK", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "BillNumber", "Nomor Tagihan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "TotalPrice", "Total Price", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPPH", "TotalPPH", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "TotalPPN", "Total PPN", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalDisc", "Total Discount", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "GrandTotal", "Grand Total", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalDownPayment", "Total Panjar", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPayment", "Total Bayar", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalReturn", "Total Retur", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "IsPostedGL", "Posted GL", 100, UI.usDefGrid.gBoolean)
        UI.usForm.SetGrid(grdView, "PostedBy", "Posted By", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PostedDate", "Posted Date", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "IsDeleted", "IsDeleted", 100, UI.usDefGrid.gBoolean, False)
        UI.usForm.SetGrid(grdView, "Remarks", "Remarks", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "IDStatus", "IDStatus", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CreatedBy", "Dibuat Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "CreatedDate", "Tanggal Buat", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogBy", "Diedit Oleh", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "LogDate", "Tanggal Edit", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "LogInc", "LogInc", 100, UI.usDefGrid.gIntNum)
        UI.usForm.SetGrid(grdView, "StatusInfo", "Status", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "JournalID", "JournalID", 100, UI.usDefGrid.gString, False)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cDetail).Enabled = bolEnable
            .Item(cDelete).Enabled = bolEnable
            .Item(cExportExcel).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvFillCombo()
        Try
            Dim dtData As DataTable = BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionSales)
            Dim dr As DataRow
            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("IDStatus") = VO.Status.Values.All
                .Item("StatusName") = "SEMUA"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()
            dtData.DefaultView.Sort = "IDStatus ASC"
            dtData = dtData.DefaultView.ToTable

            UI.usForm.FillComboBox(cboStatus, dtData, "IDStatus", "StatusName")
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvFillBPListBox()
        Try
            Dim dtData As DataTable = BL.BusinessPartner.ListDataForFilter(MPSLib.UI.usUserApp.ProgramID)
            dtData.DefaultView.Sort = "Code ASC"

            For Each dr As DataRow In dtData.Rows
                chkListCustomer.Items.Add(dr.Item("Code") & " | " & dr.Item("Name"))
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvDefaultFilter()
        intCompanyID = MPSLib.UI.usUserApp.CompanyID
        txtCompanyName.Text = MPSLib.UI.usUserApp.CompanyName
    End Sub

    Private Sub prvQuery()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdMain.DataSource = BL.SalesService.ListData(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date, cboStatus.SelectedValue, prvSelectedCheckList(chkListCustomer))
            prvSumGrid()
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvSetButton()
            prvResetProgressBar()
        End Try
    End Sub

    Private Function prvSelectedCheckList(ByVal chkList As usCheckListBoxControl) As String
        Dim strReturn As String = ""
        Dim sb As New System.Text.StringBuilder
        Dim aStr() As String
        With chkList
            For i As Integer = 0 To .Items.Count - 1
                If .GetItemChecked(i) = True Then
                    aStr = .Items.Item(i).ToString.Split(" | ")
                    sb.Append("'" & aStr(0) & "', ")
                End If
            Next
        End With
        If sb.ToString <> "" Then
            strReturn = Mid(sb.ToString, 1, Len(sb.ToString) - 2)
        End If
        Return strReturn.Trim
    End Function

    Public Sub pubRefresh(Optional ByVal strSearch As String = "")
        With grdView
            If Not grdView.FocusedValue Is Nothing And strSearch = "" Then
                strSearch = grdView.GetDataRow(grdView.FocusedRowHandle).Item("ID")
            End If
            prvQuery()
            If grdView.RowCount > 0 Then UI.usForm.GridMoveRow(grdView, "ID", strSearch)
        End With
    End Sub

    Private Function prvGetCS() As VO.CS
        Dim clsCS As New VO.CS
        clsCS.ProgramID = MPSLib.UI.usUserApp.ProgramID
        clsCS.ProgramName = MPSLib.UI.usUserApp.ProgramName
        clsCS.CompanyID = intCompanyID
        clsCS.CompanyName = txtCompanyName.Text.Trim
        Return clsCS
    End Function

    Private Function prvGetData() As VO.SalesService
        Dim clsReturn As New VO.SalesService
        clsReturn.ProgramID = grdView.GetRowCellValue(intPos, "ProgramID")
        clsReturn.ProgramName = grdView.GetRowCellValue(intPos, "ProgramName")
        clsReturn.CompanyID = grdView.GetRowCellValue(intPos, "CompanyID")
        clsReturn.CompanyName = grdView.GetRowCellValue(intPos, "CompanyName")
        clsReturn.ID = grdView.GetRowCellValue(intPos, "ID")
        clsReturn.ServiceType = grdView.GetRowCellValue(intPos, "ServiceType")
        clsReturn.BPID = grdView.GetRowCellValue(intPos, "BPID")
        clsReturn.BPName = grdView.GetRowCellValue(intPos, "BPName")
        clsReturn.SalesDate = grdView.GetRowCellValue(intPos, "SalesDate")
        clsReturn.PaymentTerm = grdView.GetRowCellValue(intPos, "PaymentTerm")
        clsReturn.DueDate = grdView.GetRowCellValue(intPos, "DueDate")
        clsReturn.SPKNumber = grdView.GetRowCellValue(intPos, "SPKNumber")
        clsReturn.TotalPPN = grdView.GetRowCellValue(intPos, "TotalPPN")
        clsReturn.TotalPPH = grdView.GetRowCellValue(intPos, "TotalPPH")
        clsReturn.TotalPrice = grdView.GetRowCellValue(intPos, "TotalPrice")
        clsReturn.GrandTotal = grdView.GetRowCellValue(intPos, "GrandTotal")
        clsReturn.IDStatus = grdView.GetRowCellValue(intPos, "IDStatus")
        clsReturn.JournalID = grdView.GetRowCellValue(intPos, "JournalID")
        clsReturn.SalesNo = grdView.GetRowCellValue(intPos, "SalesNo")
        Return clsReturn
    End Function

    Private Sub prvNew()
        prvResetProgressBar()
        Dim frmDetail As New frmTraSalesServiceDet
        With frmDetail
            .pubIsNew = True
            .pubCS = prvGetCS()
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
        End With
    End Sub

    Private Sub prvDetail()
        prvResetProgressBar()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        Dim frmDetail As New frmTraSalesServiceDet
        With frmDetail
            .pubIsNew = False
            .pubCS = prvGetCS()
            .pubID = clsData.ID
            .StartPosition = FormStartPosition.CenterScreen
            .pubShowDialog(Me)
            If .pubIsSave Then pubRefresh()
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        clsData = prvGetData()
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        If Not UI.usForm.frmAskQuestion("Hapus Nomor " & clsData.SalesNo & "?") Then Exit Sub

        Dim frmDetail As New usFormRemarks
        With frmDetail
            .ShowDialog()
            If .pubIsSave Then
                clsData.Remarks = .pubValue
            Else
                Exit Sub
            End If
        End With

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            BL.SalesService.DeleteData(clsData)
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pgMain.Value = 100
            pubRefresh(clsData.ID)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
            prvResetProgressBar()
        End Try
    End Sub

    Private Sub prvClear()
        grdMain.DataSource = Nothing
        grdView.Columns.Clear()
        prvSetGrid()
        prvSetButton()
    End Sub

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
                prvClear()
                btnExecute.Focus()
            End If
        End With
    End Sub

    Private Sub prvSumGrid()
        Dim SumTotalPrice As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "Total Price: {0:#,##0.00}")
        Dim SumTotalDownPayment As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalDownPayment", "Total Panjar: {0:#,##0.00}")
        Dim SumTotalReturn As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalReturn", "Total Retur: {0:#,##0.00}")
        Dim SumTotalPayment As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPayment", "Total Bayar: {0:#,##0.00}")

        If grdView.Columns("TotalPrice").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice").Summary.Add(SumTotalPrice)
        End If

        If grdView.Columns("TotalDownPayment").SummaryText.Trim = "" Then
            grdView.Columns("TotalDownPayment").Summary.Add(SumTotalDownPayment)
        End If

        If grdView.Columns("TotalReturn").SummaryText.Trim = "" Then
            grdView.Columns("TotalReturn").Summary.Add(SumTotalReturn)
        End If

        If grdView.Columns("TotalPayment").SummaryText.Trim = "" Then
            grdView.Columns("TotalPayment").Summary.Add(SumTotalPayment)
        End If
    End Sub

    Private Sub prvExportExcel()
        Dim dxExporter As New DX.usDXHelper
        dxExporter.DevExport(Me, grdMain, Me.Text, Me.Text, DX.usDxExportFormat.fXls, True, True, DX.usDXExportType.etDefault)
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cNew).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesService, VO.Access.Values.NewAccess)
            .Item(cDelete).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesService, VO.Access.Values.DeleteAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmTraSalesService_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraSalesService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillCombo()
        prvFillBPListBox()
        prvSetGrid()
        cboStatus.SelectedValue = VO.Status.Values.All
        dtpDateFrom.Value = Today.Date.AddDays(-7)
        dtpDateTo.Value = Today.Date
        prvDefaultFilter()
        prvQuery()
        prvUserAccess()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cNew).Name Then
            prvNew()
        ElseIf e.Button.Name = ToolBar.Buttons(cRefresh).Name Then
            pubRefresh()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        ElseIf grdView.FocusedRowHandle >= 0 Then
            Select Case e.Button.Name
                Case ToolBar.Buttons(cDetail).Name : prvDetail()
                Case ToolBar.Buttons(cDelete).Name : prvDelete()
                Case ToolBar.Buttons(cExportExcel).Name : prvExportExcel()
            End Select
        End If
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        prvQuery()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        prvClear()
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

    Private Sub grdView_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grdView.RowStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If (e.RowHandle >= 0) Then
            Dim intStatusID As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("IDStatus"))
            If intStatusID = VO.Status.Values.Deleted And e.Appearance.BackColor <> Color.Salmon Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        End If
    End Sub

#End Region

End Class