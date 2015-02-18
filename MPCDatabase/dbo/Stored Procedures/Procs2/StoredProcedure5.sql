CREATE PROCEDURE dbo.StoredProcedure5

	(
		@DeliveryNoteID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT * FROM tbl_deliverynote_details WHERE tbl_deliverynote_details.DeliveryNoteID = @DeliveryNoteID
	
	RETURN