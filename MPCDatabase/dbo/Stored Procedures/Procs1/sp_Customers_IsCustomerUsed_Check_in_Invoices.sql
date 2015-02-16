
Create PROCEDURE [dbo].[sp_Customers_IsCustomerUsed_Check_in_Invoices]

	(
		@CustomerID int
	)
AS
	SELECT tbl_invoices.ContactCompanyID
         FROM tbl_invoices WHERE tbl_invoices.ContactCompanyID=@CustomerID
         
	RETURN