CREATE PROCEDURE [dbo].[sp_Customers_CustomerTypes_IsCustomerTypeUsed]

	(
		@CustomerTypeID int
			
	)
	as
	
	SELECT tbl_ContactCompanies.TypeID FROM tbl_ContactCompanies WHERE tbl_ContactCompanies.TypeID=@CustomerTypeID

	RETURN