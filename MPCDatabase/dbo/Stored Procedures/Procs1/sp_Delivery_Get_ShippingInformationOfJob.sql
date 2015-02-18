CREATE PROCEDURE dbo.sp_Delivery_Get_ShippingInformationOfJob

	(
		@ItemID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_shippinginformation.*,tbl_customeraddresses.Address1 as AddressInfo  FROM tbl_shippinginformation
        right Outer JOIN tbl_customeraddresses on (tbl_customeraddresses.AddressID=tbl_shippinginformation.AddressID)
		WHERE tbl_shippinginformation.ItemID = @ItemID
	
	RETURN