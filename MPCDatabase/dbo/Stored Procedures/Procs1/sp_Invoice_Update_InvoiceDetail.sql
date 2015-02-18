CREATE PROCEDURE dbo.sp_Invoice_Update_InvoiceDetail

	AS
	/* SET NOCOUNT ON */
	
	select * from tbl_invoicedetails where tbl_invoicedetails.InvoiceID=0
	
	RETURN