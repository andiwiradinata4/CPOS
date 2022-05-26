Namespace VO
    Public Class AccountReceivable
        Inherits Common
        Property ID As String
        Property BPID As Integer
        Property BPCode As String
        Property BPName As String
        Property ARDate As DateTime
        Property PaymentReferencesID As Integer
        Property PaymentReferencesName As String
        Property ReferencesNote As String
        Property TotalAmount As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
        Property CoAIDOfIncomePayment As Integer
        Property CoACodeOfIncomePayment As String
        Property CoANameOfIncomePayment As String
    End Class 
End Namespace

