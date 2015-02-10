CREATE PROCEDURE dbo.sp_FinishedGoods_Get_PriceMatrixByItemID

	(
		@ItemID int
	)

AS
	/* SET NOCOUNT ON */
	
	select * from tbl_finishedgoodpricematrix where ItemID=@ItemID
	
	RETURN