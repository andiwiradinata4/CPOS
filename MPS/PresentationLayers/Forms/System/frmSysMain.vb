Public Class frmSysMain

#Region "Variable Handle"

    Dim bolLogOut As Boolean

    '# Master
    Dim frmMainMstProgram As frmMstProgram
    Dim frmMainMstStatus As frmMstStatus
    Dim frmMainMstModules As frmMstModules
    Dim frmMainMstAccess As frmMstAccess
    Dim frmMainMstCompany As frmMstCompany
    Dim frmMainMstChartOfAccountType As frmMstChartOfAccountType
    Dim frmMainMstChartOfAccountGroup As frmMstChartOfAccountGroup
    Dim frmMainMstChartOfAccount As frmMstChartOfAccount
    Dim frmMainMstUser As frmMstUser
    Dim frmMainMstBusinessPartner As frmMstBusinessPartner
    Dim frmMainMstPaymentTerm As frmMstPaymentTerm
    Dim frmMainMstPaymentReferences As frmMstPaymentReferences
    Dim frmMainMstSalesDiscount As frmMstSalesDiscount
    Dim frmMainMstUOM As frmMstUOM
    Dim frmMainMstItem As frmMstItem

    '# Transaction
    Dim frmMainTraDownPayment As frmTraDownPayment
    Dim frmMainTraSales As frmTraSales
    Dim frmMainTraSalesReturn As frmTraSalesReturn
    Dim frmMainTraImportSales As frmTraImportSales
    Dim frmMainTraDownPaymentVer1 As frmTraDownPaymentVer1
    Dim frmMainTraReceive As frmTraReceive
    Dim frmMainTraReceiveReturn As frmTraReceiveReturn
    Dim frmMainTraAccountReceivable As frmTraAccountReceivable
    Dim frmMainTraAccountPayable As frmTraAccountPayable
    Dim frmMainTraDownPaymentVer2 As frmTraDownPaymentVer2
    Dim frmMainTraSalesService As frmTraSalesService
    Dim frmMainTraCost As frmTraCost
    Dim frmMainTraJournal As frmTraJournal

    '# Reports
    Dim frmMainRptSalesReportVer00 As frmRptSalesReportVer00
    Dim frmMainRptSalesServiceReportVer00 As frmRptSalesServiceReportVer00
    Dim frmMainRptKartuPiutangVer00 As frmRptKartuPiutangVer00
    Dim frmMainRptKartuHutangVer00 As frmRptKartuHutangVer00
    Dim frmMainRptKartuHutangPurchasePrice2Ver00 As frmRptKartuHutangPurchasePrice2Ver00
    Dim frmMainRptCostReportVer00 As frmRptCostReportVer00
    Dim frmMainRptJournalReportVer00 As frmRptJournalReportVer00
    Dim frmMainRptBukuBesarVer00 As frmRptBukuBesarVer00
    Dim frmMainRptNeracaSaldoVer00 As frmRptNeracaSaldoVer00
    Dim frmMainRptLabaRugiVer00 As frmRptLabaRugiVer00
    Dim frmMainRptNeracaVer00 As frmRptNeracaVer00


    '# Setting
    Dim frmMainMstUserChangePassword As frmMstUserChangePassword
    Dim frmMainSysJournalPost As frmSysJournalPost
    Dim frmMainSysPostingOrCancelPostingGL As frmSysPostingOrCancelPostingGL
    Dim frmMainSysBackupDBMS As frmSysBackupDBMS
    Dim frmMainSysSyncJournal As frmSysSyncJournal

#End Region

    Private Sub prvSetupStatusStrip()
        tssUserID.Text = MPSLib.UI.usUserApp.UserID
        tssVersion.Text = "Versi: " & FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion
        tssProgram.Text = MPSLib.UI.usUserApp.ProgramName
        tssCompany.Text = MPSLib.UI.usUserApp.CompanyName
        tssServer.Text = MPSLib.UI.usUserApp.ServerName
    End Sub

    Private Sub prvUserAccess()
        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Me.Refresh()

        '# Master
        mnuMasterProgram.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterStatus.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterModule.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterAkses.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterPerusahaan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterCompany, VO.Access.Values.ViewAccess)
        mnuMasterTipeAkunPerkiraan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterGroupAkunPerkiraan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuMasterAkunPerkiraan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterChartOfAccount, VO.Access.Values.ViewAccess)
        mnuMasterKaryawan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUser, VO.Access.Values.ViewAccess)
        mnuMasterRekanBisnis.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterBusinessPartners, VO.Access.Values.ViewAccess)
        mnuMasterJenisPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterPaymentTerm, VO.Access.Values.ViewAccess)
        mnuMasterReferensiPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterPaymentReferences, VO.Access.Values.ViewAccess)
        mnuMasterDiskonPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterSalesDiscount, VO.Access.Values.ViewAccess)
        mnuMasterSatuan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterUOM, VO.Access.Values.ViewAccess)
        mnuMasterBarang.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.MasterItem, VO.Access.Values.ViewAccess)

        pgMain.Value = 50
        Me.Refresh()

        '# Transaction
        mnuTransaksiPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanPanjar.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesDownPayment, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanReturPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesReturn, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanPenagihan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.ViewAccess)
        mnuTransactionPenjualanImportPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.ViewAccess)

        mnuTransaksiPenjualanJasa.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesService, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanJasaPanjar.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesDownPayment, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanJasaPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesService, VO.Access.Values.ViewAccess)
        mnuTransaksiPenjualanJasaPenagihan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.ViewAccess)

        mnuTransaksiPembelianPanjar.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveDownPayment, VO.Access.Values.ViewAccess)
        mnuTransaksiPembelianPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.ViewAccess)
        mnuTransaksiPembelianReturPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.ViewAccess)
        mnuTransaksiPembelianPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.ViewAccess)

        mnuTransaksiBiaya.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.ViewAccess)
        mnuTransaksiJurnalUmum.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ViewAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.ViewAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.ViewAccess) = False Then
            mnuTransaksiSep3.Visible = False
        End If

        pgMain.Value = 80
        Me.Refresh()

        '# Reports
        mnuLaporanLaporanPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanPenjualanJasa.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesService, VO.Access.Values.PrintReportAccess)
        mnuLaporanKartuPiutang.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportKartuPiutang, VO.Access.Values.PrintReportAccess)
        mnuLaporanKartuHutang.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportKartuHutang, VO.Access.Values.PrintReportAccess)
        mnuLaporanBukuBesar.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBukuBesar, VO.Access.Values.PrintReportAccess)
        mnuLaporanNeracaSaldo.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportNeracaSaldo, VO.Access.Values.PrintReportAccess)


        mnuLaporanLaporanReturPenjualan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSalesReturn, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.PrintReportAccess) = False Then
            mnuLaporanSep1.Visible = False
        End If

        mnuLaporanLaporanPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceive, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanReturPembelian.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionReceiveReturn, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.PrintReportAccess) = False Then
            mnuLaporanSep2.Visible = False
        End If

        mnuLaporanLaporanTagihan.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountReceivable, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanPembayaran.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionAccountPayable, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.PrintReportAccess) = False Then
            mnuLaporanSep3.Visible = False
        End If

        mnuLaporanLaporanBiaya.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionCost, VO.Access.Values.PrintReportAccess)
        mnuLaporanLaporanJurnalUmum.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionJournal, VO.Access.Values.PrintReportAccess)

        If BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportProfitAndLoss, VO.Access.Values.PrintReportAccess) = False AndAlso _
            BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBalanceSheet, VO.Access.Values.PrintReportAccess) = False Then
            mnuLaporanSep4.Visible = False
        End If

        mnuLaporanLaporanLabaRugi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportProfitAndLoss, VO.Access.Values.ViewAccess)
        mnuLaporanLaporanNeraca.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.ReportBalanceSheet, VO.Access.Values.ViewAccess)

        pgMain.Value = 95
        Me.Refresh()

        '# Settings
        mnuSettingSetupPostingJurnalTransaksi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, VO.Access.Values.ViewAccess)
        mnuSysPostingAndCancelPostingDataTransaksi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.SettingPostingTransaction, VO.Access.Values.ViewAccess)
        mnuSettingBackupDatabase.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, -1)
        mnuSettingSyncJurnalTransaksi.Visible = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, -1, -1)

        Me.Cursor = Cursors.Default
        pgMain.Visible = False
    End Sub

#Region "Form Handle"

    Private Sub frmSysMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        bolLogOut = False
        prvSetupStatusStrip()
        prvUserAccess()
    End Sub

    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If Not bolLogOut Then
            If UI.usForm.frmAskQuestion("Keluar dari sistem ?") Then
                Application.Exit()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Master"

    Private Sub mnuMasterProgram_Click(sender As Object, e As EventArgs) Handles mnuMasterProgram.Click
        UI.usForm.frmOpen(frmMainMstProgram, "frmMstProgram", Me)
    End Sub

    Private Sub mnuMasterStatus_Click(sender As Object, e As EventArgs) Handles mnuMasterStatus.Click
        UI.usForm.frmOpen(frmMainMstStatus, "frmMstStatus", Me)
    End Sub

    Private Sub mnuMasterModule_Click(sender As Object, e As EventArgs) Handles mnuMasterModule.Click
        UI.usForm.frmOpen(frmMainMstModules, "frmMstModules", Me)
    End Sub

    Private Sub mnuMasterAkses_Click(sender As Object, e As EventArgs) Handles mnuMasterAkses.Click
        UI.usForm.frmOpen(frmMainMstAccess, "frmMstAccess", Me)
    End Sub

    Private Sub mnuMasterPerusahaan_Click(sender As Object, e As EventArgs) Handles mnuMasterPerusahaan.Click
        UI.usForm.frmOpen(frmMainMstCompany, "frmMstCompany", Me)
    End Sub

    Private Sub mnuMasterTipeAkunPerkiraan_Click(sender As Object, e As EventArgs) Handles mnuMasterTipeAkunPerkiraan.Click
        UI.usForm.frmOpen(frmMainMstChartOfAccountType, "frmMstChartOfAccountType", Me)
    End Sub

    Private Sub mnuMasterGroupAkunPerkiraan_Click(sender As Object, e As EventArgs) Handles mnuMasterGroupAkunPerkiraan.Click
        UI.usForm.frmOpen(frmMainMstChartOfAccountGroup, "frmMstChartOfAccountGroup", Me)
    End Sub

    Private Sub mnuMasterAkunPerkiraan_Click(sender As Object, e As EventArgs) Handles mnuMasterAkunPerkiraan.Click
        UI.usForm.frmOpen(frmMainMstChartOfAccount, "frmMstChartOfAccount", Me)
    End Sub

    Private Sub mnuMasterKaryawan_Click(sender As Object, e As EventArgs) Handles mnuMasterKaryawan.Click
        UI.usForm.frmOpen(frmMainMstUser, "frmMstUser", Me)
    End Sub

    Private Sub mnuMasterRekanBisnis_Click(sender As Object, e As EventArgs) Handles mnuMasterRekanBisnis.Click
        UI.usForm.frmOpen(frmMainMstBusinessPartner, "frmMstBusinessPartner", Me)
    End Sub

    Private Sub mnuMasterJenisPembayaran_Click(sender As Object, e As EventArgs) Handles mnuMasterJenisPembayaran.Click
        UI.usForm.frmOpen(frmMainMstPaymentTerm, "frmMstPaymentTerm", Me)
    End Sub

    Private Sub mnuMasterReferensiPembayaran_Click(sender As Object, e As EventArgs) Handles mnuMasterReferensiPembayaran.Click
        UI.usForm.frmOpen(frmMainMstPaymentReferences, "frmMstPaymentReferences", Me)
    End Sub

    Private Sub mnuMasterDiskonPenjualan_Click(sender As Object, e As EventArgs) Handles mnuMasterDiskonPenjualan.Click
        UI.usForm.frmOpen(frmMainMstSalesDiscount, "frmMstSalesDiscount", Me)
    End Sub

    Private Sub mnuMasterSatuan_Click(sender As Object, e As EventArgs) Handles mnuMasterSatuan.Click
        UI.usForm.frmOpen(frmMainMstUOM, "frmMstUOM", Me)
    End Sub

    Private Sub mnuMasterBarang_Click(sender As Object, e As EventArgs) Handles mnuMasterBarang.Click
        UI.usForm.frmOpen(frmMainMstItem, "frmMstItem", Me)
    End Sub

#End Region

#Region "Transaction"

    Private Sub mnuTransaksiPenjualanPanjar_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanPanjar.Click
        UI.usForm.frmOpen(frmMainTraDownPayment, "frmTraDownPayment", Me)
    End Sub

    Private Sub mnuTransaksiPenjualanPenjualan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanPenjualan.Click
        UI.usForm.frmOpen(frmMainTraSales, "frmTraSales", Me)
    End Sub

    Private Sub mnuTransaksiPenjualanReturPenjualan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanReturPenjualan.Click
        UI.usForm.frmOpen(frmMainTraSalesReturn, "frmTraSalesReturn", Me)
    End Sub

    Private Sub mnuTransactionPenjualanImportPenjualan_Click(sender As Object, e As EventArgs) Handles mnuTransactionPenjualanImportPenjualan.Click
        UI.usForm.frmOpen(frmMainTraImportSales, "frmTraImportSales", Me)
    End Sub

    Private Sub mnuTransaksiPenjualanPenagihan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanPenagihan.Click
        UI.usForm.frmOpen(frmMainTraAccountReceivable, "frmTraAccountReceivable", Me)
    End Sub

    Private Sub mnuTransaksiPembelianPanjar_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPembelianPanjar.Click
        UI.usForm.frmOpen(frmMainTraDownPaymentVer1, "frmTraDownPaymentVer1", Me)
    End Sub

    Private Sub mnuTransaksiPenjualanJasaPanjar_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanJasaPanjar.Click
        UI.usForm.frmOpen(frmMainTraDownPaymentVer2, "frmTraDownPaymentVer2", Me)
    End Sub

    Private Sub mnuTransaksiPenjualanJasaPenjualan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanJasaPenjualan.Click
        UI.usForm.frmOpen(frmMainTraSalesService, "frmTraSalesService", Me)
    End Sub

    Private Sub mnuTransaksiPenjualanJasaPenagihan_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPenjualanJasaPenagihan.Click
        UI.usForm.frmOpen(frmMainTraAccountReceivable, "frmTraAccountReceivable", Me)
    End Sub

    Private Sub mnuTransaksiPembelianPembelian_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPembelianPembelian.Click
        UI.usForm.frmOpen(frmMainTraReceive, "frmTraReceive", Me)
    End Sub

    Private Sub mnuTransaksiPembelianReturPembelian_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPembelianReturPembelian.Click
        UI.usForm.frmOpen(frmMainTraReceiveReturn, "frmTraReceiveReturn", Me)
    End Sub

    Private Sub mnuTransaksiPembelianPembayaran_Click(sender As Object, e As EventArgs) Handles mnuTransaksiPembelianPembayaran.Click
        UI.usForm.frmOpen(frmMainTraAccountPayable, "frmTraAccountPayable", Me)
    End Sub

    Private Sub mnuTransaksiBiaya_Click(sender As Object, e As EventArgs) Handles mnuTransaksiBiaya.Click
        UI.usForm.frmOpen(frmMainTraCost, "frmTraCost", Me)
    End Sub

    Private Sub mnuTransaksiJurnalUmum_Click(sender As Object, e As EventArgs) Handles mnuTransaksiJurnalUmum.Click
        UI.usForm.frmOpen(frmMainTraJournal, "frmTraJournal", Me)
    End Sub

#End Region

#Region "Settings"

    Private Sub mnuSettingUbahPassword_Click(sender As Object, e As EventArgs) Handles mnuSettingUbahPassword.Click
        UI.usForm.frmOpen(frmMainMstUserChangePassword, "frmMstUserChangePassword", Me)
    End Sub

    Private Sub mnuSettingPostingJurnalTransaksi_Click(sender As Object, e As EventArgs) Handles mnuSettingSetupPostingJurnalTransaksi.Click
        UI.usForm.frmOpen(frmMainSysJournalPost, "frmSysJournalPost", Me)
    End Sub

    Private Sub mnuSysPostingAndCancelPostingDataTransaksi_Click(sender As Object, e As EventArgs) Handles mnuSysPostingAndCancelPostingDataTransaksi.Click
        UI.usForm.frmOpen(frmMainSysPostingOrCancelPostingGL, "frmSysPostingOrCancelPostingGL", Me)
    End Sub

    Private Sub mnuSettingBackupDatabase_Click(sender As Object, e As EventArgs) Handles mnuSettingBackupDatabase.Click
        UI.usForm.frmOpen(frmMainSysBackupDBMS, "frmSysBackupDBMS", Me)
    End Sub

    Private Sub mnuSettingSyncJurnalTransaksi_Click(sender As Object, e As EventArgs) Handles mnuSettingSyncJurnalTransaksi.Click
        UI.usForm.frmOpen(frmMainSysSyncJournal, "frmSysSyncJournal", Me)
    End Sub

#End Region

#Region "Reports"

    Private Sub mnuLaporanLaporanPenjualan_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanPenjualan.Click
        UI.usForm.frmOpen(frmMainRptSalesReportVer00, "frmRptSalesReportVer00", Me)
    End Sub

    Private Sub mnuLaporanLaporanPenjualanJasa_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanPenjualanJasa.Click
        UI.usForm.frmOpen(frmMainRptSalesServiceReportVer00, "frmRptSalesServiceReportVer00", Me)
    End Sub

    Private Sub mnuLaporanKartuPiutang_Click(sender As Object, e As EventArgs) Handles mnuLaporanKartuPiutang.Click
        UI.usForm.frmOpen(frmMainRptKartuPiutangVer00, "frmRptKartuPiutangVer00", Me)
    End Sub

    Private Sub mnuLaporanKartuHutang_Click(sender As Object, e As EventArgs) Handles mnuLaporanKartuHutang.Click
        UI.usForm.frmOpen(frmMainRptKartuHutangVer00, "frmRptKartuHutangVer00", Me)
    End Sub

    Private Sub mnuLaporanKartuHutangHargaBeli2_Click(sender As Object, e As EventArgs) Handles mnuLaporanKartuHutangHargaBeli2.Click
        UI.usForm.frmOpen(frmMainRptKartuHutangPurchasePrice2Ver00, "frmRptKartuHutangPurchasePrice2Ver00", Me)
    End Sub

    Private Sub mnuLaporanLaporanBiaya_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanBiaya.Click
        UI.usForm.frmOpen(frmMainRptCostReportVer00, "frmRptCostReportVer00", Me)
    End Sub

    Private Sub mnuLaporanLaporanJurnalUmum_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanJurnalUmum.Click
        UI.usForm.frmOpen(frmMainRptJournalReportVer00, "frmRptJournalReportVer00", Me)
    End Sub

    Private Sub mnuLaporanBukuBesar_Click(sender As Object, e As EventArgs) Handles mnuLaporanBukuBesar.Click
        UI.usForm.frmOpen(frmMainRptBukuBesarVer00, "frmRptBukuBesarVer00", Me)
    End Sub

    Private Sub mnuLaporanNeracaSaldo_Click(sender As Object, e As EventArgs) Handles mnuLaporanNeracaSaldo.Click
        UI.usForm.frmOpen(frmMainRptNeracaSaldoVer00, "frmRptNeracaSaldoVer00", Me)
    End Sub

    Private Sub mnuLaporanLaporanLabaRugi_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanLabaRugi.Click
        UI.usForm.frmOpen(frmMainRptLabaRugiVer00, "frmRptLabaRugiVer00", Me)
    End Sub

    Private Sub mnuLaporanLaporanNeraca_Click(sender As Object, e As EventArgs) Handles mnuLaporanLaporanNeraca.Click
        UI.usForm.frmOpen(frmMainRptNeracaVer00, "frmRptNeracaVer00", Me)
    End Sub

#End Region

#Region "Windows"

    Private Sub mnuWindowsVertical_Click(sender As Object, e As EventArgs) Handles mnuWindowsVertical.Click
        LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub mnuWindowsHorizontal_Click(sender As Object, e As EventArgs) Handles mnuWindowsHorizontal.Click
        LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub mnuWindowsCascade_Click(sender As Object, e As EventArgs) Handles mnuWindowsCascade.Click
        LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub mnuWindowsCloseAll_Click(sender As Object, e As EventArgs) Handles mnuWindowsCloseAll.Click
        For Each Form As Form In Me.MdiChildren
            Form.Close()
        Next
    End Sub

#End Region

    Private Sub mnuLogout_Click(sender As Object, e As EventArgs) Handles mnuLogout.Click
        If Not UI.usForm.frmAskQuestion("Keluar dari program?") Then Exit Sub
        Application.Exit()
    End Sub

End Class