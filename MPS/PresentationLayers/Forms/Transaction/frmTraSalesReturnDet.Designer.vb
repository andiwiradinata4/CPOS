<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraSalesReturnDet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraSalesReturnDet))
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
        Me.tcHeader = New System.Windows.Forms.TabControl()
        Me.tpMain = New System.Windows.Forms.TabPage()
        Me.txtArrivalNettoUsage = New MPS.usNumeric()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtArrivalNettoAfterSales = New MPS.usNumeric()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtMaxNetto = New MPS.usNumeric()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnReferences = New DevExpress.XtraEditors.SimpleButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtReferencesID = New MPS.usTextBox()
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
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTarra = New MPS.usNumeric()
        Me.cboPaymentTerm = New MPS.usComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboStatus = New MPS.usComboBox()
        Me.txtTotalPrice = New MPS.usNumeric()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dtpSalesReturnDate = New System.Windows.Forms.DateTimePicker()
        Me.txtPrice = New MPS.usNumeric()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBrutto = New MPS.usNumeric()
        Me.btnBP = New DevExpress.XtraEditors.SimpleButton()
        Me.cboUOMID = New MPS.usComboBox()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtBPName = New MPS.usTextBox()
        Me.txtItemCode = New MPS.usTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtID = New MPS.usTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtItemName = New MPS.usTextBox()
        Me.lblPrice1 = New System.Windows.Forms.Label()
        Me.lblUomID1 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.tpHistory = New System.Windows.Forms.TabPage()
        Me.grdStatus = New DevExpress.XtraGrid.GridControl()
        Me.grdStatusView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.StatusStrip.SuspendLayout()
        Me.tcHeader.SuspendLayout()
        Me.tpMain.SuspendLayout()
        CType(Me.txtArrivalNettoUsage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtArrivalNettoAfterSales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaxNetto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNettoAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNettoBefore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTarra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBrutto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpHistory.SuspendLayout()
        CType(Me.grdStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdStatusView, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblInfo.Text = "« Retur Penjualan Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 389)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(1003, 23)
        Me.pgMain.TabIndex = 4
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 367)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1003, 22)
        Me.StatusStrip.TabIndex = 3
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
        'tcHeader
        '
        Me.tcHeader.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcHeader.Controls.Add(Me.tpMain)
        Me.tcHeader.Controls.Add(Me.tpHistory)
        Me.tcHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcHeader.Location = New System.Drawing.Point(0, 50)
        Me.tcHeader.Name = "tcHeader"
        Me.tcHeader.SelectedIndex = 0
        Me.tcHeader.Size = New System.Drawing.Size(1003, 317)
        Me.tcHeader.TabIndex = 2
        '
        'tpMain
        '
        Me.tpMain.AutoScroll = True
        Me.tpMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpMain.Controls.Add(Me.txtArrivalNettoUsage)
        Me.tpMain.Controls.Add(Me.Label24)
        Me.tpMain.Controls.Add(Me.txtArrivalNettoAfterSales)
        Me.tpMain.Controls.Add(Me.Label17)
        Me.tpMain.Controls.Add(Me.txtMaxNetto)
        Me.tpMain.Controls.Add(Me.Label9)
        Me.tpMain.Controls.Add(Me.btnReferences)
        Me.tpMain.Controls.Add(Me.Label6)
        Me.tpMain.Controls.Add(Me.txtReferencesID)
        Me.tpMain.Controls.Add(Me.txtNettoAfter)
        Me.tpMain.Controls.Add(Me.Label12)
        Me.tpMain.Controls.Add(Me.Label8)
        Me.tpMain.Controls.Add(Me.txtPlatNumber)
        Me.tpMain.Controls.Add(Me.txtDeduction)
        Me.tpMain.Controls.Add(Me.Label11)
        Me.tpMain.Controls.Add(Me.Label7)
        Me.tpMain.Controls.Add(Me.txtDriverName)
        Me.tpMain.Controls.Add(Me.Label10)
        Me.tpMain.Controls.Add(Me.txtNettoBefore)
        Me.tpMain.Controls.Add(Me.txtRemarks)
        Me.tpMain.Controls.Add(Me.Label13)
        Me.tpMain.Controls.Add(Me.txtTarra)
        Me.tpMain.Controls.Add(Me.cboPaymentTerm)
        Me.tpMain.Controls.Add(Me.Label14)
        Me.tpMain.Controls.Add(Me.Label4)
        Me.tpMain.Controls.Add(Me.cboStatus)
        Me.tpMain.Controls.Add(Me.txtTotalPrice)
        Me.tpMain.Controls.Add(Me.lblIDStatus)
        Me.tpMain.Controls.Add(Me.Label16)
        Me.tpMain.Controls.Add(Me.dtpSalesReturnDate)
        Me.tpMain.Controls.Add(Me.txtPrice)
        Me.tpMain.Controls.Add(Me.Label3)
        Me.tpMain.Controls.Add(Me.txtBrutto)
        Me.tpMain.Controls.Add(Me.btnBP)
        Me.tpMain.Controls.Add(Me.cboUOMID)
        Me.tpMain.Controls.Add(Me.lblCode)
        Me.tpMain.Controls.Add(Me.txtBPName)
        Me.tpMain.Controls.Add(Me.txtItemCode)
        Me.tpMain.Controls.Add(Me.Label2)
        Me.tpMain.Controls.Add(Me.lblName)
        Me.tpMain.Controls.Add(Me.txtID)
        Me.tpMain.Controls.Add(Me.Label1)
        Me.tpMain.Controls.Add(Me.txtItemName)
        Me.tpMain.Controls.Add(Me.lblPrice1)
        Me.tpMain.Controls.Add(Me.lblUomID1)
        Me.tpMain.Controls.Add(Me.lblQty)
        Me.tpMain.Location = New System.Drawing.Point(4, 25)
        Me.tpMain.Name = "tpMain"
        Me.tpMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMain.Size = New System.Drawing.Size(995, 288)
        Me.tpMain.TabIndex = 0
        Me.tpMain.Text = "Main - F1"
        Me.tpMain.UseVisualStyleBackColor = True
        '
        'txtArrivalNettoUsage
        '
        Me.txtArrivalNettoUsage.BackColor = System.Drawing.Color.LightYellow
        Me.txtArrivalNettoUsage.DecimalPlaces = 2
        Me.txtArrivalNettoUsage.Enabled = False
        Me.txtArrivalNettoUsage.Location = New System.Drawing.Point(791, 194)
        Me.txtArrivalNettoUsage.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtArrivalNettoUsage.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtArrivalNettoUsage.Name = "txtArrivalNettoUsage"
        Me.txtArrivalNettoUsage.Size = New System.Drawing.Size(160, 21)
        Me.txtArrivalNettoUsage.TabIndex = 22
        Me.txtArrivalNettoUsage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtArrivalNettoUsage.ThousandsSeparator = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(690, 198)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(78, 13)
        Me.Label24.TabIndex = 141
        Me.Label24.Text = "Netto Terpakai"
        '
        'txtArrivalNettoAfterSales
        '
        Me.txtArrivalNettoAfterSales.BackColor = System.Drawing.Color.LightYellow
        Me.txtArrivalNettoAfterSales.DecimalPlaces = 2
        Me.txtArrivalNettoAfterSales.Enabled = False
        Me.txtArrivalNettoAfterSales.Location = New System.Drawing.Point(791, 167)
        Me.txtArrivalNettoAfterSales.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtArrivalNettoAfterSales.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtArrivalNettoAfterSales.Name = "txtArrivalNettoAfterSales"
        Me.txtArrivalNettoAfterSales.Size = New System.Drawing.Size(160, 21)
        Me.txtArrivalNettoAfterSales.TabIndex = 21
        Me.txtArrivalNettoAfterSales.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtArrivalNettoAfterSales.ThousandsSeparator = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(703, 171)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(65, 13)
        Me.Label17.TabIndex = 139
        Me.Label17.Text = "Netto 2 Jual"
        '
        'txtMaxNetto
        '
        Me.txtMaxNetto.BackColor = System.Drawing.Color.LightYellow
        Me.txtMaxNetto.DecimalPlaces = 2
        Me.txtMaxNetto.Enabled = False
        Me.txtMaxNetto.Location = New System.Drawing.Point(791, 221)
        Me.txtMaxNetto.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtMaxNetto.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtMaxNetto.Name = "txtMaxNetto"
        Me.txtMaxNetto.Size = New System.Drawing.Size(160, 21)
        Me.txtMaxNetto.TabIndex = 23
        Me.txtMaxNetto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaxNetto.ThousandsSeparator = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(707, 225)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 13)
        Me.Label9.TabIndex = 120
        Me.Label9.Text = "Max. Netto"
        '
        'btnReferences
        '
        Me.btnReferences.Image = CType(resources.GetObject("btnReferences.Image"), System.Drawing.Image)
        Me.btnReferences.Location = New System.Drawing.Point(284, 127)
        Me.btnReferences.Name = "btnReferences"
        Me.btnReferences.Size = New System.Drawing.Size(23, 23)
        Me.btnReferences.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(29, 131)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 13)
        Me.Label6.TabIndex = 118
        Me.Label6.Text = "Nomor Referensi"
        '
        'txtReferencesID
        '
        Me.txtReferencesID.BackColor = System.Drawing.Color.Azure
        Me.txtReferencesID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReferencesID.Location = New System.Drawing.Point(135, 128)
        Me.txtReferencesID.MaxLength = 250
        Me.txtReferencesID.Name = "txtReferencesID"
        Me.txtReferencesID.ReadOnly = True
        Me.txtReferencesID.Size = New System.Drawing.Size(143, 21)
        Me.txtReferencesID.TabIndex = 5
        Me.txtReferencesID.TabStop = False
        '
        'txtNettoAfter
        '
        Me.txtNettoAfter.BackColor = System.Drawing.Color.LightYellow
        Me.txtNettoAfter.DecimalPlaces = 2
        Me.txtNettoAfter.Enabled = False
        Me.txtNettoAfter.Location = New System.Drawing.Point(502, 248)
        Me.txtNettoAfter.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNettoAfter.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNettoAfter.Name = "txtNettoAfter"
        Me.txtNettoAfter.ReadOnly = True
        Me.txtNettoAfter.Size = New System.Drawing.Size(160, 21)
        Me.txtNettoAfter.TabIndex = 18
        Me.txtNettoAfter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNettoAfter.ThousandsSeparator = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(29, 159)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 13)
        Me.Label12.TabIndex = 115
        Me.Label12.Text = "Nomor Polisi"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(425, 252)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 13)
        Me.Label8.TabIndex = 106
        Me.Label8.Text = "Netto 2"
        '
        'txtPlatNumber
        '
        Me.txtPlatNumber.BackColor = System.Drawing.Color.White
        Me.txtPlatNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPlatNumber.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPlatNumber.Location = New System.Drawing.Point(135, 155)
        Me.txtPlatNumber.MaxLength = 250
        Me.txtPlatNumber.Name = "txtPlatNumber"
        Me.txtPlatNumber.Size = New System.Drawing.Size(96, 21)
        Me.txtPlatNumber.TabIndex = 7
        '
        'txtDeduction
        '
        Me.txtDeduction.DecimalPlaces = 2
        Me.txtDeduction.Location = New System.Drawing.Point(502, 221)
        Me.txtDeduction.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtDeduction.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.Size = New System.Drawing.Size(160, 21)
        Me.txtDeduction.TabIndex = 17
        Me.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDeduction.ThousandsSeparator = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(29, 186)
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
        Me.Label7.Location = New System.Drawing.Point(425, 225)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "Potongan"
        '
        'txtDriverName
        '
        Me.txtDriverName.BackColor = System.Drawing.Color.White
        Me.txtDriverName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDriverName.Location = New System.Drawing.Point(135, 182)
        Me.txtDriverName.MaxLength = 250
        Me.txtDriverName.Name = "txtDriverName"
        Me.txtDriverName.Size = New System.Drawing.Size(220, 21)
        Me.txtDriverName.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(29, 213)
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
        Me.txtNettoBefore.Location = New System.Drawing.Point(502, 194)
        Me.txtNettoBefore.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtNettoBefore.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtNettoBefore.Name = "txtNettoBefore"
        Me.txtNettoBefore.ReadOnly = True
        Me.txtNettoBefore.Size = New System.Drawing.Size(160, 21)
        Me.txtNettoBefore.TabIndex = 16
        Me.txtNettoBefore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNettoBefore.ThousandsSeparator = True
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(135, 209)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(220, 60)
        Me.txtRemarks.TabIndex = 9
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(425, 198)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 13)
        Me.Label13.TabIndex = 102
        Me.Label13.Text = "Netto 1"
        '
        'txtTarra
        '
        Me.txtTarra.DecimalPlaces = 2
        Me.txtTarra.Location = New System.Drawing.Point(502, 167)
        Me.txtTarra.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTarra.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTarra.Name = "txtTarra"
        Me.txtTarra.Size = New System.Drawing.Size(160, 21)
        Me.txtTarra.TabIndex = 15
        Me.txtTarra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTarra.ThousandsSeparator = True
        '
        'cboPaymentTerm
        '
        Me.cboPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentTerm.FormattingEnabled = True
        Me.cboPaymentTerm.Location = New System.Drawing.Point(135, 101)
        Me.cboPaymentTerm.Name = "cboPaymentTerm"
        Me.cboPaymentTerm.Size = New System.Drawing.Size(143, 21)
        Me.cboPaymentTerm.TabIndex = 4
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(425, 171)
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
        Me.Label4.Location = New System.Drawing.Point(29, 105)
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
        Me.cboStatus.Location = New System.Drawing.Point(791, 20)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 11
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.BackColor = System.Drawing.Color.LightYellow
        Me.txtTotalPrice.DecimalPlaces = 2
        Me.txtTotalPrice.Enabled = False
        Me.txtTotalPrice.Location = New System.Drawing.Point(791, 140)
        Me.txtTotalPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalPrice.Name = "txtTotalPrice"
        Me.txtTotalPrice.ReadOnly = True
        Me.txtTotalPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtTotalPrice.TabIndex = 20
        Me.txtTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPrice.ThousandsSeparator = True
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(734, 24)
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
        Me.Label16.Location = New System.Drawing.Point(705, 143)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(63, 13)
        Me.Label16.TabIndex = 96
        Me.Label16.Text = "Total Harga"
        '
        'dtpSalesReturnDate
        '
        Me.dtpSalesReturnDate.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpSalesReturnDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSalesReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSalesReturnDate.Location = New System.Drawing.Point(135, 47)
        Me.dtpSalesReturnDate.Name = "dtpSalesReturnDate"
        Me.dtpSalesReturnDate.Size = New System.Drawing.Size(143, 21)
        Me.dtpSalesReturnDate.TabIndex = 1
        Me.dtpSalesReturnDate.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'txtPrice
        '
        Me.txtPrice.BackColor = System.Drawing.Color.White
        Me.txtPrice.DecimalPlaces = 2
        Me.txtPrice.Location = New System.Drawing.Point(791, 113)
        Me.txtPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtPrice.TabIndex = 19
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPrice.ThousandsSeparator = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Tanggal"
        '
        'txtBrutto
        '
        Me.txtBrutto.DecimalPlaces = 2
        Me.txtBrutto.Location = New System.Drawing.Point(502, 140)
        Me.txtBrutto.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtBrutto.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtBrutto.Name = "txtBrutto"
        Me.txtBrutto.Size = New System.Drawing.Size(160, 21)
        Me.txtBrutto.TabIndex = 14
        Me.txtBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBrutto.ThousandsSeparator = True
        '
        'btnBP
        '
        Me.btnBP.Image = CType(resources.GetObject("btnBP.Image"), System.Drawing.Image)
        Me.btnBP.Location = New System.Drawing.Point(360, 73)
        Me.btnBP.Name = "btnBP"
        Me.btnBP.Size = New System.Drawing.Size(23, 23)
        Me.btnBP.TabIndex = 3
        '
        'cboUOMID
        '
        Me.cboUOMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUOMID.Enabled = False
        Me.cboUOMID.FormattingEnabled = True
        Me.cboUOMID.Location = New System.Drawing.Point(502, 113)
        Me.cboUOMID.Name = "cboUOMID"
        Me.cboUOMID.Size = New System.Drawing.Size(160, 21)
        Me.cboUOMID.TabIndex = 13
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(425, 23)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(68, 13)
        Me.lblCode.TabIndex = 93
        Me.lblCode.Text = "Kode Barang"
        '
        'txtBPName
        '
        Me.txtBPName.BackColor = System.Drawing.Color.Azure
        Me.txtBPName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBPName.Location = New System.Drawing.Point(135, 74)
        Me.txtBPName.Name = "txtBPName"
        Me.txtBPName.ReadOnly = True
        Me.txtBPName.Size = New System.Drawing.Size(220, 21)
        Me.txtBPName.TabIndex = 2
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.Azure
        Me.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtItemCode.Location = New System.Drawing.Point(502, 20)
        Me.txtItemCode.MaxLength = 250
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.ReadOnly = True
        Me.txtItemCode.Size = New System.Drawing.Size(160, 21)
        Me.txtItemCode.TabIndex = 10
        Me.txtItemCode.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 78)
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
        Me.lblName.Location = New System.Drawing.Point(425, 51)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(71, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama Barang"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightYellow
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Location = New System.Drawing.Point(135, 20)
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(143, 21)
        Me.txtID.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nomor"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.Azure
        Me.txtItemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtItemName.Location = New System.Drawing.Point(502, 47)
        Me.txtItemName.MaxLength = 250
        Me.txtItemName.Multiline = True
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(449, 60)
        Me.txtItemName.TabIndex = 12
        '
        'lblPrice1
        '
        Me.lblPrice1.AutoSize = True
        Me.lblPrice1.BackColor = System.Drawing.Color.Transparent
        Me.lblPrice1.ForeColor = System.Drawing.Color.Black
        Me.lblPrice1.Location = New System.Drawing.Point(732, 116)
        Me.lblPrice1.Name = "lblPrice1"
        Me.lblPrice1.Size = New System.Drawing.Size(36, 13)
        Me.lblPrice1.TabIndex = 93
        Me.lblPrice1.Text = "Harga"
        '
        'lblUomID1
        '
        Me.lblUomID1.AutoSize = True
        Me.lblUomID1.BackColor = System.Drawing.Color.Transparent
        Me.lblUomID1.ForeColor = System.Drawing.Color.Black
        Me.lblUomID1.Location = New System.Drawing.Point(425, 117)
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
        Me.lblQty.Location = New System.Drawing.Point(425, 144)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(37, 13)
        Me.lblQty.TabIndex = 93
        Me.lblQty.Text = "Brutto"
        '
        'tpHistory
        '
        Me.tpHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpHistory.Controls.Add(Me.grdStatus)
        Me.tpHistory.Location = New System.Drawing.Point(4, 25)
        Me.tpHistory.Name = "tpHistory"
        Me.tpHistory.Padding = New System.Windows.Forms.Padding(3)
        Me.tpHistory.Size = New System.Drawing.Size(995, 288)
        Me.tpHistory.TabIndex = 1
        Me.tpHistory.Text = "History - F2"
        Me.tpHistory.UseVisualStyleBackColor = True
        '
        'grdStatus
        '
        Me.grdStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStatus.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grdStatus.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grdStatus.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grdStatus.EmbeddedNavigator.Buttons.EndEdit.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grdStatus.EmbeddedNavigator.Buttons.NextPage.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.grdStatus.EmbeddedNavigator.Buttons.PrevPage.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.grdStatus.EmbeddedNavigator.Buttons.Remove.Enabled = False
        Me.grdStatus.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grdStatus.Location = New System.Drawing.Point(3, 3)
        Me.grdStatus.MainView = Me.grdStatusView
        Me.grdStatus.Name = "grdStatus"
        Me.grdStatus.Size = New System.Drawing.Size(985, 278)
        Me.grdStatus.TabIndex = 12
        Me.grdStatus.UseEmbeddedNavigator = True
        Me.grdStatus.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdStatusView})
        '
        'grdStatusView
        '
        Me.grdStatusView.GridControl = Me.grdStatus
        Me.grdStatusView.Name = "grdStatusView"
        Me.grdStatusView.OptionsCustomization.AllowColumnMoving = False
        Me.grdStatusView.OptionsCustomization.AllowGroup = False
        Me.grdStatusView.OptionsView.ColumnAutoWidth = False
        Me.grdStatusView.OptionsView.ShowGroupPanel = False
        '
        'frmTraSalesReturnDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 412)
        Me.Controls.Add(Me.tcHeader)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTraSalesReturnDet"
        Me.Text = "Retur Penjualan"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.tcHeader.ResumeLayout(False)
        Me.tpMain.ResumeLayout(False)
        Me.tpMain.PerformLayout()
        CType(Me.txtArrivalNettoUsage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArrivalNettoAfterSales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaxNetto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNettoAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNettoBefore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTarra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBrutto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpHistory.ResumeLayout(False)
        CType(Me.grdStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdStatusView, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tcHeader As System.Windows.Forms.TabControl
    Friend WithEvents tpHistory As System.Windows.Forms.TabPage
    Friend WithEvents grdStatus As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdStatusView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents tpMain As System.Windows.Forms.TabPage
    Friend WithEvents txtArrivalNettoUsage As MPS.usNumeric
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtArrivalNettoAfterSales As MPS.usNumeric
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtMaxNetto As MPS.usNumeric
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnReferences As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtReferencesID As MPS.usTextBox
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
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTarra As MPS.usNumeric
    Friend WithEvents cboPaymentTerm As MPS.usComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents txtTotalPrice As MPS.usNumeric
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dtpSalesReturnDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPrice As MPS.usNumeric
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBrutto As MPS.usNumeric
    Friend WithEvents btnBP As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cboUOMID As MPS.usComboBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtBPName As MPS.usTextBox
    Friend WithEvents txtItemCode As MPS.usTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtID As MPS.usTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtItemName As MPS.usTextBox
    Friend WithEvents lblPrice1 As System.Windows.Forms.Label
    Friend WithEvents lblUomID1 As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
End Class
