Namespace BL

    Public Class Reports

#Region "Accounting"

        Public Shared Function KartuPiutangLastBalance(ByVal dtmDateFrom As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As Decimal
            BL.Server.ServerDefault()
            Return DL.Reports.KartuPiutangLastBalance(dtmDateFrom, intProgramID, intCompanyID, intBPID)
        End Function

        Public Shared Function KartuPiutangVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.KartuPiutangVer00Report(dtmDateFrom, dtmDateTo, intProgramID, intCompanyID, intBPID)
        End Function

        Public Shared Function KartuHutangLastBalance(ByVal dtmDateFrom As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As Decimal
            BL.Server.ServerDefault()
            Return DL.Reports.KartuHutangLastBalance(dtmDateFrom, intProgramID, intCompanyID, intBPID)
        End Function

        Public Shared Function KartuHutangVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.KartuHutangVer00Report(dtmDateFrom, dtmDateTo, intProgramID, intCompanyID, intBPID)
        End Function

        Public Shared Function KartuHutangLastBalancePurchasePrice2(ByVal dtmDateFrom As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As Decimal
            BL.Server.ServerDefault()
            Return DL.Reports.KartuHutangLastBalancePurchasePrice2(dtmDateFrom, intProgramID, intCompanyID, intBPID)
        End Function

        Public Shared Function KartuHutangVer00PurchasePrice2Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.KartuHutangVer00PurchasePrice2Report(dtmDateFrom, dtmDateTo, intProgramID, intCompanyID, intBPID)
        End Function

        Public Shared Function BukuBesarLastBalance(ByVal dtmDateFrom As DateTime, ByVal intCoAID As Integer, ByVal intProgramID As Integer, ByVal intCompanyID As Integer) As Decimal
            BL.Server.ServerDefault()
            Return DL.Reports.BukuBesarLastBalance(dtmDateFrom, intCoAID, intProgramID, intCompanyID)
        End Function

        Public Shared Function BukuBesarVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intCoAID As Integer, ByVal intProgramID As Integer, ByVal intCompanyID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.BukuBesarVer00Report(dtmDateFrom, dtmDateTo, intCoAID, intProgramID, intCompanyID)
        End Function

        Public Shared Function NeracaSaldoVer00Report(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.NeracaSaldoVer00Report(dtmDateFrom, dtmDateTo, intProgramID, intCompanyID)
        End Function

#End Region

        Public Shared Function SalesReport(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                           ByVal strCustomerCode As String, ByVal strSupplierCode As String, ByVal strRemarks As String) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.SalesReport(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, strCustomerCode, strSupplierCode, strRemarks)
        End Function

        Public Shared Function SalesServiceReport(ByVal intCompanyID As Integer, ByVal intProgramID As Integer, ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, _
                                                  ByVal strCustomerCode As String) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.SalesServiceReport(intCompanyID, intProgramID, dtmDateFrom, dtmDateTo, strCustomerCode)
        End Function

        Public Shared Function SalesReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.SalesReturnReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function PurchaseReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.PurchaseReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function PurchaseReturnReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer, ByVal intItemID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.PurchaseReturnReport(dtmDateFrom, dtmDateTo, intBPID, intItemID)
        End Function

        Public Shared Function AccountPayableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.AccountPayableReport(dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Function AccountReceivableReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intBPID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.AccountReceivableReport(dtmDateFrom, dtmDateTo, intBPID)
        End Function

        Public Shared Function CostReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.CostReport(dtmDateFrom, dtmDateTo, intCompanyID, intProgramID)
        End Function

        Public Shared Function JournalReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intCompanyID As Integer, ByVal intProgramID As Integer) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.JournalReport(dtmDateFrom, dtmDateTo, intCompanyID, intProgramID)
        End Function

#Region "Profit and Loss"

        Public Shared Function ListDataPerCOATypeBaseOnBukuBesarReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intCOAType As Integer, Optional ByVal intCOAGroupID As Integer = 0) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtmDateFrom, dtmDateTo, intProgramID, intCompanyID, intCOAType, intCOAGroupID)
        End Function

        Public Shared Function DiscountRevenueReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.DiscountRevenueReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function FirstStockReport(ByVal dtmDateFrom As DateTime) As DataTable
            BL.Server.ServerDefault()
            Return DL.Reports.FirstStockReport(dtmDateFrom)
        End Function

        Public Shared Function PurchaseStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.PurchaseStockReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function LastStockReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.LastStockReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function ExpensesReport(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.ExpensesReport(dtmDateFrom, dtmDateTo)
        End Function

        Public Shared Function CalculateProfitAndLoss(ByVal dtmDateFrom As DateTime, ByVal dtmDateTo As DateTime) As Decimal
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Dim decSalesRevenue As Decimal = 0, decCOGS As Decimal = 0, decOperationalExpenses As Decimal = 0, decOthersRevenue As Decimal = 0, decOthersExpenses As Decimal = 0, decReturn As Decimal = 0
            Dim dtSalesAndRevenue As DataTable = BL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtmDateFrom, dtmDateTo, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, VO.ChartOfAccountType.Values.Pendapatan, VO.ChartOfAccountGroup.Values.PendapatanDanPenjualan)
            Dim dtCOGS As DataTable = BL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtmDateFrom, dtmDateTo, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, VO.ChartOfAccountType.Values.Beban, VO.ChartOfAccountGroup.Values.BebanHPP)
            Dim dtOperationalExpenses As DataTable = BL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtmDateFrom, dtmDateTo, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, VO.ChartOfAccountType.Values.Beban, VO.ChartOfAccountGroup.Values.BebanOperasional)
            Dim dtOthersRevenue As DataTable = BL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtmDateFrom, dtmDateTo, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, VO.ChartOfAccountType.Values.Pendapatan, VO.ChartOfAccountGroup.Values.PendapatanLainLain)
            Dim dtOthersExpenses As DataTable = BL.Reports.ListDataPerCOATypeBaseOnBukuBesarReport(dtmDateFrom, dtmDateTo, MPSLib.UI.usUserApp.ProgramID, MPSLib.UI.usUserApp.CompanyID, VO.ChartOfAccountType.Values.Beban, VO.ChartOfAccountGroup.Values.BebanLainnya)
            
            '# Calculate Sales And Revenue
            For Each dr As DataRow In dtSalesAndRevenue.Rows
                decSalesRevenue += dr.Item("TotalAmount")
            Next
            '# End Of Calculate Sales And Revenue

            '# Calculate COGS
            For Each dr As DataRow In dtCOGS.Rows
                decCOGS += dr.Item("TotalAmount")
            Next
            '# End of Calculate COGS

            '# Calculate Operational Expenses
            For Each dr As DataRow In dtOperationalExpenses.Rows
                decOperationalExpenses += dr.Item("TotalAmount")
            Next
            '# End of Calculate Operational Expenses

            '# Calculate Others Revenue
            For Each dr As DataRow In dtOthersRevenue.Rows
                decOthersRevenue += dr.Item("TotalAmount")
            Next
            '# End of Calculate Others Revenue

            '# Calculate Others Expenses
            For Each dr As DataRow In dtOthersExpenses.Rows
                decOthersExpenses += dr.Item("TotalAmount")
            Next
            '# End of Calculate Others Expenses
            '# Return = (Sales Revenue - COGS) - Operational Expenses + (Others Revenue - Others Expenses)
            decReturn = (decSalesRevenue - decCOGS) - decOperationalExpenses + (decOthersRevenue - decOthersExpenses)
            Return decReturn
        End Function

#End Region

#Region "Balance Sheet"

        Public Shared Function ListDataPerCOATypeBaseOnChartOfAccountReport(ByVal dtmDateTo As DateTime, ByVal intProgramID As Integer, ByVal intCompanyID As Integer, ByVal intCOAType As Integer, Optional ByVal intCOAGroupID As Integer = 0) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Return DL.Reports.ListDataPerCOATypeBaseOnChartOfAccountReport(dtmDateTo, intProgramID, intCompanyID, intCOAType, intCOAGroupID)
        End Function

        Public Shared Function BalanceSheetReport(ByVal dtmDateTo As DateTime) As DataTable
            dtmDateTo = dtmDateTo.AddHours(23).AddMinutes(59).AddSeconds(59)
            BL.Server.ServerDefault()
            Dim dtDebit As DataTable = DL.Reports.BalanceSheetDebitReport(dtmDateTo)
            Dim dtStock As DataTable = DL.Reports.LastStockReport("2000/01/01", dtmDateTo)
            Dim drDebit As DataRow
            If dtStock.Rows.Count = 0 Then
                drDebit = dtDebit.NewRow
                drDebit.BeginEdit()
                drDebit.Item("CoACode") = "104"
                drDebit.Item("CoAName") = "PERSEDIAAN"
                drDebit.Item("DebitAmount") = 0
                drDebit.Item("CreditAmount") = 0
                drDebit.EndEdit()
            Else
                drDebit = dtDebit.NewRow
                drDebit.BeginEdit()
                drDebit.Item("CoACode") = "101"
                drDebit.Item("CoAName") = "PERSEDIAAN"
                drDebit.Item("DebitAmount") = 0
                For Each dr As DataRow In dtStock.Rows
                    drDebit.Item("DebitAmount") += dr.Item("TotalAmount")
                Next
                drDebit.Item("CreditAmount") = 0
                drDebit.EndEdit()
            End If
            dtDebit.Rows.Add(drDebit)
            dtDebit.AcceptChanges()

            Dim dtCredit As DataTable = DL.Reports.BalanceSheetCreditReport(dtmDateTo)
            Dim drCredit As DataRow
            Dim decProfitOrLoss As Decimal = CalculateProfitAndLoss("2000/01/01", dtmDateTo.Date)
            drCredit = dtCredit.NewRow
            drCredit.BeginEdit()
            drCredit.Item("CoACode") = "999"
            drCredit.Item("CoAName") = IIf(decProfitOrLoss > 0, "LABA", "RUGI") & " BULAN BERJALAN"
            drCredit.Item("DebitAmount") = 0
            drCredit.Item("CreditAmount") = decProfitOrLoss
            drCredit.EndEdit()
            dtCredit.Rows.Add(drCredit)
            dtCredit.AcceptChanges()

            Dim dtReturn As New DataTable
            dtReturn.Merge(dtDebit)
            dtReturn.Merge(dtCredit)
            dtReturn.DefaultView.Sort = "CoACode ASC"

            Return dtReturn
        End Function

#End Region

    End Class
End Namespace

