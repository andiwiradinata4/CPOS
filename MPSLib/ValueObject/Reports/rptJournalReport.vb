Namespace VO
    Public Class rptJournalReport
        Property ID As String
        Property JournalDate As DateTime
        Property TotalAmount As Decimal
        Property AccountCode As String
        Property AccountName As String
        Property DebitAmount As Decimal
        Property CreditAmount As Decimal
        Property CreatedBy As String
        Property Remarks As String
        Property JournalNo As String
        Property PaymentTo As String = ""
        Property CashOrBankInfo As String = ""
    End Class
End Namespace