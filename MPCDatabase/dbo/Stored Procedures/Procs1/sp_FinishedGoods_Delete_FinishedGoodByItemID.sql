CREATE PROCEDURE dbo.sp_FinishedGoods_Delete_FinishedGoodByItemID

	(
		@ItemID	int
	)

AS
	/* SET NOCOUNT ON */
	
	DELETE from tbl_finishedgoods where ItemID=@ItemID
	
	RETURN