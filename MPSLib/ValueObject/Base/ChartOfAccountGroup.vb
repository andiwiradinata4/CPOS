Namespace VO
    Public Class ChartOfAccountGroup
        Inherits Common
        Property ID As Integer
        Property Name As String
        Property COAType As Integer
        Property AliasName As String
        Property IDStatus As Integer

        Enum Values
            All = 0
            Kas = 1
            Bank = 2
            Piutang = 3
            Persediaan = 4
            PajakPajak = 5
            AktivaLancarLainnya = 6
            TanahDanBangunan = 7
            Kendaraan = 8
            AktivaTetapLainnya = 9
            AkumulasiPenyusutan = 10
            Hutang = 11
            HutangBiaya = 12
            HutangPajak = 13
            HutangLainnya = 14
            Modal = 15
            PendapatanDanPenjualan = 16
            PendapatanLainLain = 17
            BebanHPP = 18
            BebanOperasional = 19
            BebanLainnya = 20
            IktisarLabaRugi = 21
        End Enum
    End Class 
End Namespace

