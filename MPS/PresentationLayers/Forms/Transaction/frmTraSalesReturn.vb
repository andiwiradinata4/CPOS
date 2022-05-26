Imports DevExpress.XtraGrid

Public Class frmTraSalesReturn

    Private intPos As Integer = 0
    Private clsData As New VO.SalesReturn
    Private intCompanyID As Integer

    Private Const _
       cNew = 0, cDetail = 1, cDelete = 2, cSep1 = 3, cRefresh = 4, cClose = 5

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "ProgramID", "ProgramID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ProgramName", "Program", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "CompanyID", "CompanyID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CompanyName", "Perusahaan", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdView, "ID", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ReferencesID", "Nomor Referensi", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "BPID", "BPID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "BPName", "Pemasok", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "SalesReturnDate", "Tanggal", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "PaymentTerm", "PaymentTerm", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "DriverName", "Nama Supir", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PlatNumber", "Nomor Polisi", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PPN", " PPN", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "PPH", " PPH", 100, UI.usDefGrid.gReal2Num, False)
        UI.usForm.SetGrid(grdView, "ArrivalBrutto", "Brutto", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalTarra", "Tarra", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalNettoBefore", "Netto 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalDeduction", "Potongan", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalNettoAfter", "Netto 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Price", "Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPrice", "Total Harga", 100, UI.usDefGrid.gReal2Num)
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
        End With
    End Sub

    Private Sub prvFillCombo()
        Try
            Dim dtData As DataTable = BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionSalesReturn)
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

    Private Sub prvDefaultFilter()
        intCompanyID = MPSLib.UI.usUserApp.CompanyID
        txtCompanyName.Text = MPSLib.UI.usUserApp.CompanyName
    End Sub

    Private Sub prvQuery()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdMain.DataSource = BL.SalesReturn.ListData(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date, cboStatus.SelectedValue)
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

    Private Function prvGetData() As VO.SalesReturn
        Dim clsReturn As New VO.SalesReturn
        clsReturn.ProgramID = grdView.GetRowCellValue(intPos, "ProgramID")
        clsReturn.ProgramName = grdView.GetRowCellValue(intPos, "ProgramName")
        clsReturn.CompanyID = grdView.GetRowCellValue(intPos, "CompanyID")
        clsReturn.CompanyName = grdView.GetRowCellValue(intPos, "CompanyName")
        clsReturn.ID = grdView.GetRowCellValue(intPos, "ID")
        clsReturn.ReferencesID = grdView.GetRowCellValue(intPos, "ReferencesID")
        clsReturn.BPID = grdView.GetRowCellValue(intPos, "BPID")
        clsReturn.BPName = grdView.GetRowCellValue(intPos, "BPName")
        clsReturn.SalesReturnDate = grdView.GetRowCellValue(intPos, "SalesReturnDate")
        clsReturn.PaymentTerm = grdView.GetRowCellValue(intPos, "PaymentTerm")
        clsReturn.DriverName = grdView.GetRowCellValue(intPos, "DriverName")
        clsReturn.PlatNumber = grdView.GetRowCellValue(intPos, "PlatNumber")
        clsReturn.PPN = grdView.GetRowCellValue(intPos, "PPN")
        clsReturn.PPH = grdView.GetRowCellValue(intPos, "PPH")
        clsReturn.ArrivalBrutto = grdView.GetRowCellValue(intPos, "ArrivalBrutto")
        clsReturn.ArrivalTarra = grdView.GetRowCellValue(intPos, "ArrivalTarra")
        clsReturn.ArrivalNettoBefore = grdView.GetRowCellValue(intPos, "ArrivalNettoBefore")
        clsReturn.ArrivalDeduction = grdView.GetRowCellValue(intPos, "ArrivalDeduction")
        clsReturn.ArrivalNettoAfter = grdView.GetRowCellValue(intPos, "ArrivalNettoAfter")
        clsReturn.Price = grdView.GetRowCellValue(intPos, "Price")
        clsReturn.TotalPrice = grdView.GetRowCellValue(intPos, "TotalPrice")
        clsReturn.IDStatus = grdView.GetRowCellValue(intPos, "IDStatus")
        clsReturn.JournalID = grdView.GetRowCellValue(intPos, "JournalID")
        Return clsReturn
    End Function

    Private Sub prvNew()
        prvResetProgressBar()
        Dim frmDetail As New frmTraSalesReturnDet
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
        Dim frmDetail As New frmTraSalesReturnDet
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
        If Not UI.usForm.frmAskQuestion("Hapus Nomor " & clsData.ID & "?") Then Exit Sub

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
        pgMain.Value = 40
        Try
            BL.SalesReturn.DeleteData(clsData)
            pgMain.Value = 100
            UI.usForm.frmMessageBox("Hapus data berhasil.")
            pubRefresh(grdView.GetRowCellValue(intPos, "ID"))
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
        Dim SumTotalBrutto As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalBrutto", "Total Brutto: {0:#,##0.00}")
        Dim SumTotalTarra As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalTarra", "Total Tarra: {0:#,##0.00}")
        Dim SumTotalNetto1 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalNettoBefore", "Total Netto 1: {0:#,##0.00}")
        Dim SumTotalPotongan As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalDeduction", "Total Potongan: {0:#,##0.00}")
        Dim SumTotalNetto2 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalNettoAfter", "Total Netto 2: {0:#,##0.00}")
        Dim SumTotalPrice As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "Total Price: {0:#,##0.00}")

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

        If grdView.Columns("TotalPrice").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice").Summary.Add(SumTotalPrice)
        End If
    End Sub

    Private Sub prvUserAccess()
        With ToolBar.Buttons
            .Item(cNew).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesReturn, VO.Access.Values.NewAccess)
            .Item(cDelete).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesReturn, VO.Access.Values.DeleteAccess)
        End With
    End Sub

#Region "Form Handle"

    Private Sub frmTraSalesReturn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmTraSalesReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillCombo()
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
            End Select
        End If
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        prvQuery()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        prvClear()
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