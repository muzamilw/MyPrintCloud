CREATE PROCEDURE [dbo].[sp_Delivery_ShippingInformationByInvoiceID]

	(
@InvoiceID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_shippinginformation.*,tbl_addresses.Address1 as AddressInfo  FROM tbl_shippinginformation
        INNER JOIN tbl_items ON (tbl_shippinginformation.ItemID = tbl_items.ItemID)
        INNER JOIN tbl_InvoiceDetails ON (tbl_InvoiceDetails.ItemID = tbl_items.ItemID)
        right Outer JOIN tbl_addresses on (tbl_addresses.AddressID=tbl_shippinginformation.AddressID)
        WHERE tbl_InvoiceDetails.InvoiceID = @InvoiceID
	
	RETURN