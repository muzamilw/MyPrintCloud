CREATE PROCEDURE [dbo].[sp_Customers_WebADmin_GetWebAdminByCustomerID]

	(
		@CustomerID int
			
	)
AS
	SELECT tbl_contactcompanies.WebAccessAdminUserName,
       tbl_contactcompanies.WebAccessAdminPasswordHint 
       FROM tbl_contactcompanies 
       WHERE tbl_contactcompanies.ContactCompanyID=@CustomerID

	RETURN