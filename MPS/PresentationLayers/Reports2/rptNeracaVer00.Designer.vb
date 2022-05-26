<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptNeracaVer00
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
        Me.banAktivaLancar = New DevExpress.XtraReports.UI.SubBand()
        Me.banAktivaTetap = New DevExpress.XtraReports.UI.SubBand()
        Me.banTotalAktiva = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.paramTotalAktiva = New DevExpress.XtraReports.Parameters.Parameter()
        Me.banPasiva = New DevExpress.XtraReports.UI.SubBand()
        Me.banEquity = New DevExpress.XtraReports.UI.SubBand()
        Me.banTotalPasivaDanEquity = New DevExpress.XtraReports.UI.SubBand()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.paramTotalPasivaDanEquity = New DevExpress.XtraReports.Parameters.Parameter()
        Me.TopMargin = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.BottomMargin = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.ReportHeader = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.xrTitle = New DevExpress.XtraReports.UI.XRLabel()
        Me.xrCompanyName = New DevExpress.XtraReports.UI.XRLabel()
        Me.CompanyName = New DevExpress.XtraReports.Parameters.Parameter()
        Me.xrPeriod = New DevExpress.XtraReports.UI.XRLabel()
        Me.FilterPeriod = New DevExpress.XtraReports.Parameters.Parameter()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.ReportFooter = New DevExpress.XtraReports.UI.ReportFooterBand()
        Me.PageFooter = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.XrPageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.XrPageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.srAktivaLancar = New DevExpress.XtraReports.UI.XRSubreport()
        Me.srAktivaTetap = New DevExpress.XtraReports.UI.XRSubreport()
        Me.srPasiva = New DevExpress.XtraReports.UI.XRSubreport()
        Me.srEquity = New DevExpress.XtraReports.UI.XRSubreport()
        Me.ObjectDataSource1 = New DevExpress.DataAccess.ObjectBinding.ObjectDataSource(Me.components)
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ObjectDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Dpi = 100.0!
        Me.Detail.HeightF = 0.0!
        Me.Detail.Name = "Detail"
        Me.Detail.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.Detail.SubBands.AddRange(New DevExpress.XtraReports.UI.SubBand() {Me.banAktivaLancar, Me.banAktivaTetap, Me.banTotalAktiva, Me.banPasiva, Me.banEquity, Me.banTotalPasivaDanEquity})
        Me.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'banAktivaLancar
        '
        Me.banAktivaLancar.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srAktivaLancar})
        Me.banAktivaLancar.Dpi = 100.0!
        Me.banAktivaLancar.HeightF = 87.16669!
        Me.banAktivaLancar.Name = "banAktivaLancar"
        '
        'banAktivaTetap
        '
        Me.banAktivaTetap.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srAktivaTetap})
        Me.banAktivaTetap.Dpi = 100.0!
        Me.banAktivaTetap.HeightF = 59.04169!
        Me.banAktivaTetap.Name = "banAktivaTetap"
        '
        'banTotalAktiva
        '
        Me.banTotalAktiva.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1})
        Me.banTotalAktiva.Dpi = 100.0!
        Me.banTotalAktiva.HeightF = 25.0!
        Me.banTotalAktiva.Name = "banTotalAktiva"
        '
        'XrTable1
        '
        Me.XrTable1.Dpi = 100.0!
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0.0002543131!, 0.0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(786.9998!, 25.0!)
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1, Me.XrTableCell2})
        Me.XrTableRow1.Dpi = 100.0!
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.Dpi = 100.0!
        Me.XrTableCell1.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.StylePriority.UsePadding = False
        Me.XrTableCell1.Text = "TOTAL AKTIVA"
        Me.XrTableCell1.Weight = 1.0R
        '
        'XrTableCell2
        '
        Me.XrTableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.paramTotalAktiva, "Text", "{0:n2}")})
        Me.XrTableCell2.Dpi = 100.0!
        Me.XrTableCell2.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell2.Name = "XrTableCell2"
        Me.XrTableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell2.StylePriority.UseFont = False
        Me.XrTableCell2.StylePriority.UsePadding = False
        Me.XrTableCell2.StylePriority.UseTextAlignment = False
        Me.XrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell2.Weight = 1.0R
        '
        'paramTotalAktiva
        '
        Me.paramTotalAktiva.Description = "Total Aktiva"
        Me.paramTotalAktiva.Name = "paramTotalAktiva"
        Me.paramTotalAktiva.Type = GetType(Decimal)
        Me.paramTotalAktiva.ValueInfo = "0"
        '
        'banPasiva
        '
        Me.banPasiva.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srPasiva})
        Me.banPasiva.Dpi = 100.0!
        Me.banPasiva.HeightF = 59.04168!
        Me.banPasiva.Name = "banPasiva"
        '
        'banEquity
        '
        Me.banEquity.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.srEquity})
        Me.banEquity.Dpi = 100.0!
        Me.banEquity.HeightF = 59.04168!
        Me.banEquity.Name = "banEquity"
        '
        'banTotalPasivaDanEquity
        '
        Me.banTotalPasivaDanEquity.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable2})
        Me.banTotalPasivaDanEquity.Dpi = 100.0!
        Me.banTotalPasivaDanEquity.HeightF = 25.0!
        Me.banTotalPasivaDanEquity.Name = "banTotalPasivaDanEquity"
        '
        'XrTable2
        '
        Me.XrTable2.Dpi = 100.0!
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(0.0001271566!, 0.0!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(786.9998!, 25.0!)
        Me.XrTable2.StylePriority.UseTextAlignment = False
        Me.XrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell3, Me.XrTableCell4})
        Me.XrTableRow2.Dpi = 100.0!
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.Dpi = 100.0!
        Me.XrTableCell3.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.Padding = New DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100.0!)
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.StylePriority.UsePadding = False
        Me.XrTableCell3.Text = "TOTAL PASIVA DAN EQUITY"
        Me.XrTableCell3.Weight = 1.0R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.paramTotalPasivaDanEquity, "Text", "{0:n2}")})
        Me.XrTableCell4.Dpi = 100.0!
        Me.XrTableCell4.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100.0!)
        Me.XrTableCell4.StylePriority.UseFont = False
        Me.XrTableCell4.StylePriority.UsePadding = False
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.XrTableCell4.Weight = 1.0R
        '
        'paramTotalPasivaDanEquity
        '
        Me.paramTotalPasivaDanEquity.Description = "Total Pasiva dan Equity"
        Me.paramTotalPasivaDanEquity.Name = "paramTotalPasivaDanEquity"
        Me.paramTotalPasivaDanEquity.Type = GetType(Decimal)
        Me.paramTotalPasivaDanEquity.ValueInfo = "0"
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
        Me.BottomMargin.HeightF = 20.0!
        Me.BottomMargin.Name = "BottomMargin"
        Me.BottomMargin.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100.0!)
        Me.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft
        '
        'ReportHeader
        '
        Me.ReportHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.xrTitle, Me.xrCompanyName, Me.xrPeriod})
        Me.ReportHeader.Dpi = 100.0!
        Me.ReportHeader.HeightF = 99.99998!
        Me.ReportHeader.Name = "ReportHeader"
        '
        'xrTitle
        '
        Me.xrTitle.Dpi = 100.0!
        Me.xrTitle.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrTitle.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 39.99999!)
        Me.xrTitle.Name = "xrTitle"
        Me.xrTitle.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrTitle.SizeF = New System.Drawing.SizeF(787.0!, 29.99998!)
        Me.xrTitle.StylePriority.UseFont = False
        Me.xrTitle.StylePriority.UseTextAlignment = False
        Me.xrTitle.Text = "NERACA"
        Me.xrTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'xrCompanyName
        '
        Me.xrCompanyName.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.CompanyName, "Text", "")})
        Me.xrCompanyName.Dpi = 100.0!
        Me.xrCompanyName.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.xrCompanyName.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 10.00001!)
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
        'xrPeriod
        '
        Me.xrPeriod.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.xrPeriod.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.FilterPeriod, "Text", "Pertanggal: {0:dd MMMM yyyy}")})
        Me.xrPeriod.Dpi = 100.0!
        Me.xrPeriod.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.xrPeriod.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 69.99998!)
        Me.xrPeriod.Name = "xrPeriod"
        Me.xrPeriod.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.xrPeriod.SizeF = New System.Drawing.SizeF(787.0!, 30.0!)
        Me.xrPeriod.StylePriority.UseBorders = False
        Me.xrPeriod.StylePriority.UseFont = False
        Me.xrPeriod.StylePriority.UseTextAlignment = False
        Me.xrPeriod.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'FilterPeriod
        '
        Me.FilterPeriod.Description = "Filter Period"
        Me.FilterPeriod.Name = "FilterPeriod"
        Me.FilterPeriod.Type = GetType(Date)
        '
        'PageHeader
        '
        Me.PageHeader.Dpi = 100.0!
        Me.PageHeader.HeightF = 0.0!
        Me.PageHeader.Name = "PageHeader"
        '
        'ReportFooter
        '
        Me.ReportFooter.Dpi = 100.0!
        Me.ReportFooter.HeightF = 0.0!
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
        Me.XrPageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(381.2499!, 0.0!)
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
        'srAktivaLancar
        '
        Me.srAktivaLancar.Dpi = 100.0!
        Me.srAktivaLancar.LocationFloat = New DevExpress.Utils.PointFloat(0.0002543131!, 10.00001!)
        Me.srAktivaLancar.Name = "srAktivaLancar"
        Me.srAktivaLancar.ReportSource = New MPS.rptCOAPerTypeVer00()
        Me.srAktivaLancar.SizeF = New System.Drawing.SizeF(787.0!, 53.20835!)
        '
        'srAktivaTetap
        '
        Me.srAktivaTetap.Dpi = 100.0!
        Me.srAktivaTetap.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 10.0!)
        Me.srAktivaTetap.Name = "srAktivaTetap"
        Me.srAktivaTetap.ReportSource = New MPS.rptCOAPerTypeVer00()
        Me.srAktivaTetap.SizeF = New System.Drawing.SizeF(787.0!, 49.04168!)
        '
        'srPasiva
        '
        Me.srPasiva.Dpi = 100.0!
        Me.srPasiva.LocationFloat = New DevExpress.Utils.PointFloat(0.0002543131!, 10.0!)
        Me.srPasiva.Name = "srPasiva"
        Me.srPasiva.ReportSource = New MPS.rptCOAPerTypeVer00()
        Me.srPasiva.SizeF = New System.Drawing.SizeF(787.0!, 49.04168!)
        '
        'srEquity
        '
        Me.srEquity.Dpi = 100.0!
        Me.srEquity.LocationFloat = New DevExpress.Utils.PointFloat(0.0002543131!, 10.0!)
        Me.srEquity.Name = "srEquity"
        Me.srEquity.ReportSource = New MPS.rptCOAPerTypeVer00()
        Me.srEquity.SizeF = New System.Drawing.SizeF(787.0!, 49.04168!)
        '
        'ObjectDataSource1
        '
        Me.ObjectDataSource1.DataSource = GetType(MPSLib.VO.rptCOAPerType)
        Me.ObjectDataSource1.Name = "ObjectDataSource1"
        '
        'rptNeracaVer00
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.TopMargin, Me.BottomMargin, Me.ReportHeader, Me.PageHeader, Me.ReportFooter, Me.PageFooter})
        Me.Bookmark = "Neraca"
        Me.ComponentStorage.AddRange(New System.ComponentModel.IComponent() {Me.ObjectDataSource1})
        Me.DataSource = Me.ObjectDataSource1
        Me.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.Margins = New System.Drawing.Printing.Margins(20, 20, 20, 20)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.CompanyName, Me.FilterPeriod, Me.paramTotalAktiva, Me.paramTotalPasivaDanEquity})
        Me.ScriptLanguage = DevExpress.XtraReports.ScriptLanguage.VisualBasic
        Me.Version = "16.2"
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents CompanyName As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents FilterPeriod As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents xrTitle As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrCompanyName As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents xrPeriod As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents srAktivaLancar As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents srPasiva As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrPageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents XrPageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents srAktivaTetap As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents paramTotalAktiva As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents paramTotalPasivaDanEquity As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents srEquity As DevExpress.XtraReports.UI.XRSubreport
    Friend WithEvents banAktivaLancar As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents banAktivaTetap As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents banTotalAktiva As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents banPasiva As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents banEquity As DevExpress.XtraReports.UI.SubBand
    Friend WithEvents banTotalPasivaDanEquity As DevExpress.XtraReports.UI.SubBand
End Class
