Namespace VO
    Public Class Item
        Inherits Common
        Property ID As Integer
        Property Code As String
        Property Name As String
        Property UomID As String
        Property MinQty As Decimal
        Property BalanceQty As Decimal
        Property SalesPrice As Decimal
        Property PurchasePrice1 As Decimal
        Property PurchasePrice2 As Decimal
        Property Tolerance As Decimal
        Property IDStatus As Integer

        Enum HistoryFilter
            All = 0
            Sales = 1
            Receive = 2
            SalesReturn = 3
            ReceiveReturn = 4
        End Enum

        Public Shared Function DataHistoryFilter() As DataTable
            Dim dtData As New DataTable
            dtData.Columns.Add("ID", GetType(Integer))
            dtData.Columns.Add("Name", GetType(String))

            Dim dr As DataRow
            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.All
                .Item("Name") = "SEMUA"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)


            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.Sales
                .Item("Name") = "PENJUALAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)

            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.Receive
                .Item("Name") = "PEMBELIAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)

            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.SalesReturn
                .Item("Name") = "RETUR PENJUALAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)

            dr = dtData.NewRow
            With dr
                .BeginEdit()
                .Item("ID") = VO.Item.HistoryFilter.ReceiveReturn
                .Item("Name") = "RETUR PEMBELIAN"
                .EndEdit()
            End With
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()

            Return dtData
        End Function

    End Class 
End Namespace

