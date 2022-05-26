Namespace BL
    Public Class History

        Public Shared Function ListDataHistoryBussinessPartners(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal historyFilter As VO.Item.HistoryFilter, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Dim dtData As New DataTable
            If historyFilter = VO.Item.HistoryFilter.All Then
                dtData = DL.Sales.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID)
                dtData.Merge(DL.Receive.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID))
                dtData.Merge(DL.SalesReturn.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID))
                dtData.Merge(DL.ReceiveReturn.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID))
            ElseIf historyFilter = VO.Item.HistoryFilter.Sales Then
                dtData = DL.Sales.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID)
            ElseIf historyFilter = VO.Item.HistoryFilter.Receive Then
                dtData = DL.Receive.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID)
            ElseIf historyFilter = VO.Item.HistoryFilter.SalesReturn Then
                dtData = DL.SalesReturn.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID)
            ElseIf historyFilter = VO.Item.HistoryFilter.ReceiveReturn Then
                dtData = DL.ReceiveReturn.ListDataHistoryBussinessPartners(dtmDateFrom, dtmDateTo, intItemID)
            End If
            dtData.DefaultView.Sort = "TransactionDate DESC"
            Return dtData
        End Function

        Public Shared Function ListDataHistoryItem(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal historyFilter As VO.BusinessPartner.HistoryFilter, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Dim dtData As New DataTable
            If historyFilter = VO.BusinessPartner.HistoryFilter.All Then
                dtData = DL.Sales.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID)
                dtData.Merge(DL.Receive.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID))
                dtData.Merge(DL.SalesReturn.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID))
                dtData.Merge(DL.ReceiveReturn.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID))
                dtData.Merge(DL.DownPayment.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID, VO.DownPayment.Type.All))
            ElseIf historyFilter = VO.BusinessPartner.HistoryFilter.Sales Then
                dtData = DL.Sales.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID)
            ElseIf historyFilter = VO.BusinessPartner.HistoryFilter.Receive Then
                dtData = DL.Receive.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID)
            ElseIf historyFilter = VO.BusinessPartner.HistoryFilter.SalesReturn Then
                dtData = DL.SalesReturn.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID)
            ElseIf historyFilter = VO.BusinessPartner.HistoryFilter.ReceiveReturn Then
                dtData = DL.ReceiveReturn.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID)
            ElseIf historyFilter = VO.BusinessPartner.HistoryFilter.DownPaymentPurchase Then
                dtData = DL.DownPayment.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID, VO.DownPayment.Type.Purchase)
            ElseIf historyFilter = VO.BusinessPartner.HistoryFilter.DownPaymentSales Then
                dtData = DL.DownPayment.ListDataHistoryItem(dtmDateFrom, dtmDateTo, intBPID, VO.DownPayment.Type.Sales)
            End If
            dtData.DefaultView.Sort = "TransactionDate DESC"
            Return dtData
        End Function

    End Class
End Namespace

