Namespace VO
    Public Class ChartOfAccount
        Inherits Common
        Property ID As Integer
        Property AccountGroupID As Integer
        Property AccountGroupName As String
        Property Code As String
        Property Name As String
        Property FirstBalance As Decimal
        Property FirstBalanceDate As DateTime
        Property IDStatus As Integer

        Enum FilterGroup
            All
            CashOrBank
            Expense
        End Enum

    End Class 
End Namespace

