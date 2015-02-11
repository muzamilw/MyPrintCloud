CREATE PROCEDURE dbo.sp_FinishedGoods_Get_FinishedGoodsByCategoryID

	(
		@ItemLibraryGroupID int,
		@CatalogueID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_finishedgoods.ID,tbl_finishedgoods.ItemID,tbl_finishedgoods.Thumbnail,
                tbl_finishedgoods.Image,tbl_finishedgoods.ContentType,tbl_finishedgoods.Description1,tbl_finishedgoods.Description2,tbl_finishedgoods.Description3,
                tbl_finishedgoods.LockedBy,tbl_items.Title,tbl_finishedgoods.IsForWeb,tbl_finishedgoods.InStock,tbl_finishedgoods.Allocated,tbl_itemlibrary_catalogue_detail.ItemCatalogueID 
                FROM tbl_finishedgoods 
                INNER JOIN tbl_itemlibrary_catalogue_detail ON (tbl_finishedgoods.ItemID = tbl_itemlibrary_catalogue_detail.ItemID) 
                INNER JOIN tbl_items ON (tbl_finishedgoods.ItemID = tbl_items.ItemID) 
                INNER JOIN tbl_finishedgoodpricematrix ON (tbl_finishedgoods.ItemID = tbl_finishedgoodpricematrix.ItemID) 
                where (tbl_finishedgoodpricematrix.CategoryID = @ItemLibraryGroupID and tbl_itemlibrary_catalogue_detail.ItemCatalogueID=@CatalogueID) and tbl_finishedgoods.IsForWeb<>0 
                
	
	RETURN