Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class usComboBoxEdit : Inherits DevExpress.XtraEditors.LookUpEdit

    Public Sub New()
        Me.TabStop = True
        Me.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.Properties.NullText = ""
        Me.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {
                New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
            })
    End Sub

End Class
