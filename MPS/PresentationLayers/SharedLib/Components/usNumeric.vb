Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class usNumeric : Inherits NumericUpDown

    Public Sub New()
        Me.Maximum = Decimal.MaxValue
        Me.Minimum = Decimal.MinValue
        Me.ThousandsSeparator = True
        Me.TextAlign = HorizontalAlignment.Right
        AddHandler Me.GotFocus, AddressOf OnMyGotFocus
        AddHandler Me.Click, AddressOf OnMyGotFocus
    End Sub

    Sub OnMyGotFocus(ByVal sender As Object, ByVal e As EventArgs)
        Me.Select(0, Value.ToString(Thread.CurrentThread.CurrentCulture.NumberFormat.ToString()).Length)
    End Sub

End Class