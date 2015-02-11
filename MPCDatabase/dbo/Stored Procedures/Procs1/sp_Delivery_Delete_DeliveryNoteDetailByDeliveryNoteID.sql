CREATE PROCEDURE dbo.sp_Delivery_Delete_DeliveryNoteDetailByDeliveryNoteID

	(
		@DeliveryID int
	)

AS
	/* SET NOCOUNT ON */
	
	delete from tbl_deliverynote_details where DeliveryNoteID=@DeliveryID
	
	RETURN