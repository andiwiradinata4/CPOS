Namespace VO
    Public Class DownPayment
        Inherits Common
        Property ID As String
        Property BPID As Integer
        Property BPCode As String
        Property BPName As String
        Property BPID2 As Integer
        Property BPCode2 As String
        Property BPName2 As String
        Property DPType As Integer
        Property DPDate As DateTime
        Property PaymentReferencesID As Integer
        Property PaymentReferencesName As String
        Property ReferencesNote As String
        Property TotalAmount As Decimal
        Property TotalUsage As Decimal
        Property CoAIDOfActiva As Integer
        Property CoACodeOfActiva As String
        Property CoANameOfActiva As String
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer

        Enum Type
            All = 0
            Purchase = 1
            Sales = 2
            SalesService = 3
        End Enum

        Public Const JournalRemarksPurchase As String = "UANG MUKA PEMBELIAN TBS"
        Public Const JournalRemarksSales As String = "UANG MUKA PENJUALAN TBS"
        Public Const JournalRemarksSalesServiceRentalAlatBerat As String = "UANG MUKA RENTAL ALAT BERAT"
        Public Const JournalRemarksSalesServiceRentalTruk As String = "UANG MUKA RENTAL TRUK"
        Public Const JournalCashOrBankInfoSales As String = "BANK"
        Public Const JournalCashOrBankInfoPurchase As String = "CASH"
    End Class
End Namespace

