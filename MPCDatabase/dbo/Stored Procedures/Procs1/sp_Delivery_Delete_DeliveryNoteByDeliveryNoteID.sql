
Create PROCEDURE [dbo].[sp_Delivery_Delete_DeliveryNoteByDeliveryNoteID]

	(
		@DeliveryID int
	)

AS
	/* SET NOCOUNT ON */
	
	delete from tbl_deliverynotes where DeliveryNoteID=@DeliveryID
	
	RETURN