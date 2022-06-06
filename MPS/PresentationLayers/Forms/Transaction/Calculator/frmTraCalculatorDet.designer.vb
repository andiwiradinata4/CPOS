<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraCalculatorDet
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraCalculatorDet))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtTotalAmount = New MPS.usNumeric()
        Me.pnlCustomer = New System.Windows.Forms.Panel()
        Me.txtBPAddress = New MPS.usTextBox()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnBP = New DevExpress.XtraEditors.SimpleButton()
        Me.txtBPName = New MPS.usTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grdMain = New DevExpress.XtraGrid.GridControl()
        Me.grdView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnChangePassword = New DevExpress.XtraEditors.SimpleButton()
        Me.btnClear = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSales = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPay = New DevExpress.XtraEditors.SimpleButton()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtValueCombine = New MPS.usNumeric()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.cboSymbol = New MPS.usComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.txtValue = New MPS.usNumeric()
        Me.tmrNow = New System.Windows.Forms.Timer(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.txtTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCustomer.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.txtValueCombine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1277, 594)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtTotalAmount, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.pnlCustomer, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(824, 588)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.DecimalPlaces = 2
        Me.txtTotalAmount.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtTotalAmount.Enabled = False
        Me.txtTotalAmount.Location = New System.Drawing.Point(3, 536)
        Me.txtTotalAmount.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalAmount.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(818, 48)
        Me.txtTotalAmount.TabIndex = 0
        Me.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalAmount.ThousandsSeparator = True
        '
        'pnlCustomer
        '
        Me.pnlCustomer.Controls.Add(Me.txtBPAddress)
        Me.pnlCustomer.Controls.Add(Me.dtpDate)
        Me.pnlCustomer.Controls.Add(Me.Label2)
        Me.pnlCustomer.Controls.Add(Me.btnBP)
        Me.pnlCustomer.Controls.Add(Me.txtBPName)
        Me.pnlCustomer.Controls.Add(Me.Label1)
        Me.pnlCustomer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCustomer.Location = New System.Drawing.Point(3, 3)
        Me.pnlCustomer.Name = "pnlCustomer"
        Me.pnlCustomer.Size = New System.Drawing.Size(818, 204)
        Me.pnlCustomer.TabIndex = 5
        '
        'txtBPAddress
        '
        Me.txtBPAddress.BackColor = System.Drawing.Color.Azure
        Me.txtBPAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBPAddress.Location = New System.Drawing.Point(208, 134)
        Me.txtBPAddress.Name = "txtBPAddress"
        Me.txtBPAddress.Size = New System.Drawing.Size(589, 48)
        Me.txtBPAddress.TabIndex = 3
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "ddMMMMyyyy | HH:mm:ss"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(208, 21)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.dtpDate.Size = New System.Drawing.Size(485, 48)
        Me.dtpDate.TabIndex = 0
        Me.dtpDate.Value = New Date(2022, 12, 31, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(139, 41)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Tanggal"
        '
        'btnBP
        '
        Me.btnBP.Appearance.Font = New System.Drawing.Font("Tahoma", 17.0!, System.Drawing.FontStyle.Bold)
        Me.btnBP.Appearance.Options.UseFont = True
        Me.btnBP.Image = CType(resources.GetObject("btnBP.Image"), System.Drawing.Image)
        Me.btnBP.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnBP.Location = New System.Drawing.Point(710, 80)
        Me.btnBP.Name = "btnBP"
        Me.btnBP.Size = New System.Drawing.Size(87, 48)
        Me.btnBP.TabIndex = 2
        Me.btnBP.Text = "F1"
        '
        'txtBPName
        '
        Me.txtBPName.BackColor = System.Drawing.Color.Azure
        Me.txtBPName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBPName.Location = New System.Drawing.Point(208, 80)
        Me.txtBPName.Name = "txtBPName"
        Me.txtBPName.Size = New System.Drawing.Size(485, 48)
        Me.txtBPName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(175, 41)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Pelanggan"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdMain)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 213)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(818, 317)
        Me.Panel1.TabIndex = 6
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
        Me.grdMain.Location = New System.Drawing.Point(0, 0)
        Me.grdMain.MainView = Me.grdView
        Me.grdMain.Name = "grdMain"
        Me.grdMain.Size = New System.Drawing.Size(818, 317)
        Me.grdMain.TabIndex = 0
        Me.grdMain.UseEmbeddedNavigator = True
        Me.grdMain.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grdView, Me.GridView1})
        '
        'grdView
        '
        Me.grdView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.grdView.Appearance.HeaderPanel.Options.UseFont = True
        Me.grdView.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 20.0!)
        Me.grdView.Appearance.Row.Options.UseFont = True
        Me.grdView.GridControl = Me.grdMain
        Me.grdView.Name = "grdView"
        Me.grdView.OptionsCustomization.AllowColumnMoving = False
        Me.grdView.OptionsCustomization.AllowGroup = False
        Me.grdView.OptionsView.ColumnAutoWidth = False
        Me.grdView.OptionsView.ShowAutoFilterRow = True
        Me.grdView.OptionsView.ShowGroupPanel = False
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.grdMain
        Me.GridView1.Name = "GridView1"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.btnChangePassword)
        Me.Panel2.Controls.Add(Me.btnClear)
        Me.Panel2.Controls.Add(Me.btnSales)
        Me.Panel2.Controls.Add(Me.btnPay)
        Me.Panel2.Controls.Add(Me.TableLayoutPanel4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(833, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(441, 588)
        Me.Panel2.TabIndex = 1
        '
        'btnChangePassword
        '
        Me.btnChangePassword.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnChangePassword.Appearance.Font = New System.Drawing.Font("Tahoma", 17.0!, System.Drawing.FontStyle.Bold)
        Me.btnChangePassword.Appearance.Options.UseBackColor = True
        Me.btnChangePassword.Appearance.Options.UseFont = True
        Me.btnChangePassword.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnChangePassword.Image = CType(resources.GetObject("btnChangePassword.Image"), System.Drawing.Image)
        Me.btnChangePassword.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnChangePassword.Location = New System.Drawing.Point(0, 351)
        Me.btnChangePassword.Name = "btnChangePassword"
        Me.btnChangePassword.Size = New System.Drawing.Size(439, 58)
        Me.btnChangePassword.TabIndex = 0
        Me.btnChangePassword.Text = "F8 - Ganti Password"
        Me.btnChangePassword.Visible = False
        '
        'btnClear
        '
        Me.btnClear.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnClear.Appearance.Font = New System.Drawing.Font("Tahoma", 17.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.Appearance.Options.UseBackColor = True
        Me.btnClear.Appearance.Options.UseFont = True
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnClear.Location = New System.Drawing.Point(0, 409)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(439, 58)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "F9 - Hapus Data"
        '
        'btnSales
        '
        Me.btnSales.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnSales.Appearance.Font = New System.Drawing.Font("Tahoma", 17.0!, System.Drawing.FontStyle.Bold)
        Me.btnSales.Appearance.Options.UseBackColor = True
        Me.btnSales.Appearance.Options.UseFont = True
        Me.btnSales.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnSales.Image = CType(resources.GetObject("btnSales.Image"), System.Drawing.Image)
        Me.btnSales.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnSales.Location = New System.Drawing.Point(0, 467)
        Me.btnSales.Name = "btnSales"
        Me.btnSales.Size = New System.Drawing.Size(439, 60)
        Me.btnSales.TabIndex = 3
        Me.btnSales.Text = "F10 - Data Penjualan"
        '
        'btnPay
        '
        Me.btnPay.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnPay.Appearance.Font = New System.Drawing.Font("Tahoma", 17.0!, System.Drawing.FontStyle.Bold)
        Me.btnPay.Appearance.Options.UseBackColor = True
        Me.btnPay.Appearance.Options.UseFont = True
        Me.btnPay.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnPay.Image = CType(resources.GetObject("btnPay.Image"), System.Drawing.Image)
        Me.btnPay.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnPay.Location = New System.Drawing.Point(0, 527)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.Size = New System.Drawing.Size(439, 59)
        Me.btnPay.TabIndex = 2
        Me.btnPay.Text = "F12 - Proses Bayar"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.Panel3, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Panel4, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Panel5, 0, 2)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(439, 166)
        Me.TableLayoutPanel4.TabIndex = 8
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.txtValueCombine)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(433, 49)
        Me.Panel3.TabIndex = 0
        '
        'txtValueCombine
        '
        Me.txtValueCombine.DecimalPlaces = 2
        Me.txtValueCombine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValueCombine.Location = New System.Drawing.Point(0, 0)
        Me.txtValueCombine.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtValueCombine.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtValueCombine.Name = "txtValueCombine"
        Me.txtValueCombine.Size = New System.Drawing.Size(433, 48)
        Me.txtValueCombine.TabIndex = 0
        Me.txtValueCombine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtValueCombine.ThousandsSeparator = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.cboSymbol)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 58)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(433, 49)
        Me.Panel4.TabIndex = 1
        '
        'cboSymbol
        '
        Me.cboSymbol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSymbol.FormattingEnabled = True
        Me.cboSymbol.Items.AddRange(New Object() {"", "x"})
        Me.cboSymbol.Location = New System.Drawing.Point(0, 0)
        Me.cboSymbol.Name = "cboSymbol"
        Me.cboSymbol.Size = New System.Drawing.Size(433, 48)
        Me.cboSymbol.TabIndex = 0
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.txtValue)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 113)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(433, 50)
        Me.Panel5.TabIndex = 2
        '
        'txtValue
        '
        Me.txtValue.DecimalPlaces = 2
        Me.txtValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtValue.Location = New System.Drawing.Point(0, 0)
        Me.txtValue.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtValue.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(433, 48)
        Me.txtValue.TabIndex = 0
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtValue.ThousandsSeparator = True
        '
        'tmrNow
        '
        '
        'frmTraCalculatorDet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(18.0!, 40.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 594)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Tahoma", 25.0!)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(9)
        Me.Name = "frmTraCalculatorDet"
        Me.Text = "Calculator POS"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.txtTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCustomer.ResumeLayout(False)
        Me.pnlCustomer.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.txtValueCombine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.txtValue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents pnlCustomer As System.Windows.Forms.Panel
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnBP As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtBPName As MPS.usTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grdMain As DevExpress.XtraGrid.GridControl
    Friend WithEvents grdView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtTotalAmount As MPS.usNumeric
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtValueCombine As MPS.usNumeric
    Friend WithEvents cboSymbol As MPS.usComboBox
    Friend WithEvents txtValue As MPS.usNumeric
    Friend WithEvents btnClear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPay As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSales As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tmrNow As System.Windows.Forms.Timer
    Friend WithEvents txtBPAddress As MPS.usTextBox
    Friend WithEvents btnChangePassword As DevExpress.XtraEditors.SimpleButton
End Class
