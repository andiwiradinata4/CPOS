Public Class frmSysLogin

#Region "Functional Handle"

    Private Sub prvLoadXML()
        Dim strXML As String = Application.StartupPath & "\MPS-CONFIG.XML"
        If Not IO.File.Exists(strXML) Then
            UI.usForm.frmMessageBox("XML File Not Found ... ")
            Exit Sub
        End If

        Dim xmlHandle As New usXML(strXML)
        With xmlHandle
            VO.DefaultServer.Server = .GetConfigInfo("CONNECTION", "SERVER", ".").Item(1)
            VO.DefaultServer.Database = .GetConfigInfo("CONNECTION", "DATABASE", "").Item(1)
            VO.DefaultServer.UserID = .GetConfigInfo("CONNECTION", "USERID", "").Item(1)
            VO.DefaultServer.Password = .GetConfigInfo("CONNECTION", "PASSWORD", "").Item(1)
            VO.DefaultServer.StartFrom = .GetConfigInfo("CONNECTION", "STARTFROM", "2021/01/01").Item(1)
            VO.DefaultServer.DSPath = .GetConfigInfo("CONNECTION", "DSPATH", "C:\DATA").Item(1)
            VO.DefaultServer.DSTempPath = .GetConfigInfo("CONNECTION", "DSTEMPPATH", "C:\DATA").Item(1)
            VO.DefaultServer.Use2FA = CBool(.GetConfigInfo("CONNECTION", "USE2FA", "0").Item(1))
        End With
        txtUserID.Text = VO.DefaultServer.UserID
        txtPassword.Text = VO.DefaultServer.Password
    End Sub

    Private Sub prvLogin()
        If txtUserID.Text.Trim = "" Then
            txtUserID.Focus()
            UI.usForm.frmMessageBox("User ID belum diinput")
            Exit Sub
        ElseIf txtPassword.Text.Trim = "" Then
            txtUserID.Focus()
            UI.usForm.frmMessageBox("Password belum diinput")
            Exit Sub
        End If

        MPSLib.UI.usUserApp.UserID = txtUserID.Text.Trim
        Dim strPassword As String = Encryption.Encrypt(txtPassword.Text.Trim)
        Dim dtUserValid As DataTable = BL.User.ListDataByUserIDAndPassword(txtUserID.Text.Trim, Encryption.Encrypt(txtPassword.Text.Trim))
        If dtUserValid.Rows.Count > 0 Then
            MPSLib.UI.usUserApp.IsSuperUser = dtUserValid.Rows(0).Item("IsSuperUser")
            MPSLib.UI.usUserApp.IsFirstCreated = dtUserValid.Rows(0).Item("IsFirstCreated")
            MPSLib.UI.usUserApp.AccessList = BL.UserAccess.ListDataWithCompany(MPSLib.UI.usUserApp.UserID, 0)
            MPSLib.UI.usUserApp.ServerName = VO.DefaultServer.Server & "|" & VO.DefaultServer.Database

            Me.Hide()
            Dim frmDetail As New frmViewProgramCompany
            With frmDetail
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                If .pubIsChoose Then
                    MPSLib.UI.usUserApp.ProgramID = .pubLUdtRow.Item("ProgramID")
                    MPSLib.UI.usUserApp.ProgramName = .pubLUdtRow.Item("ProgramName")
                    MPSLib.UI.usUserApp.CompanyID = .pubLUdtRow.Item("CompanyID")
                    MPSLib.UI.usUserApp.CompanyName = .pubLUdtRow.Item("CompanyName")
                    MPSLib.UI.usUserApp.CompanyAddress = .pubLUdtRow.Item("Address")
                    MPSLib.UI.usUserApp.CompanyInitial = .pubLUdtRow.Item("CompanyInitial")
                    MPSLib.UI.usUserApp.JournalPost = BL.JournalPost.GetDetail(MPSLib.UI.usUserApp.ProgramID)
                    If MPSLib.UI.usUserApp.IsSuperUser Then
                        frmSysMain.Show()
                    Else
                        frmTraCalculatorDet.Show()
                    End If
                End If
            End With
        Else
            txtPassword.Focus()
            UI.usForm.frmMessageBox("User ID dan Password tidak valid!")
            Exit Sub
        End If
        tmrAutoLogin.Stop()
    End Sub

#End Region

#Region "Form Handle"

    Private Sub frmSysLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            prvLogin()
        End If
    End Sub

    Private Sub frmSysLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        txtUserID.Focus()
        txtPassword.CharacterCasing = CharacterCasing.Normal
        prvLoadXML()
        tmrAutoLogin.Start()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        prvLogin()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If Not UI.usForm.frmAskQuestion("Keluar dari program?") Then Exit Sub
        Application.Exit()
    End Sub

    Private Sub txtLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUserID.KeyDown, txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            prvLogin()
        End If
    End Sub

    Private Sub tmrAutoLogin_Tick(sender As Object, e As EventArgs) Handles tmrAutoLogin.Tick
        prvLogin()
    End Sub

#End Region
    
End Class
