CREATE PROCEDURE dbo.sp_Invoice_Update_InvoiceStatus
	(
		@InvoiceID int,
		@IsArchive smallint,
		@InvoiceStatus int =0
	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_invoices set InvoiceStatus=@InvoiceStatus,IsArchive=@IsArchive where InvoiceID=@InvoiceID
	
	RETURN