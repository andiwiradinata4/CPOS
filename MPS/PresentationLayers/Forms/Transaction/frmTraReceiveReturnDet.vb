Public Class frmTraReceiveReturnDet

#Region "Property"

    Private frmParent As Object
    Private clsData As VO.ReceiveReturn
    Private intBPID As Integer
    Private dtItem As New DataTable
    Private intPos As Integer = 0
    Private strJournalID As String = ""
    Private intItemID As Integer = 0
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
       cSave = 0, cClose = 1,
       cAdd = 0, cEdit = 1, cDelete = 2

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
        UI.usForm.SetGrid(grdStatusView, "ID", "ID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "ReceiveReturnID", "ReceiveReturnID", 100, UI.usDefGrid.gString, False)
        UI.usForm.SetGrid(grdStatusView, "Status", "Status", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusBy", "Oleh", 200, UI.usDefGrid.gString)
        UI.usForm.SetGrid(grdStatusView, "StatusDate", "Tanggal", 180, UI.usDefGrid.gFullDate)
        UI.usForm.SetGrid(grdStatusView, "Remarks", "Keterangan", 300, UI.usDefGrid.gString)
    End Sub

    Private Sub prvFillStatus()
        Try
            UI.usForm.FillComboBox(cboStatus, BL.StatusModules.ListDataByModulesID(VO.Modules.Values.TransactionReceiveReturn), "IDStatus", "StatusName")
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
                clsData = New VO.ReceiveReturn
                clsData = BL.ReceiveReturn.GetDetail(pubID)
                txtID.Text = clsData.ID
                dtpReceiveReturnDate.Value = clsData.ReceiveReturnDate
                txtReferencesID.Text = clsData.ReferencesID
                intBPID = clsData.BPID
                txtBPName.Text = clsData.BPName
                cboPaymentTerm.SelectedValue = clsData.PaymentTerm
                txtPlatNumber.Text = clsData.PlatNumber
                txtDriverName.Text = clsData.DriverName
                txtDONumber.Text = clsData.DONumber
                txtSPBNumber.Text = clsData.SPBNumber
                txtSegelNumber.Text = clsData.SegelNumber
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
                txtMaxNetto.Value = clsData.MaxNetto + (clsData.ArrivalUsage * clsData.Tolerance / 100)
                txtPrice1.Value = clsData.Price1
                txtTotalPrice1.Value = clsData.TotalPrice1
                txtPrice2.Value = clsData.Price2
                txtTotalPrice2.Value = clsData.TotalPrice2
                txtArrivalNettoAfterReceive.Value = clsData.ArrivalNettoAfterReceive
                txtArrivalNettoUsage.Value = clsData.ArrivalUsage
                txtSpecification.Text = clsData.Specification
                cboStatus.SelectedValue = clsData.IDStatus
                ToolStripLogInc.Text = "Jumlah Edit : " & clsData.LogInc
                ToolStripLogBy.Text = "Dibuat Oleh : " & clsData.LogBy
                ToolStripLogDate.Text = Format(clsData.LogDate, UI.usDefCons.DateFull)
                strJournalID = clsData.JournalID
                btnReferences.Enabled = False
                btnBP.Enabled = False
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
        If txtBPName.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih pelanggan terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            txtBPName.Focus()
            Exit Sub
        ElseIf cboPaymentTerm.SelectedIndex = -1 Then
            UI.usForm.frmMessageBox("Pilih jenis pembayaran terlebih dahulu")
            tcHeader.SelectedTab = tpMain
            cboPaymentTerm.Focus()
            Exit Sub
        ElseIf txtReferencesID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Pilih nomor referensi terlebih dahulu")
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
            txtItemCode.Focus()
            Exit Sub
        ElseIf txtBrutto.Value <= 0 Then
            UI.usForm.frmMessageBox("Brutto harus lebih besar dari 0")
            txtBrutto.Focus()
            Exit Sub
        ElseIf txtNettoAfter.Value > txtMaxNetto.Value Then
            UI.usForm.frmMessageBox("Nilai Netto 2 tidak boleh lebih besar dari nilai Max. Netto")
            txtBrutto.Focus()
            Exit Sub
        ElseIf txtPrice1.Value <= 0 Then
            UI.usForm.frmMessageBox("Harga 1 harus lebih besar dari 0")
            txtPrice1.Focus()
            Exit Sub
        ElseIf txtPrice2.Value <= 0 Then
            UI.usForm.frmMessageBox("Harga 2 harus lebih besar dari 0")
            txtPrice2.Focus()
            Exit Sub
        ElseIf cboStatus.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Status kosong. Mohon untuk tutup form dan buka kembali")
            cboStatus.Focus()
            Exit Sub
        ElseIf cboUOMID.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Satuan kosong. Mohon untuk tutup form dan buka kembali")
            cboUOMID.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data retur pembelian?") Then Exit Sub

        clsData = New VO.ReceiveReturn
        clsData.ProgramID = pubCS.ProgramID
        clsData.CompanyID = pubCS.CompanyID
        clsData.ID = txtID.Text.Trim
        clsData.ReceiveReturnDate = dtpReceiveReturnDate.Value
        clsData.BPID = intBPID
        clsData.BPName = txtBPName.Text.Trim
        clsData.PaymentTerm = cboPaymentTerm.SelectedValue
        clsData.ReferencesID = txtReferencesID.Text.Trim
        clsData.PlatNumber = txtPlatNumber.Text.Trim
        clsData.DriverName = txtDriverName.Text.Trim
        clsData.DONumber = txtDONumber.Text.Trim
        clsData.SPBNumber = txtSPBNumber.Text.Trim
        clsData.SegelNumber = txtSegelNumber.Text.Trim
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
        clsData.Price1 = txtPrice1.Value
        clsData.TotalPrice1 = txtTotalPrice1.Value
        clsData.Price2 = txtPrice2.Value
        clsData.TotalPrice2 = txtTotalPrice2.Value
        clsData.Tolerance = 0
        clsData.Specification = txtSpecification.Text.Trim
        clsData.IDStatus = cboStatus.SelectedValue
        clsData.LogBy = MPSLib.UI.usUserApp.UserID
        clsData.JournalID = strJournalID

        pgMain.Value = 30
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim strID As String = BL.ReceiveReturn.SaveDataDefault(pubIsNew, clsData)
            pgMain.Value = 50
            If strID.Trim <> "" Then
                If pubIsNew Then
                    pgMain.Value = 100
                    UI.usForm.frmMessageBox("Data berhasil disimpan. " & vbCrLf & "Nomor Retur Pembelian: " & strID)
                    frmParent.pubRefresh(clsData.ID)
                    prvClear()
                    prvQueryHistory()
                Else
                    pgMain.Value = 100
                    pubIsSave = True
                    Me.Close()
                End If
            Else
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
        txtID.Text = ""
        intBPID = 0
        dtpReceiveReturnDate.Value = Now
        txtBPName.Text = ""
        cboPaymentTerm.SelectedIndex = -1
        txtReferencesID.Text = ""
        txtPlatNumber.Text = ""
        txtDriverName.Text = ""
        txtDONumber.Text = ""
        txtSPBNumber.Text = ""
        txtSegelNumber.Text = ""
        txtRemarks.Text = ""
        intItemID = 0
        txtItemCode.Text = ""
        txtItemName.Text = ""
        cboUOMID.SelectedIndex = -1
        txtBrutto.Value = 0
        txtTarra.Value = 0
        txtNettoBefore.Value = 0
        txtDeduction.Value = 0
        txtNettoAfter.Value = 0
        txtMaxNetto.Value = 0
        txtPrice1.Value = 0
        txtTotalPrice1.Value = 0
        txtPrice2.Value = 0
        txtTotalPrice2.Value = 0
        txtArrivalNettoAfterReceive.Value = 0
        txtArrivalNettoUsage.Value = 0
        txtSpecification.Text = ""
        cboStatus.SelectedValue = VO.Status.Values.Draft
        ToolStripLogInc.Text = "Jumlah Edit : -"
        ToolStripLogBy.Text = "Dibuat Oleh : -"
        ToolStripLogDate.Text = Format(Now, UI.usDefCons.DateFull)
    End Sub

    Private Sub prvChooseBP()
        Dim frmDetail As New frmMstBusinessPartner
        With frmDetail
            .pubIsLookUp = True
            .pubCompanyID = pubCS.CompanyID
            .pubProgramID = pubCS.ProgramID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intBPID = .pubLUdtRow.Item("ID")
                txtBPName.Text = .pubLUdtRow.Item("Name")
                cboPaymentTerm.SelectedValue = .pubLUdtRow.Item("PaymentTermID")
                txtReferencesID.Text = ""
                txtPlatNumber.Text = ""
                txtDriverName.Text = ""
                txtDONumber.Text = ""
                txtSPBNumber.Text = ""
                txtSegelNumber.Text = ""
                intItemID = 0
                txtItemCode.Text = ""
                txtItemName.Text = ""
                cboUOMID.SelectedIndex = -1
                txtBrutto.Value = 0
                txtTarra.Value = 0
                txtDeduction.Value = 0
                txtMaxNetto.Value = 0
                txtPrice1.Value = 0
                txtPrice2.Value = 0
                txtArrivalNettoAfterReceive.Value = 0
                txtArrivalNettoUsage.Value = 0
                txtSpecification.Text = ""
            End If
        End With
    End Sub

    Private Sub prvChooseReferencesID()
        If intBPID = 0 Then
            UI.usForm.frmMessageBox("Pilih pemasok terlebih dahulu")
            Exit Sub
        End If
        Dim frmDetail As New frmViewOutstandingReceiveForReturn
        With frmDetail
            .pubCS = pubCS
            .pubBPID = intBPID
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                txtReferencesID.Text = .pubLUdtRow.Item("ID")
                txtPlatNumber.Text = .pubLUdtRow.Item("PlatNumber")
                txtDriverName.Text = .pubLUdtRow.Item("DriverName")
                txtDONumber.Text = .pubLUdtRow.Item("DONumber")
                txtSPBNumber.Text = .pubLUdtRow.Item("SPBNumber")
                txtSegelNumber.Text = .pubLUdtRow.Item("SegelNumber")
                intItemID = .pubLUdtRow.Item("ItemID")
                txtItemCode.Text = .pubLUdtRow.Item("ItemCode")
                txtItemName.Text = .pubLUdtRow.Item("ItemName")
                cboUOMID.SelectedValue = .pubLUdtRow.Item("UomID")
                txtBrutto.Value = .pubLUdtRow.Item("ArrivalBrutto")
                txtTarra.Value = .pubLUdtRow.Item("ArrivalTarra")
                txtNettoBefore.Value = .pubLUdtRow.Item("ArrivalNettoBefore")
                txtDeduction.Value = .pubLUdtRow.Item("ArrivalDeduction")
                txtNettoAfter.Value = .pubLUdtRow.Item("ArrivalNettoAfter")
                txtMaxNetto.Value = .pubLUdtRow.Item("MaxNetto")
                txtSpecification.Text = .pubLUdtRow.Item("Specification")
                txtPrice1.Value = .pubLUdtRow.Item("Price1")
                txtTotalPrice1.Value = .pubLUdtRow.Item("TotalPrice1")
                txtPrice2.Value = .pubLUdtRow.Item("Price2")
                txtTotalPrice2.Value = .pubLUdtRow.Item("TotalPrice2")
                txtArrivalNettoAfterReceive.Value = .pubLUdtRow.Item("ArrivalNettoAfterReceive")
                txtArrivalNettoUsage.Value = .pubLUdtRow.Item("ArrivalNettoUsage")
                txtBrutto.Focus()
            End If
        End With
    End Sub

    Private Sub prvUserAccess()
        ToolBar.Buttons(cSave).Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, pubCS.ProgramID, VO.Modules.Values.TransactionReceiveReturn, IIf(pubIsNew, VO.Access.Values.NewAccess, VO.Access.Values.EditAccess))
    End Sub

#Region "Item Handle"

    Private Sub prvCalculate()
        txtNettoBefore.Value = txtBrutto.Value - txtTarra.Value
        txtNettoAfter.Value = txtNettoBefore.Value - txtDeduction.Value
        txtTotalPrice1.Value = txtNettoAfter.Value * txtPrice1.Value
        txtTotalPrice2.Value = txtNettoAfter.Value * txtPrice2.Value
    End Sub

#End Region

#Region "History Handle"

    Private Sub prvQueryHistory()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            grdStatus.DataSource = BL.ReceiveReturn.ListDataStatus(txtID.Text.Trim)
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

    Private Sub frmTraReceiveReturnDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            tcHeader.SelectedTab = tpMain
        ElseIf e.KeyCode = Keys.F2 Then
            tcHeader.SelectedTab = tpHistory
        ElseIf e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmTraReceiveReturnDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvSetTitleForm()
        prvSetGrid()
        prvFillForm()
        prvQueryHistory()
        prvUserAccess()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnBP_Click(sender As Object, e As EventArgs) Handles btnBP.Click
        prvChooseBP()
    End Sub

    Private Sub btnReferences_Click(sender As Object, e As EventArgs) Handles btnReferences.Click
        prvChooseReferencesID()
    End Sub

    Private Sub txtValue_ValueChanged(sender As Object, e As EventArgs) Handles txtBrutto.ValueChanged, txtTarra.ValueChanged, _
        txtDeduction.ValueChanged, txtPrice1.ValueChanged, txtPrice2.ValueChanged

        txtNettoBefore.Value = txtBrutto.Value - txtTarra.Value
        txtNettoAfter.Value = txtNettoBefore.Value - txtDeduction.Value
        txtTotalPrice1.Value = txtNettoAfter.Value * txtPrice1.Value
        txtTotalPrice2.Value = txtNettoAfter.Value * txtPrice2.Value
    End Sub

#End Region

End Class