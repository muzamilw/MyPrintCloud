CREATE PROCEDURE dbo.StoredProcedure6

	(
		@DeliveryDetailid int,
		@Description text
	)

AS
	/* SET NOCOUNT ON */
	
	Update tbl_deliverynote_details set Description=@Description where DeliveryDetailid=@DeliveryDetailid
	
	RETURN