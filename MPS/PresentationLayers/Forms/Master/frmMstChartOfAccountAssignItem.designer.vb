<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstChartOfAccountAssignItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMstChartOfAccountAssignItem))
        Me.ToolBar = New MPS.usToolBar()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.btnCompany = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New MPS.usTextBox()
        Me.btnProgram = New DevExpress.XtraEditors.SimpleButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtProgramName = New MPS.usTextBox()
        Me.txtFirstBalance = New MPS.usNumeric()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtpFirstBalanceDate = New System.Windows.Forms.DateTimePicker()
        Me.BarSave = New System.Windows.Forms.ToolBarButton()
        Me.pnlDetail.SuspendLayout()
        CType(Me.txtFirstBalance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarSave, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(509, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
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
        Me.lblInfo.Size = New System.Drawing.Size(509, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Item"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.btnCompany)
        Me.pnlDetail.Controls.Add(Me.Label2)
        Me.pnlDetail.Controls.Add(Me.txtCompanyName)
        Me.pnlDetail.Controls.Add(Me.btnProgram)
        Me.pnlDetail.Controls.Add(Me.Label1)
        Me.pnlDetail.Controls.Add(Me.txtProgramName)
        Me.pnlDetail.Controls.Add(Me.txtFirstBalance)
        Me.pnlDetail.Controls.Add(Me.lblAmount)
        Me.pnlDetail.Controls.Add(Me.lblStartDate)
        Me.pnlDetail.Controls.Add(Me.dtpFirstBalanceDate)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(509, 122)
        Me.pnlDetail.TabIndex = 2
        '
        'btnCompany
        '
        Me.btnCompany.Image = CType(resources.GetObject("btnCompany.Image"), System.Drawing.Image)
        Me.btnCompany.Location = New System.Drawing.Point(463, 45)
        Me.btnCompany.Name = "btnCompany"
        Me.btnCompany.Size = New System.Drawing.Size(23, 23)
        Me.btnCompany.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(20, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Perusahaan"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.BackColor = System.Drawing.Color.LightYellow
        Me.txtCompanyName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCompanyName.Location = New System.Drawing.Point(96, 47)
        Me.txtCompanyName.MaxLength = 250
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.ReadOnly = True
        Me.txtCompanyName.Size = New System.Drawing.Size(362, 21)
        Me.txtCompanyName.TabIndex = 2
        '
        'btnProgram
        '
        Me.btnProgram.Image = CType(resources.GetObject("btnProgram.Image"), System.Drawing.Image)
        Me.btnProgram.Location = New System.Drawing.Point(463, 18)
        Me.btnProgram.Name = "btnProgram"
        Me.btnProgram.Size = New System.Drawing.Size(23, 23)
        Me.btnProgram.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(20, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 101
        Me.Label1.Text = "Program"
        '
        'txtProgramName
        '
        Me.txtProgramName.BackColor = System.Drawing.Color.LightYellow
        Me.txtProgramName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtProgramName.Location = New System.Drawing.Point(96, 20)
        Me.txtProgramName.MaxLength = 250
        Me.txtProgramName.Name = "txtProgramName"
        Me.txtProgramName.ReadOnly = True
        Me.txtProgramName.Size = New System.Drawing.Size(362, 21)
        Me.txtProgramName.TabIndex = 0
        '
        'txtFirstBalance
        '
        Me.txtFirstBalance.Location = New System.Drawing.Point(96, 74)
        Me.txtFirstBalance.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtFirstBalance.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtFirstBalance.Name = "txtFirstBalance"
        Me.txtFirstBalance.Size = New System.Drawing.Size(160, 21)
        Me.txtFirstBalance.TabIndex = 4
        Me.txtFirstBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFirstBalance.ThousandsSeparator = True
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblAmount.ForeColor = System.Drawing.Color.Black
        Me.lblAmount.Location = New System.Drawing.Point(20, 77)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(59, 13)
        Me.lblAmount.TabIndex = 98
        Me.lblAmount.Text = "Saldo Awal"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.BackColor = System.Drawing.Color.Transparent
        Me.lblStartDate.ForeColor = System.Drawing.Color.Black
        Me.lblStartDate.Location = New System.Drawing.Point(294, 77)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(59, 13)
        Me.lblStartDate.TabIndex = 99
        Me.lblStartDate.Text = "Pertanggal"
        '
        'dtpFirstBalanceDate
        '
        Me.dtpFirstBalanceDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFirstBalanceDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFirstBalanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFirstBalanceDate.Location = New System.Drawing.Point(357, 74)
        Me.dtpFirstBalanceDate.Name = "dtpFirstBalanceDate"
        Me.dtpFirstBalanceDate.Size = New System.Drawing.Size(101, 21)
        Me.dtpFirstBalanceDate.TabIndex = 5
        Me.dtpFirstBalanceDate.Value = New Date(2000, 1, 1, 0, 0, 0, 0)
        '
        'BarSave
        '
        Me.BarSave.Name = "BarSave"
        Me.BarSave.Tag = "Save"
        Me.BarSave.Text = "Simpan"
        '
        'frmMstChartOfAccountAssignItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 172)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstChartOfAccountAssignItem"
        Me.Text = "Assign"
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.txtFirstBalance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProgramName As MPS.usTextBox
    Friend WithEvents txtFirstBalance As MPS.usNumeric
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents dtpFirstBalanceDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnProgram As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCompany As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As MPS.usTextBox
    Friend WithEvents BarSave As System.Windows.Forms.ToolBarButton
End Class
