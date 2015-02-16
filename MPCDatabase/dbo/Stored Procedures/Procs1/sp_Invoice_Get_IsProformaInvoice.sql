CREATE PROCEDURE dbo.sp_Invoice_Get_IsProformaInvoice

	(
		@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT IsProformaInvoice from tbl_invoices
         WHERE  tbl_invoices.InvoiceID = @InvoiceID
	
	RETURN