Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class usTextBox : Inherits TextBox

    Public Sub New()
        Me.CharacterCasing = Windows.Forms.CharacterCasing.Upper
        AddHandler Me.GotFocus, AddressOf OnMyGotFocus
    End Sub

    Sub OnMyGotFocus(ByVal sender As Object, ByVal e As EventArgs)
        Me.Select(0, Me.Text.Length)
    End Sub

End Class