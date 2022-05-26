Imports DevExpress.XtraPrinting

Namespace DX
    Public Class usDXHelper
        Public myReportTitle As String = ""

        Public Sub DevExport(ByVal frmMe As Form, ByVal grdMain As DevExpress.XtraGrid.GridControl, ByVal strFileName As String, ByVal strSheetName As String, _
                             ByVal bytFormatExport As Byte, Optional ByVal bolShowGroupSummary As Boolean = False, Optional ByVal bolShowTotalSummary As Boolean = False, _
                             Optional ByVal bytExportType As Byte = DX.usDXExportType.etDefault, Optional ByVal bytTextExportMode As Byte = DX.usDXTextExportMode.temValue)
            Dim strPath As String = ""
            Dim bolSuccess As Boolean = False
            strPath = modUsFolderBrowserDialog.usFolderBrowserDialog.Show(bolSuccess).Trim
            If strPath <> "" And bolSuccess = True Then
                strPath = strPath & "\Result_" & strFileName & "_" & Now.Year.ToString & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & _
                                    Now.Minute.ToString & Now.Second.ToString
                If Not UI.usForm.frmAskQuestion("Save data to " & strPath & "?") Then Exit Sub

                Try
                    frmMe.Cursor = Cursors.WaitCursor
                    Dim advOption As New Object

                    Select Case bytFormatExport
                        Case DX.usDxExportFormat.fXls : advOption = New XlsExportOptionsEx() : strPath += ".xls"
                        Case DX.usDxExportFormat.fXlsx : advOption = New XlsxExportOptionsEx() : strPath += ".xlsx"
                    End Select

                    With advOption
                        .AllowGrouping = DevExpress.Utils.DefaultBoolean.True
                        If bolShowGroupSummary Then .ShowGroupSummaries = DevExpress.Utils.DefaultBoolean.True
                        If bolShowTotalSummary Then .ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.True
                        .ExportType = bytExportType
                        .SheetName = strSheetName.Trim
                        .ShowBandHeaders = DevExpress.Utils.DefaultBoolean.True
                        .TextExportMode = bytTextExportMode

                        Select Case bytFormatExport
                            Case DX.usDxExportFormat.fXls : grdMain.ExportToXls(strPath.Trim, advOption)
                            Case DX.usDxExportFormat.fXlsx : grdMain.ExportToXlsx(strPath.Trim, advOption)
                        End Select
                        UI.usForm.frmMessageBox("Export data success. " & strPath)
                    End With
                Catch ex As Exception
                    UI.usForm.frmMessageBox(ex.Message)
                End Try
            End If
            frmMe.Cursor = Cursors.Default
        End Sub

#Region "Print Preview"

        Private Sub myLink_CreateReportHeaderArea(ByVal sender As Object, ByVal e As DevExpress.XtraPrinting.CreateAreaEventArgs)
            Dim brick As DevExpress.XtraPrinting.TextBrick
            'brick = e.Graph.DrawString(myReportTitle, Color.Navy, New RectangleF(0, 0, 500, 40), DevExpress.XtraPrinting.BorderSide.None)
            brick = e.Graph.DrawString(myReportTitle, Color.Navy, New RectangleF(0, 0, 700, 40), DevExpress.XtraPrinting.BorderSide.None)
            brick.Font = New Font("Arial", 14)
            brick.StringFormat = New DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Near)
        End Sub

        Public Sub PrintPreview(ByVal grdMain As DevExpress.XtraGrid.GridControl, Optional ByVal strPaperKind As System.Drawing.Printing.PaperKind = Printing.PaperKind.Ledger, _
                                Optional ByVal bolLandscape As Boolean = False, Optional ByVal strReportTitle As String = "")

            Try
                Dim myLink As New PrintableComponentLink(New PrintingSystem())
                With myLink
                    .PaperKind = strPaperKind
                    .Landscape = bolLandscape
                    .Margins = New System.Drawing.Printing.Margins(0.1, 0.1, 0.1, 0.1)
                    .PrintingSystem.Document.AutoFitToPagesWidth = 1
                    .Component = grdMain

                    If strReportTitle.Trim <> "" Then
                        myReportTitle = strReportTitle
                        AddHandler .CreateReportHeaderArea, AddressOf myLink_CreateReportHeaderArea
                    End If

                    .ShowPreview()
                End With
            Catch ex As Exception
                UI.usForm.frmMessageBox(ex.Message)
            End Try
        End Sub

#End Region
    End Class
End Namespace
