Namespace VO
    Public Class Journal
        Inherits Common
        Property ID As String
        Property JournalDate As DateTime
        Property ReferencesID As String
        Property TotalAmount As Decimal
        Property IsAutoGenerate As Boolean
        Property IsPostedGL As Boolean
        Property PostedBy As String
        Property PostedDate As DateTime
        Property Remarks As String
        Property IDStatus As Integer
        Property JournalNo As String
        Property PaymentTo As String = ""
        Property CashOrBankInfo As String = ""
    End Class 
End Namespace

