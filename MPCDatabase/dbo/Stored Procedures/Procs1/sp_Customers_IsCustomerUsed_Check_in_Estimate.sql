
Create PROCEDURE [dbo].[sp_Customers_IsCustomerUsed_Check_in_Estimate]

	(
		@Customer_ID int
	)
AS
	SELECT tbl_estimates.ContactCompanyID
         FROM tbl_estimates WHERE tbl_estimates.ContactCompanyID=@Customer_ID
	
	RETURN