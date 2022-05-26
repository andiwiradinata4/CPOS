<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraSalesServiceDet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraSalesServiceDet))
        Me.ToolBar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.BarPrint = New System.Windows.Forms.ToolBarButton()
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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPPNPercentage = New MPS.usNumeric()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBillNumber = New MPS.usTextBox()
        Me.txtSalesNo = New MPS.usTextBox()
        Me.txtCustomerCode = New MPS.usTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSPKNumber = New MPS.usTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRemarks = New MPS.usTextBox()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboPaymentTerm = New MPS.usComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboStatus = New MPS.usComboBox()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.dtpSalesDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnBP = New DevExpress.XtraEditors.SimpleButton()
        Me.txtCustomerName = New MPS.usTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtID = New MPS.usTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tpDownPayment = New System.Windows.Forms.TabPage()
        Me.grdDownPayment = New DevExpress.XtraGrid.GridControl()
        Me.grdDownPaymentView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.rpiAmount = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ToolBarDP = New MPS.usToolBar()
        Me.BarAddDP = New System.Windows.Forms.ToolBarButton()
        Me.BarDeleteDP = New System.Windows.Forms.ToolBarButton()
        Me.tpHistory = New System.Windows.Forms.TabPage()
        Me.grdStatus = New DevExpress.XtraGrid.GridControl()
        Me.grdStatusView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ToolBarDetail = New MPS.usToolBar()
        Me.BarAddItem = New System.Windows.Forms.ToolBarButton()
        Me.BarEditItem = New System.Windows.Forms.ToolBarButton()
        Me.BarDeleteItem = New System.Windows.Forms.ToolBarButton()
        Me.grdItem = New DevExpress.XtraGrid.GridControl()
        Me.grdItemView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.StatusStrip.SuspendLayout()
        Me.tcHeader.SuspendLayout()
        Me.tpMain.SuspendLayout()
        CType(Me.txtPPNPercentage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDownPayment.SuspendLayout()
        CType(Me.grdDownPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDownPaymentView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rpiAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpHistory.SuspendLayout()
        CType(Me.grdStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdStatusView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItemView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose, Me.BarPrint})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(1248, 28)
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
        'BarPrint
        '
        Me.BarPrint.Name = "BarPrint"
        Me.BarPrint.Tag = "Print"
        Me.BarPrint.Text = "Cetak Bon"
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.CadetBlue
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(0, 28)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(1248, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Penjualan Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 589)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(1248, 23)
        Me.pgMain.TabIndex = 6
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 567)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1248, 22)
        Me.StatusStrip.TabIndex = 6
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(1125, 17)
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
        Me.tcHeader.Controls.Add(Me.tpDownPayment)
        Me.tcHeader.Controls.Add(Me.tpHistory)
        Me.tcHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.tcHeader.Location = New System.Drawing.Point(0, 50)
        Me.tcHeader.Name = "tcHeader"
        Me.tcHeader.SelectedIndex = 0
        Me.tcHeader.Size = New System.Drawing.Size(1248, 216)
        Me.tcHeader.TabIndex = 2
        '
        'tpMain
        '
        Me.tpMain.AutoScroll = True
        Me.tpMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpMain.Controls.Add(Me.Label9)
        Me.tpMain.Controls.Add(Me.txtPPNPercentage)
        Me.tpMain.Controls.Add(Me.Label8)
        Me.tpMain.Controls.Add(Me.Label6)
        Me.tpMain.Controls.Add(Me.txtBillNumber)
        Me.tpMain.Controls.Add(Me.txtSalesNo)
        Me.tpMain.Controls.Add(Me.txtCustomerCode)
        Me.tpMain.Controls.Add(Me.Label12)
        Me.tpMain.Controls.Add(Me.txtSPKNumber)
        Me.tpMain.Controls.Add(Me.Label10)
        Me.tpMain.Controls.Add(Me.txtRemarks)
        Me.tpMain.Controls.Add(Me.dtpDueDate)
        Me.tpMain.Controls.Add(Me.Label5)
        Me.tpMain.Controls.Add(Me.cboPaymentTerm)
        Me.tpMain.Controls.Add(Me.Label4)
        Me.tpMain.Controls.Add(Me.cboStatus)
        Me.tpMain.Controls.Add(Me.lblIDStatus)
        Me.tpMain.Controls.Add(Me.dtpSalesDate)
        Me.tpMain.Controls.Add(Me.Label3)
        Me.tpMain.Controls.Add(Me.btnBP)
        Me.tpMain.Controls.Add(Me.txtCustomerName)
        Me.tpMain.Controls.Add(Me.Label2)
        Me.tpMain.Controls.Add(Me.txtID)
        Me.tpMain.Controls.Add(Me.Label1)
        Me.tpMain.Location = New System.Drawing.Point(4, 25)
        Me.tpMain.Name = "tpMain"
        Me.tpMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMain.Size = New System.Drawing.Size(1240, 187)
        Me.tpMain.TabIndex = 0
        Me.tpMain.Text = "Main - F1"
        Me.tpMain.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(411, 79)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(18, 13)
        Me.Label9.TabIndex = 120
        Me.Label9.Text = "%"
        '
        'txtPPNPercentage
        '
        Me.txtPPNPercentage.DecimalPlaces = 2
        Me.txtPPNPercentage.Location = New System.Drawing.Point(320, 74)
        Me.txtPPNPercentage.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPPNPercentage.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPPNPercentage.Name = "txtPPNPercentage"
        Me.txtPPNPercentage.Size = New System.Drawing.Size(85, 21)
        Me.txtPPNPercentage.TabIndex = 7
        Me.txtPPNPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPPNPercentage.ThousandsSeparator = True
        Me.txtPPNPercentage.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(279, 79)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "PPN"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(476, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 13)
        Me.Label6.TabIndex = 117
        Me.Label6.Text = "Nomor Tagihan"
        '
        'txtBillNumber
        '
        Me.txtBillNumber.BackColor = System.Drawing.Color.White
        Me.txtBillNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBillNumber.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNumber.Location = New System.Drawing.Point(574, 47)
        Me.txtBillNumber.MaxLength = 250
        Me.txtBillNumber.Name = "txtBillNumber"
        Me.txtBillNumber.Size = New System.Drawing.Size(270, 21)
        Me.txtBillNumber.TabIndex = 9
        '
        'txtSalesNo
        '
        Me.txtSalesNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSalesNo.Location = New System.Drawing.Point(135, 20)
        Me.txtSalesNo.Name = "txtSalesNo"
        Me.txtSalesNo.Size = New System.Drawing.Size(143, 21)
        Me.txtSalesNo.TabIndex = 0
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.BackColor = System.Drawing.Color.Azure
        Me.txtCustomerCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerCode.Location = New System.Drawing.Point(135, 47)
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.Size = New System.Drawing.Size(43, 21)
        Me.txtCustomerCode.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(496, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 115
        Me.Label12.Text = "Nomor SPK"
        '
        'txtSPKNumber
        '
        Me.txtSPKNumber.BackColor = System.Drawing.Color.White
        Me.txtSPKNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSPKNumber.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtSPKNumber.Location = New System.Drawing.Point(574, 20)
        Me.txtSPKNumber.MaxLength = 250
        Me.txtSPKNumber.Name = "txtSPKNumber"
        Me.txtSPKNumber.Size = New System.Drawing.Size(270, 21)
        Me.txtSPKNumber.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(492, 109)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 110
        Me.Label10.Text = "Keterangan"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(574, 105)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(270, 50)
        Me.txtRemarks.TabIndex = 11
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(135, 101)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(96, 21)
        Me.dtpDueDate.TabIndex = 5
        Me.dtpDueDate.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 13)
        Me.Label5.TabIndex = 101
        Me.Label5.Text = "Jatuh Tempo"
        '
        'cboPaymentTerm
        '
        Me.cboPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentTerm.FormattingEnabled = True
        Me.cboPaymentTerm.Location = New System.Drawing.Point(135, 128)
        Me.cboPaymentTerm.Name = "cboPaymentTerm"
        Me.cboPaymentTerm.Size = New System.Drawing.Size(96, 21)
        Me.cboPaymentTerm.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(29, 132)
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
        Me.cboStatus.Location = New System.Drawing.Point(574, 78)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 10
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(517, 82)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblIDStatus.TabIndex = 97
        Me.lblIDStatus.Text = "Status"
        '
        'dtpSalesDate
        '
        Me.dtpSalesDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpSalesDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSalesDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSalesDate.Location = New System.Drawing.Point(135, 74)
        Me.dtpSalesDate.Name = "dtpSalesDate"
        Me.dtpSalesDate.Size = New System.Drawing.Size(96, 21)
        Me.dtpSalesDate.TabIndex = 4
        Me.dtpSalesDate.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Tanggal"
        '
        'btnBP
        '
        Me.btnBP.Image = CType(resources.GetObject("btnBP.Image"), System.Drawing.Image)
        Me.btnBP.Location = New System.Drawing.Point(411, 46)
        Me.btnBP.Name = "btnBP"
        Me.btnBP.Size = New System.Drawing.Size(23, 23)
        Me.btnBP.TabIndex = 3
        Me.btnBP.TabStop = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.Color.Azure
        Me.txtCustomerName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCustomerName.Location = New System.Drawing.Point(177, 47)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(228, 21)
        Me.txtCustomerName.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Pelanggan"
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
        Me.txtID.Visible = False
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
        'tpDownPayment
        '
        Me.tpDownPayment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpDownPayment.Controls.Add(Me.grdDownPayment)
        Me.tpDownPayment.Controls.Add(Me.ToolBarDP)
        Me.tpDownPayment.Location = New System.Drawing.Point(4, 25)
        Me.tpDownPayment.Name = "tpDownPayment"
        Me.tpDownPayment.Size = New System.Drawing.Size(1240, 187)
        Me.tpDownPayment.TabIndex = 2
        Me.tpDownPayment.Text = "Panjar - F2"
        Me.tpDownPayment.UseVisualStyleBackColor = True
        '
        'grdDownPayment
        '
        Me.grdDownPayment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDownPayment.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.EndEdit.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.NextPage.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.PrevPage.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.Remove.Enabled = False
        Me.grdDownPayment.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grdDownPayment.Location = New System.Drawing.Point(0, 28)
        Me.grdDownPayment.MainView = Me.grdDownPaymentView
        Me.grdDownPayment.Name = "grdDownPayment"
        Me.grdDownPayment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpiAmount})
        Me.grdDownPayment.Size = New System.Drawing.Size(1236, 155)
        Me.grdDownPayment.TabIndex = 1
        Me.grdDownPayment.UseEmbeddedNavigator = True
        Me.grdDownPayment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdDownPaymentView})
        '
        'grdDownPaymentView
        '
        Me.grdDownPaymentView.GridControl = Me.grdDownPayment
        Me.grdDownPaymentView.Name = "grdDownPaymentView"
        Me.grdDownPaymentView.OptionsCustomization.AllowColumnMoving = False
        Me.grdDownPaymentView.OptionsCustomization.AllowGroup = False
        Me.grdDownPaymentView.OptionsView.ColumnAutoWidth = False
        Me.grdDownPaymentView.OptionsView.ShowFooter = True
        Me.grdDownPaymentView.OptionsView.ShowGroupPanel = False
        '
        'rpiAmount
        '
        Me.rpiAmount.AutoHeight = False
        Me.rpiAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.rpiAmount.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.rpiAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.rpiAmount.Name = "rpiAmount"
        Me.rpiAmount.NullText = "0"
        '
        'ToolBarDP
        '
        Me.ToolBarDP.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBarDP.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarAddDP, Me.BarDeleteDP})
        Me.ToolBarDP.DropDownArrows = True
        Me.ToolBarDP.Location = New System.Drawing.Point(0, 0)
        Me.ToolBarDP.Name = "ToolBarDP"
        Me.ToolBarDP.ShowToolTips = True
        Me.ToolBarDP.Size = New System.Drawing.Size(1236, 28)
        Me.ToolBarDP.TabIndex = 0
        Me.ToolBarDP.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        Me.ToolBarDP.Visible = False
        '
        'BarAddDP
        '
        Me.BarAddDP.Name = "BarAddDP"
        Me.BarAddDP.Tag = "Add"
        Me.BarAddDP.Text = "Tambah"
        '
        'BarDeleteDP
        '
        Me.BarDeleteDP.Name = "BarDeleteDP"
        Me.BarDeleteDP.Tag = "Delete"
        Me.BarDeleteDP.Text = "Hapus"
        '
        'tpHistory
        '
        Me.tpHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpHistory.Controls.Add(Me.grdStatus)
        Me.tpHistory.Location = New System.Drawing.Point(4, 25)
        Me.tpHistory.Name = "tpHistory"
        Me.tpHistory.Padding = New System.Windows.Forms.Padding(3)
        Me.tpHistory.Size = New System.Drawing.Size(1240, 187)
        Me.tpHistory.TabIndex = 1
        Me.tpHistory.Text = "History - F3"
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
        Me.grdStatus.Size = New System.Drawing.Size(1230, 177)
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
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.CadetBlue
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(0, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1248, 22)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "« Item"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolBarDetail
        '
        Me.ToolBarDetail.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBarDetail.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarAddItem, Me.BarEditItem, Me.BarDeleteItem})
        Me.ToolBarDetail.DropDownArrows = True
        Me.ToolBarDetail.Location = New System.Drawing.Point(0, 288)
        Me.ToolBarDetail.Name = "ToolBarDetail"
        Me.ToolBarDetail.ShowToolTips = True
        Me.ToolBarDetail.Size = New System.Drawing.Size(1248, 28)
        Me.ToolBarDetail.TabIndex = 4
        Me.ToolBarDetail.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarAddItem
        '
        Me.BarAddItem.Name = "BarAddItem"
        Me.BarAddItem.Tag = "Add"
        Me.BarAddItem.Text = "Tambah"
        '
        'BarEditItem
        '
        Me.BarEditItem.Name = "BarEditItem"
        Me.BarEditItem.Tag = "Edit"
        Me.BarEditItem.Text = "Edit"
        '
        'BarDeleteItem
        '
        Me.BarDeleteItem.Name = "BarDeleteItem"
        Me.BarDeleteItem.Tag = "Delete"
        Me.BarDeleteItem.Text = "Hapus"
        '
        'grdItem
        '
        Me.grdItem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItem.EmbeddedNavigator.Buttons.Append.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.Append.Visible = False
        Me.grdItem.EmbeddedNavigator.Buttons.CancelEdit.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.grdItem.EmbeddedNavigator.Buttons.Edit.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.grdItem.EmbeddedNavigator.Buttons.EndEdit.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.grdItem.EmbeddedNavigator.Buttons.NextPage.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.grdItem.EmbeddedNavigator.Buttons.PrevPage.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.grdItem.EmbeddedNavigator.Buttons.Remove.Enabled = False
        Me.grdItem.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.grdItem.Location = New System.Drawing.Point(0, 316)
        Me.grdItem.MainView = Me.grdItemView
        Me.grdItem.Name = "grdItem"
        Me.grdItem.Size = New System.Drawing.Size(1248, 251)
        Me.grdItem.TabIndex = 5
        Me.grdItem.UseEmbeddedNavigator = True
        Me.grdItem.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdItemView})
        '
        'grdItemView
        '
        Me.grdItemView.GridControl = Me.grdItem
        Me.grdItemView.Name = "grdItemView"
        Me.grdItemView.OptionsCustomization.AllowColumnMoving = False
        Me.grdItemView.OptionsCustomization.AllowGroup = False
        Me.grdItemView.OptionsView.ColumnAutoWidth = False
        Me.grdItemView.OptionsView.ShowAutoFilterRow = True
        Me.grdItemView.OptionsView.ShowFooter = True
        Me.grdItemView.OptionsView.ShowGroupPanel = False
        '
        'frmTraSalesServiceDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1248, 612)
        Me.Controls.Add(Me.grdItem)
        Me.Controls.Add(Me.ToolBarDetail)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.tcHeader)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmTraSalesServiceDet"
        Me.Text = "Penjualan Jasa"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.tcHeader.ResumeLayout(False)
        Me.tpMain.ResumeLayout(False)
        Me.tpMain.PerformLayout()
        CType(Me.txtPPNPercentage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDownPayment.ResumeLayout(False)
        Me.tpDownPayment.PerformLayout()
        CType(Me.grdDownPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDownPaymentView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rpiAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpHistory.ResumeLayout(False)
        CType(Me.grdStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdStatusView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItemView, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtSalesNo As MPS.usTextBox
    Friend WithEvents txtCustomerCode As MPS.usTextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSPKNumber As MPS.usTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As MPS.usTextBox
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboPaymentTerm As MPS.usComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents dtpSalesDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnBP As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtCustomerName As MPS.usTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtID As MPS.usTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tpDownPayment As System.Windows.Forms.TabPage
    Friend WithEvents grdDownPayment As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdDownPaymentView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents rpiAmount As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ToolBarDP As MPS.usToolBar
    Friend WithEvents tpHistory As System.Windows.Forms.TabPage
    Friend WithEvents grdStatus As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdStatusView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ToolBarDetail As MPS.usToolBar
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents grdItem As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdItemView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BarAddItem As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarEditItem As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarDeleteItem As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarAddDP As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarDeleteDP As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBillNumber As MPS.usTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPPNPercentage As MPS.usNumeric
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
