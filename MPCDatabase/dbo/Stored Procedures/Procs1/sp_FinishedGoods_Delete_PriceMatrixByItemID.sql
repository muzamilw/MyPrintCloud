CREATE PROCEDURE dbo.sp_FinishedGoods_Delete_PriceMatrixByItemID

	(
		@ItemID int
	)

AS
	/* SET NOCOUNT ON */
	
	DELETE from tbl_finishedgoodpricematrix where ItemID=@ItemID
	
	RETURN