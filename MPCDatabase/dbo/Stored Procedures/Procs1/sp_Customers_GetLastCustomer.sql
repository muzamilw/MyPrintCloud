CREATE PROCEDURE [dbo].[sp_Customers_GetLastCustomer]


AS
	Select Max(ContactCompanyID) as ContactCompanyID from tbl_contactcompanies
	
	RETURN