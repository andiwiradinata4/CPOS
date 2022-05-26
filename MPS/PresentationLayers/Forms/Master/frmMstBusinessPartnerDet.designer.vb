<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstBusinessPartnerDet
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
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripEmpty = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogInc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogBy = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripLogDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPurchasePrice1 = New System.Windows.Forms.Label()
        Me.lblSalesPrice = New System.Windows.Forms.Label()
        Me.lblPICPhoneNumber = New System.Windows.Forms.Label()
        Me.lblPICName = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.tcBP = New System.Windows.Forms.TabControl()
        Me.tpMain = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCode = New MPS.usTextBox()
        Me.txtName = New MPS.usTextBox()
        Me.txtPurchasePrice2 = New MPS.usNumeric()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMaxPurchaseLimit = New MPS.usNumeric()
        Me.txtPICPhoneNumber = New MPS.usTextBox()
        Me.chkIsUsePurchaseLimit = New DevExpress.XtraEditors.CheckEdit()
        Me.cboPaymentTerm = New MPS.usComboBox()
        Me.txtPICName = New MPS.usTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPurchasePrice1 = New MPS.usNumeric()
        Me.txtAddress = New MPS.usTextBox()
        Me.txtSalesPrice = New MPS.usNumeric()
        Me.cboStatus = New MPS.usComboBox()
        Me.tpStatus = New System.Windows.Forms.TabPage()
        Me.grdStatus = New DevExpress.XtraGrid.GridControl()
        Me.grdStatusView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.StatusStrip.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        Me.tcBP.SuspendLayout()
        Me.tpMain.SuspendLayout()
        CType(Me.txtPurchasePrice2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaxPurchaseLimit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkIsUsePurchaseLimit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchasePrice1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpStatus.SuspendLayout()
        CType(Me.grdStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdStatusView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.CadetBlue
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(0, 28)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(465, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Rekan Bisnis Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 340)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(465, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(342, 17)
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
        'lblPurchasePrice1
        '
        Me.lblPurchasePrice1.AutoSize = True
        Me.lblPurchasePrice1.BackColor = System.Drawing.Color.Transparent
        Me.lblPurchasePrice1.ForeColor = System.Drawing.Color.Black
        Me.lblPurchasePrice1.Location = New System.Drawing.Point(472, 104)
        Me.lblPurchasePrice1.Name = "lblPurchasePrice1"
        Me.lblPurchasePrice1.Size = New System.Drawing.Size(64, 13)
        Me.lblPurchasePrice1.TabIndex = 93
        Me.lblPurchasePrice1.Text = "Harga Beli 1"
        Me.lblPurchasePrice1.Visible = False
        '
        'lblSalesPrice
        '
        Me.lblSalesPrice.AutoSize = True
        Me.lblSalesPrice.BackColor = System.Drawing.Color.Transparent
        Me.lblSalesPrice.ForeColor = System.Drawing.Color.Black
        Me.lblSalesPrice.Location = New System.Drawing.Point(472, 76)
        Me.lblSalesPrice.Name = "lblSalesPrice"
        Me.lblSalesPrice.Size = New System.Drawing.Size(58, 13)
        Me.lblSalesPrice.TabIndex = 93
        Me.lblSalesPrice.Text = "Harga Jual"
        Me.lblSalesPrice.Visible = False
        '
        'lblPICPhoneNumber
        '
        Me.lblPICPhoneNumber.AutoSize = True
        Me.lblPICPhoneNumber.BackColor = System.Drawing.Color.Transparent
        Me.lblPICPhoneNumber.ForeColor = System.Drawing.Color.Black
        Me.lblPICPhoneNumber.Location = New System.Drawing.Point(26, 158)
        Me.lblPICPhoneNumber.Name = "lblPICPhoneNumber"
        Me.lblPICPhoneNumber.Size = New System.Drawing.Size(60, 13)
        Me.lblPICPhoneNumber.TabIndex = 93
        Me.lblPICPhoneNumber.Text = "No. HP PIC"
        '
        'lblPICName
        '
        Me.lblPICName.AutoSize = True
        Me.lblPICName.BackColor = System.Drawing.Color.Transparent
        Me.lblPICName.ForeColor = System.Drawing.Color.Black
        Me.lblPICName.Location = New System.Drawing.Point(26, 131)
        Me.lblPICName.Name = "lblPICName"
        Me.lblPICName.Size = New System.Drawing.Size(54, 13)
        Me.lblPICName.TabIndex = 93
        Me.lblPICName.Text = "Nama PIC"
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.BackColor = System.Drawing.Color.Transparent
        Me.lblAddress.ForeColor = System.Drawing.Color.Black
        Me.lblAddress.Location = New System.Drawing.Point(26, 77)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(40, 13)
        Me.lblAddress.TabIndex = 93
        Me.lblAddress.Text = "Alamat"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(26, 50)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(34, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama"
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(26, 186)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblIDStatus.TabIndex = 95
        Me.lblIDStatus.Text = "Status"
        '
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.tcBP)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(465, 290)
        Me.pnlDetail.TabIndex = 2
        '
        'tcBP
        '
        Me.tcBP.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcBP.Controls.Add(Me.tpMain)
        Me.tcBP.Controls.Add(Me.tpStatus)
        Me.tcBP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcBP.Location = New System.Drawing.Point(0, 0)
        Me.tcBP.Name = "tcBP"
        Me.tcBP.SelectedIndex = 0
        Me.tcBP.Size = New System.Drawing.Size(461, 286)
        Me.tcBP.TabIndex = 102
        '
        'tpMain
        '
        Me.tpMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpMain.Controls.Add(Me.Label3)
        Me.tpMain.Controls.Add(Me.txtCode)
        Me.tpMain.Controls.Add(Me.txtName)
        Me.tpMain.Controls.Add(Me.txtPurchasePrice2)
        Me.tpMain.Controls.Add(Me.lblPurchasePrice1)
        Me.tpMain.Controls.Add(Me.Label2)
        Me.tpMain.Controls.Add(Me.lblSalesPrice)
        Me.tpMain.Controls.Add(Me.txtMaxPurchaseLimit)
        Me.tpMain.Controls.Add(Me.txtPICPhoneNumber)
        Me.tpMain.Controls.Add(Me.chkIsUsePurchaseLimit)
        Me.tpMain.Controls.Add(Me.lblPICPhoneNumber)
        Me.tpMain.Controls.Add(Me.cboPaymentTerm)
        Me.tpMain.Controls.Add(Me.txtPICName)
        Me.tpMain.Controls.Add(Me.Label1)
        Me.tpMain.Controls.Add(Me.lblPICName)
        Me.tpMain.Controls.Add(Me.txtPurchasePrice1)
        Me.tpMain.Controls.Add(Me.txtAddress)
        Me.tpMain.Controls.Add(Me.txtSalesPrice)
        Me.tpMain.Controls.Add(Me.lblAddress)
        Me.tpMain.Controls.Add(Me.cboStatus)
        Me.tpMain.Controls.Add(Me.lblName)
        Me.tpMain.Controls.Add(Me.lblIDStatus)
        Me.tpMain.Location = New System.Drawing.Point(4, 25)
        Me.tpMain.Name = "tpMain"
        Me.tpMain.Size = New System.Drawing.Size(453, 257)
        Me.tpMain.TabIndex = 0
        Me.tpMain.Text = "Main - F1"
        Me.tpMain.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(26, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 13)
        Me.Label3.TabIndex = 103
        Me.Label3.Text = "Kode"
        '
        'txtCode
        '
        Me.txtCode.BackColor = System.Drawing.Color.LightYellow
        Me.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.Location = New System.Drawing.Point(118, 20)
        Me.txtCode.MaxLength = 250
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(101, 21)
        Me.txtCode.TabIndex = 102
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtName.Location = New System.Drawing.Point(118, 47)
        Me.txtName.MaxLength = 250
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(300, 21)
        Me.txtName.TabIndex = 0
        '
        'txtPurchasePrice2
        '
        Me.txtPurchasePrice2.Enabled = False
        Me.txtPurchasePrice2.Location = New System.Drawing.Point(612, 128)
        Me.txtPurchasePrice2.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPurchasePrice2.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPurchasePrice2.Name = "txtPurchasePrice2"
        Me.txtPurchasePrice2.Size = New System.Drawing.Size(160, 21)
        Me.txtPurchasePrice2.TabIndex = 9
        Me.txtPurchasePrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasePrice2.ThousandsSeparator = True
        Me.txtPurchasePrice2.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(472, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = "Harga Beli 2"
        Me.Label2.Visible = False
        '
        'txtMaxPurchaseLimit
        '
        Me.txtMaxPurchaseLimit.Enabled = False
        Me.txtMaxPurchaseLimit.Location = New System.Drawing.Point(612, 47)
        Me.txtMaxPurchaseLimit.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtMaxPurchaseLimit.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtMaxPurchaseLimit.Name = "txtMaxPurchaseLimit"
        Me.txtMaxPurchaseLimit.Size = New System.Drawing.Size(160, 21)
        Me.txtMaxPurchaseLimit.TabIndex = 6
        Me.txtMaxPurchaseLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaxPurchaseLimit.ThousandsSeparator = True
        Me.txtMaxPurchaseLimit.Visible = False
        '
        'txtPICPhoneNumber
        '
        Me.txtPICPhoneNumber.BackColor = System.Drawing.Color.White
        Me.txtPICPhoneNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPICPhoneNumber.Location = New System.Drawing.Point(118, 155)
        Me.txtPICPhoneNumber.MaxLength = 250
        Me.txtPICPhoneNumber.Name = "txtPICPhoneNumber"
        Me.txtPICPhoneNumber.Size = New System.Drawing.Size(300, 21)
        Me.txtPICPhoneNumber.TabIndex = 3
        '
        'chkIsUsePurchaseLimit
        '
        Me.chkIsUsePurchaseLimit.Location = New System.Drawing.Point(472, 48)
        Me.chkIsUsePurchaseLimit.Name = "chkIsUsePurchaseLimit"
        Me.chkIsUsePurchaseLimit.Properties.Caption = "Maks. Limit Pembelian"
        Me.chkIsUsePurchaseLimit.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.[Default]
        Me.chkIsUsePurchaseLimit.Size = New System.Drawing.Size(127, 19)
        Me.chkIsUsePurchaseLimit.TabIndex = 99
        Me.chkIsUsePurchaseLimit.Visible = False
        '
        'cboPaymentTerm
        '
        Me.cboPaymentTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentTerm.FormattingEnabled = True
        Me.cboPaymentTerm.Location = New System.Drawing.Point(612, 20)
        Me.cboPaymentTerm.Name = "cboPaymentTerm"
        Me.cboPaymentTerm.Size = New System.Drawing.Size(160, 21)
        Me.cboPaymentTerm.TabIndex = 5
        Me.cboPaymentTerm.Visible = False
        '
        'txtPICName
        '
        Me.txtPICName.BackColor = System.Drawing.Color.White
        Me.txtPICName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPICName.Location = New System.Drawing.Point(118, 128)
        Me.txtPICName.MaxLength = 250
        Me.txtPICName.Name = "txtPICName"
        Me.txtPICName.Size = New System.Drawing.Size(300, 21)
        Me.txtPICName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(472, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Jenis Pembayaran"
        Me.Label1.Visible = False
        '
        'txtPurchasePrice1
        '
        Me.txtPurchasePrice1.Enabled = False
        Me.txtPurchasePrice1.Location = New System.Drawing.Point(612, 101)
        Me.txtPurchasePrice1.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPurchasePrice1.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPurchasePrice1.Name = "txtPurchasePrice1"
        Me.txtPurchasePrice1.Size = New System.Drawing.Size(160, 21)
        Me.txtPurchasePrice1.TabIndex = 8
        Me.txtPurchasePrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasePrice1.ThousandsSeparator = True
        Me.txtPurchasePrice1.Visible = False
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.White
        Me.txtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAddress.Location = New System.Drawing.Point(118, 74)
        Me.txtAddress.MaxLength = 250
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(300, 48)
        Me.txtAddress.TabIndex = 1
        '
        'txtSalesPrice
        '
        Me.txtSalesPrice.Enabled = False
        Me.txtSalesPrice.Location = New System.Drawing.Point(612, 74)
        Me.txtSalesPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtSalesPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtSalesPrice.Name = "txtSalesPrice"
        Me.txtSalesPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtSalesPrice.TabIndex = 7
        Me.txtSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSalesPrice.ThousandsSeparator = True
        Me.txtSalesPrice.Visible = False
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(118, 182)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 4
        '
        'tpStatus
        '
        Me.tpStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.tpStatus.Controls.Add(Me.grdStatus)
        Me.tpStatus.Location = New System.Drawing.Point(4, 25)
        Me.tpStatus.Name = "tpStatus"
        Me.tpStatus.Size = New System.Drawing.Size(453, 257)
        Me.tpStatus.TabIndex = 1
        Me.tpStatus.Text = "Status - F2"
        Me.tpStatus.UseVisualStyleBackColor = True
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
        Me.grdStatus.Location = New System.Drawing.Point(0, 0)
        Me.grdStatus.MainView = Me.grdStatusView
        Me.grdStatus.Name = "grdStatus"
        Me.grdStatus.Size = New System.Drawing.Size(449, 253)
        Me.grdStatus.TabIndex = 13
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
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(465, 28)
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
        'frmMstBusinessPartnerDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 362)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstBusinessPartnerDet"
        Me.Text = "Rekan Bisnis"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.tcBP.ResumeLayout(False)
        Me.tpMain.ResumeLayout(False)
        Me.tpMain.PerformLayout()
        CType(Me.txtPurchasePrice2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaxPurchaseLimit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkIsUsePurchaseLimit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchasePrice1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpStatus.ResumeLayout(False)
        CType(Me.grdStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdStatusView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripEmpty As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogInc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogBy As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLogDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblPurchasePrice1 As System.Windows.Forms.Label
    Friend WithEvents lblSalesPrice As System.Windows.Forms.Label
    Friend WithEvents txtPICPhoneNumber As MPS.usTextBox
    Friend WithEvents lblPICPhoneNumber As System.Windows.Forms.Label
    Friend WithEvents txtPICName As MPS.usTextBox
    Friend WithEvents lblPICName As System.Windows.Forms.Label
    Friend WithEvents txtAddress As MPS.usTextBox
    Friend WithEvents lblAddress As System.Windows.Forms.Label
    Friend WithEvents txtName As MPS.usTextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents txtPurchasePrice1 As MPS.usNumeric
    Friend WithEvents txtSalesPrice As MPS.usNumeric
    Friend WithEvents cboPaymentTerm As MPS.usComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMaxPurchaseLimit As MPS.usNumeric
    Friend WithEvents chkIsUsePurchaseLimit As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents txtPurchasePrice2 As MPS.usNumeric
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tcBP As System.Windows.Forms.TabControl
    Friend WithEvents tpMain As System.Windows.Forms.TabPage
    Friend WithEvents tpStatus As System.Windows.Forms.TabPage
    Friend WithEvents grdStatus As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdStatusView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCode As MPS.usTextBox
End Class


