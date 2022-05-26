Public Class frmMstBusinessPartnerPriceDet

#Region "Property"

    Private frmParent As frmMstBusinessPartnerPrice
    Private clsData As VO.BusinessPartnerPrice
    Property pubID As Integer
    Property pubIsNew As Boolean = False
    Property pubIsSave As Boolean = False
    Property pubClsBP As VO.BusinessPartner

    Public Sub pubShowDialog(ByVal frmGetParent As Form)
        frmParent = frmGetParent
        Me.ShowDialog()
    End Sub

#End Region

    Private Const _
       cSave = 0, cClose = 1

    Private Sub prvClear()
        dtpDateFrom.Value = Today.Date
        dtpDateTo.Value = "3000/01/01"
        txtSalesPrice.Value = 0
        txtPurchasePrice1.Value = 0
        txtPurchasePrice2.Value = 0
    End Sub

    Private Sub prvFillForm()
        Try
            If pubIsNew Then
                prvClear()
            Else
                clsData = New VO.BusinessPartnerPrice
                clsData = BL.BusinessPartner.GetDetailPrice(pubID)
                dtpDateFrom.Value = clsData.DateFrom
                dtpDateTo.Value = clsData.DateTo
                txtSalesPrice.Value = clsData.SalesPrice
                txtPurchasePrice1.Value = clsData.PurchasePrice1
                txtPurchasePrice2.Value = clsData.PurchasePrice2
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub prvSave()
        If dtpDateFrom.Value.Date > dtpDateTo.Value.Date Then
            UI.usForm.frmMessageBox("Periode salah")
            dtpDateFrom.Focus()
            Exit Sub
        ElseIf txtSalesPrice.Value <= 0 Then
            UI.usForm.frmMessageBox("Harga jual harus lebih besar dari 0")
            txtSalesPrice.Focus()
            Exit Sub
        ElseIf txtPurchasePrice1.Value <= 0 Then
            UI.usForm.frmMessageBox("Harga beli 1 harus lebih besar dari 0")
            txtPurchasePrice1.Focus()
            Exit Sub
        ElseIf txtPurchasePrice2.Value <= 0 Then
            UI.usForm.frmMessageBox("Harga beli 2 harus lebih besar dari 0")
            txtPurchasePrice2.Focus()
            Exit Sub
        End If

        If Not UI.usForm.frmAskQuestion("Simpan data?") Then Exit Sub

        clsData = New VO.BusinessPartnerPrice
        clsData.ID = pubID
        clsData.BPID = pubClsBP.ID
        clsData.DateFrom = dtpDateFrom.Value.Date
        clsData.DateTo = dtpDateTo.Value.Date
        clsData.SalesPrice = txtSalesPrice.Value
        clsData.PurchasePrice1 = txtPurchasePrice1.Value
        clsData.PurchasePrice2 = txtPurchasePrice2.Value
        clsData.LogBy = MPSLib.UI.usUserApp.UserID

        Try
            BL.BusinessPartner.SaveDataPrice(pubIsNew, clsData)
            If pubIsNew Then
                UI.usForm.frmMessageBox("Data berhasil disimpan.")
                frmParent.pubRefresh()
                frmParent.grdView.MoveLast()
                prvClear()
            Else
                pubIsSave = True
                Me.Close()
            End If
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmMstBusinessPartnerPriceDet_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If UI.usForm.frmAskQuestion("Tutup form ini?") Then Me.Close()
        ElseIf (e.Control And e.KeyCode = Keys.S) Then
            prvSave()
        End If
    End Sub

    Private Sub frmMstBusinessPartnerPriceDet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        prvFillForm()
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Simpan" : prvSave()
            Case "Tutup" : Me.Close()
        End Select

    End Sub

#End Region

End Class