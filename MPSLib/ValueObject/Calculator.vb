Namespace VO
    Public Class Calculator
        Inherits Common
        Property ID As String
        Property TransactionNo As String
        Property BPID As Integer
        Property BPName As String
        Property BPAddress As String
        Property TransactionDate As DateTime
        Property PPN As Decimal
        Property PPH As Decimal
        Property TotalPrice As Decimal
        Property TotalPPN As Decimal
        Property TotalPPH As Decimal
        Property GrandTotal As Decimal
        Property TotalDownPayment As Decimal
        Property TotalPayment As Decimal
        Property TotalReturn As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
    End Class 
End Namespace

