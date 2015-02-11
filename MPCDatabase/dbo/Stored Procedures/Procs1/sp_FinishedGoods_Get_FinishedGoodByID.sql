CREATE PROCEDURE dbo.sp_FinishedGoods_Get_FinishedGoodByID

	(
		@ID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_items.ItemID,tbl_items.ItemLibrarayGroupID,tbl_items.Title,
       tbl_items.Qty1NetTotal,tbl_items.Qty1,  tbl_items.Qty2NetTotal,tbl_items.Qty2,tbl_items.Qty3NetTotal,
tbl_items.Qty3,tbl_items.IsMultipleQty,tbl_items.IsRunOnQty,tbl_items.RunOnQty, 
tbl_items.IsItemLibraray,tbl_items.CanCopyToEstimate, tbl_items.IsFinishedGoodPrivate ,
       tbl_finishedgoods.* FROM tbl_items 
       INNER JOIN tbl_finishedgoods ON (tbl_items.ItemID = tbl_finishedgoods.ItemID)
       WHERE tbl_finishedgoods.ID=@ID
	
	RETURN