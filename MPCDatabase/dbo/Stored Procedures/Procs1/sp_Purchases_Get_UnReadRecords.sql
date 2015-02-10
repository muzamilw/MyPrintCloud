CREATE PROCEDURE dbo.sp_Purchases_Get_UnReadRecords
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT PurchaseID,Status FROM tbl_purchase WHERE (IsRead = 0)
	
	RETURN