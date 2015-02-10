CREATE PROCEDURE [dbo].[sp_Delivery_Update_DeliveryNoteDetail]

	(
		@ItemQty int,
		@ItemGrossTotal float,
		@DeliveryNoteID int,
		@Description text,
		@ItemId int,
		@DeliveryDetailid int
	)

AS
	/* SET NOCOUNT ON */
	UPDATE    tbl_deliverynote_details
SET               DeliveryNoteID =@DeliveryNoteID, Description =@Description, ItemId =@ItemId, ItemQty =@ItemQty, GrossItemTotal =@ItemGrossTotal  where DeliveryDetailid =@DeliveryDetailid
	
	
	RETURN