Imports System.IO
Imports DevExpress.XtraReports.UI

Namespace UI

    Public Class usForm

        Public Shared Sub SetIcon(ByVal frmMe As Form, ByVal strIconName As String)
            frmMe.Icon = New Icon(frmMe.GetType(), strIconName & ".ico")
        End Sub

        Public Shared Sub frmMessageBox(ByVal strMessage As String, Optional ByVal strCaption As String = "Pesan")
            MessageBox.Show(strMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Sub

        Public Shared Function frmAskQuestion(ByVal strQuestion As String) As Boolean
            If MessageBox.Show(strQuestion, "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Sub frmOpen(ByRef f As Object, ByVal fT As String, ByRef fMe As Form)
            Dim s_fT As String = fMe.GetType.Namespace & "." & fT
            fMe.Cursor = Cursors.WaitCursor
            If Not IsNothing(f) Then
                If Not f.IsDisposed Then
                    f.WindowState = FormWindowState.Normal
                    f.BringToFront()
                    f.WindowState = FormWindowState.Maximized
                Else
                    f = Activator.CreateInstance(Type.GetType(s_fT))
                    f.MdiParent = fMe
                    f.Show()
                End If
            Else
                f = Activator.CreateInstance(Type.GetType(s_fT))
                f.MdiParent = fMe
                f.Show()
            End If
            fMe.Cursor = Cursors.Arrow
        End Sub

        Public Shared Sub SetToolBar(ByRef frmMe As Form, ByVal tobToolBar As ToolBar, ByVal strIcon As String)
            Dim iltImageList As New ImageList
            Dim Icon() = Split(strIcon, ",")
            Dim bteI, bteJ As Byte
            tobToolBar.ImageList = iltImageList
            With iltImageList
                .ImageSize = New Size(20, 20)
                .ColorDepth = ColorDepth.Depth32Bit
                bteI = 1
                While bteI <= UBound(Icon)
                    .Images.Add(New Icon(frmMe.GetType(), Icon(bteI) + ".ico"))
                    bteI += 2
                End While
                bteI = 0 : bteJ = 0
                While bteI < UBound(Icon)
                    tobToolBar.Buttons(CByte(Icon(bteI))).ImageIndex = bteJ
                    bteI += 2 : bteJ += 1
                End While
            End With
        End Sub

        Public Shared Sub SetGrid(ByVal grdView As DevExpress.XtraGrid.Views.Grid.GridView, _
                                  ByVal strFieldName As String, _
                                  ByVal strCaption As String, _
                                  ByVal intColoumnWidth As Integer, _
                                  ByVal intColoumnType As Integer, _
                                  Optional ByVal bolVisible As Boolean = True, _
                                  Optional ByVal bolReadonly As Boolean = True)

            Dim grdNewColumn As New DevExpress.XtraGrid.Columns.GridColumn
            With grdNewColumn
                .Name = strFieldName
                .FieldName = strFieldName
                .Caption = strCaption
                .Width = intColoumnWidth

                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                .AppearanceCell.Options.UseTextOptions = True


                Select Case intColoumnType
                    Case usDefGrid.gBoolean
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    Case usDefGrid.gFullDate
                        .DisplayFormat.FormatString = usDefCons.DateFull
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    Case usDefGrid.gIntNum
                        .DisplayFormat.FormatString = usDefCons.IntNum
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gReal1Num
                        .DisplayFormat.FormatString = usDefCons.Real1Num
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gReal2Num
                        .DisplayFormat.FormatString = usDefCons.Real2Num
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gReal3Num
                        .DisplayFormat.FormatString = usDefCons.Real3Num
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gReal4Num
                        .DisplayFormat.FormatString = usDefCons.Real4Num
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gReal5Num
                        .DisplayFormat.FormatString = usDefCons.Real5Num
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gReal6Num
                        .DisplayFormat.FormatString = usDefCons.Real6Num
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    Case usDefGrid.gSmallDate
                        .DisplayFormat.FormatString = usDefCons.DateSmall
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    Case usDefGrid.gString
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                End Select

                .Visible = bolVisible
                .VisibleIndex = IIf(bolVisible, grdView.Columns.Count, -1)

                .OptionsColumn.AllowEdit = Not bolReadonly
                .OptionsColumn.ReadOnly = bolReadonly

            End With
            grdView.Columns.Add(grdNewColumn)
            grdView.OptionsNavigation.AutoMoveRowFocus = False
            grdView.OptionsSelection.EnableAppearanceFocusedCell = False
            grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

        End Sub

        Public Shared Sub GridMoveRow(ByVal grdView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal strColoumnName As String, ByVal strSearch As Object)
            If strSearch = "" Then Exit Sub

            Dim Col As DevExpress.XtraGrid.Columns.GridColumn = grdView.Columns(strColoumnName)
            Dim Pos As Integer = grdView.LocateByValue(0, Col, strSearch)
            Dim strSelectedValue As String = ""
            If grdView.GroupCount > 0 Then
                grdView.ExpandAllGroups()
                For i As Integer = 0 To grdView.RowCount - 1
                    strSelectedValue = grdView.GetRowCellValue(i, strColoumnName)
                    If Not strSelectedValue Is Nothing Then
                        If strSelectedValue.Trim() = strSearch.ToString.Trim() Then
                            grdView.FocusedRowHandle = i
                            Exit For
                        End If
                    End If
                Next
            Else
                grdView.MoveBy(Pos)
            End If
        End Sub

        Public Shared Sub GridFocusedRow(ByVal grdView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal strColoumnName As String, ByVal strSearch As Object)
            Dim Col As DevExpress.XtraGrid.Columns.GridColumn = grdView.Columns(strColoumnName)
            Dim Pos As Integer = grdView.LocateByValue(0, Col, strSearch)
            grdView.FocusedRowHandle = Pos
        End Sub

        Public Shared Sub FillComboBox(ByRef usComboBox As usComboBox, ByVal dtSource As DataTable, ByVal valueMember As String, ByVal displayMember As String, _
                                       Optional ByVal bolSortDisplayMember As Boolean = False, Optional ByVal intSelectedIndex As Integer = -1)
            If bolSortDisplayMember Then dtSource.DefaultView.Sort = displayMember & " ASC"
            With usComboBox
                .MaxDropDownItems = dtSource.Rows.Count + 5
                .DataSource = dtSource
                .ValueMember = valueMember
                .DisplayMember = displayMember
                .SelectedIndex = intSelectedIndex
            End With
        End Sub

        Public Shared Sub FillComboBoxEdit(ByRef usComboBoxEdit As usComboBoxEdit, ByVal dtSource As DataTable, ByVal valueMember As String, ByVal displayMember As String, Optional ByVal initialMember As String = "")
            Dim col As DevExpress.XtraEditors.Controls.LookUpColumnInfo = New DevExpress.XtraEditors.Controls.LookUpColumnInfo(displayMember, initialMember)
            col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            With usComboBoxEdit
                .Properties.DataSource = dtSource
                .Properties.ValueMember = valueMember
                .Properties.DisplayMember = displayMember
                .Properties.Columns.Add(col)
                .SelectedText = ""
            End With
        End Sub

        Public Shared Function ChooseImage() As String
            Dim strReturn As String = ""
            Dim ofd As New OpenFileDialog
            With ofd
                .Filter = "(Image Files)|*.jpeg;*.jpg;*.png;*.bmp;*.gif;*.ico|Jpg, | *.jpg|Png, | *.png|Bmp, |*.bmp|Gif, | *.gif|Ico | *.ico|Jpeg, | *.jpeg"
                If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                    strReturn = .FileName.Trim
                End If
            End With
            Return strReturn
        End Function

        Public Shared Sub LoadImage(ByRef pictureBoxEdit As DevExpress.XtraEditors.PictureEdit, ByVal strPath As String)
            Dim fs As New FileStream(strPath, FileMode.Open, FileAccess.Read)
            Dim bytImage As Byte() = New Byte(fs.Length - 1) {}
            fs.Read(bytImage, 0, System.Convert.ToInt32(fs.Length))
            pictureBoxEdit.Image = Image.FromStream(fs)
            fs.Close()
        End Sub

        Public Shared Function ConvertBytetoImage(ByVal bytImage As Byte()) As Image
            Dim img As Image
            Dim ms As New IO.MemoryStream(CType(bytImage, Byte()))
            img = Image.FromStream(ms)
            Return img
        End Function

        Public Shared Function PrintDO(ByVal frmMe As Form, ByVal strID As String, ByVal doColor As List(Of SharedLib.usDOColors)) As Boolean
            Dim bolReturn As Boolean
            frmMe.Cursor = Cursors.WaitCursor
            Try
                For i As Integer = 0 To doColor.Count - 1
                    Using cr As New rptDeliveryOrder
                        cr.CreateDocument(True)
                        'cr.DataSource = BL.Sales.ListDataDeliveryOrder(strID)
                        cr.ShowPreviewMarginLines = False

                        cr.DisplayName = doColor(i).DisplayName
                        cr.labelMark.BackColor = doColor(i).TextColor
                        cr.labelMark.Text = doColor(i).TextName

                        Using tool As New ReportPrintTool(cr)
                            tool.Print()
                        End Using
                    End Using
                Next
            Catch ex As Exception
                UI.usForm.frmMessageBox(ex.Message)
            Finally
                frmMe.Cursor = Cursors.Default
            End Try
            Return bolReturn
        End Function

        Public Shared Function PrintBonFaktur(ByVal frmMe As Form, ByVal strID As String) As Boolean
            Dim bolReturn As Boolean
            frmMe.Cursor = Cursors.WaitCursor
            Try
                Using cr As New rptBonFaktur
                    cr.DataSource = BL.Sales.ListDataFakturPenjualan(strID)
                    cr.CreateDocument(True)
                    cr.ShowPreviewMarginLines = False
                    cr.ShowPrintMarginsWarning = False
                    cr.DisplayName = strID

                    Using tool As New ReportPrintTool(cr)
                        tool.Print()
                    End Using
                End Using
                bolReturn = True
            Catch ex As Exception
                UI.usForm.frmMessageBox(ex.Message)
            Finally
                frmMe.Cursor = Cursors.Default
            End Try
            Return bolReturn
        End Function

        Public Shared Function PrintDirect(ByVal frmMe As Form, ByVal crPrint As DevExpress.XtraReports.UI.XtraReport, ByVal dtData As DataTable, ByVal strDisplayName As String) As Boolean
            Dim bolReturn As Boolean
            frmMe.Cursor = Cursors.WaitCursor
            Try
                crPrint.DataSource = dtData
                crPrint.CreateDocument(True)
                crPrint.ShowPreviewMarginLines = False
                crPrint.ShowPrintMarginsWarning = False
                crPrint.DisplayName = strDisplayName

                Using tool As New ReportPrintTool(crPrint)
                    tool.Print()
                End Using
             
                bolReturn = True
            Catch ex As Exception
                UI.usForm.frmMessageBox(ex.Message)
            Finally
                frmMe.Cursor = Cursors.Default
            End Try
            Return bolReturn
        End Function

    End Class

End Namespace

