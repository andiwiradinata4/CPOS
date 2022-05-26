Public Class frmSysSyncJournal

    Private intCompanyID As Integer
    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvResetProgressBar()
        pgMain.Value = 0
    End Sub

    Private Sub prvSetProgressBar(ByVal intMax As Integer)
        pgMain.Value = 0
        pgMain.Maximum = intMax
    End Sub

    Private Sub prvRefreshProgressBar()
        pgMain.Value += 1
        Me.Refresh()
    End Sub

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
            End If
        End With
    End Sub

    Private Sub prvSave()
        ToolBar.Focus()
        If intCompanyID <= 0 Then
            UI.usForm.frmMessageBox("Pilih perusahaan terlebih dahulu")
            txtCompanyName.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion( _
            "Proses sync jurnal transaksi " & txtCompanyName.Text.Trim & " periode " & _
            Format(dtpDateFrom.Value.Date, "dd MMMM yyyy") & " - " & Format(dtpDateTo.Value.Date, "dd MMMM yyyy") & "?") Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        Dim dtData As New DataTable
        Try
            DL.SQL.OpenConnection()
            DL.SQL.BeginTransaction()

            '# Down Payment
            prvResetProgressBar()
            dtData = BL.DownPayment.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim dtDownPaymentJournal As DataTable = DL.DownPayment.ListDataReCalculateJournal(dr.Item("CompanyID"), dr.Item("ProgramID"), dr.Item("ID"))
                BL.DownPayment.GenerateJournal(False, dtDownPaymentJournal, dr.Item("DPType"))
                prvRefreshProgressBar()
            Next

            '# Receive
            prvResetProgressBar()
            dtData = BL.Receive.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim clsData As New VO.Receive
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                    .BPName = dr.Item("BPName")
                End With
                BL.Receive.GenerateJournal(False, clsData)
                prvRefreshProgressBar()
            Next

            '# Account Payable
            prvResetProgressBar()
            dtData = BL.AccountPayable.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim clsData As New VO.AccountPayable
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                    .BPName = dr.Item("BPName")
                End With
                BL.AccountPayable.GenerateJournal(False, clsData)
                prvRefreshProgressBar()
            Next

            '# Sales
            prvResetProgressBar()
            dtData = BL.Sales.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim clsData As New VO.Sales
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                    .BPName = dr.Item("BPName")
                End With
                BL.Sales.GenerateJournal(False, clsData)
                prvRefreshProgressBar()
            Next

            '# Sales Service
            prvResetProgressBar()
            dtData = BL.SalesService.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim clsData As New VO.SalesService
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                    .BPName = dr.Item("BPName")
                End With
                BL.SalesService.GenerateJournal(False, clsData)
                prvRefreshProgressBar()
            Next

            '# Account Receivable
            prvResetProgressBar()
            dtData = BL.AccountReceivable.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim clsData As New VO.AccountReceivable
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                    .BPName = dr.Item("BPName")
                End With
                BL.AccountReceivable.GenerateJournal(False, clsData)
                prvRefreshProgressBar()
            Next

            '# Cost
            prvResetProgressBar()
            dtData = BL.Cost.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr.Item("ID"))

                '# Re-Calculate Journal
                Dim clsData As New VO.Cost
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                End With
                BL.Cost.GenerateJournal(False, clsData)
                prvRefreshProgressBar()
            Next

            '# Journal
            prvResetProgressBar()
            dtData = BL.Journal.ListDataSyncJournal(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date)
            prvSetProgressBar(dtData.Rows.Count)
            For Each dr As DataRow In dtData.Rows
                '# Delete Buku Besar
                '# Get Detail For Delete Buku Besar
                Dim dtDetail As DataTable = DL.Journal.ListDataDetail(dr.Item("ID"))
                For Each dr1 As DataRow In dtDetail.Rows
                    DL.BukuBesar.DeleteData(dr.Item("ProgramID"), dr.Item("CompanyID"), dr1.Item("ID"))
                Next

                '# Re-Calculate Journal
                Dim clsData As New VO.Journal
                With clsData
                    .CompanyID = dr.Item("CompanyID")
                    .ProgramID = dr.Item("ProgramID")
                    .ID = dr.Item("ID")
                End With
                BL.Journal.GenerateJournal(clsData)
                prvRefreshProgressBar()
            Next
            DL.SQL.CommitTransaction()
            UI.usForm.frmMessageBox("Sync jurnal transaksi berhasil")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            DL.SQL.RollBackTransaction()
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            DL.SQL.CloseConnection()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmSysSyncJournal_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form?") Then Me.Close()
        End If
    End Sub

    Private Sub frmSysSyncJournal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        dtpDateFrom.Value = Today.Date.AddDays(-14)
        dtpDateTo.Value = Today.Date
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        If e.Button.Name = ToolBar.Buttons(cSave).Name Then
            prvSave()
        ElseIf e.Button.Name = ToolBar.Buttons(cClose).Name Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

#End Region


End Class