CREATE PROCEDURE dbo.sp_Delivery_Delete_ShippingInformationByItemID
	@ItemID int
AS
	Delete from tbl_shippinginformation where ItemID=@ItemID
	RETURN