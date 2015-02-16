CREATE PROCEDURE dbo.sp_Invoice_Delete_Invoice

	(
		@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	DELETE FROM tbl_invoices where tbl_invoices.InvoiceID=@InvoiceID
	
	RETURN