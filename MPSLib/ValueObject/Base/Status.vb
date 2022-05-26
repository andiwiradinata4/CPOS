Namespace VO
    Public Class Status
        Inherits Common
        Property ID As Integer
        Property Name As String

        Enum Values
            All = 0
            Active = 1
            InActive = 2
            Draft = 3
            Printed = 4
            Deleted = 5
        End Enum
    End Class
End Namespace

