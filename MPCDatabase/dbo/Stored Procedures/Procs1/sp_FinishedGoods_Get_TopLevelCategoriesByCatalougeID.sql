CREATE PROCEDURE dbo.sp_FinishedGoods_Get_TopLevelCategoriesByCatalougeID
(
	@CatalogID int
)
as
SELECT DISTINCT 
tbl_finishgood_categories.ID, tbl_finishgood_categories.ItemLibrarayGroupName, CONVERT(nvarchar(4000),tbl_finishgood_categories.Description1)  as Description1
FROM tbl_finishedgoodpricematrix 
INNER JOIN tbl_items ON tbl_finishedgoodpricematrix.ItemID = tbl_items.ItemID
INNER JOIN tbl_finishedgoods ON tbl_items.ItemID = tbl_finishedgoods.ItemID
INNER JOIN tbl_finishgood_categories ON tbl_finishedgoodpricematrix.CategoryID = tbl_finishgood_categories.ID
inner join tbl_itemlibrary_catalogue_detail on tbl_itemlibrary_catalogue_detail.ItemID = tbl_items.ItemID
WHERE 
(tbl_finishgood_categories.ParentID is null or tbl_finishgood_categories.ParentID= 0) 
and ( tbl_itemlibrary_catalogue_detail.ItemCatalogueID = @CatalogID)