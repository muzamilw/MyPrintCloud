CREATE PROCEDURE [dbo].[sp_Suppliers_Supplier_GetLastInsertedSupplier]
	
AS
	Select Max(ContactCompanyID) as ContactCompanyID FROM tbl_ContactCompanies
	
	
	RETURN