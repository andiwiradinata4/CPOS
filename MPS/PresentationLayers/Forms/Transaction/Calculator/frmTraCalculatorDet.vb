Imports DevExpress.XtraGrid
Public Class frmTraCalculatorDet

#Region "Property"

    Private clsData As VO.Calculator
    Private intBPID As Integer = 0
    Private dtData As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = "", strTransactionNo As String = ""
    Private bolLogOut As Boolean
    Private decAmount As Decimal
    Property pubID As String
    Property pubIsNew As Boolean = True
    Property pubIsSave As Boolean = False
    Property pubIsFromMain As Boolean = True
    Property pubCS As New VO.CS

#End Region

    Private Sub prvSetGrid()
        UI.usForm.SetGrid(grdView, "Idx", "No", 80, UI.usDefGrid.gIntNum, False)
        UI.usForm.SetGrid(grdView, "Remarks", "#", 300, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdView, "Amount", "Nilai", 350, UI.usDefGrid.gReal2Num)
        UI.usForm.SetGrid(grdView, "Symbol", "...", 100, UI.usDefGrid.gString)
    End Sub

    Private Sub prvSetColumn()
        dtData.Columns.Add("Idx", GetType(Integer))
        dtData.Columns.Add("Remarks", GetType(String))
        dtData.Columns.Add("Amount", GetType(Decimal))
        dtData.Columns.Add("Symbol", GetType(String))
    End Sub

    Private Sub prvTableHandle(ByVal strRemarks As String, ByVal decValue As Decimal, ByVal strSymbol As String)
        Dim dr As DataRow
        dr = dtData.NewRow
        dr.BeginEdit()
        dr.Item("Idx") = dtData.Rows.Count
        dr.Item("Remarks") = strRemarks
        dr.Item("Amount") = decValue
        dr.Item("Symbol") = strSymbol
        dr.EndEdit()
        dtData.Rows.Add(dr)
        dtData.AcceptChanges()
        txtTotalAmount.Value = decAmount
    End Sub

    Private Sub prvAdd(ByVal decValue As Decimal)
        If decValue = 0 Then Exit Sub
        If decValue < 0 Then decValue *= -1
        decAmount += decValue
        prvTableHandle("", decValue, "+")
    End Sub

    Private Sub prvMinus(ByVal decValue As Decimal)
        If decValue = 0 Then Exit Sub
        If decValue < 0 Then decValue *= -1
        decAmount -= decValue
        prvTableHandle("", decValue, "-")
    End Sub

    Private Sub prvMultiply(ByVal decValue As Decimal)
        If decValue = 0 Then prvResetAmount() : Exit Sub
        If cboSymbol.SelectedIndex <> 1 Then Exit Sub
        decAmount += txtValueCombine.Value * decValue
        prvTableHandle(txtValueCombine.Value & " x " & decValue, txtValueCombine.Value * decValue, "+")
    End Sub

    Private Sub prvResetAmount()
        txtValue.Value = 0
        txtValue.Focus()
        txtValueCombine.Value = 0
        cboSymbol.SelectedIndex = 0
    End Sub

    Private Sub prvClear()
        dtData.Clear()
        dtData.AcceptChanges()
        decAmount = 0
        txtTotalAmount.Value = 0
        txtValue.Focus()
        txtValueCombine.Value = 0
        cboSymbol.SelectedIndex = 0
        dtpDate.Value = Now
        intBPID = 0
        txtBPName.Text = ""
        txtBPAddress.Text = ""
    End Sub

    Private Sub prvPay()
        If intBPID = 0 Then
            UI.usForm.frmMessageBox("Pilih pelanggan terlebih dahulu")
            txtBPName.Focus()
            Exit Sub
        End If

        If txtTotalAmount.Value <= 0 Then Exit Sub
        Dim clsDataDetail As New VO.CalculatorDet
        Dim clsDataDetailAll(grdView.RowCount - 1) As VO.CalculatorDet

        clsData = New VO.Calculator
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = pubID
        clsData.BPID = intBPID
        clsData.TransactionNo = strTransactionNo
        clsData.TransactionDate = Now
        clsData.TotalPrice = txtTotalAmount.Value
        clsData.GrandTotal = clsData.TotalPrice + clsData.TotalPPN - clsData.TotalPPN
        clsData.IDStatus = VO.Status.Values.Draft
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.Remarks = ""

        With grdView
            For i As Integer = 0 To .RowCount - 1
                clsDataDetail = New VO.CalculatorDet
                clsDataDetail.Idx = i + 1
                clsDataDetail.Remarks = .GetRowCellValue(i, "Remarks")
                clsDataDetail.Amount = .GetRowCellValue(i, "Amount")
                clsDataDetail.Symbol = .GetRowCellValue(i, "Symbol")
                clsDataDetailAll(i) = clsDataDetail
            Next
        End With

        Dim frmDetail As New frmTraCalculatorPayment
        With frmDetail
            .pubIsNew = pubIsNew
            .pubData = clsData
            .pubDataDetail = clsDataDetailAll
            .ShowDialog()
            If .pubIsSuccess Then
                prvClear()
            End If
        End With
    End Sub

    Private Sub prvDelete()
        intPos = grdView.FocusedRowHandle
        If intPos < 0 Then Exit Sub
        Dim strSymbol As String = grdView.GetRowCellValue(intPos, "Symbol")
        Dim decGridAmount As String = grdView.GetRowCellValue(intPos, "Amount")

        If strSymbol = "+" Then
            decAmount -= decGridAmount
        ElseIf strSymbol = "-" Then
            decAmount += decGridAmount
        End If

        txtTotalAmount.Value = decAmount

        grdView.DeleteSelectedRows()
        dtData.AcceptChanges()
        With dtData
            For i As Integer = 0 To dtData.Rows.Count - 1
                .Rows(i).BeginEdit()
                .Rows(i).Item("Idx") = i + 1
                .Rows(i).EndEdit()
            Next
            .AcceptChanges()
        End With
        txtValue.Focus()
    End Sub

    Private Sub prvGetBP()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
            .pubCompanyID = 0 'IIf(pubIsFromMain, MPSLib.UI.usUserApp.CompanyID, pubCS.CompanyID)
            .pubProgramID = 0 'IIf(pubIsFromMain, MPSLib.UI.usUserApp.ProgramID, pubCS.ProgramID)
            .ShowDialog()
            If .pubIsLookUpGet Then
                intBPID = .pubLUdtRow.Item("ID")
                txtBPName.Text = .pubLUdtRow.Item("Name")
                txtBPAddress.Text = .pubLUdtRow.Item("Address")
                txtValue.Focus()
            End If
        End With
    End Sub

    Private Sub prvViewSales()
        Dim frmDetail As New frmTraCalculator
        With frmDetail
            .WindowState = FormWindowState.Maximized
            .ShowDialog()
        End With
    End Sub

    Private Sub prvChangePassword()
        Dim frmDetail As New frmMstUserChangePassword
        With frmDetail
            .pubID = MPSLib.UI.usUserApp.UserID
            .ShowDialog()
        End With
    End Sub

    Private Sub prvFillForm()
        If pubIsNew Then Exit Sub
        Try
            clsData = New VO.Calculator
            clsData = BL.Calculator.GetDetail(pubID)
            strTransactionNo = clsData.TransactionNo
            txtTotalAmount.Value = clsData.TotalPrice
            dtpDate.Value = clsData.TransactionDate
            intBPID = clsData.BPID
            txtBPName.Text = clsData.BPName
            txtBPAddress.Text = clsData.BPAddress
            dtData = BL.Calculator.ListDataDetail(clsData.ID)
            grdMain.DataSource = dtData
            tmrNow.Stop()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmSalesInput_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Add Then
            prvAdd(txtValue.Value)
            prvResetAmount()
        ElseIf e.KeyCode = Keys.Subtract Then
            prvMinus(txtValue.Value)
            prvResetAmount()
        ElseIf e.KeyCode = Keys.Multiply Then
            If txtValue.Value = 0 Then Exit Sub
            txtValueCombine.Value = IIf(txtValue.Value < 0, txtValue.Value * -1, txtValue.Value)
            cboSymbol.SelectedIndex = 1
            txtValue.Value = 0
            txtValue.Focus()
        ElseIf e.KeyCode = Keys.F1 Then
            prvGetBP()
        ElseIf e.KeyCode = Keys.F9 Then
            prvChangePassword()
        ElseIf e.KeyCode = Keys.F10 Then
            prvClear()
        ElseIf e.KeyCode = Keys.F11 Then
            prvPay()
        ElseIf e.KeyCode = Keys.F12 Then
            prvViewSales()
        ElseIf e.KeyCode = Keys.Enter Then
            If cboSymbol.SelectedIndex = 0 Then
                prvAdd(txtValue.Value)
            ElseIf cboSymbol.SelectedIndex = 1 Then
                prvMultiply(txtValue.Value)
            End If
            prvResetAmount()
        ElseIf e.KeyCode = Keys.Delete Then
            prvDelete()
        End If
        grdView.MoveLast()
    End Sub

    Private Sub frmSalesInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        bolLogOut = False
        prvSetGrid()
        prvSetColumn()
        grdMain.DataSource = dtData
        txtValue.Focus()
        dtpDate.Value = Now
        tmrNow.Start()
        prvFillForm()
        If Not pubIsFromMain Then
            btnChangePassword.Visible = False
            btnClear.Visible = False
            btnPay.Visible = False
            btnSales.Visible = False
        Else
            pubCS.ProgramID = MPSLib.UI.usUserApp.ProgramID
            pubCS.CompanyID = MPSLib.UI.usUserApp.CompanyID
        End If
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not pubIsFromMain Then

        Else
            If Not bolLogOut Then
                If UI.usForm.frmAskQuestion("Keluar dari sistem ?") Then
                    Application.Exit()
                Else
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub btnBP_Click(sender As Object, e As EventArgs) Handles btnBP.Click
        prvGetBP()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        prvClear()
    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        prvPay()
    End Sub

    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        prvViewSales()
    End Sub

    Private Sub tmrNow_Tick(sender As Object, e As EventArgs) Handles tmrNow.Tick
        dtpDate.Value = Now
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        prvChangePassword()
    End Sub

#End Region

End Class