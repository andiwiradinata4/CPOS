Namespace UI

    Public Class usUserApp
        Public Shared UserID As String
        Public Shared AccessList As DataTable
        Public Shared ProgramID As Integer
        Public Shared ProgramName As String
        Public Shared CompanyID As Integer
        Public Shared CompanyName As String
        Public Shared CompanyAddress As String
        Public Shared CompanyPhoneNumber As String
        Public Shared CompanyInitial As String
        Public Shared UserEmail As String
        Public Shared IsSuperUser As Boolean = False
        Public Shared IsFirstCreated As Boolean
        Public Shared JournalPost As VO.JournalPost
        Public Shared ServerName As String
    End Class

End Namespace
