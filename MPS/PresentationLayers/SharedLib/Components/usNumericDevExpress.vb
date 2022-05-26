Imports System
Imports System.Threading
Imports System.Windows.Forms

Public Class usNumericDevExpress : Inherits DevExpress.XtraEditors.TextEdit

    Public Sub New()
        Me.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.Properties.DisplayFormat.FormatString = "#,##0.00"
        Me.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.Properties.Mask.EditMask = "n2"
    End Sub

End Class
