CREATE PROCEDURE dbo.sp_Delivery_Update_ShippingInformation

	(
		@ID int,
		@ItemID int,
		@AddressID int,
		@Quantity int,
		@Price float,		
		@DeliveryDate datetime,
		@Raised bit
	)

AS	
	
	update tbl_shippinginformation set ItemID=@ItemID, AddressID = @AddressID,Quantity = @Quantity,Price = @Price, DeliveryDate = @DeliveryDate, DeliveryNoteRaised=@Raised where ShippingID=@ID
        
        
	RETURN