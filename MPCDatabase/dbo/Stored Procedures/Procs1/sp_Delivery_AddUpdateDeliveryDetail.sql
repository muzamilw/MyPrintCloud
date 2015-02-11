CREATE PROCEDURE dbo.sp_Delivery_AddUpdateDeliveryDetail

	(
		@DeliveryNoteID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT * FROM tbl_deliverynote_details WHERE tbl_deliverynote_details.DeliveryNoteID = @DeliveryNoteID
	
	RETURN