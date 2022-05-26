Public Class frmSysJournalPost

#Region "Property"

    Private _
        intCoAIDofRevenue As Integer = 0, intCoAIDofRevenueRentalAlatBerat As Integer = 0, intCoAIDofRevenueRentalTruk As Integer = 0, _
        intCoAIDofAccountReceivable As Integer = 0, intCoAIDofSalesDiscount As Integer = 0, intCoAIDofPrepaidIncome As Integer = 0, _
        intCoAIDofCOGS As Integer = 0, intCoAIDofStock As Integer = 0, intCoAIDofCashOrBank As Integer = 0, intCoAIDofAccountPayable As Integer = 0, _
        intCoAIDofPurchaseDiscount As Integer = 0, intCoAIDofPurchaseEquipments As Integer = 0, intCoAIDofAdvancePayment As Integer = 0, _
        intCoAIDofSalesTax As Integer = 0, intCoAIDofPurchaseTax As Integer = 0
    Private clsData As New VO.JournalPost

    Private Const _
       cSave = 0, cClose = 1

#End Region

    Private Sub prvFillForm()
        Try
            clsData = BL.JournalPost.GetDetail(MPSLib.UI.usUserApp.ProgramID)
            intCoAIDofRevenue = clsData.CoAofRevenue
            txtCoACodeOfRevenue.Text = clsData.CoACodeofRevenue
            txtCoANameOfRevenue.Text = clsData.CoANameofRevenue

            intCoAIDofAccountReceivable = clsData.CoAofAccountReceivable
            txtCoACodeOfAccountReceivable.Text = clsData.CoACodeofAccountReceivable
            txtCoANameOfAccountReceivable.Text = clsData.CoANameofAccountReceivable

            intCoAIDofSalesDiscount = clsData.CoAofSalesDisc
            txtCoACodeOfSalesDiscount.Text = clsData.CoACodeofSalesDisc
            txtCoANameOfSalesDiscount.Text = clsData.CoANameofSalesDisc

            intCoAIDofPrepaidIncome = clsData.CoAofPrepaidIncome
            txtCoACodeOfPrepaidIncome.Text = clsData.CoACodeofPrepaidIncome
            txtCoANameOfPrepaidIncome.Text = clsData.CoANameofPrepaidIncome

            intCoAIDofCOGS = clsData.CoAofCOGS
            txtCoACodeOfCOGS.Text = clsData.CoACodeofCOGS
            txtCoANameOfCOGS.Text = clsData.CoANameofCOGS

            intCoAIDofStock = clsData.CoAofStock
            txtCoACodeOfStock.Text = clsData.CoACodeofStock
            txtCoANameOfStock.Text = clsData.CoANameofStock

            intCoAIDofCashOrBank = clsData.CoAofCash
            txtCoACodeOfCashOrBank.Text = clsData.CoACodeofCash
            txtCoANameOfCashOrBank.Text = clsData.CoANameofCash

            intCoAIDofAccountPayable = clsData.CoAofAccountPayable
            txtCoACodeOfAccountPayable.Text = clsData.CoACodeofAccountPayable
            txtCoANameOfAccountPayable.Text = clsData.CoANameofAccountPayable

            intCoAIDofPurchaseDiscount = clsData.CoAofPurchaseDisc
            txtCoACodeOfPurchaseDiscount.Text = clsData.CoACodeofPurchaseDisc
            txtCoANameOfPurchaseDiscount.Text = clsData.CoANameofPurchaseDisc

            intCoAIDofPurchaseEquipments = clsData.CoAofPurchaseEquipments
            txtCoACodeOfEquipments.Text = clsData.CoACodeofPurchaseEquipments
            txtCoANameOfEquipments.Text = clsData.CoANameofPurchaseEquipments

            intCoAIDofAdvancePayment = clsData.CoAofAdvancePayment
            txtCoACodeOfAdvancePayment.Text = clsData.CoACodeofAdvancePayment
            txtCoANameOfAdvancePayment.Text = clsData.CoANameofAdvancePayment

            intCoAIDofSalesTax = clsData.CoAofSalesTax
            txtCoACodeOfSalesTax.Text = clsData.CoACodeofSalesTax
            txtCoANameOfSalesTax.Text = clsData.CoANameofSalesTax

            intCoAIDofPurchaseTax = clsData.CoAofPurchaseTax
            txtCoACodeOfPurchaseTax.Text = clsData.CoACodeofPurchaseTax
            txtCoANameOfPurchaseTax.Text = clsData.CoANameofPurchaseTax

            ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
            ToolStripLogBy.Text = "Dibuat Oleh : " & IIf(clsData.LogBy Is Nothing, MPSLib.UI.usUserApp.UserID, clsData.LogBy)
            ToolStripLogDate.Text = Format(IIf(clsData.LogBy Is Nothing, Now(), clsData.LogDate), UI.usDefCons.DateFull)
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        'If txtCoACodeOfRevenue.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun pendapatan belum dipilih")
        '    txtCoACodeOfRevenue.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfAccountReceivable.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun piutang usaha belum dipilih")
        '    txtCoACodeOfAccountReceivable.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfSalesDiscount.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun potongan penjualan belum dipilih")
        '    txtCoACodeOfSalesDiscount.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfPrepaidIncome.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun pendapatan diterima dimuka belum dipilih")
        '    txtCoACodeOfPrepaidIncome.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfCOGS.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun harga pokok penjualan belum dipilih")
        '    txtCoACodeOfCOGS.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfStock.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun persediaan belum dipilih")
        '    txtCoACodeOfStock.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfCashOrBank.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun kas / bank belum dipilih")
        '    txtCoACodeOfCashOrBank.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfAccountPayable.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun hutang usaha belum dipilih")
        '    txtCoACodeOfAccountPayable.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfPurchaseDiscount.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun potongan pembelian belum dipilih")
        '    txtCoACodeOfPurchaseDiscount.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfEquipments.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun peralatan belum dipilih")
        '    txtCoACodeOfEquipments.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfAdvancePayment.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun pembayaran dimuka belum dipilih")
        '    txtCoACodeOfAdvancePayment.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfSalesTax.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun pajak penjualan belum dipilih")
        '    txtCoACodeOfSalesTax.Focus()
        '    Exit Sub
        'ElseIf txtCoACodeOfPurchaseTax.Text.Trim = "" Then
        '    UI.usForm.frmMessageBox("Akun pajak pembelian belum dipilih")
        '    txtCoACodeOfPurchaseTax.Focus()
        '    Exit Sub
        'End If

        pgMain.Value = 30
        clsData = New VO.JournalPost
        clsData.ProgramID = MPSLib.UI.usUserApp.ProgramID
        clsData.CoAofRevenue = intCoAIDofRevenue
        clsData.CoAofAccountReceivable = intCoAIDofAccountReceivable
        clsData.CoAofSalesDisc = intCoAIDofSalesDiscount
        clsData.CoAofPrepaidIncome = intCoAIDofPrepaidIncome
        clsData.CoAofCOGS = intCoAIDofCOGS
        clsData.CoAofStock = intCoAIDofStock
        clsData.CoAofCash = intCoAIDofCashOrBank
        clsData.CoAofAccountPayable = intCoAIDofAccountPayable
        clsData.CoAofPurchaseDisc = intCoAIDofPurchaseDiscount
        clsData.CoAofPurchaseEquipments = intCoAIDofPurchaseEquipments
        clsData.CoAofAdvancePayment = intCoAIDofAdvancePayment
        clsData.CoAofSalesTax = intCoAIDofSalesTax
        clsData.CoAofPurchaseTax = intCoAIDofPurchaseTax
        clsData.Remarks = ""
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Me.Cursor = Cursors.WaitCursor
        Try
            If BL.JournalPost.SaveData(clsData) Then
                pgMain.Value = 100
                UI.usForm.frmMessageBox("Simpan data berhasil.")
                MPSLib.UI.usUserApp.JournalPost = BL.JournalPost.GetDetail(MPSLib.UI.usUserApp.ProgramID)
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
        End Try
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.SettingSetupPostingJournalTransaction, VO.Access.Values.EditAccess)
    End Sub

#Region "Form Handle"

    Private Sub frmSysJournalPost_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmSysJournalPost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillForm()
        prvUserAccess()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnCoAOfRevenue_Click(sender As Object, e As EventArgs) Handles btnCoAOfRevenue.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofRevenue = .pubLUdtRow.Item("ID")
                txtCoACodeOfRevenue.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfRevenue.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfAccountReceivable_Click(sender As Object, e As EventArgs) Handles btnCoAOfAccountReceivable.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofAccountReceivable = .pubLUdtRow.Item("ID")
                txtCoACodeOfAccountReceivable.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfAccountReceivable.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfSalesDiscount_Click(sender As Object, e As EventArgs) Handles btnCoAOfSalesDiscount.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofSalesDiscount = .pubLUdtRow.Item("ID")
                txtCoACodeOfSalesDiscount.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfSalesDiscount.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfPrepaidIncome_Click(sender As Object, e As EventArgs) Handles btnCoAOfPrepaidIncome.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofPrepaidIncome = .pubLUdtRow.Item("ID")
                txtCoACodeOfPrepaidIncome.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfPrepaidIncome.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfCOGS_Click(sender As Object, e As EventArgs) Handles btnCoAOfCOGS.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofCOGS = .pubLUdtRow.Item("ID")
                txtCoACodeOfCOGS.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfCOGS.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfStock_Click(sender As Object, e As EventArgs) Handles btnCoAOfStock.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofStock = .pubLUdtRow.Item("ID")
                txtCoACodeOfStock.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfStock.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfCashOrBank_Click(sender As Object, e As EventArgs) Handles btnCoAOfCashOrBank.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofCashOrBank = .pubLUdtRow.Item("ID")
                txtCoACodeOfCashOrBank.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfCashOrBank.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfAccountPayable_Click(sender As Object, e As EventArgs) Handles btnCoAOfAccountPayable.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofAccountPayable = .pubLUdtRow.Item("ID")
                txtCoACodeOfAccountPayable.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfAccountPayable.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfPurchaseDiscount_Click(sender As Object, e As EventArgs) Handles btnCoAOfPurchaseDiscount.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofPurchaseDiscount = .pubLUdtRow.Item("ID")
                txtCoACodeOfPurchaseDiscount.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfPurchaseDiscount.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfEquipments_Click(sender As Object, e As EventArgs) Handles btnCoAOfEquipments.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofPurchaseEquipments = .pubLUdtRow.Item("ID")
                txtCoACodeOfEquipments.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfEquipments.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfAdvancePayment_Click(sender As Object, e As EventArgs) Handles btnCoAOfAdvancePayment.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofAdvancePayment = .pubLUdtRow.Item("ID")
                txtCoACodeOfAdvancePayment.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfAdvancePayment.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfSalesTax_Click(sender As Object, e As EventArgs) Handles btnCoAOfSalesTax.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofSalesTax = .pubLUdtRow.Item("ID")
                txtCoACodeOfSalesTax.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfSalesTax.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

    Private Sub btnCoAOfPurchaseTax_Click(sender As Object, e As EventArgs) Handles btnCoAOfPurchaseTax.Click
        Dim frmDetail As New frmMstChartOfAccount
        With frmDetail
            .pubIsLookUp = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCoAIDofPurchaseTax = .pubLUdtRow.Item("ID")
                txtCoACodeOfPurchaseTax.Text = .pubLUdtRow.Item("Code")
                txtCoANameOfPurchaseTax.Text = .pubLUdtRow.Item("Name")
            End If
        End With
    End Sub

#End Region

End Class