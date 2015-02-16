CREATE PROCEDURE dbo.sp_Invoice_Update_Flag

	(
		@InvoiceID int,
		@FlagID int
	)

AS
	/* SET NOCOUNT ON */
	
	Update tbl_invoices set FlagID=@FlagID where InvoiceID=@InvoiceID
	
	RETURN