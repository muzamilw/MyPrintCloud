CREATE PROCEDURE [dbo].[sp_Delivery_Add_DeliveryNoteDetail]

	(
		@ItemQty int,
		@ItemGrossTotal float,
		@DeliveryNoteID int,
		@Description text,
		@ItemId int
	)

AS
	/* SET NOCOUNT ON */
	
	Insert into tbl_deliverynote_details (ItemQty,GrossItemTotal,DeliveryNoteID,Description,ItemId) 
	VALUES(@ItemQty,@ItemGrossTotal,@DeliveryNoteID,@Description,@ItemId)
	
	RETURN