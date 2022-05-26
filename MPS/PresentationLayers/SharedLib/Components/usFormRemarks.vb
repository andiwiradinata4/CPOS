Public Class usFormRemarks

#Region "Properties"

    Private strTitle As String = "Isi Keterangan"
    Private strInfo As String = "Keterangan"
    Private strValue As String = ""
    Private bolIsSave As Boolean = False
    Private Const _
        cSave = 0, cClose = 1

    Public WriteOnly Property pubTitle As String
        Set(value As String)
            strTitle = value
        End Set
    End Property

    Public WriteOnly Property pubInfo As String
        Set(value As String)
            strInfo = value
        End Set
    End Property

    Public ReadOnly Property pubValue As String
        Get
            Return strValue
        End Get
    End Property

    Public ReadOnly Property pubIsSave As Boolean
        Get
            Return bolIsSave
        End Get
    End Property

#End Region

    Private Sub prvSave()
        If txtValue.Text.Trim = "" Then
            UI.usForm.frmMessageBox("Mohon diisi keterangan terlebih dahulu")
            txtValue.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        strValue = txtValue.Text.Trim
        bolIsSave = True
        Me.Close()
    End Sub

#Region "Form Handle"

    Private Sub usFormRemarks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        Me.Text = strTitle
        lblInfo.Text += strInfo
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

#End Region
    
End Class