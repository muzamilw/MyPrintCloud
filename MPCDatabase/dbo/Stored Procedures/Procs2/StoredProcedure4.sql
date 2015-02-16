CREATE PROCEDURE dbo.StoredProcedure4
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