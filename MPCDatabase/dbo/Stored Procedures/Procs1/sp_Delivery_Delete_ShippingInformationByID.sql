CREATE PROCEDURE dbo.sp_Delivery_Delete_ShippingInformationByID
	@ID int
AS
	Delete from tbl_shippinginformation where ShippingID=@ID
	RETURN