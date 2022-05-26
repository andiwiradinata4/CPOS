Public Class usToolBar : Inherits Windows.Forms.ToolBar

    'Using for Form List or many buttons
    Public Sub New()
        Me.TabStop = False
        Me.DropDownArrows = True
        Me.TextAlign = ToolBarTextAlign.Right
        Me.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
    End Sub

    Public Sub SetIcon(ByRef frmMe As Form)
        Dim iltImageList As New ImageList
        Dim strIcon As String = ""
        Dim bteI, bteJ As Byte
        Me.ImageList = iltImageList
        With iltImageList
            .ImageSize = New Size(20, 20)
            .ColorDepth = ColorDepth.Depth32Bit
            For i As Integer = 0 To Me.Buttons.Count - 1
                If Not Me.Buttons(i).Tag Is Nothing Or Me.Buttons(i).Tag <> "" Then
                    strIcon += "," & i & "," & Me.Buttons(i).Tag
                    .Images.Add(New Icon(frmMe.GetType(), Me.Buttons(i).Tag + ".ico"))
                End If
            Next
            If strIcon.Length > 1 Then strIcon = strIcon.Substring(1, strIcon.Length - 1)
            Dim Icon() = Split(strIcon, ",")
            While bteI < UBound(Icon)
                Me.Buttons(CByte(Icon(bteI))).ImageIndex = bteJ
                bteI += 2 : bteJ += 1
            End While
        End With
    End Sub

    'Using for Form Detail
    Public _frmMe As Form
    Public Shared Sub AddToolBarButtons(ByRef tobToolBar As ToolBar, ByVal strButtons As String, ByVal strModuleID As String, ByVal strAccessCode As String)
        Dim Buttons() = Split(strButtons, ",")
        Dim Button As ToolBarButton

        For i As Integer = 1 To Buttons.Count - 1 Step 2
            Button = New ToolBarButton
            Button.Style = ToolBarButtonStyle.PushButton
            Button.Text = Buttons.GetValue(i)
            Button.Name = "Bar" & Buttons.GetValue(i)
            'If strProgramID.Trim <> "" And strModuleID.Trim <> "" And strAccessCode.Trim <> "" Then Button.Visible = BL.UserAccess.CheckAccess(UI.usUserApp.UserID, UI.usUserApp.DelegateUserID, strProgramID, strModuleID, strAccessCode)
            tobToolBar.Buttons.Add(Button)
        Next
    End Sub

    Public Sub New(ByVal frmMe As Form, ByVal strIcon As String, _
                   ByVal strModuleID As String, ByVal strAccessCode As String)
        _frmMe = frmMe
        With Me
            .Appearance = ToolBarAppearance.Flat
            .DropDownArrows = True
            .Font = New Font("Tahoma", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
            .Location = New System.Drawing.Point(0, 0)
            .Name = "ToolBar"
            .ShowToolTips = True
            .TabIndex = 0
            .TextAlign = ToolBarTextAlign.Right
            .Dock = DockStyle.Top
            .BringToFront()
            AddToolBarButtons(Me, strIcon, strModuleID, strAccessCode)
            UI.usForm.SetToolBar(frmMe, Me, strIcon)
        End With
        AddHandler Me.ButtonClick, AddressOf usToolBar_ButtonClick
    End Sub

    Private Sub usToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs)
        If e.Button.Text = "Save" Then
            sender._frmMe.prvSave()
        ElseIf e.Button.Text = "Add" Then
            sender._frmMe.prvAdd()
        ElseIf e.Button.Text = "Close" Then
            sender._frmMe.Close()
        End If
    End Sub

End Class
