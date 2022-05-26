Namespace VO
    Public Class ReceiveReturn
        Inherits Common
        Property ID As String
        Property ReferencesID As String
        Property BPID As Integer
        Property BPName As String
        Property ReceiveReturnDate As DateTime
        Property TimeIn As TimeSpan = Today.TimeOfDay
        Property TimeOut As TimeSpan = Today.TimeOfDay
        Property PaymentTerm As Integer
        Property DriverName As String
        Property PlatNumber As String
        Property DONumber As String
        Property SPBNumber As String
        Property SegelNumber As String
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
        Property ArrivalNettoAfterReceive As Decimal
        Property ArrivalUsage As Decimal
        Property Specification As String
        Property Tolerance As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
    End Class 
End Namespace

