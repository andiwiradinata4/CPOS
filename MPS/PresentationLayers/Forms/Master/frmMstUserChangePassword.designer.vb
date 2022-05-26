<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstUserChangePassword
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
        Me.ToolBar = New MPS.usToolBar()
        Me.BarRefresh = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.chkViewNewPassword = New DevExpress.XtraEditors.CheckEdit()
        Me.chkViewConfirmPassword = New DevExpress.XtraEditors.CheckEdit()
        Me.chkViewOldPassword = New DevExpress.XtraEditors.CheckEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNewPassword = New MPS.usTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtConfirmPassword = New MPS.usTextBox()
        Me.lblID = New System.Windows.Forms.Label()
        Me.txtID = New MPS.usTextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtOldPassword = New MPS.usTextBox()
        Me.pnlDetail.SuspendLayout()
        CType(Me.chkViewNewPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkViewConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkViewOldPassword.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarRefresh, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(483, 28)
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
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.CadetBlue
        Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblInfo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(0, 28)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(483, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Ubah Password"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDetail
        '
        Me.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlDetail.Controls.Add(Me.chkViewNewPassword)
        Me.pnlDetail.Controls.Add(Me.chkViewConfirmPassword)
        Me.pnlDetail.Controls.Add(Me.chkViewOldPassword)
        Me.pnlDetail.Controls.Add(Me.Label2)
        Me.pnlDetail.Controls.Add(Me.txtNewPassword)
        Me.pnlDetail.Controls.Add(Me.Label1)
        Me.pnlDetail.Controls.Add(Me.txtConfirmPassword)
        Me.pnlDetail.Controls.Add(Me.lblID)
        Me.pnlDetail.Controls.Add(Me.txtID)
        Me.pnlDetail.Controls.Add(Me.lblPassword)
        Me.pnlDetail.Controls.Add(Me.txtOldPassword)
        Me.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetail.Location = New System.Drawing.Point(0, 50)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(483, 147)
        Me.pnlDetail.TabIndex = 2
        '
        'chkViewNewPassword
        '
        Me.chkViewNewPassword.Location = New System.Drawing.Point(370, 67)
        Me.chkViewNewPassword.Name = "chkViewNewPassword"
        Me.chkViewNewPassword.Properties.Caption = "Lihat"
        Me.chkViewNewPassword.Size = New System.Drawing.Size(75, 19)
        Me.chkViewNewPassword.TabIndex = 4
        '
        'chkViewConfirmPassword
        '
        Me.chkViewConfirmPassword.Location = New System.Drawing.Point(370, 93)
        Me.chkViewConfirmPassword.Name = "chkViewConfirmPassword"
        Me.chkViewConfirmPassword.Properties.Caption = "Lihat"
        Me.chkViewConfirmPassword.Size = New System.Drawing.Size(75, 19)
        Me.chkViewConfirmPassword.TabIndex = 6
        '
        'chkViewOldPassword
        '
        Me.chkViewOldPassword.Location = New System.Drawing.Point(370, 41)
        Me.chkViewOldPassword.Name = "chkViewOldPassword"
        Me.chkViewOldPassword.Properties.Caption = "Lihat"
        Me.chkViewOldPassword.Size = New System.Drawing.Size(75, 19)
        Me.chkViewOldPassword.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(28, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = "Password Baru"
        '
        'txtNewPassword
        '
        Me.txtNewPassword.BackColor = System.Drawing.Color.White
        Me.txtNewPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNewPassword.Location = New System.Drawing.Point(148, 67)
        Me.txtNewPassword.MaxLength = 250
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.Size = New System.Drawing.Size(216, 21)
        Me.txtNewPassword.TabIndex = 3
        Me.txtNewPassword.UseSystemPasswordChar = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(28, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "Konfirmasi Password"
        '
        'txtConfirmPassword
        '
        Me.txtConfirmPassword.BackColor = System.Drawing.Color.White
        Me.txtConfirmPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtConfirmPassword.Location = New System.Drawing.Point(148, 93)
        Me.txtConfirmPassword.MaxLength = 250
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.Size = New System.Drawing.Size(216, 21)
        Me.txtConfirmPassword.TabIndex = 5
        Me.txtConfirmPassword.UseSystemPasswordChar = True
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.BackColor = System.Drawing.Color.Transparent
        Me.lblID.ForeColor = System.Drawing.Color.Black
        Me.lblID.Location = New System.Drawing.Point(28, 18)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(43, 13)
        Me.lblID.TabIndex = 93
        Me.lblID.Text = "User ID"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightYellow
        Me.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtID.Location = New System.Drawing.Point(148, 15)
        Me.txtID.MaxLength = 250
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(160, 21)
        Me.txtID.TabIndex = 0
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.BackColor = System.Drawing.Color.Transparent
        Me.lblPassword.ForeColor = System.Drawing.Color.Black
        Me.lblPassword.Location = New System.Drawing.Point(28, 44)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(81, 13)
        Me.lblPassword.TabIndex = 93
        Me.lblPassword.Text = "Password Lama"
        '
        'txtOldPassword
        '
        Me.txtOldPassword.BackColor = System.Drawing.Color.White
        Me.txtOldPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtOldPassword.Location = New System.Drawing.Point(148, 41)
        Me.txtOldPassword.MaxLength = 250
        Me.txtOldPassword.Name = "txtOldPassword"
        Me.txtOldPassword.Size = New System.Drawing.Size(216, 21)
        Me.txtOldPassword.TabIndex = 1
        Me.txtOldPassword.UseSystemPasswordChar = True
        '
        'frmMstUserChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 197)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstUserChangePassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ubah Password"
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        CType(Me.chkViewNewPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkViewConfirmPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkViewOldPassword.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents chkViewOldPassword As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNewPassword As MPS.usTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtConfirmPassword As MPS.usTextBox
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtID As MPS.usTextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtOldPassword As MPS.usTextBox
    Friend WithEvents chkViewNewPassword As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents chkViewConfirmPassword As DevExpress.XtraEditors.CheckEdit
End Class
