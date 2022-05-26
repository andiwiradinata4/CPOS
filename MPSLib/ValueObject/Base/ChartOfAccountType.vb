Namespace VO
    Public Class ChartOfAccountType
        Inherits Common
        Property ID As Integer
        Property Name As String
        Property IDStatus As Integer

        Enum Values
            All = 0
            AktivaLancar = 1
            AktivaTetap = 2
            Pasiva = 3
            Modal = 4
            Pendapatan = 5
            Beban = 6
        End Enum

    End Class
End Namespace

