Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class usCheckListBoxControl : Inherits DevExpress.XtraEditors.CheckedListBoxControl

    Public Sub New()
        Me.TabStop = True
        Me.CheckOnClick = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

End Class
