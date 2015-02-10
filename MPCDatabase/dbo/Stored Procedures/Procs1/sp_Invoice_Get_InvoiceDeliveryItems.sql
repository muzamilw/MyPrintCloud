CREATE PROCEDURE dbo.sp_Invoice_Get_InvoiceDeliveryItems

	(
		@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_items.ItemID,tbl_items.ItemCode,tbl_items.Title,
         (case when tbl_items.JobSelectedQty=1 then tbl_items.Qty1 when tbl_items.JobSelectedQty=2 then tbl_items.Qty2 else tbl_items.Qty3 end)       as Quantity, 
         (case when tbl_items.JobSelectedQty=1 then tbl_items.Qty1GrossTotal when tbl_items.JobSelectedQty=2 then tbl_items.Qty2grossTotal else tbl_items.Qty3GrossTotal end)       as TotalPrice, 
         dbo.Fnc_Items_getEstimateDescription(tbl_items.ItemID) as  EstimateDescription 
         FROM tbl_items 
         Inner Join  tbl_invoicedetails on (tbl_invoicedetails.ItemID = tbl_items.ItemID) 
         WHERE  tbl_invoicedetails.InvoiceID = @InvoiceID
	
	RETURN