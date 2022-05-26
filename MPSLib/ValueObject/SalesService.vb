Namespace VO
    Public Class SalesService
        Inherits Common
        Property ID As String
        Property ServiceType As Integer
        Property BPID As Integer
        Property CustomerCode As String
        Property BPName As String
        Property SalesDate As DateTime
        Property PaymentTerm As Integer
        Property DueDate As DateTime
        Property SPKNumber As String
        Property PPNPercentage As Decimal
        Property TotalPPN As Decimal
        Property TotalPPH As Decimal
        Property TotalPrice As Decimal
        Property TotalDisc As Decimal
        Property TotalDownPayment As Decimal
        Property GrandTotal As Decimal
        Property TotalPayment As Decimal
        Property TotalReturn As Decimal
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
        Property SalesNo As String
        Property BillNumber As String

        Enum Type
            RentalAlatBerat = 1
            RentalTruk = 2
        End Enum

        Public Const JournalCashOrBankInfo As String = "BANK"

        Public Function JournalRemarks(ByVal bytType As Type) As String
            If bytType = Type.RentalAlatBerat Then
                Return "RENTAL ALAT BERAT"
            Else
                Return "RENTAL TRUK"
            End If
        End Function

    End Class 
End Namespace

