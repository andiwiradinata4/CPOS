<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSysLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSysLogin))
        Me.pictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.pnlLogin = New DevExpress.XtraEditors.PanelControl()
        Me.btnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnLogin = New DevExpress.XtraEditors.SimpleButton()
        Me.labelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPassword = New MPS.usTextBox()
        Me.txtUserID = New MPS.usTextBox()
        Me.tmrAutoLogin = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlLogin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLogin.SuspendLayout()
        Me.SuspendLayout()
        '
        'pictureEdit1
        '
        Me.pictureEdit1.Cursor = System.Windows.Forms.Cursors.Default
        Me.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Top
        Me.pictureEdit1.EditValue = CType(resources.GetObject("pictureEdit1.EditValue"), Object)
        Me.pictureEdit1.Location = New System.Drawing.Point(0, 0)
        Me.pictureEdit1.Name = "pictureEdit1"
        Me.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.pictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.pictureEdit1.Properties.ShowMenu = False
        Me.pictureEdit1.Properties.ZoomAccelerationFactor = 1.0R
        Me.pictureEdit1.Size = New System.Drawing.Size(800, 296)
        Me.pictureEdit1.TabIndex = 1
        '
        'simpleButton1
        '
        Me.simpleButton1.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.simpleButton1.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.simpleButton1.Appearance.Options.UseBackColor = True
        Me.simpleButton1.Appearance.Options.UseFont = True
        Me.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.simpleButton1.Dock = System.Windows.Forms.DockStyle.Top
        Me.simpleButton1.Image = CType(resources.GetObject("simpleButton1.Image"), System.Drawing.Image)
        Me.simpleButton1.ImageLocation = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.simpleButton1.Location = New System.Drawing.Point(0, 296)
        Me.simpleButton1.Name = "simpleButton1"
        Me.simpleButton1.Size = New System.Drawing.Size(800, 71)
        Me.simpleButton1.TabIndex = 2
        Me.simpleButton1.TabStop = False
        Me.simpleButton1.Text = "LOGIN"
        '
        'pnlLogin
        '
        Me.pnlLogin.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlLogin.Controls.Add(Me.txtPassword)
        Me.pnlLogin.Controls.Add(Me.txtUserID)
        Me.pnlLogin.Controls.Add(Me.btnExit)
        Me.pnlLogin.Controls.Add(Me.labelControl1)
        Me.pnlLogin.Controls.Add(Me.btnLogin)
        Me.pnlLogin.Controls.Add(Me.labelControl2)
        Me.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLogin.Location = New System.Drawing.Point(0, 367)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Size = New System.Drawing.Size(800, 178)
        Me.pnlLogin.TabIndex = 3
        '
        'btnExit
        '
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.Location = New System.Drawing.Point(414, 123)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(111, 39)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Exit"
        '
        'labelControl1
        '
        Me.labelControl1.Location = New System.Drawing.Point(275, 16)
        Me.labelControl1.Name = "labelControl1"
        Me.labelControl1.Size = New System.Drawing.Size(36, 13)
        Me.labelControl1.TabIndex = 4
        Me.labelControl1.Text = "User ID"
        '
        'btnLogin
        '
        Me.btnLogin.Image = CType(resources.GetObject("btnLogin.Image"), System.Drawing.Image)
        Me.btnLogin.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.btnLogin.Location = New System.Drawing.Point(275, 123)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(111, 39)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "Login"
        '
        'labelControl2
        '
        Me.labelControl2.Location = New System.Drawing.Point(275, 70)
        Me.labelControl2.Name = "labelControl2"
        Me.labelControl2.Size = New System.Drawing.Size(46, 13)
        Me.labelControl2.TabIndex = 6
        Me.labelControl2.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPassword.Location = New System.Drawing.Point(275, 89)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(250, 21)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'txtUserID
        '
        Me.txtUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUserID.Location = New System.Drawing.Point(275, 35)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(250, 21)
        Me.txtUserID.TabIndex = 0
        '
        'tmrAutoLogin
        '
        '
        'frmSysLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 545)
        Me.Controls.Add(Me.pnlLogin)
        Me.Controls.Add(Me.simpleButton1)
        Me.Controls.Add(Me.pictureEdit1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSysLogin"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.TopMost = True
        CType(Me.pictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlLogin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLogin.ResumeLayout(False)
        Me.pnlLogin.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pictureEdit1 As DevExpress.XtraEditors.PictureEdit
    Private WithEvents simpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents pnlLogin As DevExpress.XtraEditors.PanelControl
    Private WithEvents btnExit As DevExpress.XtraEditors.SimpleButton
    Private WithEvents labelControl1 As DevExpress.XtraEditors.LabelControl
    Private WithEvents btnLogin As DevExpress.XtraEditors.SimpleButton
    Private WithEvents labelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPassword As MPS.usTextBox
    Friend WithEvents txtUserID As MPS.usTextBox
    Friend WithEvents tmrAutoLogin As System.Windows.Forms.Timer

End Class
