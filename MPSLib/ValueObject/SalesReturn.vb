Namespace VO
    Public Class SalesReturn
        Inherits Common
        Property ID As String
        Property ReferencesID As String
        Property BPID As Integer
        Property BPName As String
        Property SalesReturnDate As DateTime
        Property PaymentTerm As Integer
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
        Property ArrivalUsage As Decimal
        Property ArrivalNettoAfterSales As Decimal
        Property MaxNetto As Decimal
        Property Price As Decimal
        Property TotalPrice As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
    End Class 
End Namespace

