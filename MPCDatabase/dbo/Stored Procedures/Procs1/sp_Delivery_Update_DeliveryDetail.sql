CREATE PROCEDURE dbo.sp_Delivery_Update_DeliveryDetail
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	/* SET NOCOUNT ON */
	
	SELECT * FROM tbl_deliverynote_details WHERE tbl_deliverynote_details.DeliveryDetailid = 0
	
	RETURN