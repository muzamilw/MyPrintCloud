CREATE PROCEDURE dbo.sp_Customer_IsContactUsed_check_in_Invoices
	(
		@ContactID int
	)

AS
	SELECT tbl_invoices.ContactID
         FROM tbl_invoices WHERE tbl_invoices.ContactID=@ContactID
         
	RETURN