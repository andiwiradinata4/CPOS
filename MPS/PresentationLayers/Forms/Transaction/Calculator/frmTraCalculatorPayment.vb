Public Class frmTraCalculatorPayment

#Region "Property"

    Private clsData As VO.Calculator
    Private clsDataDet() As VO.CalculatorDet
    Private bolIsSuccess As Boolean = False, bolIsNew As Boolean = False

    Public WriteOnly Property pubData As VO.Calculator
        Set(value As VO.Calculator)
            clsData = value
        End Set
    End Property

    Public WriteOnly Property pubDataDetail As VO.CalculatorDet()
        Set(value As VO.CalculatorDet())
            clsDataDet = value
        End Set
    End Property

    Public ReadOnly Property pubIsSuccess As Boolean
        Get
            Return bolIsSuccess
        End Get
    End Property

    Public WriteOnly Property pubIsNew As Boolean
        Set(value As Boolean)
            bolIsNew = value
        End Set
    End Property

#End Region

    Private Sub prvSave()
        If txtPay.Value < txtTotalAmount.Value Then
            UI.usForm.frmMessageBox("Nilai bayar tidak boleh lebih kecil dari Total Belanja")
            txtPay.Focus()
            Exit Sub
        End If

        clsData.TotalPayment = txtPay.Value
        Try
            BL.Calculator.SaveData(bolIsNew, clsData, clsDataDet)
            If UI.usForm.frmAskQuestion("Apakah Anda ingin cetak struk?") Then
                UI.usForm.PrintDirect(Me, New rptBonCalculator870, BL.Calculator.ListDataStruk(clsData.ID), "Struk-" & clsData.ID)
            End If
            bolIsSuccess = True
            Me.Close()
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message)
        End Try
    End Sub

#Region "Form Handle"

    Private Sub frmPayment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            prvSave()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtTotalAmount.Value = clsData.GrandTotal
        txtPay.Focus()
    End Sub

    Private Sub txtPay_ValueChanged(sender As Object, e As EventArgs) Handles txtPay.ValueChanged
        txtRefund.Value = txtPay.Value - txtTotalAmount.Value
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        prvSave()
    End Sub

#End Region

End Class