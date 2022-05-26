Namespace VO
    Public Class Sales
        Inherits Common
        Property ID As String
        Property BPID As Integer
        Property CustomerCode As String
        Property BPName As String
        Property SupplierID As Integer
        Property SupplierCode As String
        Property SupplierName As String
        Property SalesDate As DateTime
        Property PaymentTerm As Integer
        Property DueDate As DateTime
        Property DriverName As String
        Property PlatNumber As String
        Property PPN As Decimal
        Property PPH As Decimal
        Property ItemID As Integer
        Property ItemCode As String
        Property ItemName As String
        Property UOMID As Integer
        Property ArrivalBrutto As Decimal
        Property ArrivalTarra As Decimal
        Property ArrivalNettoBefore As Decimal
        Property ArrivalDeduction As Decimal
        Property ArrivalNettoAfter As Decimal
        Property Price As Decimal
        Property TotalPrice As Decimal
        Property ArrivalUsage As Decimal
        Property ArrivalReturn As Decimal
        Property TotalPayment As Decimal
        Property TotalDownPayment As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
        Property Tolerance As Decimal
        Property ReceiveID As String
        Property ReceiveNo As String
        Property PurchasePrice1 As Decimal
        Property PurchasePrice2 As Decimal
        Property SalesNo As String
        Property JournalIDReceive As String

        Public Const JournalRemarks As String = "PENJUALAN TBS"
        Public Const JournalCashOrBankInfo As String = "BANK"
    End Class
End Namespace