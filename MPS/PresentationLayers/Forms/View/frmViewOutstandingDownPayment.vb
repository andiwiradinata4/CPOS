Imports DevExpress.XtraGrid
Public Class frmViewOutstandingDownPayment

#Region "Property"

    Private intPos As Integer = 0
    Private clsData As New VO.DownPayment
    Private intCompanyID As Integer, intProgramID As Integer, intBPID As Integer, intBPID2 As Integer
    Private intDPType As VO.DownPayment.Type
    Private dtItem As New DataTable
    Private dtRow() As DataRow
    Private dtParent As New DataTable
    Private bolIsLookUpGet As Boolean = False

    Public WriteOnly Property pubCompanyID As Integer
        Set(value As Integer)
            intCompanyID = value
        End Set
    End Property
    
    Public WriteOnly Property pubProgramID As Integer
        Set(value As Integer)
            intProgramID = value
        End Set
    End Property

    Public WriteOnly Property pubBPID As Integer
        Set(value As Integer)
            intBPID = value
        End Set
    End Property

    Public WriteOnly Property pubBPID2 As Integer
        Set(value As Integer)
            intBPID2 = value
        End Set
    End Property

    Public WriteOnly Property pubDPType As VO.DownPayment.Type
        Set(value As VO.DownPayment.Type)
            intDPType = value
        End Set
    End Property

    Public WriteOnly Property pubTableParent As DataTable
        Set(value As DataTable)
            dtParent = value
        End Set
    End Property

    Public ReadOnly Property pubLURow As DataRow()
        Get
            Return dtRow
        End Get
    End Property

    Public ReadOnly Property pubIsLookUpGet As Boolean
        Get
            Return bolIsLookUpGet
        End Get
    End Property

#End Region

    Private Const _
       cGet = 0, cSep1 = 1, cRefresh = 2, cClose = 3

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "Pick", "Pilih", 100, UI.usDefGrid.gBoolean, True, False)
        UI.usForm.SetGrid(grdView, "ID", "Nomor", 100, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "DPType", "DPType", 100, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "DPDate", "Tanggal", 100, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdView, "TotalAmount", "Total Panjar", 100, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "MaxTotalAmount", "Maks. Panjar", 100, UI.usDefGrid.gReal2Num)
    End Sub

    Private Sub prvSetButton()
        Dim bolEnable As Boolean = IIf(grdView.RowCount > 0, True, False)
        With ToolBar.Buttons
            .Item(cGet).Enabled = bolEnable
        End With
    End Sub

    Private Sub prvQuery()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            dtItem = BL.DownPayment.ListDataForLookup(intCompanyID, intProgramID, intBPID, intDPType, intBPID2)
            grdMain.DataSource = dtItem
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

    Private Sub prvSumGrid()
        Dim SumTotalAmount As New GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "Total Pembayaran: {0:#,##0.00}")

        If grdView.Columns("TotalAmount").SummaryText.Trim = "" Then
            grdView.Columns("TotalAmount").Summary.Add(SumTotalAmount)
        End If
    End Sub

    Private Sub prvGet()
        ToolBar.Focus()
        Dim drPick() As DataRow = dtItem.Select("Pick=True")
        If drPick.Count = 0 Then UI.usForm.frmMessageBox("Mohon pilih panjar terlebih dahulu") : Exit Sub
        If Not UI.usForm.frmAskQuestion("Ambil semua item yang terpilih?") Then Exit Sub

        Dim bolExists As Boolean = False
        For Each drParent As DataRow In dtParent.Rows
            For Each dr As DataRow In dtItem.Rows
                If drParent.Item("DPID") = dr.Item("ID") And dr.Item("Pick") Then
                    bolExists = True
                    UI.usForm.frmMessageBox("Nomor " & dr.Item("ID") & " telah dipilih sebelumnya")
                    Exit For
                End If
            Next
            If bolExists Then Exit For
        Next

        If bolExists Then
            Exit Sub
        Else
            dtRow = drPick
            bolIsLookUpGet = True
            Me.Close()
        End If
    End Sub

#Region "Form Handle"

    Private Sub frmViewOutstandingDownPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetGrid()
        prvQuery()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Ambil" : prvGet()
            Case "Refresh" : prvQuery()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

#End Region

End Class