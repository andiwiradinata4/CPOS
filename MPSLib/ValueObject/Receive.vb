Namespace VO
    Public Class Receive
        Inherits Common
        Property ID As String
        Property ReferencesID As String
        Property BPID As Integer
        Property BPCode As String
        Property BPName As String
        Property ReceiveDate As DateTime
        Property TimeIn As TimeSpan = Today.TimeOfDay
        Property TimeOut As TimeSpan = Today.TimeOfDay
        Property PaymentTerm As Integer
        Property DueDate As DateTime
        Property PlatNumber As String
        Property DriverName As String
        Property DONumber As String
        Property SPBNumber As String
        Property SegelNumber As String
        Property Specification As String
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
        Property MaxNetto As Decimal
        Property Price1 As Decimal
        Property Price2 As Decimal
        Property TotalPrice1 As Decimal
        Property TotalPrice2 As Decimal
        Property TotalDownPayment As Decimal
        Property ArrivalReturn As Decimal
        Property ArrivalUsage As Decimal
        Property ArrivalNettoAfterSales As Decimal
        Property TotalPayment As Decimal
        Property TotalReturn As Decimal
        Property Tolerance As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
        Property ReceiveNo As String
        Property SalesNo As String

        Public Const JournalRemarks As String = "PEMBELIAN TBS"
        Public Const JournalCashOrBankInfo As String = "CASH"
    End Class
End Namespace