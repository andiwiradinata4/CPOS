Namespace BL
    Public Class COGS

        Public Shared Function CalculateCOGS(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, intItemID As Integer, _
                                             ByVal decArrivalNettoAfter As Decimal, ByVal strReferencesID As String, ByVal dtmReferencesDate As DateTime) As Decimal
            Dim decReturn As Decimal = 0
            Dim dtStockIn As DataTable = DL.StockIN.ListDataOutstanding(intCompanyID, intProgramID, intItemID)
            Dim decQty As Decimal = decArrivalNettoAfter

            For Each drIn As DataRow In dtStockIn.Rows
                If drIn.Item("Qty") - drIn.Item("QtyOut") >= decQty Then
                    DL.StockIN.UpdateQtyStockOut(drIn.Item("ID"), drIn.Item("QtyOut") + decQty)

                    Dim clsOut As New VO.StockOut
                    clsOut.CompanyID = intCompanyID
                    clsOut.ProgramID = intProgramID
                    clsOut.ID = DL.StockOut.GetMaxID
                    clsOut.IDStockIN = drIn.Item("ID")
                    clsOut.ItemID = drIn.Item("ItemID")
                    clsOut.ReferencesID = strReferencesID
                    clsOut.ReferencesDate = dtmReferencesDate
                    clsOut.Qty = decQty
                    DL.StockOut.SaveData(clsOut)

                    decReturn += decQty * drIn.Item("NetPrice")
                    Exit For
                ElseIf drIn.Item("Qty") - drIn.Item("QtyOut") < decQty Then
                    DL.StockIN.UpdateQtyStockOut(drIn.Item("ID"), drIn.Item("QtyOut") + (drIn.Item("Qty") - drIn.Item("QtyOut"))) '# Qty - Qty Out = Qty Sisa -> Update Sisanya
                    decQty = decQty - (drIn.Item("Qty") - drIn.Item("QtyOut"))

                    Dim clsOut As New VO.StockOut
                    clsOut.CompanyID = intCompanyID
                    clsOut.ProgramID = intProgramID
                    clsOut.ID = DL.StockOut.GetMaxID
                    clsOut.IDStockIN = drIn.Item("ID")
                    clsOut.ItemID = drIn.Item("ItemID")
                    clsOut.ReferencesID = strReferencesID
                    clsOut.ReferencesDate = dtmReferencesDate
                    clsOut.Qty = drIn.Item("Qty") - drIn.Item("QtyOut")
                    DL.StockOut.SaveData(clsOut)

                    decReturn += (drIn.Item("Qty") - drIn.Item("QtyOut")) * drIn.Item("NetPrice")
                End If
            Next
            Return decReturn
        End Function

        Public Shared Function RevertCalculateCOGS(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, intItemID As Integer, _
                                                   ByVal decArrivalNettoAfter As Decimal, ByVal strID As String, ByVal strReferencesID As String, ByVal dtmReferencesDate As DateTime) As Decimal
            Dim decReturn As Decimal = 0
            Dim dtStockIn As DataTable = DL.StockIN.ListDataByReferencesID(intCompanyID, intProgramID, strReferencesID)
            Dim decQty As Decimal = decArrivalNettoAfter
            Dim clsStockIn As New VO.StockIN

            For Each drIn As DataRow In dtStockIn.Rows
                If drIn.Item("Qty") >= decQty Then
                    decReturn += decQty * drIn.Item("NetPrice")

                    clsStockIn = New VO.StockIN
                    clsStockIn.CompanyID = intCompanyID
                    clsStockIn.ProgramID = intProgramID
                    clsStockIn.ID = DL.StockIN.GetMaxID
                    clsStockIn.ItemID = intItemID
                    clsStockIn.ReferencesID = strID
                    clsStockIn.ReferencesDate = dtmReferencesDate
                    clsStockIn.Qty = decQty
                    clsStockIn.Price = drIn.Item("Price")
                    clsStockIn.NetPrice = drIn.Item("NetPrice")
                    clsStockIn.QtyOut = 0
                    DL.StockIN.SaveData(True, clsStockIn)
                    Exit For
                ElseIf drIn.Item("Qty") < decQty Then
                    decReturn += drIn.Item("Qty") * drIn.Item("NetPrice")

                    clsStockIn = New VO.StockIN
                    clsStockIn.CompanyID = intCompanyID
                    clsStockIn.ProgramID = intProgramID
                    clsStockIn.ID = DL.StockIN.GetMaxID
                    clsStockIn.ItemID = intItemID
                    clsStockIn.ReferencesID = strID
                    clsStockIn.ReferencesDate = dtmReferencesDate
                    clsStockIn.Qty = drIn.Item("Qty")
                    clsStockIn.Price = drIn.Item("Price")
                    clsStockIn.NetPrice = drIn.Item("NetPrice")
                    clsStockIn.QtyOut = 0
                    DL.StockIN.SaveData(True, clsStockIn)
                    decQty = decQty - drIn.Item("Qty")
                End If
            Next
            Return decReturn
        End Function

        Public Shared Sub UnpostCOGS(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal strReferencesID As String)
            Dim dtStockOut As New DataTable
            dtStockOut = DL.StockOut.ListDataByReferencesID(intCompanyID, intProgramID, strReferencesID)

            For Each drOut As DataRow In dtStockOut.Rows
                DL.StockIN.DecreaseQtyStockOut(drOut.Item("IDStockIN"), drOut.Item("Qty"))
            Next
        End Sub

    End Class
End Namespace