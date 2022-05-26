Public Class frmRptSalesServiceReportVer00

#Region "Property"

    Private bolExport As Boolean = False
    Private intCompanyID As Integer
    Private Const _
       cPreview = 0, cClose = 1

#End Region

    Private Sub prvChooseCompany()
        Dim frmDetail As New frmViewCompany
        With frmDetail
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()
            If .pubIsLookUpGet Then
                intCompanyID = .pubLUdtRow.Item("CompanyID")
                txtCompanyName.Text = .pubLUdtRow.Item("CompanyName")
                prvFillCombo()
            End If
        End With
    End Sub

    Private Sub prvFillBPListBox()
        Try
            Dim dtData As DataTable = BL.BusinessPartner.ListDataForFilter(MPSLib.UI.usUserApp.ProgramID)
            dtData.DefaultView.Sort = "Code ASC"

            For Each dr As DataRow In dtData.Rows
                chkListCustomer.Items.Add(dr.Item("Code") & " | " & dr.Item("Name"))
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Function prvSelectedCheckList(ByVal chkList As usCheckListBoxControl) As String
        Dim strReturn As String = ""
        Dim sb As New System.Text.StringBuilder
        Dim aStr() As String
        With chkList
            For i As Integer = 0 To .Items.Count - 1
                If .GetItemChecked(i) = True Then
                    aStr = .Items.Item(i).ToString.Split(" | ")
                    sb.Append("'" & aStr(0) & "', ")
                End If
            Next
        End With
        If sb.ToString <> "" Then
            strReturn = Mid(sb.ToString, 1, Len(sb.ToString) - 2)
        End If
        Return strReturn.Trim
    End Function

    Private Sub prvFillCombo()
        Try
            Dim dtData As DataTable = BL.Sales.ListDataRemarks(intCompanyID, MPSLib.UI.usUserApp.ProgramID)
            Dim dr As DataRow
            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("Remarks") = "SEMUA"
                .Item("Remarks") = "SEMUA"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()
            dtData.DefaultView.Sort = "Remarks ASC"
            dtData = dtData.DefaultView.ToTable
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvPreview()
        ToolBar.Focus()
        If dtpDateFrom.Value.Date > dtpDateTo.Value.Date Then
            UI.usForm.frmMessageBox("Period salah")
            dtpDateFrom.Focus()
            Exit Sub
        End If

        Dim strFilterDate As String = Format(dtpDateFrom.Value, "dd-MMM-yyyy") & " - " & Format(dtpDateTo.Value, "dd-MMM-yyyy")

        Me.Cursor = Cursors.WaitCursor
        pgMain.Value = 30
        Try
            Dim crReport As New rptSalesServiceReportVer00
            With crReport
                .DataSource = BL.Reports.SalesServiceReport(intCompanyID, MPSLib.UI.usUserApp.ProgramID, dtpDateFrom.Value.Date, dtpDateTo.Value.Date, prvSelectedCheckList(chkListCustomer))
                .Parameters.Item("FilterPeriod").Value = strFilterDate
                .Parameters.Item("CompanyName").Value = MPSLib.UI.usUserApp.CompanyName
                .DisplayName = Me.Text
                .CreateDocument(True)
                .ShowPreviewMarginLines = False
                .ShowPrintMarginsWarning = False
            End With

            Dim frmDetail As New frmReportPreview
            With frmDetail
                .docViewer.DocumentSource = crReport
                .pgExportButton.Enabled = bolExport
                .Text = Me.Text & " - " & VO.Reports.PrintOut
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            pgMain.Value = 100
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmRptSalesServiceReportVer00_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        intCompanyID = MPSLib.UI.usUserApp.CompanyID
        txtCompanyName.Text = MPSLib.UI.usUserApp.CompanyName
        dtpDateFrom.Value = Today.Date
        dtpDateTo.Value = Today.Date
        bolExport = BL.UserAccess.IsCanAccess(MPSLib.UI.usUserApp.UserID, MPSLib.UI.usUserApp.ProgramID, VO.Modules.Values.TransactionSales, VO.Access.Values.ExportReportAccess)
        prvFillCombo()
        prvFillBPListBox()
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Lihat Laporan" : prvPreview()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

    Private Sub btnCompany_Click(sender As Object, e As EventArgs) Handles btnCompany.Click
        prvChooseCompany()
    End Sub

#End Region

End Class