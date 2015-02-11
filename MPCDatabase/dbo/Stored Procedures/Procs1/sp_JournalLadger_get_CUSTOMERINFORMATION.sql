CREATE PROCEDURE [dbo].[sp_JournalLadger_get_CUSTOMERINFORMATION]
		
	@CustomerID as int

AS

	SELECT     tbl_ContactCompanies.ContactCompanyID, tbl_ContactCompanies.AccountNumber, tbl_ContactCompanies.Name, tbl_ContactCompanies.AccountBalance, tbl_ContactCompanies.CreditLimit, 
                      tbl_ContactCompanies.DefaultNominalCode, tbl_Addresses.AddressName, tbl_Addresses.Address1, tbl_Addresses.Address2, 
                      tbl_Addresses.Address3, tbl_Addresses.City, tbl_Addresses.Country, tbl_Addresses.State
FROM         tbl_ContactCompanies INNER JOIN
                      tbl_Addresses ON tbl_ContactCompanies.ContactCompanyID = tbl_Addresses.ContactCompanyID 
                      where tbl_ContactCompanies.ContactCompanyID=@CustomerID
					
RETURN