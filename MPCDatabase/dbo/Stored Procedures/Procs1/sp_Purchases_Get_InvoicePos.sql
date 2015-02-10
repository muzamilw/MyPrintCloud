CREATE PROCEDURE [dbo].[sp_Purchases_Get_InvoicePos]

	(
		@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_purchase.PurchaseID,tbl_purchase.Code,tbl_contactcompanies.Name ,tbl_purchase.GrandTotal
        FROM tbl_purchase 
        INNER JOIN tbl_contactcompanies ON (tbl_purchase.SupplierID = tbl_contactcompanies.contactcompanyid) 
        INNER JOIN tbl_invoicedetails ON (tbl_purchase.JobID = tbl_invoicedetails.ItemID and tbl_invoicedetails.ItemID > 0 ) 
        WHERE tbl_contactcompanies.iscustomer = 4 and tbl_invoicedetails.InvoiceID =  @InvoiceID
	
	RETURN