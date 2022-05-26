<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTraCalculatorPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTraCalculatorPayment))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnPay = New DevExpress.XtraEditors.SimpleButton()
        Me.txtRefund = New MPS.usNumeric()
        Me.txtPay = New MPS.usNumeric()
        Me.txtTotalAmount = New MPS.usNumeric()
        CType(Me.txtRefund, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTotalAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(205, 40)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Total Belanja"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 40)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Bayar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 202)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(167, 40)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Kembalian"
        '
        'btnClose
        '
        Me.btnClose.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnClose.Appearance.Font = New System.Drawing.Font("Tahoma", 17.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.Appearance.Options.UseBackColor = True
        Me.btnClose.Appearance.Options.UseFont = True
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(0, 337)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(818, 56)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Tutup"
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
        Me.btnPay.Location = New System.Drawing.Point(0, 278)
        Me.btnPay.Name = "btnPay"
        Me.btnPay.Size = New System.Drawing.Size(818, 59)
        Me.btnPay.TabIndex = 3
        Me.btnPay.Text = "Bayar"
        '
        'txtRefund
        '
        Me.txtRefund.DecimalPlaces = 2
        Me.txtRefund.Enabled = False
        Me.txtRefund.Location = New System.Drawing.Point(248, 200)
        Me.txtRefund.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtRefund.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtRefund.Name = "txtRefund"
        Me.txtRefund.Size = New System.Drawing.Size(542, 47)
        Me.txtRefund.TabIndex = 2
        Me.txtRefund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRefund.ThousandsSeparator = True
        '
        'txtPay
        '
        Me.txtPay.DecimalPlaces = 2
        Me.txtPay.Location = New System.Drawing.Point(248, 116)
        Me.txtPay.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtPay.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtPay.Name = "txtPay"
        Me.txtPay.Size = New System.Drawing.Size(542, 47)
        Me.txtPay.TabIndex = 1
        Me.txtPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtPay.ThousandsSeparator = True
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.DecimalPlaces = 2
        Me.txtTotalAmount.Enabled = False
        Me.txtTotalAmount.Location = New System.Drawing.Point(248, 37)
        Me.txtTotalAmount.Maximum = New Decimal(New Integer() {-1, -1, -1, 0})
        Me.txtTotalAmount.Minimum = New Decimal(New Integer() {-1, -1, -1, -2147483648})
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.Size = New System.Drawing.Size(542, 47)
        Me.txtTotalAmount.TabIndex = 0
        Me.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtTotalAmount.ThousandsSeparator = True
        '
        'frmCalculatorPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(18.0!, 40.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 393)
        Me.Controls.Add(Me.btnPay)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRefund)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPay)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTotalAmount)
        Me.Font = New System.Drawing.Font("Tahoma", 24.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(9)
        Me.Name = "frmCalculatorPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPayment"
        CType(Me.txtRefund, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTotalAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTotalAmount As MPS.usNumeric
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPay As MPS.usNumeric
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRefund As MPS.usNumeric
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnPay As DevExpress.XtraEditors.SimpleButton
End Class
