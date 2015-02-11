CREATE PROCEDURE dbo.sp_Invoice_Detail_Update_Flag

	(
		@InvoiceDetailID int,
		@FlagID int
	)

AS
	/* SET NOCOUNT ON */
	
	Update tbl_invoicedetails set FlagID=@FlagID where InvoiceDetailID=@InvoiceDetailID
	
	RETURN