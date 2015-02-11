CREATE PROCEDURE [dbo].[sp_BrokerCategoryTree] 
 -- Add the parameters for the stored procedure here
 -- dbo.sp_BrokerCategoryTree 1606
 @ContactCompanyID int = 0
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
WITH CTE(SortOrder, ProductCategoryID, ParentCategoryID, ThumbnailPath, CategoryName,Description1, ShelfProductCategory ,level)
  AS (SELECT p2.DisplayOrder, p2.ProductCategoryID, p2.ParentCategoryID,p2.ThumbnailPath, p2.CategoryName, p2.Description1 , p2.isShelfProductCategory, 0 as level
 
 from tbl_items i 
 inner join tbl_ContactCompanyItems ci on i.ItemID = ci.ItemID and ci.IsdisplayToUser = 1 and ci.ContactCompanyId = @ContactCompanyID
 inner join tbl_ProductCategory p2 on i.ProductCategoryID = p2.ProductCategoryID
 where i.EstimateID is null and i.IsPublished = 1 and i.IsEnabled = 1 and i.IsArchived = 0
 and p2.IsPublished = 1 and p2.IsEnabled = 1 and p2.IsArchived = 0
 UNION ALL
 SELECT PC.DisplayOrder, PC.ProductCategoryID, pc.ParentCategoryID,PC.ThumbnailPath, pc.CategoryName, PC.Description1, PC.isShelfProductCategory ,level - 1
 from tbl_ProductCategory PC
 Inner join CTE on CTE.ParentCategoryID = PC.ProductCategoryID
 where pc.isPublished = 1 and pc.IsEnabled = 1 and pc.IsArchived = 0
 )
 SELECT distinct ProductCategoryID, ISNULL(ParentCategoryID, 0) AS ParentCategoryID, CategoryName ,ISNULL(SortOrder, 0) as SortOrder,ISNULL(ThumbnailPath, '') as ThumbnailPath, ISNULL(Description1, '') as Description1, ISNULL(ShelfProductCategory, 0) as ShelfProductCategory
 FROM CTE AS CTE_1
 
 
END