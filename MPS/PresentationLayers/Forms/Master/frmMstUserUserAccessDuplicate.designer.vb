<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMstUserUserAccessDuplicate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMstUserUserAccessDuplicate))
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.btnBaseUserID = New DevExpress.XtraEditors.SimpleButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pgMain = New System.Windows.Forms.ProgressBar()
        Me.txtNewUserID = New MPS.usTextBox()
        Me.txtBaseUserID = New MPS.usTextBox()
        Me.ToolBar = New MPS.usToolBar()
        Me.BarSave = New System.Windows.Forms.ToolBarButton()
        Me.BarClose = New System.Windows.Forms.ToolBarButton()
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
        Me.lblInfo.Size = New System.Drawing.Size(417, 22)
        Me.lblInfo.TabIndex = 1
        Me.lblInfo.Text = "« Salin User Akses"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnBaseUserID
        '
        Me.btnBaseUserID.Image = CType(resources.GetObject("btnBaseUserID.Image"), System.Drawing.Image)
        Me.btnBaseUserID.Location = New System.Drawing.Point(348, 67)
        Me.btnBaseUserID.Name = "btnBaseUserID"
        Me.btnBaseUserID.Size = New System.Drawing.Size(23, 23)
        Me.btnBaseUserID.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Dari User ID"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Untuk User ID"
        '
        'pgMain
        '
        Me.pgMain.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pgMain.Location = New System.Drawing.Point(0, 158)
        Me.pgMain.Name = "pgMain"
        Me.pgMain.Size = New System.Drawing.Size(417, 23)
        Me.pgMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pgMain.TabIndex = 9
        Me.pgMain.UseWaitCursor = True
        '
        'txtNewUserID
        '
        Me.txtNewUserID.BackColor = System.Drawing.Color.Azure
        Me.txtNewUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNewUserID.Location = New System.Drawing.Point(123, 106)
        Me.txtNewUserID.Name = "txtNewUserID"
        Me.txtNewUserID.ReadOnly = True
        Me.txtNewUserID.Size = New System.Drawing.Size(220, 21)
        Me.txtNewUserID.TabIndex = 4
        '
        'txtBaseUserID
        '
        Me.txtBaseUserID.BackColor = System.Drawing.Color.Azure
        Me.txtBaseUserID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBaseUserID.Location = New System.Drawing.Point(123, 68)
        Me.txtBaseUserID.Name = "txtBaseUserID"
        Me.txtBaseUserID.ReadOnly = True
        Me.txtBaseUserID.Size = New System.Drawing.Size(220, 21)
        Me.txtBaseUserID.TabIndex = 2
        '
        'ToolBar
        '
        Me.ToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.BarSave, Me.BarClose})
        Me.ToolBar.DropDownArrows = True
        Me.ToolBar.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar.Name = "ToolBar"
        Me.ToolBar.ShowToolTips = True
        Me.ToolBar.Size = New System.Drawing.Size(417, 28)
        Me.ToolBar.TabIndex = 0
        Me.ToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right
        '
        'BarSave
        '
        Me.BarSave.Name = "BarSave"
        Me.BarSave.Tag = "Save"
        Me.BarSave.Text = "Save"
        '
        'BarClose
        '
        Me.BarClose.Name = "BarClose"
        Me.BarClose.Tag = "Close"
        Me.BarClose.Text = "Tutup"
        '
        'frmMstUserUserAccessDuplicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 181)
        Me.Controls.Add(Me.pgMain)
        Me.Controls.Add(Me.txtNewUserID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnBaseUserID)
        Me.Controls.Add(Me.txtBaseUserID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ToolBar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMstUserUserAccessDuplicate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Salin User Akses"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolBar As MPS.usToolBar
    Friend WithEvents BarClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents BarSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents btnBaseUserID As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtBaseUserID As MPS.usTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNewUserID As MPS.usTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pgMain As System.Windows.Forms.ProgressBar
End Class
