CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_GetSupplierBySupplierID]

	(
		@SupplierID  int
	)

AS
	SELECT tbl_ContactCompanies.*,
       tbl_Addresses.Address1, tbl_Addresses.Tel1
       FROM tbl_ContactCompanies INNER JOIN tbl_Addresses ON tbl_ContactCompanies.ContactCompanyID = tbl_Addresses.ContactCompanyID WHERE (tbl_ContactCompanies.ContactCompanyID = @SupplierID )
	RETURN