CREATE PROCEDURE dbo.sp_FinishedGoods_Get_FinishedGoodsByCategoryIDAndCatalog

	(
		@ItemLibraryGroupID int,
		@CatalogueID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT DISTINCT 
                      tbl_finishedgoods.ItemID, tbl_finishedgoods.ID, CONVERT(nvarchar(1000), tbl_finishedgoods.Description1) AS Description1, CONVERT(nvarchar(1000), 
                      tbl_finishedgoods.Description1) AS Description2, CONVERT(nvarchar(1000), tbl_finishedgoods.Description3) AS Description3, tbl_finishedgoods.LockedBy, 
                      tbl_items.Title, tbl_finishedgoods.IsForWeb, tbl_finishedgoods.InStock, tbl_finishedgoods.Allocated,tbl_finishedgoods.IsShowStockOnWeb,tbl_finishedgoods.IsShowAllocatedStockOnWeb,tbl_finishedgoods.IsShowFreeStockOnWeb,tbl_finishedgoods.IsShowPriceOnWeb 
						FROM         tbl_items 
						INNER JOIN  tbl_finishedgoods ON tbl_items.ItemID = tbl_finishedgoods.ItemID 
						INNER JOIN   tbl_finishedgoodpricematrix ON tbl_items.ItemID = tbl_finishedgoodpricematrix.ItemID 
						INNER JOIN   tbl_itemlibrary_catalogue_detail ON tbl_items.ItemID = tbl_itemlibrary_catalogue_detail.ItemID
							WHERE (tbl_finishedgoodpricematrix.CategoryID = @ItemLibraryGroupID and tbl_itemlibrary_catalogue_detail.ItemCatalogueID=@CatalogueID) and tbl_finishedgoods.IsForWeb<>0 
                
	
	RETURN