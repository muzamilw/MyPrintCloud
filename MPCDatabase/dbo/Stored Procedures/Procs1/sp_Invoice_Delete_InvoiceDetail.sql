CREATE PROCEDURE dbo.sp_Invoice_Delete_InvoiceDetail

	(
	@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	DELETE FROM tbl_invoicedetails where tbl_invoicedetails.InvoiceID=@InvoiceID
	
	RETURN