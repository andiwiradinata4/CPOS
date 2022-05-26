<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstBusinessPartnerPriceDet
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lblPurchasePrice1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSalesPrice = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPurchasePrice2 = New MPS.usNumeric()
        Me.txtPurchasePrice1 = New MPS.usNumeric()
        Me.txtSalesPrice = New MPS.usNumeric()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarSave = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.pnlMain.SuspendLayout()
        CType(Me.txtPurchasePrice2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPurchasePrice1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.lblInfo.Size = New System.Drawing.Size(374, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Harga"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlMain
        '
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMain.Controls.Add(Me.txtPurchasePrice2)
        Me.pnlMain.Controls.Add(Me.lblPurchasePrice1)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.lblSalesPrice)
        Me.pnlMain.Controls.Add(Me.txtPurchasePrice1)
        Me.pnlMain.Controls.Add(Me.txtSalesPrice)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.dtpDateTo)
        Me.pnlMain.Controls.Add(Me.dtpDateFrom)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 50)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(374, 162)
        Me.pnlMain.TabIndex = 2
        '
        'lblPurchasePrice1
        '
        Me.lblPurchasePrice1.AutoSize = True
        Me.lblPurchasePrice1.BackColor = System.Drawing.Color.Transparent
        Me.lblPurchasePrice1.ForeColor = System.Drawing.Color.Black
        Me.lblPurchasePrice1.Location = New System.Drawing.Point(29, 70)
        Me.lblPurchasePrice1.Name = "lblPurchasePrice1"
        Me.lblPurchasePrice1.Size = New System.Drawing.Size(64, 13)
        Me.lblPurchasePrice1.TabIndex = 105
        Me.lblPurchasePrice1.Text = "Harga Beli 1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(29, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "Harga Beli 2"
        '
        'lblSalesPrice
        '
        Me.lblSalesPrice.AutoSize = True
        Me.lblSalesPrice.BackColor = System.Drawing.Color.Transparent
        Me.lblSalesPrice.ForeColor = System.Drawing.Color.Black
        Me.lblSalesPrice.Location = New System.Drawing.Point(29, 43)
        Me.lblSalesPrice.Name = "lblSalesPrice"
        Me.lblSalesPrice.Size = New System.Drawing.Size(58, 13)
        Me.lblSalesPrice.TabIndex = 106
        Me.lblSalesPrice.Text = "Harga Jual"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(210, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "-"
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(227, 13)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(101, 21)
        Me.dtpDateTo.TabIndex = 1
        Me.dtpDateTo.Value = New Date(3000, 1, 1, 0, 0, 0, 0)
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpDateFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(103, 13)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(101, 21)
        Me.dtpDateFrom.TabIndex = 0
        Me.dtpDateFrom.Value = New Date(2019, 5, 1, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Periode"
        '
        'txtPurchasePrice2
        '
        Me.txtPurchasePrice2.Location = New System.Drawing.Point(103, 94)
        Me.txtPurchasePrice2.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPurchasePrice2.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPurchasePrice2.Name = "txtPurchasePrice2"
        Me.txtPurchasePrice2.Size = New System.Drawing.Size(225, 21)
        Me.txtPurchasePrice2.TabIndex = 4
        Me.txtPurchasePrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasePrice2.ThousandsSeparator = True
        '
        'txtPurchasePrice1
        '
        Me.txtPurchasePrice1.Location = New System.Drawing.Point(103, 67)
        Me.txtPurchasePrice1.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPurchasePrice1.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPurchasePrice1.Name = "txtPurchasePrice1"
        Me.txtPurchasePrice1.Size = New System.Drawing.Size(225, 21)
        Me.txtPurchasePrice1.TabIndex = 3
        Me.txtPurchasePrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPurchasePrice1.ThousandsSeparator = True
        '
        'txtSalesPrice
        '
        Me.txtSalesPrice.Location = New System.Drawing.Point(103, 40)
        Me.txtSalesPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtSalesPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtSalesPrice.Name = "txtSalesPrice"
        Me.txtSalesPrice.Size = New System.Drawing.Size(225, 21)
        Me.txtSalesPrice.TabIndex = 2
        Me.txtSalesPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSalesPrice.ThousandsSeparator = True
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarSave, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(374, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarSave
        '
        Me.BarSave.Name = "BarSave"
        Me.BarSave.Tag = "Save"
        Me.BarSave.Text = "Simpan"
        '
        'BarClose
        '
        Me.BarClose.Name = "BarClose"
        Me.BarClose.Tag = "Close"
        Me.BarClose.Text = "Tutup"
        '
        'frmMstBusinessPartnerPriceDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(374, 212)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstBusinessPartnerPriceDet"
        Me.Text = "Harga"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        CType(Me.txtPurchasePrice2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPurchasePrice1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSalesPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPurchasePrice2 As MPS.usNumeric
    Friend WithEvents lblPurchasePrice1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSalesPrice As System.Windows.Forms.Label
    Friend WithEvents txtPurchasePrice1 As MPS.usNumeric
    Friend WithEvents txtSalesPrice As MPS.usNumeric
End Class
