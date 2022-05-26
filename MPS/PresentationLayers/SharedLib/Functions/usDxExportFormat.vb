Namespace DX
    Public Class usDxExportFormat
        Public Const fXls As Byte = 0
        Public Const fXlsx As Byte = 1
        Public Const fPDF As Byte = 2
    End Class

    Public Class usDXExportType
        Public Const etDefault As Byte = 0
        Public Const etDataAware As Byte = 1
        Public Const etETWYSIWYG As Byte = 2
    End Class

    Public Class usDXTextExportMode
        Public Const temValue As Byte = 0
        Public Const temText As Byte = 1
    End Class
End Namespace