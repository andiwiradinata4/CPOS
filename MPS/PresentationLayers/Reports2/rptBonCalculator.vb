Public Class rptBonCalculator

    Private Sub rptBonCalculator_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If GetCurrentColumnValue("BPAddress").ToString = "" Then
            sbBPAddress.Visible = False
        End If

    End Sub
End Class