Create PROCEDURE [dbo].[sp_Customers_Customers_GetCustomerDByContactCompanyID]

	(
       @ContactCompanyID int
	)

AS
	SELECT tbl_ContactCompanies.ContactCompanyID,tbl_ContactCompanies.AccountNumber,
       tbl_ContactCompanies.Name,tbl_ContactCompanies.URL,tbl_ContactCompanies.CreditReference,tbl_ContactCompanies.CreditLimit,
       tbl_ContactCompanies.Terms,tbl_ContactCompanies.TypeID,tbl_ContactCompanies.DefaultNominalCode,
       tbl_ContactCompanies.DefaultTill,tbl_ContactCompanies.DefaultMarkUpID,tbl_ContactCompanies.AccountOpenDate,tbl_ContactCompanies.AccountManagerID,
       tbl_ContactCompanies.Status,tbl_ContactCompanies.IsCustomer,tbl_ContactCompanies.Notes,tbl_ContactCompanies.ISBN,tbl_ContactCompanies.NotesLastUpdatedDate,
       tbl_ContactCompanies.NotesLastUpdatedBy,tbl_ContactCompanies.AccountOnHandDesc,tbl_ContactCompanies.AccountStatusID,tbl_ContactCompanies.IsDisabled,
       tbl_ContactCompanies.LockedBy,tbl_ContactCompanies.AccountBalance,tbl_ContactCompanies.CreationDate,tbl_ContactCompanies.VATRegNumber,
       tbl_ContactCompanies.IsParaentCompany,tbl_ContactCompanies.ParaentCompanyID,tbl_ContactCompanies.SystemSiteID,tbl_ContactCompanies.VATRegReference,FlagID,
       tbl_ContactCompanies.Image,tbl_ContactCompanies.IsEmailSubscription,tbl_ContactCompanies.IsMailSubscription,tbl_ContactCompanies.IsEmailFormat,
       --tbl_ContactCompanies.IsAllowWebAccess,
       tbl_ContactCompanies.HomeContact,tbl_ContactCompanies.AbountUs,tbl_ContactCompanies.ContactUs,
       tbl_ContactCompanies.IsGeneral,tbl_ContactCompanies.IsShowFinishedGoodPrices,tbl_ContactCompanies.DepartmentID,tbl_Addresses.Tel1, tbl_Addresses.Address1 
       ,SalesPerson
       FROM tbl_ContactCompanies 
       INNER JOIN tbl_Addresses ON tbl_ContactCompanies.ContactCompanyID = tbl_Addresses.ContactCompanyID 
       WHERE (tbl_ContactCompanies.ContactCompanyID = @ContactCompanyID and tbl_Addresses.IsDefaultAddress<>0)
	RETURN