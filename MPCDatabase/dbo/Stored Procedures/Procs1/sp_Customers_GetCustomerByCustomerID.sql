CREATE PROCEDURE [dbo].[sp_Customers_GetCustomerByCustomerID]

	(
		@CustomerID int
	)

AS
	SELECT tbl_contactCompanies.ContactCompanyID,tbl_contactCompanies.AccountNumber,
        tbl_contactCompanies.Name,tbl_contactCompanies.URL,tbl_contactCompanies.CreditReference,tbl_contactCompanies.CreditLimit,
         tbl_contactCompanies.Terms,tbl_contactCompanies.TypeID,tbl_contactCompanies.DefaultNominalCode,
         tbl_contactCompanies.DefaultTill,tbl_contactCompanies.DefaultMarkUpID,tbl_contactCompanies.AccountOpenDate,tbl_contactCompanies.AccountManagerID,
         tbl_Statuses.StatusName as CustomerStatus,tbl_contactCompanies.IsCustomer,tbl_contactCompanies.Notes,tbl_contactCompanies.ISBN,tbl_contactCompanies.NotesLastUpdatedDate,
         tbl_contactCompanies.NotesLastUpdatedBy,tbl_contactCompanies.AccountOnHandDesc,tbl_contactCompanies.AccountStatusID,tbl_contactCompanies.IsDisabled,
         tbl_contactCompanies.LockedBy,tbl_contactCompanies.AccountBalance,tbl_contactCompanies.CreationDate,tbl_contactCompanies.VATRegNumber,
         tbl_contactCompanies.IsParaentCompany,tbl_contactCompanies.ParaentCompanyID,tbl_contactCompanies.SystemSiteID,tbl_contactCompanies.VATRegReference,FlagID,
         tbl_contactCompanies.Image,tbl_contactCompanies.IsEmailSubscription,tbl_contactCompanies.IsMailSubscription,tbl_contactCompanies.IsEmailFormat,
         tbl_contactCompanies.HomeContact,tbl_contactCompanies.AbountUs,tbl_contactCompanies.ContactUs,
         tbl_contactCompanies.IsGeneral,tbl_contactCompanies.IsShowFinishedGoodPrices,SalesPerson as CustomerSalesPerson
          FROM tbl_contactCompanies 
          LEFT OUTER join tbl_Statuses on (tbl_Statuses.statusid = tbl_Contactcompanies.status)
          WHERE (ContactCompanyID = @CustomerID)

	RETURN