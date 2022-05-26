<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptLabaRugiVer00
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.xrPeriod = New DevExpress.XtraReports.UI.XRLabel()
        Me.FilterPeriod = New DevExpress.XtraReports.Parameters.Parameter()
        Me.xrCompanyName = New DevExpress.XtraReports.UI.XRLabel()
        Me.CompanyName = New DevExpress.XtraReports.Parameters.Parameter()
        Me.xrTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.bandRevenueAndSales = New DevExpress.XtraReports.UI.SubBand()
        Me.srRevenueAndSales = New DevExpress.XtraReports.UI.XRSubreport()
        Me.banCOGS = New DevExpress.XtraReports.UI.SubBand()
        Me.srCOGS = New DevExpress.XtraReports.UI.XRSubreport()
        Me.banLabaKotor = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrTotalLabaKotor = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.paramTotalLabaKotor = New DevExpress.XtraReports.Parameters.Parameter()
        Me.banOperationalExpense = New DevExpress.XtraReports.UI.SubBand()
        Me.srOperationalExpenses = New DevExpress.XtraReports.UI.XRSubreport()
        Me.banTotalLabaUsaha = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrTotalLabaUsaha = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.paramTotalLabaUsaha = New DevExpress.XtraReports.Parameters.Parameter()
        Me.banOtherRevenue = New DevExpress.XtraReports.UI.SubBand()
        Me.srOthersRevenue = New DevExpress.XtraReports.UI.XRSubreport()
        Me.banOtherExpenses = New DevExpress.XtraReports.UI.SubBand()
        Me.srOthersExpenses = New DevExpress.XtraReports.UI.XRSubreport()
        Me.banTotalLabaRugiBersih = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTable3 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.xrTotalLabaRugiBersih = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.paramTotalLabaRugiBersih = New DevExpress.XtraReports.Parameters.Parameter()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.ObjectDataSource1 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ObjectDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 100.0!
        Me.Detail.Expanded = False
        Me.Detail.HeightF = 100.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'TopMargin
        '
        Me.TopMargin.Dpi = 100.0!
        Me.TopMargin.HeightF = 20.0!
        Me.TopMargin.Name = "TopMargin"
        Me.TopMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'BottomMargin
        '
        Me.BottomMargin.Dpi = 100.0!
        Me.BottomMargin.HeightF = 22.99995!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrPeriod, Me.xrCompanyName, Me.xrTitle})
        Me.ReportHeader.Dpi = 100.0!
        Me.ReportHeader.HeightF = 111.625!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'xrPeriod
        '
        Me.xrPeriod.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.FilterPeriod, "Text", "Tanggal: {0}")})
        Me.xrPeriod.Dpi = 100.0!
        Me.xrPeriod.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.xrPeriod.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 70.00002!)
        Me.xrPeriod.Name = "xrPeriod"
        Me.xrPeriod.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrPeriod.SizeF = New System.Drawing.SizeF(787.0!, 30.0!)
        Me.xrPeriod.StylePriority.UseFont = False
        Me.xrPeriod.StylePriority.UseTextAlignment = False
        Me.xrPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'FilterPeriod
        '
        Me.FilterPeriod.Description = "FilterPeriod"
        Me.FilterPeriod.Name = "FilterPeriod"
        '
        'xrCompanyName
        '
        Me.xrCompanyName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.CompanyName, "Text", "")})
        Me.xrCompanyName.Dpi = 100.0!
        Me.xrCompanyName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrCompanyName.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 9.999974!)
        Me.xrCompanyName.Name = "xrCompanyName"
        Me.xrCompanyName.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrCompanyName.SizeF = New System.Drawing.SizeF(787.0!, 30.0!)
        Me.xrCompanyName.StylePriority.UseFont = False
        Me.xrCompanyName.StylePriority.UseTextAlignment = False
        Me.xrCompanyName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'CompanyName
        '
        Me.CompanyName.Description = "CompanyName"
        Me.CompanyName.Name = "CompanyName"
        '
        'xrTitle
        '
        Me.xrTitle.Dpi = 100.0!
        Me.xrTitle.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrTitle.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 39.99996!)
        Me.xrTitle.Name = "xrTitle"
        Me.xrTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrTitle.SizeF = New System.Drawing.SizeF(787.0!, 29.99999!)
        Me.xrTitle.StylePriority.UseFont = False
        Me.xrTitle.StylePriority.UseTextAlignment = False
        Me.xrTitle.Text = "Laporan Laba Rugi"
        Me.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 100.0!
        Me.PageHeader.HeightF = 0.0!
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.bandRevenueAndSales, Me.banCOGS, Me.banLabaKotor, Me.banOperationalExpense, Me.banTotalLabaUsaha, Me.banOtherRevenue, Me.banOtherExpenses, Me.banTotalLabaRugiBersih})
        '
        'bandRevenueAndSales
        '
        Me.bandRevenueAndSales.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srRevenueAndSales})
        Me.bandRevenueAndSales.Dpi = 100.0!
        Me.bandRevenueAndSales.Expanded = False
        Me.bandRevenueAndSales.HeightF = 50.0!
        Me.bandRevenueAndSales.Name = "bandRevenueAndSales"
        '
        'srRevenueAndSales
        '
        Me.srRevenueAndSales.Dpi = 100.0!
        Me.srRevenueAndSales.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.srRevenueAndSales.Name = "srRevenueAndSales"
        Me.srRevenueAndSales.ReportSource = New MPS.rptCOAPerGroupVer00()
        Me.srRevenueAndSales.SizeF = New System.Drawing.SizeF(787.0!, 50.0!)
        '
        'banCOGS
        '
        Me.banCOGS.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srCOGS})
        Me.banCOGS.Dpi = 100.0!
        Me.banCOGS.HeightF = 50.0!
        Me.banCOGS.Name = "banCOGS"
        '
        'srCOGS
        '
        Me.srCOGS.Dpi = 100.0!
        Me.srCOGS.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.srCOGS.Name = "srCOGS"
        Me.srCOGS.ReportSource = New MPS.rptCOAPerGroupVer00()
        Me.srCOGS.SizeF = New System.Drawing.SizeF(787.0!, 50.0!)
        '
        'banLabaKotor
        '
        Me.banLabaKotor.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.banLabaKotor.Dpi = 100.0!
        Me.banLabaKotor.Expanded = False
        Me.banLabaKotor.HeightF = 35.0!
        Me.banLabaKotor.Name = "banLabaKotor"
        '
        'XrTable1
        '
        Me.XrTable1.Dpi = 100.0!
        Me.XrTable1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 10.0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(787.0!, 25.0!)
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTotalLabaKotor, Me.XrTableCell3})
        Me.XrTableRow1.Dpi = 100.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'xrTotalLabaKotor
        '
        Me.xrTotalLabaKotor.Dpi = 100.0!
        Me.xrTotalLabaKotor.Name = "xrTotalLabaKotor"
        Me.xrTotalLabaKotor.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.xrTotalLabaKotor.StylePriority.UsePadding = False
        Me.xrTotalLabaKotor.StylePriority.UseTextAlignment = False
        Me.xrTotalLabaKotor.Text = "TOTAL LABA KOTOR"
        Me.xrTotalLabaKotor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.xrTotalLabaKotor.Weight = 1.1166961941361579R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.paramTotalLabaKotor, "Text", "{0:n2}")})
        Me.XrTableCell3.Dpi = 100.0!
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell3.StylePriority.UseBorders = False
        Me.XrTableCell3.StylePriority.UsePadding = False
        Me.XrTableCell3.Weight = 0.88330380586384216R
        '
        'paramTotalLabaKotor
        '
        Me.paramTotalLabaKotor.Description = "Total Laba Kotor"
        Me.paramTotalLabaKotor.Name = "paramTotalLabaKotor"
        Me.paramTotalLabaKotor.Type = GetType(Decimal)
        Me.paramTotalLabaKotor.ValueInfo = "0"
        '
        'banOperationalExpense
        '
        Me.banOperationalExpense.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srOperationalExpenses})
        Me.banOperationalExpense.Dpi = 100.0!
        Me.banOperationalExpense.Expanded = False
        Me.banOperationalExpense.HeightF = 50.0!
        Me.banOperationalExpense.Name = "banOperationalExpense"
        '
        'srOperationalExpenses
        '
        Me.srOperationalExpenses.Dpi = 100.0!
        Me.srOperationalExpenses.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.srOperationalExpenses.Name = "srOperationalExpenses"
        Me.srOperationalExpenses.ReportSource = New MPS.rptCOAPerGroupVer00()
        Me.srOperationalExpenses.SizeF = New System.Drawing.SizeF(787.0!, 50.0!)
        '
        'banTotalLabaUsaha
        '
        Me.banTotalLabaUsaha.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.banTotalLabaUsaha.Dpi = 100.0!
        Me.banTotalLabaUsaha.Expanded = False
        Me.banTotalLabaUsaha.HeightF = 35.0!
        Me.banTotalLabaUsaha.Name = "banTotalLabaUsaha"
        '
        'XrTable2
        '
        Me.XrTable2.Dpi = 100.0!
        Me.XrTable2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 10.0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(787.0!, 25.0!)
        Me.XrTable2.StylePriority.UseFont = False
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTotalLabaUsaha, Me.XrTableCell4})
        Me.XrTableRow2.Dpi = 100.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'xrTotalLabaUsaha
        '
        Me.xrTotalLabaUsaha.Dpi = 100.0!
        Me.xrTotalLabaUsaha.Name = "xrTotalLabaUsaha"
        Me.xrTotalLabaUsaha.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.xrTotalLabaUsaha.StylePriority.UsePadding = False
        Me.xrTotalLabaUsaha.StylePriority.UseTextAlignment = False
        Me.xrTotalLabaUsaha.Text = "TOTAL LABA USAHA"
        Me.xrTotalLabaUsaha.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.xrTotalLabaUsaha.Weight = 1.1166961941361579R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.paramTotalLabaUsaha, "Text", "{0:n2}")})
        Me.XrTableCell4.Dpi = 100.0!
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell4.StylePriority.UseBorders = False
        Me.XrTableCell4.StylePriority.UsePadding = False
        Me.XrTableCell4.Weight = 0.88330380586384216R
        '
        'paramTotalLabaUsaha
        '
        Me.paramTotalLabaUsaha.Description = "Total Laba Usaha"
        Me.paramTotalLabaUsaha.Name = "paramTotalLabaUsaha"
        Me.paramTotalLabaUsaha.Type = GetType(Decimal)
        Me.paramTotalLabaUsaha.ValueInfo = "0"
        '
        'banOtherRevenue
        '
        Me.banOtherRevenue.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srOthersRevenue})
        Me.banOtherRevenue.Dpi = 100.0!
        Me.banOtherRevenue.Expanded = False
        Me.banOtherRevenue.HeightF = 50.0!
        Me.banOtherRevenue.Name = "banOtherRevenue"
        '
        'srOthersRevenue
        '
        Me.srOthersRevenue.Dpi = 100.0!
        Me.srOthersRevenue.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.srOthersRevenue.Name = "srOthersRevenue"
        Me.srOthersRevenue.ReportSource = New MPS.rptCOAPerGroupVer00()
        Me.srOthersRevenue.SizeF = New System.Drawing.SizeF(787.0!, 50.0!)
        '
        'banOtherExpenses
        '
        Me.banOtherExpenses.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srOthersExpenses})
        Me.banOtherExpenses.Dpi = 100.0!
        Me.banOtherExpenses.Expanded = False
        Me.banOtherExpenses.HeightF = 50.0!
        Me.banOtherExpenses.Name = "banOtherExpenses"
        '
        'srOthersExpenses
        '
        Me.srOthersExpenses.Dpi = 100.0!
        Me.srOthersExpenses.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.srOthersExpenses.Name = "srOthersExpenses"
        Me.srOthersExpenses.ReportSource = New MPS.rptCOAPerGroupVer00()
        Me.srOthersExpenses.SizeF = New System.Drawing.SizeF(787.0!, 50.0!)
        '
        'banTotalLabaRugiBersih
        '
        Me.banTotalLabaRugiBersih.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable3})
        Me.banTotalLabaRugiBersih.Dpi = 100.0!
        Me.banTotalLabaRugiBersih.Expanded = False
        Me.banTotalLabaRugiBersih.HeightF = 35.0!
        Me.banTotalLabaRugiBersih.Name = "banTotalLabaRugiBersih"
        '
        'XrTable3
        '
        Me.XrTable3.Dpi = 100.0!
        Me.XrTable3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.XrTable3.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 10.0!)
        Me.XrTable3.Name = "XrTable3"
        Me.XrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow3})
        Me.XrTable3.SizeF = New System.Drawing.SizeF(787.0!, 25.0!)
        Me.XrTable3.StylePriority.UseFont = False
        Me.XrTable3.StylePriority.UseTextAlignment = False
        Me.XrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.xrTotalLabaRugiBersih, Me.XrTableCell2})
        Me.XrTableRow3.Dpi = 100.0!
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1.0R
        '
        'xrTotalLabaRugiBersih
        '
        Me.xrTotalLabaRugiBersih.Dpi = 100.0!
        Me.xrTotalLabaRugiBersih.Name = "xrTotalLabaRugiBersih"
        Me.xrTotalLabaRugiBersih.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.xrTotalLabaRugiBersih.StylePriority.UsePadding = False
        Me.xrTotalLabaRugiBersih.StylePriority.UseTextAlignment = False
        Me.xrTotalLabaRugiBersih.Text = "TOTAL LABA / RUGI BERSIH"
        Me.xrTotalLabaRugiBersih.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.xrTotalLabaRugiBersih.Weight = 1.1166961941361579R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.paramTotalLabaRugiBersih, "Text", "{0:n2}")})
        Me.XrTableCell2.Dpi = 100.0!
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell2.StylePriority.UseBorders = False
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.Weight = 0.88330380586384216R
        '
        'paramTotalLabaRugiBersih
        '
        Me.paramTotalLabaRugiBersih.Description = "Total Laba Rugi Bersih"
        Me.paramTotalLabaRugiBersih.Name = "paramTotalLabaRugiBersih"
        Me.paramTotalLabaRugiBersih.Type = GetType(Decimal)
        Me.paramTotalLabaRugiBersih.ValueInfo = "0"
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 100.0!
        Me.ReportFooter.Expanded = False
        Me.ReportFooter.HeightF = 100.0!
        Me.ReportFooter.Name = "ReportFooter"
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPageInfo1, Me.XrPageInfo2})
        Me.PageFooter.Dpi = 100.0!
        Me.PageFooter.HeightF = 23.0!
        Me.PageFooter.Name = "PageFooter"
        '
        'XrPageInfo1
        '
        Me.XrPageInfo1.Dpi = 100.0!
        Me.XrPageInfo1.Format = "Halaman: {0} dari {1}"
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(381.25!, 0.0!)
        Me.XrPageInfo1.Name = "XrPageInfo1"
        Me.XrPageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 5, 0, 0, 100.0!)
        Me.XrPageInfo1.SizeF = New System.Drawing.SizeF(405.75!, 23.0!)
        Me.XrPageInfo1.StylePriority.UsePadding = False
        Me.XrPageInfo1.StylePriority.UseTextAlignment = False
        Me.XrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        '
        'XrPageInfo2
        '
        Me.XrPageInfo2.Dpi = 100.0!
        Me.XrPageInfo2.Format = "Tanggal Cetak: {0:dd MMMM yyyy HH:mm:ss}"
        Me.XrPageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrPageInfo2.Name = "XrPageInfo2"
        Me.XrPageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100.0!)
        Me.XrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.XrPageInfo2.SizeF = New System.Drawing.SizeF(381.2499!, 23.0!)
        Me.XrPageInfo2.StylePriority.UsePadding = False
        Me.XrPageInfo2.StylePriority.UseTextAlignment = False
        Me.XrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'ObjectDataSource1
        '
        Me.ObjectDataSource1.DataSource = GetType(MPSLib.VO.rptLabaRugi)
        Me.ObjectDataSource1.Name = "ObjectDataSource1"
        '
        'rptLabaRugiVer00
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.ReportFooter, Me.PageFooter})
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.ObjectDataSource1})
        Me.DataSource = Me.ObjectDataSource1
        Me.DisplayName = "Laba Rugi"
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Margins = New System.Drawing.Printing.Margins(20, 20, 20, 23)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.CompanyName, Me.FilterPeriod, Me.paramTotalLabaKotor, Me.paramTotalLabaUsaha, Me.paramTotalLabaRugiBersih})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "16.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ObjectDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents TopMargin As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents BottomMargin As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents ObjectDataSource1 As DevExpress.DataAccess.ObjectBinding.ObjectDataSource
    Friend WithEvents ReportHeader As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents ReportFooter As DevExpress.XtraReports.UI.ReportFooterBand
    Friend WithEvents PageFooter As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents xrCompanyName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrTitle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents CompanyName As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents FilterPeriod As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents xrPeriod As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents srRevenueAndSales As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents bandRevenueAndSales As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents banCOGS As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents srCOGS As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents banLabaKotor As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents paramTotalLabaKotor As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrTotalLabaKotor As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents banOperationalExpense As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents srOperationalExpenses As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents banTotalLabaUsaha As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrTotalLabaUsaha As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents paramTotalLabaUsaha As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents banOtherRevenue As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents srOthersRevenue As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents banOtherExpenses As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents srOthersExpenses As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents banTotalLabaRugiBersih As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents XrTable3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents xrTotalLabaRugiBersih As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents paramTotalLabaRugiBersih As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
End Class
