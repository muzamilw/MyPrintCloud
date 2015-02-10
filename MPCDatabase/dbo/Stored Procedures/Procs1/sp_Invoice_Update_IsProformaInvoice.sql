CREATE PROCEDURE dbo.sp_Invoice_Update_IsProformaInvoice
@IsProformaInvoice bit,
@InvoiceID int
AS
	update tbl_invoices set IsProformaInvoice=@IsProformaInvoice where InvoiceID=@InvoiceID
	RETURN