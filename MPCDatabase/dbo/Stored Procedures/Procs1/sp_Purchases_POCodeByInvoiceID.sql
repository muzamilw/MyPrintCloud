CREATE PROCEDURE dbo.sp_Purchases_POCodeByInvoiceID

	(
		@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	SELECT			tbl_purchase.Code
	FROM			tbl_invoices 
	INNER JOIN      tbl_invoicedetails ON tbl_invoices.InvoiceID = tbl_invoicedetails.InvoiceID 
	INNER JOIN      tbl_purchase ON tbl_invoicedetails.ItemID = tbl_purchase.JobID
	where tbl_Invoices.InvoiceID=@InvoiceID
	
	RETURN