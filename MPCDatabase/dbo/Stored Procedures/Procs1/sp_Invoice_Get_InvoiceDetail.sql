CREATE PROCEDURE dbo.sp_Invoice_Get_InvoiceDetail

	(
@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_invoicedetails.*, tbl_items.EstimateID AS EstimateID,tbl_items.Title,tbl_items.Status,tbl_items.FlagID
	FROM tbl_invoicedetails 
	LEFT OUTER JOIN tbl_items ON tbl_invoicedetails.ItemID = tbl_items.ItemID where tbl_invoicedetails.InvoiceID=@InvoiceID
	
	RETURN