Namespace VO

    Public Class Server

        Private strCompanyID, strServer, strDBName, strUserID, strUserPassword As String

        Public Property CompanyID As String
            Get
                Return strCompanyID
            End Get
            Set(ByVal value As String)
                strCompanyID = value
            End Set
        End Property

        Public Property Server As String
            Get
                Return strServer
            End Get
            Set(ByVal value As String)
                strServer = value
            End Set
        End Property

        Public Property DBName As String
            Get
                Return strDBName
            End Get
            Set(ByVal value As String)
                strDBName = value
            End Set
        End Property

        Public Property UserID As String
            Get
                Return strUserID
            End Get
            Set(ByVal value As String)
                strUserID = value
            End Set
        End Property

        Public Property UserPassword As String
            Get
                Return strUserPassword
            End Get
            Set(ByVal value As String)
                strUserPassword = value
            End Set
        End Property

    End Class

End Namespace
