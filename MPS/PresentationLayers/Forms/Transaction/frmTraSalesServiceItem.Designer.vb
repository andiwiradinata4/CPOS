<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraSalesServiceItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraSalesServiceItem))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRemarks = New MPS.usTextBox()
        Me.btnItem = New DevExpress.XtraEditors.SimpleButton()
        Me.txtTotalPrice = New MPS.usNumeric()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPrice = New MPS.usNumeric()
        Me.txtQty = New MPS.usNumeric()
        Me.cboUOMID = New MPS.usComboBox()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtItemCode = New MPS.usTextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtItemName = New MPS.usTextBox()
        Me.lblUomID1 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.lblSalesPrice = New System.Windows.Forms.Label()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolBar = New MPS.usToolBar()
        CType(Me.txtTotalPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(31, 218)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 98
        Me.Label3.Text = "Keterangan"
        '
        'txtRemarks
        '
        Me.txtRemarks.BackColor = System.Drawing.Color.White
        Me.txtRemarks.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRemarks.Location = New System.Drawing.Point(105, 215)
        Me.txtRemarks.MaxLength = 250
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(300, 60)
        Me.txtRemarks.TabIndex = 7
        '
        'btnItem
        '
        Me.btnItem.Image = CType(resources.GetObject("btnItem.Image"), System.Drawing.Image)
        Me.btnItem.Location = New System.Drawing.Point(270, 13)
        Me.btnItem.Name = "btnItem"
        Me.btnItem.Size = New System.Drawing.Size(23, 23)
        Me.btnItem.TabIndex = 1
        '
        'txtTotalPrice
        '
        Me.txtTotalPrice.BackColor = System.Drawing.Color.LightYellow
        Me.txtTotalPrice.DecimalPlaces = 2
        Me.txtTotalPrice.Enabled = False
        Me.txtTotalPrice.Location = New System.Drawing.Point(105, 188)
        Me.txtTotalPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalPrice.Name = "txtTotalPrice"
        Me.txtTotalPrice.ReadOnly = True
        Me.txtTotalPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtTotalPrice.TabIndex = 6
        Me.txtTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalPrice.ThousandsSeparator = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(31, 191)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 96
        Me.Label2.Text = "Total Harga"
        '
        'txtPrice
        '
        Me.txtPrice.DecimalPlaces = 2
        Me.txtPrice.Location = New System.Drawing.Point(105, 134)
        Me.txtPrice.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPrice.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(160, 21)
        Me.txtPrice.TabIndex = 4
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPrice.ThousandsSeparator = True
        '
        'txtQty
        '
        Me.txtQty.DecimalPlaces = 2
        Me.txtQty.Location = New System.Drawing.Point(105, 161)
        Me.txtQty.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtQty.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(160, 21)
        Me.txtQty.TabIndex = 5
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtQty.ThousandsSeparator = True
        '
        'cboUOMID
        '
        Me.cboUOMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUOMID.Enabled = False
        Me.cboUOMID.FormattingEnabled = True
        Me.cboUOMID.Location = New System.Drawing.Point(105, 107)
        Me.cboUOMID.Name = "cboUOMID"
        Me.cboUOMID.Size = New System.Drawing.Size(160, 21)
        Me.cboUOMID.TabIndex = 3
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(31, 17)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(31, 13)
        Me.lblCode.TabIndex = 93
        Me.lblCode.Text = "Kode"
        '
        'txtItemCode
        '
        Me.txtItemCode.BackColor = System.Drawing.Color.Azure
        Me.txtItemCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtItemCode.Location = New System.Drawing.Point(105, 14)
        Me.txtItemCode.MaxLength = 250
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(160, 21)
        Me.txtItemCode.TabIndex = 0
        Me.txtItemCode.TabStop = False
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.Color.Transparent
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(31, 44)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(34, 13)
        Me.lblName.TabIndex = 93
        Me.lblName.Text = "Nama"
        '
        'txtItemName
        '
        Me.txtItemName.BackColor = System.Drawing.Color.Azure
        Me.txtItemName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtItemName.Location = New System.Drawing.Point(105, 41)
        Me.txtItemName.MaxLength = 250
        Me.txtItemName.Multiline = True
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.ReadOnly = True
        Me.txtItemName.Size = New System.Drawing.Size(300, 60)
        Me.txtItemName.TabIndex = 2
        '
        'lblUomID1
        '
        Me.lblUomID1.AutoSize = True
        Me.lblUomID1.BackColor = System.Drawing.Color.Transparent
        Me.lblUomID1.ForeColor = System.Drawing.Color.Black
        Me.lblUomID1.Location = New System.Drawing.Point(31, 111)
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
        Me.lblQty.Location = New System.Drawing.Point(31, 165)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(40, 13)
        Me.lblQty.TabIndex = 93
        Me.lblQty.Text = "Jumlah"
        '
        'lblSalesPrice
        '
        Me.lblSalesPrice.AutoSize = True
        Me.lblSalesPrice.BackColor = System.Drawing.Color.Transparent
        Me.lblSalesPrice.ForeColor = System.Drawing.Color.Black
        Me.lblSalesPrice.Location = New System.Drawing.Point(31, 137)
        Me.lblSalesPrice.Name = "lblSalesPrice"
        Me.lblSalesPrice.Size = New System.Drawing.Size(36, 13)
        Me.lblSalesPrice.TabIndex = 93
        Me.lblSalesPrice.Text = "Harga"
        '
        'BarClose
        '
        Me.BarClose.Name = "BarClose"
        Me.BarClose.Tag = "Close"
        Me.BarClose.Text = "Tutup"
        '
        'BarRefresh
        '
        Me.BarRefresh.Name = "BarRefresh"
        Me.BarRefresh.Tag = "Save"
        Me.BarRefresh.Text = "Simpan"
        '
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.Label3)
        Me.pnlDetail.Controls.Add(Me.txtRemarks)
        Me.pnlDetail.Controls.Add(Me.btnItem)
        Me.pnlDetail.Controls.Add(Me.txtTotalPrice)
        Me.pnlDetail.Controls.Add(Me.Label2)
        Me.pnlDetail.Controls.Add(Me.txtPrice)
        Me.pnlDetail.Controls.Add(Me.txtQty)
        Me.pnlDetail.Controls.Add(Me.cboUOMID)
        Me.pnlDetail.Controls.Add(Me.lblCode)
        Me.pnlDetail.Controls.Add(Me.txtItemCode)
        Me.pnlDetail.Controls.Add(Me.lblName)
        Me.pnlDetail.Controls.Add(Me.txtItemName)
        Me.pnlDetail.Controls.Add(Me.lblUomID1)
        Me.pnlDetail.Controls.Add(Me.lblQty)
        Me.pnlDetail.Controls.Add(Me.lblSalesPrice)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(465, 317)
        Me.pnlDetail.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.CadetBlue
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(0, 28)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(465, 22)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "« Item"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'frmTraSalesServiceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 367)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.Name = "frmTraSalesServiceItem"
        Me.Text = "Item Penjualan Jasa"
        CType(Me.txtTotalPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As MPS.usTextBox
    Friend WithEvents btnItem As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtTotalPrice As MPS.usNumeric
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As MPS.usNumeric
    Friend WithEvents txtQty As MPS.usNumeric
    Friend WithEvents cboUOMID As MPS.usComboBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As MPS.usTextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtItemName As MPS.usTextBox
    Friend WithEvents lblUomID1 As System.Windows.Forms.Label
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents lblSalesPrice As System.Windows.Forms.Label
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolBar As MPS.usToolBar
End Class
