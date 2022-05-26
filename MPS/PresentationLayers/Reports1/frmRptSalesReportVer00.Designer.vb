<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptSalesReportVer00
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptSalesReportVer00))
        Me.ToolBar = New MPS.usToolBar()
        Me.BarPreview = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlMain = New DevExpress.XtraEditors.PanelControl()
        Me.xscMain = New MPS.usXtraScrollTabControl()
        Me.chkListCustomer = New MPS.usCheckListBoxControl()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkListSupplier = New MPS.usCheckListBoxControl()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnCompany = New DevExpress.XtraEditors.SimpleButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New MPS.usTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.pgMain = New System.Windows.Forms.ProgressBar()
        Me.cboRemarks = New MPS.usComboBox()
        Me.lblIDStatus = New System.Windows.Forms.Label()
        CType(Me.pnlMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.xscMain.SuspendLayout()
        CType(Me.chkListCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkListSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarPreview, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(488, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarPreview
        '
        Me.BarPreview.Name = "BarPreview"
        Me.BarPreview.Tag = "Print"
        Me.BarPreview.Text = "Lihat Laporan"
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
        Me.lblInfo.Size = New System.Drawing.Size(488, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Laporan Penjualan"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMain
        '
        Me.pnlMain.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar
        Me.pnlMain.AllowTouchScroll = True
        Me.pnlMain.Controls.Add(Me.xscMain)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.FireScrollEventOnMouseWheel = True
        Me.pnlMain.InvertTouchScroll = True
        Me.pnlMain.Location = New System.Drawing.Point(0, 50)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(488, 285)
        Me.pnlMain.TabIndex = 2
        '
        'xscMain
        '
        Me.xscMain.Controls.Add(Me.cboRemarks)
        Me.xscMain.Controls.Add(Me.lblIDStatus)
        Me.xscMain.Controls.Add(Me.chkListCustomer)
        Me.xscMain.Controls.Add(Me.Label6)
        Me.xscMain.Controls.Add(Me.chkListSupplier)
        Me.xscMain.Controls.Add(Me.Label5)
        Me.xscMain.Controls.Add(Me.btnCompany)
        Me.xscMain.Controls.Add(Me.Label4)
        Me.xscMain.Controls.Add(Me.txtCompanyName)
        Me.xscMain.Controls.Add(Me.Label3)
        Me.xscMain.Controls.Add(Me.dtpDateTo)
        Me.xscMain.Controls.Add(Me.Label2)
        Me.xscMain.Controls.Add(Me.dtpDateFrom)
        Me.xscMain.Cursor = System.Windows.Forms.Cursors.Default
        Me.xscMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.xscMain.Location = New System.Drawing.Point(2, 2)
        Me.xscMain.Name = "xscMain"
        Me.xscMain.Size = New System.Drawing.Size(484, 281)
        Me.xscMain.TabIndex = 0
        '
        'chkListCustomer
        '
        Me.chkListCustomer.CheckOnClick = True
        Me.chkListCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkListCustomer.Location = New System.Drawing.Point(107, 70)
        Me.chkListCustomer.Name = "chkListCustomer"
        Me.chkListCustomer.Size = New System.Drawing.Size(323, 77)
        Me.chkListCustomer.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(27, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 110
        Me.Label6.Text = "Pelanggan"
        '
        'chkListSupplier
        '
        Me.chkListSupplier.CheckOnClick = True
        Me.chkListSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkListSupplier.Location = New System.Drawing.Point(107, 153)
        Me.chkListSupplier.Name = "chkListSupplier"
        Me.chkListSupplier.Size = New System.Drawing.Size(323, 77)
        Me.chkListSupplier.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(27, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 108
        Me.Label5.Text = "Pemasok"
        '
        'btnCompany
        '
        Me.btnCompany.Image = CType(resources.GetObject("btnCompany.Image"), System.Drawing.Image)
        Me.btnCompany.Location = New System.Drawing.Point(341, 14)
        Me.btnCompany.Name = "btnCompany"
        Me.btnCompany.Size = New System.Drawing.Size(23, 23)
        Me.btnCompany.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(27, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "Perusahaan"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.BackColor = System.Drawing.Color.LightYellow
        Me.txtCompanyName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCompanyName.Location = New System.Drawing.Point(107, 16)
        Me.txtCompanyName.MaxLength = 250
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.ReadOnly = True
        Me.txtCompanyName.Size = New System.Drawing.Size(228, 21)
        Me.txtCompanyName.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(216, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "-"
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(234, 43)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(101, 21)
        Me.dtpDateTo.TabIndex = 3
        Me.dtpDateTo.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Tanggal"
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(107, 43)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(101, 21)
        Me.dtpDateFrom.TabIndex = 2
        Me.dtpDateFrom.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 335)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(488, 23)
        Me.pgMain.TabIndex = 4
        '
        'cboRemarks
        '
        Me.cboRemarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRemarks.FormattingEnabled = True
        Me.cboRemarks.Location = New System.Drawing.Point(107, 236)
        Me.cboRemarks.Name = "cboRemarks"
        Me.cboRemarks.Size = New System.Drawing.Size(323, 21)
        Me.cboRemarks.TabIndex = 6
        '
        'lblIDStatus
        '
        Me.lblIDStatus.AutoSize = True
        Me.lblIDStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblIDStatus.ForeColor = System.Drawing.Color.Black
        Me.lblIDStatus.Location = New System.Drawing.Point(27, 239)
        Me.lblIDStatus.Name = "lblIDStatus"
        Me.lblIDStatus.Size = New System.Drawing.Size(63, 13)
        Me.lblIDStatus.TabIndex = 112
        Me.lblIDStatus.Text = "Keterangan"
        '
        'frmRptSalesReportVer00
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 358)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmRptSalesReportVer00"
        Me.Text = "Laporan Penjualan"
        CType(Me.pnlMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.xscMain.ResumeLayout(False)
        Me.xscMain.PerformLayout()
        CType(Me.chkListCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkListSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarPreview As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pnlMain As DevExpress.XtraEditors.PanelControl
    Friend WithEvents xscMain As MPS.usXtraScrollTabControl
    Friend WithEvents chkListCustomer As MPS.usCheckListBoxControl
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkListSupplier As MPS.usCheckListBoxControl
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnCompany As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As MPS.usTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pgMain As System.Windows.Forms.ProgressBar
    Friend WithEvents cboRemarks As MPS.usComboBox
    Friend WithEvents lblIDStatus As System.Windows.Forms.Label
End Class
