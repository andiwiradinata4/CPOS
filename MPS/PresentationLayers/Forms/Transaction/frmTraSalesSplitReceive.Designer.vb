<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraSalesSplitReceive
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pgMain = New System.Windows.Forms.ProgressBar()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripEmpty = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogInc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogBy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtNettoAfter = New MPS.usNumeric()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPlatNumber = New MPS.usTextBox()
        Me.txtDeduction = New MPS.usNumeric()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDriverName = New MPS.usTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNettoBefore = New MPS.usNumeric()
        Me.txtRemarks = New MPS.usTextBox()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTarra = New MPS.usNumeric()
        Me.cboPaymentTerm = New MPS.usComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboStatus = New MPS.usComboBox()
        Me.txtTotalPrice = New MPS.usNumeric()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dtpSalesDate = New System.Windows.Forms.DateTimePicker()
        Me.txtPrice = New MPS.usNumeric()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBrutto = New MPS.usNumeric()
        Me.cboUOMID = New MPS.usComboBox()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtBPName = New MPS.usTextBox()
        Me.txtItemCode = New MPS.usTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtID = New MPS.usTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItemName = New MPS.usTextBox()
        Me.lblSalesPrice = New System.Windows.Forms.Label()
        Me.lblUomID1 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.txtMaxNetto = New MPS.usNumeric()
        Me.lblMaxNetto = New System.Windows.Forms.Label()
        Me.txtArrivalNettoUsage = New MPS.usNumeric()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTolerance = New MPS.usNumeric()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grdMain = New DevExpress.XtraGrid.GridControl()
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.rpiValue = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.StatusStrip.SuspendLayout()
        CType(Me.txtNettoAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNettoBefore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTarra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBrutto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        CType(Me.txtMaxNetto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtArrivalNettoUsage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rpiValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(1003, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarRefresh
        '
        Me.BarRefresh.Name = "BarRefresh"
        Me.BarRefresh.Tag = "Save"
        Me.BarRefresh.Text = "Simpan"
        '
        'BarClose
        '
        Me.BarClose.Name = "BarClose"
        Me.BarClose.Tag = "Close"
        Me.BarClose.Text = "Tutup"
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.CadetBlue
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(0, 28)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(1003, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Penjualan Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 599)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(1003, 23)
        Me.pgMain.TabIndex = 6
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 577)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1003, 22)
        Me.StatusStrip.TabIndex = 5
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(880, 17)
        Me.ToolStripEmpty.Spring = True
        '
        'ToolStripLogInc
        '
        Me.ToolStripLogInc.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLogInc.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLogInc.Name = "ToolStripLogInc"
        Me.ToolStripLogInc.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripLogInc.Text = "Log Inc : "
        '
        'ToolStripLogBy
        '
        Me.ToolStripLogBy.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLogBy.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLogBy.Name = "ToolStripLogBy"
        Me.ToolStripLogBy.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripLogBy.Text = "Last Log :"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripLogDate
        '
        Me.ToolStripLogDate.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripLogDate.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.ToolStripLogDate.Name = "ToolStripLogDate"
        Me.ToolStripLogDate.Size = New System.Drawing.Size(12, 17)
        Me.ToolStripLogDate.Text = "-"
        '
        'txtNettoAfter
        '
        Me.txtNettoAfter.BackColor = System.Drawing.Color.LightYellow
        Me.txtNettoAfter.DecimalPlaces = 2
        Me.txtNettoAfter.Enabled = False
        Me.txtNettoAfter.Location = New System.Drawing.Point(492, 239)
        Me.txtNettoAfter.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNettoAfter.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNettoAfter.Name = "txtNettoAfter"
        Me.txtNettoAfter.ReadOnly = True
        Me.txtNettoAfter.Size = New System.Drawing.Size(160, 21)
        Me.txtNettoAfter.TabIndex = 16
        Me.txtNettoAfter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNettoAfter.ThousandsSeparator = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(22, 154)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 115
        Me.Label12.Text = "Nomor Plat"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(415, 242)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 106
        Me.Label8.Text = "Netto 2"
        '
        'txtPlatNumber
        '
        Me.txtPlatNumber.BackColor = System.Drawing.Color.LightYellow
        Me.txtPlatNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlatNumber.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPlatNumber.Location = New System.Drawing.Point(128, 150)
        Me.txtPlatNumber.MaxLength = 250
        Me.txtPlatNumber.Name = "txtPlatNumber"
        Me.txtPlatNumber.ReadOnly = True
        Me.txtPlatNumber.Size = New System.Drawing.Size(96, 21)
        Me.txtPlatNumber.TabIndex = 5
        '
        'txtDeduction
        '
        Me.txtDeduction.BackColor = System.Drawing.Color.LightYellow
        Me.txtDeduction.DecimalPlaces = 2
        Me.txtDeduction.Enabled = False
        Me.txtDeduction.Location = New System.Drawing.Point(492, 212)
        Me.txtDeduction.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDeduction.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.Size = New System.Drawing.Size(160, 21)
        Me.txtDeduction.TabIndex = 15
        Me.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeduction.ThousandsSeparator = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(22, 181)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 113
        Me.Label11.Text = "Nama Supir"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(415, 216)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "Potongan"
        '
        'txtDriverName
        '
        Me.txtDriverName.BackColor = System.Drawing.Color.LightYellow
        Me.txtDriverName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDriverName.Location = New System.Drawing.Point(128, 177)
        Me.txtDriverName.MaxLength = 250
        Me.txtDriverName.Name = "txtDriverName"
        Me.txtDriverName.ReadOnly = True
        Me.txtDriverName.Size = New System.Drawing.Size(220, 21)
        Me.txtDriverName.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(22, 208)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 110
        Me.Label10.Text = "Keterangan"
        '
        'txtNettoBefore
        '
        Me.txtNettoBefore.BackColor = System.Drawing.Color.LightYellow
        Me.txtNettoBefore.DecimalPlaces = 2
        Me.txtNettoBefore.Enabled = False
        Me.txtNettoBefore.Location = New System.Drawing.Point(492, 185)
        Me.txtNettoBefore.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNettoBefore.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNettoBefore.Name = "txtNettoBefore"
        Me.txtNettoBefore.ReadOnly = True
        Me.txtNettoBefore.Size = New System.Drawing.Size(160, 21)
        Me.txtNettoBefore.TabIndex = 14
        Me.txtNettoBefore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNettoBefore.ThousandsSeparator = True
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.LightYellow
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(128, 204)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReadOnly = True
        Me.txtRemarks.Size = New System.Drawing.Size(220, 50)
        Me.txtRemarks.TabIndex = 7
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDueDate.Enabled = False
        Me.dtpDueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(128, 123)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(96, 21)
        Me.dtpDueDate.TabIndex = 4
        Me.dtpDueDate.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(415, 188)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 102
        Me.Label13.Text = "Netto 1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 101
        Me.Label5.Text = "Jatuh Tempo"
        '
        'txtTarra
        '
        Me.txtTarra.BackColor = System.Drawing.Color.LightYellow
        Me.txtTarra.DecimalPlaces = 2
        Me.txtTarra.Enabled = False
        Me.txtTarra.Location = New System.Drawing.Point(492, 158)
        Me.txtTarra.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTarra.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTarra.Name = "txtTarra"
        Me.txtTarra.Size = New System.Drawing.Size(160, 21)
        Me.txtTarra.TabIndex = 13
        Me.txtTarra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarra.ThousandsSeparator = True
        '
        'cboPaymentTerm
        '
        Me.cboPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentTerm.Enabled = False
        Me.cboPaymentTerm.FormattingEnabled = True
        Me.cboPaymentTerm.Location = New System.Drawing.Point(128, 96)
        Me.cboPaymentTerm.Name = "cboPaymentTerm"
        Me.cboPaymentTerm.Size = New System.Drawing.Size(143, 21)
        Me.cboPaymentTerm.TabIndex = 3
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(415, 162)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(33, 13)
        Me.Label14.TabIndex = 100
        Me.Label14.Text = "Tarra"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(22, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 99
        Me.Label4.Text = "Jenis Pembayaran"
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(781, 11)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 9
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.BackColor = System.Drawing.Color.LightYellow
        Me.txtTotalPrice.DecimalPlaces = 2
        Me.txtTotalPrice.Enabled = False
        Me.txtTotalPrice.Location = New System.Drawing.Point(781, 131)
        Me.txtTotalPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalPrice.Name = "txtTotalPrice"
        Me.txtTotalPrice.ReadOnly = True
        Me.txtTotalPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtTotalPrice.TabIndex = 18
        Me.txtTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPrice.ThousandsSeparator = True
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(724, 15)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblIDStatus.TabIndex = 97
        Me.lblIDStatus.Text = "Status"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(699, 134)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 13)
        Me.Label16.TabIndex = 96
        Me.Label16.Text = "Total Harga"
        '
        'dtpSalesDate
        '
        Me.dtpSalesDate.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpSalesDate.Enabled = False
        Me.dtpSalesDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSalesDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSalesDate.Location = New System.Drawing.Point(128, 69)
        Me.dtpSalesDate.Name = "dtpSalesDate"
        Me.dtpSalesDate.Size = New System.Drawing.Size(143, 21)
        Me.dtpSalesDate.TabIndex = 2
        Me.dtpSalesDate.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'txtPrice
        '
        Me.txtPrice.BackColor = System.Drawing.Color.LightYellow
        Me.txtPrice.DecimalPlaces = 2
        Me.txtPrice.Enabled = False
        Me.txtPrice.Location = New System.Drawing.Point(781, 104)
        Me.txtPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtPrice.TabIndex = 17
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPrice.ThousandsSeparator = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Tanggal"
        '
        'txtBrutto
        '
        Me.txtBrutto.BackColor = System.Drawing.Color.LightYellow
        Me.txtBrutto.DecimalPlaces = 2
        Me.txtBrutto.Enabled = False
        Me.txtBrutto.Location = New System.Drawing.Point(492, 131)
        Me.txtBrutto.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtBrutto.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtBrutto.Name = "txtBrutto"
        Me.txtBrutto.Size = New System.Drawing.Size(160, 21)
        Me.txtBrutto.TabIndex = 12
        Me.txtBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBrutto.ThousandsSeparator = True
        '
        'cboUOMID
        '
        Me.cboUOMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUOMID.Enabled = False
        Me.cboUOMID.FormattingEnabled = True
        Me.cboUOMID.Location = New System.Drawing.Point(492, 104)
        Me.cboUOMID.Name = "cboUOMID"
        Me.cboUOMID.Size = New System.Drawing.Size(160, 21)
        Me.cboUOMID.TabIndex = 11
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(415, 14)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(68, 13)
        Me.lblCode.TabIndex = 93
        Me.lblCode.Text = "Kode Barang"
        '
        'txtBPName
        '
        Me.txtBPName.BackColor = System.Drawing.Color.LightYellow
        Me.txtBPName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBPName.Location = New System.Drawing.Point(128, 42)
        Me.txtBPName.Name = "txtBPName"
        Me.txtBPName.ReadOnly = True
        Me.txtBPName.Size = New System.Drawing.Size(220, 21)
        Me.txtBPName.TabIndex = 1
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.LightYellow
        Me.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtItemCode.Location = New System.Drawing.Point(492, 11)
        Me.txtItemCode.MaxLength = 250
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReadOnly = True
        Me.txtItemCode.Size = New System.Drawing.Size(160, 21)
        Me.txtItemCode.TabIndex = 8
        Me.txtItemCode.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Pelanggan"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(415, 41)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(71, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama Barang"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightYellow
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Location = New System.Drawing.Point(128, 15)
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(143, 21)
        Me.txtID.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nomor"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.LightYellow
        Me.txtItemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtItemName.Location = New System.Drawing.Point(492, 38)
        Me.txtItemName.MaxLength = 250
        Me.txtItemName.Multiline = True
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(449, 60)
        Me.txtItemName.TabIndex = 10
        '
        'lblSalesPrice
        '
        Me.lblSalesPrice.AutoSize = True
        Me.lblSalesPrice.BackColor = System.Drawing.Color.Transparent
        Me.lblSalesPrice.ForeColor = System.Drawing.Color.Black
        Me.lblSalesPrice.Location = New System.Drawing.Point(726, 107)
        Me.lblSalesPrice.Name = "lblSalesPrice"
        Me.lblSalesPrice.Size = New System.Drawing.Size(36, 13)
        Me.lblSalesPrice.TabIndex = 93
        Me.lblSalesPrice.Text = "Harga"
        '
        'lblUomID1
        '
        Me.lblUomID1.AutoSize = True
        Me.lblUomID1.BackColor = System.Drawing.Color.Transparent
        Me.lblUomID1.ForeColor = System.Drawing.Color.Black
        Me.lblUomID1.Location = New System.Drawing.Point(415, 108)
        Me.lblUomID1.Name = "lblUomID1"
        Me.lblUomID1.Size = New System.Drawing.Size(41, 13)
        Me.lblUomID1.TabIndex = 93
        Me.lblUomID1.Text = "Satuan"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.BackColor = System.Drawing.Color.Transparent
        Me.lblQty.ForeColor = System.Drawing.Color.Black
        Me.lblQty.Location = New System.Drawing.Point(415, 135)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(37, 13)
        Me.lblQty.TabIndex = 93
        Me.lblQty.Text = "Brutto"
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.txtMaxNetto)
        Me.pnlMain.Controls.Add(Me.lblMaxNetto)
        Me.pnlMain.Controls.Add(Me.txtArrivalNettoUsage)
        Me.pnlMain.Controls.Add(Me.Label24)
        Me.pnlMain.Controls.Add(Me.txtTolerance)
        Me.pnlMain.Controls.Add(Me.Label15)
        Me.pnlMain.Controls.Add(Me.txtNettoAfter)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.lblQty)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.lblUomID1)
        Me.pnlMain.Controls.Add(Me.txtPlatNumber)
        Me.pnlMain.Controls.Add(Me.lblSalesPrice)
        Me.pnlMain.Controls.Add(Me.txtDeduction)
        Me.pnlMain.Controls.Add(Me.txtItemName)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.txtID)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.lblName)
        Me.pnlMain.Controls.Add(Me.txtDriverName)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.txtItemCode)
        Me.pnlMain.Controls.Add(Me.txtNettoBefore)
        Me.pnlMain.Controls.Add(Me.txtBPName)
        Me.pnlMain.Controls.Add(Me.txtRemarks)
        Me.pnlMain.Controls.Add(Me.lblCode)
        Me.pnlMain.Controls.Add(Me.dtpDueDate)
        Me.pnlMain.Controls.Add(Me.cboUOMID)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.txtBrutto)
        Me.pnlMain.Controls.Add(Me.txtTarra)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.cboPaymentTerm)
        Me.pnlMain.Controls.Add(Me.txtPrice)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.dtpSalesDate)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label16)
        Me.pnlMain.Controls.Add(Me.lblIDStatus)
        Me.pnlMain.Controls.Add(Me.cboStatus)
        Me.pnlMain.Controls.Add(Me.txtTotalPrice)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMain.Location = New System.Drawing.Point(0, 50)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1003, 284)
        Me.pnlMain.TabIndex = 2
        '
        'txtMaxNetto
        '
        Me.txtMaxNetto.BackColor = System.Drawing.Color.LightYellow
        Me.txtMaxNetto.DecimalPlaces = 2
        Me.txtMaxNetto.Enabled = False
        Me.txtMaxNetto.Location = New System.Drawing.Point(781, 185)
        Me.txtMaxNetto.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtMaxNetto.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtMaxNetto.Name = "txtMaxNetto"
        Me.txtMaxNetto.Size = New System.Drawing.Size(160, 21)
        Me.txtMaxNetto.TabIndex = 20
        Me.txtMaxNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaxNetto.ThousandsSeparator = True
        '
        'lblMaxNetto
        '
        Me.lblMaxNetto.AutoSize = True
        Me.lblMaxNetto.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxNetto.ForeColor = System.Drawing.Color.Black
        Me.lblMaxNetto.Location = New System.Drawing.Point(701, 189)
        Me.lblMaxNetto.Name = "lblMaxNetto"
        Me.lblMaxNetto.Size = New System.Drawing.Size(65, 13)
        Me.lblMaxNetto.TabIndex = 145
        Me.lblMaxNetto.Text = "Maks. Netto"
        '
        'txtArrivalNettoUsage
        '
        Me.txtArrivalNettoUsage.BackColor = System.Drawing.Color.LightYellow
        Me.txtArrivalNettoUsage.DecimalPlaces = 2
        Me.txtArrivalNettoUsage.Enabled = False
        Me.txtArrivalNettoUsage.Location = New System.Drawing.Point(781, 212)
        Me.txtArrivalNettoUsage.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtArrivalNettoUsage.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtArrivalNettoUsage.Name = "txtArrivalNettoUsage"
        Me.txtArrivalNettoUsage.Size = New System.Drawing.Size(160, 21)
        Me.txtArrivalNettoUsage.TabIndex = 21
        Me.txtArrivalNettoUsage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtArrivalNettoUsage.ThousandsSeparator = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(675, 216)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(87, 13)
        Me.Label24.TabIndex = 143
        Me.Label24.Text = "Netto 2 Terpakai"
        '
        'txtTolerance
        '
        Me.txtTolerance.BackColor = System.Drawing.Color.LightYellow
        Me.txtTolerance.DecimalPlaces = 2
        Me.txtTolerance.Enabled = False
        Me.txtTolerance.Location = New System.Drawing.Point(781, 158)
        Me.txtTolerance.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTolerance.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.Size = New System.Drawing.Size(160, 21)
        Me.txtTolerance.TabIndex = 19
        Me.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTolerance.ThousandsSeparator = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(712, 162)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 13)
        Me.Label15.TabIndex = 139
        Me.Label15.Text = "Toleransi"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.CadetBlue
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(0, 334)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1003, 22)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "« Pembelian Detail"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdMain
        '
        Me.grdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMain.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grdMain.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grdMain.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grdMain.EmbeddedNavigator.Buttons.EndEdit.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grdMain.EmbeddedNavigator.Buttons.NextPage.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.grdMain.EmbeddedNavigator.Buttons.PrevPage.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.grdMain.EmbeddedNavigator.Buttons.Remove.Enabled = False
        Me.grdMain.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grdMain.Location = New System.Drawing.Point(0, 356)
        Me.grdMain.MainView = Me.grdView
        Me.grdMain.Name = "grdMain"
        Me.grdMain.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpiValue})
        Me.grdMain.Size = New System.Drawing.Size(1003, 221)
        Me.grdMain.TabIndex = 4
        Me.grdMain.UseEmbeddedNavigator = True
        Me.grdMain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdView})
        '
        'grdView
        '
        Me.grdView.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grdView.Appearance.FooterPanel.Options.UseFont = True
        Me.grdView.GridControl = Me.grdMain
        Me.grdView.Name = "grdView"
        Me.grdView.OptionsCustomization.AllowColumnMoving = False
        Me.grdView.OptionsCustomization.AllowGroup = False
        Me.grdView.OptionsView.ColumnAutoWidth = False
        Me.grdView.OptionsView.ShowFooter = True
        Me.grdView.OptionsView.ShowGroupPanel = False
        '
        'rpiValue
        '
        Me.rpiValue.AutoHeight = False
        Me.rpiValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.rpiValue.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.rpiValue.Name = "rpiValue"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(947, 162)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(18, 13)
        Me.Label9.TabIndex = 146
        Me.Label9.Text = "%"
        '
        'frmTraSalesSplitReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 622)
        Me.Controls.Add(Me.grdMain)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTraSalesSplitReceive"
        Me.Text = "Split Pembelian"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        CType(Me.txtNettoAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNettoBefore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTarra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBrutto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.txtMaxNetto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArrivalNettoUsage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rpiValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pgMain As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripEmpty As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogInc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogBy As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtNettoAfter As MPS.usNumeric
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPlatNumber As MPS.usTextBox
    Friend WithEvents txtDeduction As MPS.usNumeric
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtDriverName As MPS.usTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtNettoBefore As MPS.usNumeric
    Friend WithEvents txtRemarks As MPS.usTextBox
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTarra As MPS.usNumeric
    Friend WithEvents cboPaymentTerm As MPS.usComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents txtTotalPrice As MPS.usNumeric
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dtpSalesDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPrice As MPS.usNumeric
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBrutto As MPS.usNumeric
    Friend WithEvents cboUOMID As MPS.usComboBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtBPName As MPS.usTextBox
    Friend WithEvents txtItemCode As MPS.usTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtID As MPS.usTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As MPS.usTextBox
    Friend WithEvents lblSalesPrice As System.Windows.Forms.Label
    Friend WithEvents lblUomID1 As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grdMain As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtTolerance As MPS.usNumeric
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtArrivalNettoUsage As MPS.usNumeric
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtMaxNetto As MPS.usNumeric
    Friend WithEvents lblMaxNetto As System.Windows.Forms.Label
    Friend WithEvents rpiValue As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
