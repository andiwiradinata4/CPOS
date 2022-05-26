<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraReceiveReturnDet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraReceiveReturnDet))
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
        Me.txtArrivalNettoAfterReceive = New MPS.usNumeric()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTotalPrice2 = New MPS.usNumeric()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtPrice2 = New MPS.usNumeric()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtSpecification = New MPS.usTextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtSegelNumber = New MPS.usTextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtSPBNumber = New MPS.usTextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDONumber = New MPS.usTextBox()
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
        Me.txtTotalPrice1 = New MPS.usNumeric()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.dtpReceiveReturnDate = New System.Windows.Forms.DateTimePicker()
        Me.txtPrice1 = New MPS.usNumeric()
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
        CType(Me.txtArrivalNettoAfterReceive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPrice2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrice2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaxNetto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNettoAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDeduction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNettoBefore, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTarra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalPrice1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrice1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ToolBar.Size = New System.Drawing.Size(1002, 28)
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
        Me.lblInfo.Size = New System.Drawing.Size(1002, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Retur Pembelian Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 475)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(1002, 23)
        Me.pgMain.TabIndex = 6
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 453)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1002, 22)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(879, 17)
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
        Me.tcHeader.Size = New System.Drawing.Size(1002, 403)
        Me.tcHeader.TabIndex = 2
        '
        'tpMain
        '
        Me.tpMain.AutoScroll = True
        Me.tpMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpMain.Controls.Add(Me.txtArrivalNettoUsage)
        Me.tpMain.Controls.Add(Me.Label24)
        Me.tpMain.Controls.Add(Me.txtArrivalNettoAfterReceive)
        Me.tpMain.Controls.Add(Me.Label17)
        Me.tpMain.Controls.Add(Me.txtTotalPrice2)
        Me.tpMain.Controls.Add(Me.Label22)
        Me.tpMain.Controls.Add(Me.txtPrice2)
        Me.tpMain.Controls.Add(Me.Label23)
        Me.tpMain.Controls.Add(Me.Label21)
        Me.tpMain.Controls.Add(Me.txtSpecification)
        Me.tpMain.Controls.Add(Me.Label20)
        Me.tpMain.Controls.Add(Me.txtSegelNumber)
        Me.tpMain.Controls.Add(Me.Label19)
        Me.tpMain.Controls.Add(Me.txtSPBNumber)
        Me.tpMain.Controls.Add(Me.Label18)
        Me.tpMain.Controls.Add(Me.txtDONumber)
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
        Me.tpMain.Controls.Add(Me.txtTotalPrice1)
        Me.tpMain.Controls.Add(Me.lblIDStatus)
        Me.tpMain.Controls.Add(Me.Label16)
        Me.tpMain.Controls.Add(Me.dtpReceiveReturnDate)
        Me.tpMain.Controls.Add(Me.txtPrice1)
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
        Me.tpMain.Size = New System.Drawing.Size(994, 374)
        Me.tpMain.TabIndex = 0
        Me.tpMain.Text = "Main - F1"
        Me.tpMain.UseVisualStyleBackColor = True
        '
        'txtArrivalNettoUsage
        '
        Me.txtArrivalNettoUsage.BackColor = System.Drawing.Color.LightYellow
        Me.txtArrivalNettoUsage.DecimalPlaces = 2
        Me.txtArrivalNettoUsage.Enabled = False
        Me.txtArrivalNettoUsage.Location = New System.Drawing.Point(791, 275)
        Me.txtArrivalNettoUsage.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtArrivalNettoUsage.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtArrivalNettoUsage.Name = "txtArrivalNettoUsage"
        Me.txtArrivalNettoUsage.Size = New System.Drawing.Size(160, 21)
        Me.txtArrivalNettoUsage.TabIndex = 31
        Me.txtArrivalNettoUsage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtArrivalNettoUsage.ThousandsSeparator = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(685, 279)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(87, 13)
        Me.Label24.TabIndex = 141
        Me.Label24.Text = "Netto 2 Terpakai"
        '
        'txtArrivalNettoAfterReceive
        '
        Me.txtArrivalNettoAfterReceive.BackColor = System.Drawing.Color.LightYellow
        Me.txtArrivalNettoAfterReceive.DecimalPlaces = 2
        Me.txtArrivalNettoAfterReceive.Enabled = False
        Me.txtArrivalNettoAfterReceive.Location = New System.Drawing.Point(791, 248)
        Me.txtArrivalNettoAfterReceive.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtArrivalNettoAfterReceive.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtArrivalNettoAfterReceive.Name = "txtArrivalNettoAfterReceive"
        Me.txtArrivalNettoAfterReceive.Size = New System.Drawing.Size(160, 21)
        Me.txtArrivalNettoAfterReceive.TabIndex = 30
        Me.txtArrivalNettoAfterReceive.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtArrivalNettoAfterReceive.ThousandsSeparator = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(710, 252)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 139
        Me.Label17.Text = "Netto 2 Beli"
        '
        'txtTotalPrice2
        '
        Me.txtTotalPrice2.BackColor = System.Drawing.Color.LightYellow
        Me.txtTotalPrice2.DecimalPlaces = 2
        Me.txtTotalPrice2.Enabled = False
        Me.txtTotalPrice2.Location = New System.Drawing.Point(791, 194)
        Me.txtTotalPrice2.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalPrice2.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalPrice2.Name = "txtTotalPrice2"
        Me.txtTotalPrice2.ReadOnly = True
        Me.txtTotalPrice2.Size = New System.Drawing.Size(160, 21)
        Me.txtTotalPrice2.TabIndex = 28
        Me.txtTotalPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPrice2.ThousandsSeparator = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(700, 197)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(72, 13)
        Me.Label22.TabIndex = 136
        Me.Label22.Text = "Total Harga 2"
        '
        'txtPrice2
        '
        Me.txtPrice2.BackColor = System.Drawing.Color.White
        Me.txtPrice2.DecimalPlaces = 2
        Me.txtPrice2.Location = New System.Drawing.Point(791, 167)
        Me.txtPrice2.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPrice2.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPrice2.Name = "txtPrice2"
        Me.txtPrice2.Size = New System.Drawing.Size(160, 21)
        Me.txtPrice2.TabIndex = 27
        Me.txtPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPrice2.ThousandsSeparator = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(727, 170)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(45, 13)
        Me.Label23.TabIndex = 135
        Me.Label23.Text = "Harga 2"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(425, 306)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 13)
        Me.Label21.TabIndex = 132
        Me.Label21.Text = "Spesifikasi"
        '
        'txtSpecification
        '
        Me.txtSpecification.BackColor = System.Drawing.Color.White
        Me.txtSpecification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpecification.Location = New System.Drawing.Point(502, 302)
        Me.txtSpecification.MaxLength = 250
        Me.txtSpecification.Multiline = True
        Me.txtSpecification.Name = "txtSpecification"
        Me.txtSpecification.Size = New System.Drawing.Size(449, 62)
        Me.txtSpecification.TabIndex = 24
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(29, 267)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 13)
        Me.Label20.TabIndex = 130
        Me.Label20.Text = "Nomor Segel"
        '
        'txtSegelNumber
        '
        Me.txtSegelNumber.BackColor = System.Drawing.Color.White
        Me.txtSegelNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSegelNumber.Location = New System.Drawing.Point(135, 263)
        Me.txtSegelNumber.MaxLength = 250
        Me.txtSegelNumber.Name = "txtSegelNumber"
        Me.txtSegelNumber.Size = New System.Drawing.Size(220, 21)
        Me.txtSegelNumber.TabIndex = 11
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(29, 240)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(59, 13)
        Me.Label19.TabIndex = 128
        Me.Label19.Text = "Nomor SPB"
        '
        'txtSPBNumber
        '
        Me.txtSPBNumber.BackColor = System.Drawing.Color.White
        Me.txtSPBNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSPBNumber.Location = New System.Drawing.Point(135, 236)
        Me.txtSPBNumber.MaxLength = 250
        Me.txtSPBNumber.Name = "txtSPBNumber"
        Me.txtSPBNumber.Size = New System.Drawing.Size(220, 21)
        Me.txtSPBNumber.TabIndex = 10
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(29, 213)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 13)
        Me.Label18.TabIndex = 126
        Me.Label18.Text = "Nomor DO"
        '
        'txtDONumber
        '
        Me.txtDONumber.BackColor = System.Drawing.Color.White
        Me.txtDONumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDONumber.Location = New System.Drawing.Point(135, 209)
        Me.txtDONumber.MaxLength = 250
        Me.txtDONumber.Name = "txtDONumber"
        Me.txtDONumber.Size = New System.Drawing.Size(220, 21)
        Me.txtDONumber.TabIndex = 9
        '
        'txtMaxNetto
        '
        Me.txtMaxNetto.BackColor = System.Drawing.Color.LightYellow
        Me.txtMaxNetto.DecimalPlaces = 2
        Me.txtMaxNetto.Enabled = False
        Me.txtMaxNetto.Location = New System.Drawing.Point(502, 275)
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
        Me.Label9.Location = New System.Drawing.Point(425, 279)
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
        Me.txtNettoAfter.TabIndex = 22
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
        Me.txtDeduction.TabIndex = 21
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
        Me.Label10.Location = New System.Drawing.Point(29, 294)
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
        Me.txtNettoBefore.TabIndex = 20
        Me.txtNettoBefore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtNettoBefore.ThousandsSeparator = True
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(135, 290)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(220, 41)
        Me.txtRemarks.TabIndex = 12
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
        Me.txtTarra.TabIndex = 19
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
        Me.cboStatus.TabIndex = 15
        '
        'txtTotalPrice1
        '
        Me.txtTotalPrice1.BackColor = System.Drawing.Color.LightYellow
        Me.txtTotalPrice1.DecimalPlaces = 2
        Me.txtTotalPrice1.Enabled = False
        Me.txtTotalPrice1.Location = New System.Drawing.Point(791, 140)
        Me.txtTotalPrice1.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalPrice1.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalPrice1.Name = "txtTotalPrice1"
        Me.txtTotalPrice1.ReadOnly = True
        Me.txtTotalPrice1.Size = New System.Drawing.Size(160, 21)
        Me.txtTotalPrice1.TabIndex = 26
        Me.txtTotalPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPrice1.ThousandsSeparator = True
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
        Me.Label16.Location = New System.Drawing.Point(700, 143)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(72, 13)
        Me.Label16.TabIndex = 96
        Me.Label16.Text = "Total Harga 1"
        '
        'dtpReceiveReturnDate
        '
        Me.dtpReceiveReturnDate.CustomFormat = "dd/MM/yyyy HH:mm"
        Me.dtpReceiveReturnDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpReceiveReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReceiveReturnDate.Location = New System.Drawing.Point(135, 47)
        Me.dtpReceiveReturnDate.Name = "dtpReceiveReturnDate"
        Me.dtpReceiveReturnDate.Size = New System.Drawing.Size(143, 21)
        Me.dtpReceiveReturnDate.TabIndex = 1
        Me.dtpReceiveReturnDate.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'txtPrice1
        '
        Me.txtPrice1.BackColor = System.Drawing.Color.White
        Me.txtPrice1.DecimalPlaces = 2
        Me.txtPrice1.Location = New System.Drawing.Point(791, 113)
        Me.txtPrice1.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPrice1.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPrice1.Name = "txtPrice1"
        Me.txtPrice1.Size = New System.Drawing.Size(160, 21)
        Me.txtPrice1.TabIndex = 25
        Me.txtPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPrice1.ThousandsSeparator = True
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
        Me.txtBrutto.TabIndex = 18
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
        Me.cboUOMID.TabIndex = 17
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
        Me.txtItemCode.TabIndex = 14
        Me.txtItemCode.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Pemasok"
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
        Me.txtItemName.TabIndex = 16
        '
        'lblPrice1
        '
        Me.lblPrice1.AutoSize = True
        Me.lblPrice1.BackColor = System.Drawing.Color.Transparent
        Me.lblPrice1.ForeColor = System.Drawing.Color.Black
        Me.lblPrice1.Location = New System.Drawing.Point(727, 116)
        Me.lblPrice1.Name = "lblPrice1"
        Me.lblPrice1.Size = New System.Drawing.Size(45, 13)
        Me.lblPrice1.TabIndex = 93
        Me.lblPrice1.Text = "Harga 1"
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
        Me.tpHistory.Size = New System.Drawing.Size(994, 374)
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
        Me.grdStatus.Size = New System.Drawing.Size(984, 364)
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
        'frmTraReceiveReturnDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 498)
        Me.Controls.Add(Me.tcHeader)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTraReceiveReturnDet"
        Me.Text = "Retur Pembelian"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.tcHeader.ResumeLayout(False)
        Me.tpMain.ResumeLayout(False)
        Me.tpMain.PerformLayout()
        CType(Me.txtArrivalNettoUsage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtArrivalNettoAfterReceive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPrice2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrice2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaxNetto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNettoAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDeduction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNettoBefore, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTarra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalPrice1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrice1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tpMain As System.Windows.Forms.TabPage
    Friend WithEvents txtArrivalNettoUsage As MPS.usNumeric
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtArrivalNettoAfterReceive As MPS.usNumeric
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTotalPrice2 As MPS.usNumeric
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtPrice2 As MPS.usNumeric
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSpecification As MPS.usTextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtSegelNumber As MPS.usTextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtSPBNumber As MPS.usTextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDONumber As MPS.usTextBox
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
    Friend WithEvents txtTotalPrice1 As MPS.usNumeric
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents dtpReceiveReturnDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPrice1 As MPS.usNumeric
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
    Friend WithEvents tpHistory As System.Windows.Forms.TabPage
    Friend WithEvents grdStatus As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdStatusView As DevExpress.XtraGrid.Views.Grid.GridView
End Class
