Namespace BL

    Public Class Server

        Public Shared Sub ServerTemp()
            DL.SQL.strServer = "isql.local.com"
            DL.SQL.strDatabase = "CPS"
            DL.SQL.strSAID = ""
            DL.SQL.strSAPassword = ""
        End Sub

        <System.Diagnostics.DebuggerStepThrough>
        Public Shared Sub ServerDefault()
            DL.SQL.strServer = VO.DefaultServer.Server
            DL.SQL.strDatabase = VO.DefaultServer.Database
            DL.SQL.strSAID = VO.DefaultServer.UserID
            DL.SQL.strSAPassword = VO.DefaultServer.Password
        End Sub

        '<System.Diagnostics.DebuggerStepThrough>
        'Public Shared Sub SetUserServerLocation(ByVal intComLocDivSubDivID As Integer)
        '    ServerDefault()
        '    Dim clsData As VO.UserServerLocation = DL.UserServerLocation.GetDetail(intComLocDivSubDivID)
        '    If clsData Is Nothing Then Exit Sub
        '    DL.SQL.strServer = clsData.Server
        '    DL.SQL.strDatabase = clsData.DBName
        '    DL.SQL.strSAID = ""
        '    DL.SQL.strSAPassword = ""
        'End Sub

        '<System.Diagnostics.DebuggerStepThrough>
        'Public Shared Function DefaultRemotingService() As MM_Remoting_Service.SqlHelper
        '    Dim MM_Remoting As New MM_Remoting_Service.SqlHelper
        '    With MM_Remoting
        '        .ServerName = DL.SQL.strServer
        '        .Database = DL.SQL.strDatabase
        '        .ApplicationName = DL.SQL.strAplicationName
        '        Return MM_Remoting
        '    End With
        'End Function

        Public Shared Function ServerList() As DataTable
            Return DL.Server.ServerList
        End Function

        Public Shared Sub ServerAllCompanyList()
            ServerDefault()
            VO.ServerList.ServerList = DL.Server.ServerAllCompanyList
        End Sub

        <System.Diagnostics.DebuggerStepThrough>
        Public Shared Sub SetServer(ByVal strCompanyID As String)
            For Each dtRow As DataRow In VO.ServerList.ServerList.Rows
                If dtRow.Item("CompanyID") = strCompanyID Then
                    DL.SQL.strServer = dtRow.Item("Server")
                    DL.SQL.strDatabase = dtRow.Item("DBName")
                    DL.SQL.strSAID = dtRow.Item("UserID")
                    DL.SQL.strSAPassword = dtRow.Item("UserPassword")
                    Exit For
                End If
            Next
        End Sub

        <System.Diagnostics.DebuggerStepThrough>
        Public Shared Sub SetServer(ByVal strServer As String, ByVal strDBName As String, ByVal strUserID As String, ByVal strUserPassword As String)
            DL.SQL.strServer = strServer
            DL.SQL.strDatabase = strDBName
            DL.SQL.strSAID = strUserID
            DL.SQL.strSAPassword = strUserPassword
        End Sub

        Public Shared Function IsServerAlpa() As Boolean
            Dim bolReturn As Boolean = False
            If VO.DefaultServer.Server = "idtjm-fs-01\sqlserver" Or VO.DefaultServer.Server = "172.18.3.12\sqlserver" Then
                bolReturn = True
            End If
            Return bolReturn
        End Function

        Public Shared Function IsServerUAT() As Boolean
            Dim bolReturn As Boolean = False
            If VO.DefaultServer.Server = "192.168.80.151" Then
                bolReturn = True
            End If
            Return bolReturn
        End Function

        Public Shared Function IsServerTesting() As Boolean
            Dim bolReturn As Boolean = False
            If IsServerAlpa() Or IsServerUAT() Then
                bolReturn = True
            End If
            Return bolReturn
        End Function
    End Class

End Namespace
