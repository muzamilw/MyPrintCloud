CREATE PROCEDURE dbo.sp_Delivery_Insert_ShippingInformation

	(
		@ItemID int,
		@AddressID int,
		@Quantity int,
		@Price float,		
		@DeliveryDate datetime,
		@Raised bit
	)

AS	
	
	insert into tbl_shippinginformation (ItemID,AddressID,Quantity,Price,DeliveryNoteRaised,DeliveryDate)
        VALUES
        (@ItemID,@AddressID,@Quantity,@Price,@Raised,@DeliveryDate);select @@Identity as ShippingID	
	RETURN