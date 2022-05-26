Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraGrid
Public Class frmTraImportSales

    Private WithEvents ofd As New OpenFileDialog

    Private Const _
       cSave = 0, cChooseFileExcel = 1, cClose = 2

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "CustomerID", "CustomerID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "CustomerName", "Pelanggan", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "SupplierID", "SupplierID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "SupplierName", "Pemasok", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "SalesDate", "Tanggal", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "DueDate", "Jatuh Tempo", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "DriverName", "Nama Supir", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "PlatNumber", "Nomor Polisi", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Remarks", "Remarks", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ItemID", "ItemID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "ItemName", "ItemName", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "UOMID", "UOMID", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "UOMName", "UOM", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "ArrivalBrutto", "Brutto", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalTarra", "Tarra", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalNettoBefore", "Netto 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalDeduction", "Potongan", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "ArrivalNettoAfter", "Netto 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Price", "Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPrice", "Total Harga", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "PurchasePrice1", "Harga Beli 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPrice1", "Total Harga Beli 1", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "PurchasePrice2", "Harga Beli 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "TotalPrice2", "Total Harga Beli 2", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Tolerance", "Toleransi", 100, UI.usDefGrid.gReal2Num)
        prvSumGrid()
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cSave).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvOpenFile()
        With ofd
            .Filter = "Excel files |*.xls;*.xlsx"
            ofd.ShowDialog()
        End With

    End Sub

    Private Sub prvLoadFile()
        Dim bolValid As Boolean = True
        Dim strFilePath As String = ofd.FileName
        Dim MyConnection As New System.Data.OleDb.OleDbConnection
        Dim dtSet As New System.Data.DataSet, dtData As New DataTable
        Dim MyCommand As New System.Data.OleDb.OleDbDataAdapter
        Dim strSheetName As String = "Penjualan$"

        If bolValid = False Or strFilePath.Trim = "" Then Exit Sub

        Try
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & strFilePath & ";Extended Properties=Excel 12.0;")
            MyConnection.Open()
            'MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & strFilePath & ";Extended Properties=Excel 8.0;")
            MyCommand = New System.Data.OleDb.OleDbDataAdapter _
                ( _
                "SELECT CustomerID, CustomerName, SupplierID, SupplierName, SalesDate, DueDate, DriverName, PlatNumber, Remarks, ItemID, ItemName, UOMID, UOMName, " & _
                "ArrivalBrutto, ArrivalTarra, ArrivalNettoBefore, ArrivalDeduction, ArrivalNettoAfter, Price, TotalPrice, PurchasePrice1, TotalPrice1, PurchasePrice2, " & _
                "TotalPrice2, Tolerance FROM [" & strSheetName & "]", _
                MyConnection
                )
            MyCommand.TableMappings.Add("Table", "Net-informations.com")
            dtSet = New System.Data.DataSet
            MyCommand.Fill(dtSet)
            dtData = dtSet.Tables(0)
            grdMain.DataSource = dtData
            grdView.BestFitColumns()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            MyCommand.Dispose()
            MyConnection.Close()
            prvSetButton()
        End Try
    End Sub

    Private Sub prvSave()
        ToolBar.Focus()

        If Not UI.usForm.frmAskQuestion("Simpan semua data yang telah diimport?") Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 50
        With grdView
            '# Checking Null Value
            For i As Integer = 0 To .RowCount - 1
                If IsDBNull(.GetRowCellValue(i, "CustomerID")) Or IsDBNull(.GetRowCellValue(i, "CustomerName")) Then
                    UI.usForm.frmMessageBox("Kolom CustomerID / CustomerName harus diisi terlebih dahulu")
                    Exit Sub
                ElseIf IsDBNull(.GetRowCellValue(i, "SupplierID")) Or IsDBNull(.GetRowCellValue(i, "SupplierName")) Then
                    UI.usForm.frmMessageBox("Kolom SupplierID / SupplierName harus diisi terlebih dahulu")
                    Exit Sub
                ElseIf IsDBNull(.GetRowCellValue(i, "PlatNumber")) Or .GetRowCellValue(i, "PlatNumber") = "" Then
                    UI.usForm.frmMessageBox("Kolom PlatNumber harus diisi terlebih dahulu")
                    Exit Sub
                ElseIf IsDBNull(.GetRowCellValue(i, "DriverName")) Or .GetRowCellValue(i, "DriverName") = "" Then
                    UI.usForm.frmMessageBox("Kolom DriverName harus diisi terlebih dahulu")
                    Exit Sub
                ElseIf IsDBNull(.GetRowCellValue(i, "ItemID")) Or IsDBNull(.GetRowCellValue(i, "ItemName")) Then
                    UI.usForm.frmMessageBox("Kolom ItemID / ItemName harus diisi terlebih dahulu")
                    Exit Sub
                ElseIf IsDBNull(.GetRowCellValue(i, "ArrivalBrutto")) Or .GetRowCellValue(i, "ArrivalBrutto") < 0 Then
                    UI.usForm.frmMessageBox("Kolom ArrivalBrutto harus lebih besar dari 0")
                    Exit Sub
                ElseIf IsDBNull(.GetRowCellValue(i, "UOMID")) Or IsDBNull(.GetRowCellValue(i, "UOMName")) Then
                    UI.usForm.frmMessageBox("Kolom UOMID / UOMName harus diisi terlebih dahulu")
                    Exit Sub
                End If
            Next

            Dim clsData As New VO.Sales
            Dim clsDataAll(.RowCount - 1) As VO.Sales
            Dim clsReceive As New VO.Receive
            Dim clsReceiveAll(.RowCount - 1) As VO.Receive
            For i As Integer = 0 To .RowCount - 1
                '# Sales
                clsData = New VO.Sales
                clsData.ProgramID = MPSLib.UI.usUserApp.ProgramID
                clsData.CompanyID = MPSLib.UI.usUserApp.CompanyID
                clsData.ID = ""
                clsData.BPID = .GetRowCellValue(i, "CustomerID")
                clsData.BPName = .GetRowCellValue(i, "CustomerName")
                clsData.SupplierID = .GetRowCellValue(i, "SupplierID")
                clsData.SupplierName = .GetRowCellValue(i, "SupplierName")
                clsData.SalesDate = CDate(.GetRowCellValue(i, "SalesDate")).AddHours(23).AddMinutes(59)
                clsData.PaymentTerm = 1 '# Cash
                clsData.DueDate = CDate(.GetRowCellValue(i, "DueDate")).AddHours(23).AddMinutes(59)
                clsData.DriverName = .GetRowCellValue(i, "DriverName")
                clsData.PlatNumber = .GetRowCellValue(i, "PlatNumber")
                clsData.Remarks = IIf(IsDBNull(.GetRowCellValue(i, "Remarks")), "", .GetRowCellValue(i, "Remarks"))
                clsData.ItemID = .GetRowCellValue(i, "ItemID")
                clsData.ItemCode = ""
                clsData.ItemName = .GetRowCellValue(i, "ItemName")
                clsData.UOMID = .GetRowCellValue(i, "UOMID")
                clsData.ArrivalBrutto = .GetRowCellValue(i, "ArrivalBrutto")
                clsData.ArrivalTarra = .GetRowCellValue(i, "ArrivalTarra")
                clsData.ArrivalNettoBefore = clsData.ArrivalBrutto - clsData.ArrivalTarra
                clsData.ArrivalDeduction = .GetRowCellValue(i, "ArrivalDeduction")
                clsData.ArrivalNettoAfter = clsData.ArrivalNettoBefore - clsData.ArrivalDeduction
                clsData.Price = .GetRowCellValue(i, "Price")
                clsData.TotalPrice = .GetRowCellValue(i, "TotalPrice")
                clsData.IDStatus = VO.Status.Values.Draft
                clsData.LogBy = MPSLib.UI.usUserApp.UserID
                clsData.JournalID = ""
                clsData.PurchasePrice1 = .GetRowCellValue(i, "PurchasePrice1")
                clsData.PurchasePrice2 = .GetRowCellValue(i, "PurchasePrice2")
                clsData.Tolerance = .GetRowCellValue(i, "Tolerance")
                clsDataAll(i) = clsData
            Next
            Me.Refresh()
            Try
                Dim bolSuccess As Boolean = BL.Sales.ImportData(clsDataAll)
                Me.Cursor = Cursors.Default
                pgMain.Value = 100
                If bolSuccess Then
                    UI.usForm.frmMessageBox("Import data penjualan berhasil")
                Else
                    UI.usForm.frmMessageBox("Import data penjualan gagal")
                End If
            Catch ex As Exception
                pgMain.Value = 100
                Me.Cursor = Cursors.Default
                UI.usForm.frmMessageBox(ex.Message)
            Finally
                prvClear()
                pgMain.Value = 0
                Me.Cursor = Cursors.Default
            End Try
        End With
    End Sub

    Private Sub prvClear()
        grdMain.DataSource = Nothing
        grdView.Columns.Clear()
        prvSetGrid()
        prvSetButton()
    End Sub

    Private Sub prvSumGrid()
        Dim SumTotalBrutto As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalBrutto", "Total Brutto: {0:#,##0.00}")
        Dim SumTotalTarra As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalTarra", "Total Tarra: {0:#,##0.00}")
        Dim SumTotalNetto1 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalNettoBefore", "Total Netto 1: {0:#,##0.00}")
        Dim SumTotalPotongan As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalDeduction", "Total Potongan: {0:#,##0.00}")
        Dim SumTotalNetto2 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ArrivalNettoAfter", "Total Netto 2: {0:#,##0.00}")
        Dim SumTotalPrice As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "Total Price: {0:#,##0.00}")
        Dim SumTotalPrice1 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice1", "Total Harga Beli 1: {0:#,##0.00}")
        Dim SumTotalPrice2 As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice2", "Total Harga Beli 2: {0:#,##0.00}")

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

        If grdView.Columns("TotalPrice1").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice1").Summary.Add(SumTotalPrice1)
        End If

        If grdView.Columns("TotalPrice2").SummaryText.Trim = "" Then
            grdView.Columns("TotalPrice2").Summary.Add(SumTotalPrice2)
        End If
    End Sub

#Region "Form Handle"

    Private Sub frmTraImportSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvSetButton()

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Name
            Case ToolBar.Buttons(cSave).Name : prvSave()
            Case ToolBar.Buttons(cChooseFileExcel).Name : ofd.ShowDialog()
            Case ToolBar.Buttons(cClose).Name : Me.Close()
        End Select
    End Sub

    Private Sub ofd_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofd.FileOk
        prvLoadFile()
    End Sub

#End Region

End Class