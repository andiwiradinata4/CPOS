<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstItemDet
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
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.txtSalesPrice = New MPS.usNumeric()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTolerance = New MPS.usNumeric()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBalanceQty = New MPS.usNumeric()
        Me.cboUOMID = New MPS.usComboBox()
        Me.cboStatus = New MPS.usComboBox()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtCode = New MPS.usTextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtName = New MPS.usTextBox()
        Me.lblUomID1 = New System.Windows.Forms.Label()
        Me.lblBalanceQty = New System.Windows.Forms.Label()
        Me.Toolbar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.StatusStrip.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBalanceQty, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblInfo.Size = New System.Drawing.Size(622, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Barang Detail"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip
        '
        Me.StatusStrip.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripEmpty, Me.ToolStripLogInc, Me.ToolStripLogBy, Me.ToolStripStatusLabel1, Me.ToolStripLogDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 234)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(622, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripEmpty
        '
        Me.ToolStripEmpty.Name = "ToolStripEmpty"
        Me.ToolStripEmpty.Size = New System.Drawing.Size(499, 17)
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
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.txtSalesPrice)
        Me.pnlDetail.Controls.Add(Me.Label1)
        Me.pnlDetail.Controls.Add(Me.Label4)
        Me.pnlDetail.Controls.Add(Me.txtTolerance)
        Me.pnlDetail.Controls.Add(Me.Label3)
        Me.pnlDetail.Controls.Add(Me.txtBalanceQty)
        Me.pnlDetail.Controls.Add(Me.cboUOMID)
        Me.pnlDetail.Controls.Add(Me.cboStatus)
        Me.pnlDetail.Controls.Add(Me.lblIDStatus)
        Me.pnlDetail.Controls.Add(Me.lblCode)
        Me.pnlDetail.Controls.Add(Me.txtCode)
        Me.pnlDetail.Controls.Add(Me.lblName)
        Me.pnlDetail.Controls.Add(Me.txtName)
        Me.pnlDetail.Controls.Add(Me.lblUomID1)
        Me.pnlDetail.Controls.Add(Me.lblBalanceQty)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(622, 184)
        Me.pnlDetail.TabIndex = 2
        '
        'txtSalesPrice
        '
        Me.txtSalesPrice.DecimalPlaces = 2
        Me.txtSalesPrice.Location = New System.Drawing.Point(423, 14)
        Me.txtSalesPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtSalesPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtSalesPrice.Name = "txtSalesPrice"
        Me.txtSalesPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtSalesPrice.TabIndex = 4
        Me.txtSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSalesPrice.ThousandsSeparator = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(338, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "Harga Jual"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(587, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(18, 13)
        Me.Label4.TabIndex = 102
        Me.Label4.Text = "%"
        '
        'txtTolerance
        '
        Me.txtTolerance.DecimalPlaces = 2
        Me.txtTolerance.Location = New System.Drawing.Point(422, 107)
        Me.txtTolerance.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTolerance.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTolerance.Name = "txtTolerance"
        Me.txtTolerance.Size = New System.Drawing.Size(161, 21)
        Me.txtTolerance.TabIndex = 5
        Me.txtTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTolerance.ThousandsSeparator = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(337, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 101
        Me.Label3.Text = "Toleransi"
        '
        'txtBalanceQty
        '
        Me.txtBalanceQty.DecimalPlaces = 2
        Me.txtBalanceQty.Enabled = False
        Me.txtBalanceQty.Location = New System.Drawing.Point(422, 134)
        Me.txtBalanceQty.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtBalanceQty.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtBalanceQty.Name = "txtBalanceQty"
        Me.txtBalanceQty.Size = New System.Drawing.Size(160, 21)
        Me.txtBalanceQty.TabIndex = 6
        Me.txtBalanceQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtBalanceQty.ThousandsSeparator = True
        '
        'cboUOMID
        '
        Me.cboUOMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUOMID.FormattingEnabled = True
        Me.cboUOMID.Location = New System.Drawing.Point(116, 107)
        Me.cboUOMID.Name = "cboUOMID"
        Me.cboUOMID.Size = New System.Drawing.Size(160, 21)
        Me.cboUOMID.TabIndex = 2
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Enabled = False
        Me.cboStatus.FormattingEnabled = True
        Me.cboStatus.Location = New System.Drawing.Point(116, 134)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(160, 21)
        Me.cboStatus.TabIndex = 3
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(31, 138)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblIDStatus.TabIndex = 95
        Me.lblIDStatus.Text = "Status"
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(31, 18)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(31, 13)
        Me.lblCode.TabIndex = 93
        Me.lblCode.Text = "Kode"
        '
        'txtCode
        '
        Me.txtCode.BackColor = System.Drawing.Color.LightYellow
        Me.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCode.Location = New System.Drawing.Point(116, 14)
        Me.txtCode.MaxLength = 250
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ReadOnly = True
        Me.txtCode.Size = New System.Drawing.Size(160, 21)
        Me.txtCode.TabIndex = 0
        Me.txtCode.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(31, 45)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(34, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtName.Location = New System.Drawing.Point(116, 41)
        Me.txtName.MaxLength = 250
        Me.txtName.Multiline = True
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(466, 60)
        Me.txtName.TabIndex = 1
        '
        'lblUomID1
        '
        Me.lblUomID1.AutoSize = True
        Me.lblUomID1.BackColor = System.Drawing.Color.Transparent
        Me.lblUomID1.ForeColor = System.Drawing.Color.Black
        Me.lblUomID1.Location = New System.Drawing.Point(31, 112)
        Me.lblUomID1.Name = "lblUomID1"
        Me.lblUomID1.Size = New System.Drawing.Size(41, 13)
        Me.lblUomID1.TabIndex = 93
        Me.lblUomID1.Text = "Satuan"
        '
        'lblBalanceQty
        '
        Me.lblBalanceQty.AutoSize = True
        Me.lblBalanceQty.BackColor = System.Drawing.Color.Transparent
        Me.lblBalanceQty.ForeColor = System.Drawing.Color.Black
        Me.lblBalanceQty.Location = New System.Drawing.Point(337, 138)
        Me.lblBalanceQty.Name = "lblBalanceQty"
        Me.lblBalanceQty.Size = New System.Drawing.Size(54, 13)
        Me.lblBalanceQty.TabIndex = 93
        Me.lblBalanceQty.Text = "Saldo Qty"
        '
        'Toolbar
        '
        Me.Toolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.Toolbar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.Toolbar.DropDownArrows = True
        Me.Toolbar.Location = New System.Drawing.Point(0, 0)
        Me.Toolbar.Name = "Toolbar"
        Me.Toolbar.ShowToolTips = True
        Me.Toolbar.Size = New System.Drawing.Size(622, 28)
        Me.Toolbar.TabIndex = 0
        Me.Toolbar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
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
        'frmMstItemDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 256)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.Toolbar)
        Me.Controls.Add(Me.StatusStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstItemDet"
        Me.Text = "Barang"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTolerance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBalanceQty, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtCode As usTextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As usTextBox
    Friend WithEvents lblUomID1 As System.Windows.Forms.Label
    Friend WithEvents lblBalanceQty As System.Windows.Forms.Label
    Friend WithEvents cboStatus As MPS.usComboBox
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
    Friend WithEvents txtBalanceQty As MPS.usNumeric
    Friend WithEvents cboUOMID As MPS.usComboBox
    Friend WithEvents Toolbar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents txtTolerance As MPS.usNumeric
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSalesPrice As MPS.usNumeric
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class


