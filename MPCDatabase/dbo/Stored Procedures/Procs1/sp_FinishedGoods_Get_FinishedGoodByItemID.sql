CREATE PROCEDURE dbo.sp_FinishedGoods_Get_FinishedGoodByItemID

	(
		@ID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_items.ItemID,tbl_items.ItemLibrarayGroupID,tbl_items.Title,
       tbl_items.Qty1NetTotal,tbl_items.Qty1, tbl_items.Qty2NetTotal,tbl_items.Qty2,tbl_items.Qty3NetTotal,tbl_items.Qty3,tbl_items.IsMultipleQty,tbl_items.IsRunOnQty,tbl_items.RunOnQty, tbl_items.IsItemLibraray,tbl_items.CanCopyToEstimate, tbl_items.IsFinishedGoodPrivate ,tbl_finishedgoods.ID,tbl_finishedgoods.ItemID,tbl_finishedgoods.Thumbnail,tbl_finishedgoods.Image,tbl_finishedgoods.ContentType,tbl_finishedgoods.Description1,tbl_finishedgoods.Description2,tbl_finishedgoods.InStock,tbl_finishedgoods.Allocated,
       tbl_finishedgoods.Description3,tbl_finishedgoods.IsForWeb,tbl_finishedgoods.IsShowStockOnWeb,tbl_finishedgoods.ThresholdLevel,tbl_finishedgoods.ThresholdProductionQuantity,tbl_finishedgoods.Location,tbl_finishedgoods.File1,tbl_finishedgoods.IsShowAllocatedStockOnWeb,tbl_finishedgoods.IsShowFreeStockOnWeb,tbl_finishedgoods.IsShowPriceOnWeb  FROM tbl_items
       INNER JOIN tbl_finishedgoods ON (tbl_items.ItemID = tbl_finishedgoods.ItemID)
       WHERE tbl_finishedgoods.ItemID=@ID
	
	RETURN