CREATE PROCEDURE dbo.sp_Delivery_Update_DeliveryDescription

	(
		@DeliveryDetailid int,
		@Description text
	)

AS
	/* SET NOCOUNT ON */
	
	Update tbl_deliverynote_details set Description=@Description where DeliveryDetailid=@DeliveryDetailid
	
	RETURN