<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptFakturBiayaVer00
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptFakturBiayaVer00))
        Me.gboJenisFaktur = New System.Windows.Forms.GroupBox()
        Me.rdFakturPenerimaan = New System.Windows.Forms.RadioButton()
        Me.rdFakturPembayaran = New System.Windows.Forms.RadioButton()
        Me.btnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.gboJenisFaktur.SuspendLayout()
        Me.SuspendLayout()
        '
        'gboJenisFaktur
        '
        Me.gboJenisFaktur.Controls.Add(Me.rdFakturPembayaran)
        Me.gboJenisFaktur.Controls.Add(Me.rdFakturPenerimaan)
        Me.gboJenisFaktur.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.gboJenisFaktur.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.gboJenisFaktur.Location = New System.Drawing.Point(31, 20)
        Me.gboJenisFaktur.Name = "gboJenisFaktur"
        Me.gboJenisFaktur.Size = New System.Drawing.Size(348, 69)
        Me.gboJenisFaktur.TabIndex = 0
        Me.gboJenisFaktur.TabStop = False
        Me.gboJenisFaktur.Text = "Jenis Faktur"
        '
        'rdFakturPenerimaan
        '
        Me.rdFakturPenerimaan.AutoSize = True
        Me.rdFakturPenerimaan.Checked = True
        Me.rdFakturPenerimaan.Location = New System.Drawing.Point(23, 31)
        Me.rdFakturPenerimaan.Name = "rdFakturPenerimaan"
        Me.rdFakturPenerimaan.Size = New System.Drawing.Size(133, 17)
        Me.rdFakturPenerimaan.TabIndex = 0
        Me.rdFakturPenerimaan.TabStop = True
        Me.rdFakturPenerimaan.Text = "Faktur Penerimaan"
        Me.rdFakturPenerimaan.UseVisualStyleBackColor = True
        '
        'rdFakturPembayaran
        '
        Me.rdFakturPembayaran.AutoSize = True
        Me.rdFakturPembayaran.Location = New System.Drawing.Point(180, 31)
        Me.rdFakturPembayaran.Name = "rdFakturPembayaran"
        Me.rdFakturPembayaran.Size = New System.Drawing.Size(137, 17)
        Me.rdFakturPembayaran.TabIndex = 1
        Me.rdFakturPembayaran.Text = "Faktur Pembayaran"
        Me.rdFakturPembayaran.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrint.Appearance.Options.UseFont = True
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnPrint.Location = New System.Drawing.Point(31, 104)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(162, 51)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "  Cetak"
        '
        'btnCancel
        '
        Me.btnCancel.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancel.Appearance.Options.UseFont = True
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnCancel.Location = New System.Drawing.Point(216, 104)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(163, 51)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "  Batal"
        '
        'frmRptFakturBiayaVer00
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 185)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.gboJenisFaktur)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptFakturBiayaVer00"
        Me.Text = "Print Faktur"
        Me.gboJenisFaktur.ResumeLayout(False)
        Me.gboJenisFaktur.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gboJenisFaktur As System.Windows.Forms.GroupBox
    Friend WithEvents rdFakturPenerimaan As System.Windows.Forms.RadioButton
    Friend WithEvents rdFakturPembayaran As System.Windows.Forms.RadioButton
    Friend WithEvents btnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
End Class
