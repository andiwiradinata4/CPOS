Public Class frmSysBackupDBMS

    Private _
        dtAccess, dtModulesAccess, dtModules, dtProgram, dtProgramModule, _
        dtStatus, dtStatusModules, dtCompany, dtPaymentReferences, dtPaymentTerm, _
        dtItem, dtSalesDiscount, dtUOM, dtUser, dtUserAccess, dtUserCompany As New DataTable

#Region "Functional Handle"

    Private Sub prvSetProgressBar(ByVal intMax As Integer)
        pgMain.Value = 0
        pgMain.Maximum = intMax
    End Sub

    Private Sub prvRefreshProgressBar()
        pgMain.Value += 1
        Me.Refresh()
    End Sub

#Region "Base Master"

    Private Sub prvCollectBaseMaster()
        Dim strDebug As String = ""
        Try
            '# Access
            strDebug = "Get Data Access"
            dtAccess = BL.Access.ListData

            '# Modules
            strDebug = "Get Data Modules"
            dtModules = BL.Modules.ListData

            '# Modules Access
            strDebug = "Get Data Modules Access"
            dtModulesAccess = BL.ModulesAccess.ListDataAll

            '# Program
            strDebug = "Get Data Program"
            dtProgram = BL.Program.ListData

            '# Program Modules
            strDebug = "Get Data Program Modules"
            dtProgram = BL.ProgramModules.ListDataAll
        Catch ex As Exception

        End Try

    End Sub

#End Region

    Private Sub prvProcessBaseMaster()
        prvCollectBaseMaster()
        Try
            Dim strDebug As String = ""
            '# Access
            strDebug = "Process Access"
            prvSetProgressBar(dtAccess.Rows.Count)
            BL.Access.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtAccess.Rows
                Dim clsData As New VO.Access
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.IsDeleted = dr.Item("IsDeleted")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.Access.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Modules
            strDebug = "Process Modules"
            prvSetProgressBar(dtModules.Rows.Count)
            BL.Modules.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtModules.Rows
                Dim clsData As New VO.Modules
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.IsDeleted = dr.Item("IsDeleted")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.Modules.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Modules Access
            strDebug = "Process Modules Access"
            prvSetProgressBar(dtModulesAccess.Rows.Count)
            BL.ModulesAccess.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtModulesAccess.Rows
                Dim clsData As New VO.ModulesAccess
                clsData.ID = dr.Item("ID")
                clsData.ModulesID = dr.Item("ModulesID")
                clsData.AccessID = dr.Item("AccessID")
                BL.ModulesAccess.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Modules Program
            strDebug = "Process Program"
            prvSetProgressBar(dtProgram.Rows.Count)
            BL.Program.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtProgram.Rows
                Dim clsData As New VO.Program
                clsData.ID = dr.Item("ProgramID")
                clsData.Name = dr.Item("ProgramName")
                clsData.IsDeleted = dr.Item("IsDeleted")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.Program.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Program Modules
            strDebug = "Process Program Modules"
            prvSetProgressBar(dtProgramModule.Rows.Count)
            BL.ProgramModules.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtProgramModule.Rows
                Dim clsData As New VO.ProgramModules
                clsData.ID = dr.Item("ID")
                clsData.ModulesID = dr.Item("ModulesID")
                clsData.ProgramID = dr.Item("ProgramID")
                BL.ProgramModules.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Status
            Dim dtStatus As DataTable = BL.Status.ListData
            prvSetProgressBar(dtStatus.Rows.Count)
            BL.Status.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtStatus.Rows
                Dim clsData As New VO.Status
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.IsDeleted = dr.Item("IsDeleted")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.Status.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Status Modules
            Dim dtStatusModules As DataTable = BL.StatusModules.ListDataAll
            prvSetProgressBar(dtStatusModules.Rows.Count)
            BL.StatusModules.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtStatusModules.Rows
                Dim clsData As New VO.StatusModules
                clsData.ID = dr.Item("ID")
                clsData.ModulesID = dr.Item("ModulesID")
                clsData.IDStatus = dr.Item("IDStatus")
                BL.StatusModules.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Company
            Dim dtCompany As DataTable = BL.Company.ListDataAll
            prvSetProgressBar(dtCompany.Rows.Count)
            BL.Company.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtCompany.Rows
                Dim clsData As New VO.Company
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.Address = dr.Item("Address")
                clsData.PhoneNumber = dr.Item("PhoneNumber")
                clsData.CompanyInitial = dr.Item("CompanyInitial")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.Company.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Payment References
            Dim dtPaymentReferences As DataTable = BL.PaymentReferences.ListDataAll
            prvSetProgressBar(dtPaymentReferences.Rows.Count)
            BL.PaymentReferences.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtPaymentReferences.Rows
                Dim clsData As New VO.PaymentReferences
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.PaymentReferences.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Payment Term
            Dim dtPaymentTerm As DataTable = BL.PaymentTerm.ListDataAll
            prvSetProgressBar(dtPaymentTerm.Rows.Count)
            BL.PaymentTerm.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtPaymentTerm.Rows
                Dim clsData As New VO.PaymentTerm
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.PaymentTerm.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Item
            Dim dtItem As DataTable = BL.Item.ListDataAll
            prvSetProgressBar(dtItem.Rows.Count)
            BL.Item.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtItem.Rows
                Dim clsData As New VO.Item
                clsData.ID = dr.Item("ID")
                clsData.Code = dr.Item("Code")
                clsData.Name = dr.Item("Name")
                clsData.UomID = dr.Item("UomID")
                clsData.MinQty = dr.Item("MinQty")
                clsData.BalanceQty = dr.Item("BalanceQty")
                clsData.SalesPrice = dr.Item("SalesPrice")
                clsData.PurchasePrice1 = dr.Item("PurchasePrice1")
                clsData.PurchasePrice2 = dr.Item("PurchasePrice2")
                clsData.Tolerance = dr.Item("Tolerance")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.Item.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Item Status
            '# Not used

            '# Sales Discount
            Dim dtSalesDiscount As DataTable = BL.SalesDiscount.ListDataAll
            prvSetProgressBar(dtSalesDiscount.Rows.Count)
            BL.SalesDiscount.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtSalesDiscount.Rows
                Dim clsData As New VO.SalesDiscount
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.Amount = dr.Item("Amount")
                clsData.StartDate = dr.Item("StartDate")
                clsData.EndDate = dr.Item("EndDate")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.SalesDiscount.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# UOM
            Dim dtUOM As DataTable = BL.UOM.ListDataAll
            prvSetProgressBar(dtUOM.Rows.Count)
            BL.UOM.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtUOM.Rows
                Dim clsData As New VO.UOM
                clsData.ID = dr.Item("ID")
                clsData.Code = dr.Item("Code")
                clsData.Name = dr.Item("Name")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.UOM.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# User
            Dim dtUser As DataTable = BL.User.ListDataAll
            prvSetProgressBar(dtUser.Rows.Count)
            BL.User.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtUser.Rows
                Dim clsData As New VO.User
                clsData.ID = dr.Item("ID")
                clsData.StaffID = dr.Item("StaffID")
                clsData.Name = dr.Item("Name")
                clsData.Password = dr.Item("Password")
                clsData.Position = dr.Item("Position")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                clsData.IsSuperUser = dr.Item("IsSuperUser")
                clsData.IsFirstCreated = dr.Item("IsFirstCreated")
                BL.User.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# User Access
            Dim dtUserAccess As DataTable = BL.UserAccess.ListDataAll
            prvSetProgressBar(dtUserAccess.Rows.Count)
            BL.UserAccess.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtUserAccess.Rows
                Dim clsData As New VO.UserAccess
                clsData.ID = dr.Item("ID")
                clsData.UserID = dr.Item("UserID")
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.ModulesID = dr.Item("ModulesID")
                clsData.AccessID = dr.Item("AccessID")
                BL.UserAccess.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# User Company
            Dim dtUserCompany As DataTable = BL.UserCompany.ListDataAll
            prvSetProgressBar(dtUserCompany.Rows.Count)
            BL.UserCompany.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtUserCompany.Rows
                Dim clsData As New VO.UserCompany
                clsData.ID = dr.Item("ID")
                clsData.UserID = dr.Item("UserID")
                clsData.CompanyID = dr.Item("CompanyID")
                BL.UserCompany.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, "Proses Master Awal")
        End Try
    End Sub

    Private Sub prvProcessChartOfAccount()
        Try
            '# Chart of Account Type
            Dim dtChartOfAccountType As DataTable = BL.ChartOfAccountType.ListData
            prvSetProgressBar(dtChartOfAccountType.Rows.Count)
            BL.ChartOfAccountType.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtChartOfAccountType.Rows
                Dim clsData As New VO.ChartOfAccountType
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.ChartOfAccountType.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Chart of Account Group
            Dim dtChartOfAccountGroup As DataTable = BL.ChartOfAccountGroup.ListDataAll
            prvSetProgressBar(dtChartOfAccountGroup.Rows.Count)
            BL.ChartOfAccountGroup.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtChartOfAccountGroup.Rows
                Dim clsData As New VO.ChartOfAccountGroup
                clsData.ID = dr.Item("ID")
                clsData.Name = dr.Item("Name")
                clsData.AliasName = dr.Item("AliasName")
                clsData.COAType = dr.Item("COAType")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.ChartOfAccountGroup.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Chart of Account
            Dim dtChartOfAccount As DataTable = BL.ChartOfAccount.ListDataAll
            prvSetProgressBar(dtChartOfAccount.Rows.Count)
            BL.ChartOfAccount.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtChartOfAccount.Rows
                Dim clsData As New VO.ChartOfAccount
                clsData.ID = dr.Item("ID")
                clsData.AccountGroupID = dr.Item("AccountGroupID")
                clsData.Code = dr.Item("Code")
                clsData.Name = dr.Item("Name")
                clsData.FirstBalance = dr.Item("FirstBalance")
                clsData.FirstBalanceDate = dr.Item("FirstBalanceDate")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.ChartOfAccount.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Chart of Account Assign
            Dim dtChartOfAccountAssign As DataTable = BL.ChartOfAccountAssign.ListDataAll
            prvSetProgressBar(dtChartOfAccountAssign.Rows.Count)
            BL.ChartOfAccountAssign.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtChartOfAccountAssign.Rows
                Dim clsData As New VO.ChartOfAccountAssign
                clsData.ID = dr.Item("ID")
                clsData.COAID = dr.Item("COAID")
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.CompanyID = dr.Item("CompanyID")
                clsData.FirstBalance = dr.Item("FirstBalance")
                clsData.FirstBalanceDate = dr.Item("FirstBalanceDate")
                BL.ChartOfAccountAssign.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, "Proses Akun Perkiraan")
        End Try
    End Sub

    Private Sub prvProcessBusinessPartner()
        Try
            '# Business Partner
            Dim dtBusinessPartner As DataTable = BL.BusinessPartner.ListDataAll
            prvSetProgressBar(dtBusinessPartner.Rows.Count)
            BL.BusinessPartner.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtBusinessPartner.Rows
                Dim clsData As New VO.BusinessPartner
                clsData.ID = dr.Item("ID")
                clsData.Code = dr.Item("Code")
                clsData.Name = dr.Item("Name")
                clsData.Address = dr.Item("Address")
                clsData.PICName = dr.Item("PICName")
                clsData.PICPhoneNumber = dr.Item("PICPhoneNumber")
                clsData.PaymentTermID = dr.Item("PaymentTermID")
                clsData.IsUsePurchaseLimit = dr.Item("IsUsePurchaseLimit")
                clsData.MaxPurchaseLimit = dr.Item("MaxPurchaseLimit")
                clsData.APBalance = dr.Item("APBalance")
                clsData.ARBalance = dr.Item("ARBalance")
                clsData.SalesPrice = dr.Item("SalesPrice")
                clsData.PurchasePrice1 = dr.Item("PurchasePrice1")
                clsData.PurchasePrice2 = dr.Item("PurchasePrice2")
                clsData.IDStatus = dr.Item("IDStatus")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                clsData.LogInc = dr.Item("LogInc")
                BL.BusinessPartner.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Business Partner Status
            Dim dtBusinessPartnerStatus As DataTable = BL.BusinessPartner.ListDataAllStatus
            prvSetProgressBar(dtBusinessPartnerStatus.Rows.Count)
            BL.BusinessPartner.DeleteDataAllStatus(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtBusinessPartnerStatus.Rows
                Dim clsData As New VO.BusinessPartnerStatus
                clsData.ID = dr.Item("ID")
                clsData.BPID = dr.Item("BPID")
                clsData.Status = dr.Item("Status")
                clsData.StatusBy = dr.Item("StatusBy")
                clsData.StatusDate = dr.Item("StatusDate")
                clsData.Remarks = dr.Item("Remarks")
                BL.BusinessPartner.SaveDataAllStatus(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Business Partner Price
            Dim dtBusinessPartnerPrice As DataTable = BL.BusinessPartner.ListDataAllPrice
            prvSetProgressBar(dtBusinessPartnerPrice.Rows.Count)
            BL.BusinessPartner.DeleteDataAllPrice(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtBusinessPartnerPrice.Rows
                Dim clsData As New VO.BusinessPartnerPrice
                clsData.ID = dr.Item("ID")
                clsData.BPID = dr.Item("BPID")
                clsData.DateFrom = dr.Item("DateFrom")
                clsData.DateTo = dr.Item("DateTo")
                clsData.SalesPrice = dr.Item("SalesPrice")
                clsData.PurchasePrice1 = dr.Item("PurchasePrice1")
                clsData.PurchasePrice2 = dr.Item("PurchasePrice2")
                BL.BusinessPartner.SaveDataAllPrice(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# Business Partner Assign
            Dim dtBusinessPartnerAssign As DataTable = BL.BusinessPartnerAssign.ListDataAll
            prvSetProgressBar(dtBusinessPartnerAssign.Rows.Count)
            BL.BusinessPartnerAssign.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtBusinessPartnerAssign.Rows
                Dim clsData As New VO.BusinessPartnerAssign
                clsData.CompanyID = dr.Item("CompanyID")
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.ID = dr.Item("ID")
                clsData.BPID = dr.Item("BPID")
                clsData.APBalance = dr.Item("APBalance")
                clsData.ARBalance = dr.Item("ARBalance")
                BL.BusinessPartnerAssign.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, "Proses Rekan Bisnis")
        End Try
    End Sub

    Private Sub prvProcessSystem()
        Try
            '# System Journal Post
            Dim dtJournalPost As DataTable = BL.JournalPost.ListDataAll
            prvSetProgressBar(dtJournalPost.Rows.Count)
            BL.JournalPost.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtJournalPost.Rows
                Dim clsData As New VO.JournalPost
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.CoAofRevenue = dr.Item("CoAofRevenue")
                clsData.CoAofAccountReceivable = dr.Item("CoAofAccountReceivable")
                clsData.CoAofSalesDisc = dr.Item("CoAofSalesDisc")
                clsData.CoAofPrepaidIncome = dr.Item("CoAofPrepaidIncome")
                clsData.CoAofCOGS = dr.Item("CoAofCOGS")
                clsData.CoAofStock = dr.Item("CoAofStock")
                clsData.CoAofCash = dr.Item("CoAofCash")
                clsData.CoAofAccountPayable = dr.Item("CoAofAccountPayable")
                clsData.CoAofPurchaseDisc = dr.Item("CoAofPurchaseDisc")
                clsData.CoAofPurchaseEquipments = dr.Item("CoAofPurchaseEquipments")
                clsData.CoAofAdvancePayment = dr.Item("CoAofAdvancePayment")
                clsData.CoAofSalesTax = dr.Item("CoAofSalesTax")
                clsData.CoAofPurchaseTax = dr.Item("CoAofPurchaseTax")
                clsData.Remarks = dr.Item("Remarks")
                clsData.CreatedBy = dr.Item("CreatedBy")
                clsData.CreatedDate = dr.Item("CreatedDate")
                clsData.LogInc = dr.Item("LogInc")
                clsData.LogBy = dr.Item("LogBy")
                clsData.LogDate = dr.Item("LogDate")
                BL.JournalPost.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# System Post GL
            Dim dtPostGL As DataTable = BL.PostGL.ListDataAll
            prvSetProgressBar(dtPostGL.Rows.Count)
            BL.PostGL.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtPostGL.Rows
                Dim clsData As New VO.PostGL
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.CompanyID = dr.Item("CompanyID")
                clsData.ID = dr.Item("ID")
                clsData.DateFrom = dr.Item("DateFrom")
                clsData.DateTo = dr.Item("DateTo")
                clsData.PostedBy = dr.Item("PostedBy")
                clsData.PostedDate = dr.Item("PostedDate")
                BL.PostGL.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# System Stock IN
            Dim dtStockIN As DataTable = BL.StockIN.ListDataAll
            prvSetProgressBar(dtStockIN.Rows.Count)
            BL.StockIN.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtStockIN.Rows
                Dim clsData As New VO.StockIN
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.CompanyID = dr.Item("CompanyID")
                clsData.ID = dr.Item("ID")
                clsData.ItemID = dr.Item("ItemID")
                clsData.ReferencesID = dr.Item("ReferencesID")
                clsData.ReferencesDate = dr.Item("ReferencesDate")
                clsData.Qty = dr.Item("Qty")
                clsData.Price = dr.Item("Price")
                clsData.NetPrice = dr.Item("NetPrice")
                clsData.QtyOut = dr.Item("QtyOut")
                clsData.LogDate = dr.Item("LogDate")
                BL.StockIN.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next

            '# System Stock Out
            Dim dtStockOut As DataTable = BL.StockOut.ListDataAll
            prvSetProgressBar(dtStockOut.Rows.Count)
            BL.StockOut.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            For Each dr As DataRow In dtStockOut.Rows
                Dim clsData As New VO.StockOut
                clsData.ProgramID = dr.Item("ProgramID")
                clsData.CompanyID = dr.Item("CompanyID")
                clsData.ID = dr.Item("ID")
                clsData.IDStockIN = dr.Item("IDStockIN")
                clsData.ItemID = dr.Item("ItemID")
                clsData.ReferencesID = dr.Item("ReferencesID")
                clsData.ReferencesDate = dr.Item("ReferencesDate")
                clsData.Qty = dr.Item("Qty")
                clsData.LogDate = dr.Item("LogDate")
                BL.StockOut.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
                prvRefreshProgressBar()
            Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, "Proses Akun Perkiraan")
        End Try
    End Sub

    Private Sub prvProcessTransaction()
        Dim strDebug As String = "Proses Transaksi"
        Try
            '# Teansaction Account Payable
            strDebug = "Proses Transaksi Account Payable"
            Dim dtAP As DataTable = BL.AccountPayable.ListDataAll
            prvSetProgressBar(dtAP.Rows.Count)
            'BL.AccountPayable.DeleteDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
            'For Each dr As DataRow In dtAP.Rows
            '    Dim clsData As New VO.JournalPost
            '    clsData.ProgramID = dr.Item("ProgramID")
            '    clsData.CoAofRevenue = dr.Item("CoAofRevenue")
            '    clsData.CoAofAccountReceivable = dr.Item("CoAofAccountReceivable")
            '    clsData.CoAofSalesDisc = dr.Item("CoAofSalesDisc")
            '    clsData.CoAofPrepaidIncome = dr.Item("CoAofPrepaidIncome")
            '    clsData.CoAofCOGS = dr.Item("CoAofCOGS")
            '    clsData.CoAofStock = dr.Item("CoAofStock")
            '    clsData.CoAofCash = dr.Item("CoAofCash")
            '    clsData.CoAofAccountPayable = dr.Item("CoAofAccountPayable")
            '    clsData.CoAofPurchaseDisc = dr.Item("CoAofPurchaseDisc")
            '    clsData.CoAofPurchaseEquipments = dr.Item("CoAofPurchaseEquipments")
            '    clsData.CoAofAdvancePayment = dr.Item("CoAofAdvancePayment")
            '    clsData.CoAofSalesTax = dr.Item("CoAofSalesTax")
            '    clsData.CoAofPurchaseTax = dr.Item("CoAofPurchaseTax")
            '    clsData.Remarks = dr.Item("Remarks")
            '    clsData.CreatedBy = dr.Item("CreatedBy")
            '    clsData.CreatedDate = dr.Item("CreatedDate")
            '    clsData.LogInc = dr.Item("LogInc")
            '    clsData.LogBy = dr.Item("LogBy")
            '    clsData.LogDate = dr.Item("LogDate")
            '    BL.JournalPost.SaveDataAll(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim, clsData)
            '    prvRefreshProgressBar()
            'Next
        Catch ex As Exception
            UI.usForm.frmMessageBox(ex.Message, strDebug)
        End Try
    End Sub

    Private Sub prvProcess()

        'BL.Server.SetServer(txtServerTo.Text.Trim, txtDBMSTo.Text.Trim, txtUserID.Text.Trim, txtPassword.Text.Trim)
        Try
            'DL.SQL.OpenConnection()
            'DL.SQL.BeginTransaction()

            'prvProcessBaseMaster()
            'prvProcessChartOfAccount()
            'prvProcessBusinessPartner()
            'prvProcessSystem()
            prvProcessTransaction()

            DL.SQL.CommitTransaction()

            UI.usForm.frmMessageBox("Backup Database Berhasil!")
        Catch ex As Exception
            DL.SQL.RollBackTransaction()
            Throw ex
        Finally
            DL.SQL.CloseConnection()
        End Try

    End Sub

#End Region

#Region "Form Handle"

    Private Sub frmSysBackupDBMS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UI.usForm.SetIcon(Me, "MyLogo")
        ToolBar.SetIcon(Me)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub ToolBar_ButtonClick(sender As Object, e As ToolBarButtonClickEventArgs) Handles ToolBar.ButtonClick
        Select Case e.Button.Text.Trim
            Case "Proses" : prvProcess()
            Case "Tutup" : Me.Close()
        End Select
    End Sub

#End Region

End Class