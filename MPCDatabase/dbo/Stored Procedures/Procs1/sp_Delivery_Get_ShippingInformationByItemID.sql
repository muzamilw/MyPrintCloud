CREATE PROCEDURE dbo.sp_Delivery_Get_ShippingInformationByItemID
	@ItemID int
AS
	SELECT ShippingID, ItemID, AddressID, Quantity, Price,DeliveryDate
	FROM tbl_shippinginformation
WHERE (ItemID = @ItemID)
	RETURN