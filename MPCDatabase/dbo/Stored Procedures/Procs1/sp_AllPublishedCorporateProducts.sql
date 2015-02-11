CREATE PROCEDURE [dbo].[sp_AllPublishedCorporateProducts]
	-- Add the parameters for the stored procedure here
	@ContactCompanyID int,
	@ContactID int
AS
BEGIN

      
    if(@ContactID != 0)
    Begin
		WITH CTE(SortOrder, ProductCategoryID, ParentCategoryID, ThumbnailPath, CategoryName,Description1, level)  
		AS (SELECT   p2.DisplayOrder, p2.ProductCategoryID, p2.ParentCategoryID,p2.ThumbnailPath, p2.CategoryName, p2.Description1 , 0 as level  
	                             
		from tbl_ProductCategory p2   
		where p2.ContactCompanyId = @ContactCompanyID
		 and p2.isArchived = 0 and p2.isPublished = 1 and p2.isEnabled = 1
		UNION ALL  
	      
		SELECT    PC.DisplayOrder, PC.ProductCategoryID, pc.ParentCategoryID,PC.ThumbnailPath, pc.CategoryName, PC.Description1, level - 1  
		from tbl_ProductCategory PC  
		Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
		where pc.isArchived = 0 and pc.isPublished = 1 and pc.isEnabled = 1  
	      
		)
		Select items.ItemID, CTE_1.CategoryName, items.ProductName ,items.ProductCategoryID, items.IsFinishedGoods, items.isMarketingBrief from tbl_items items
		inner join CTE as CTE_1 on items.ProductCategoryID = CTE_1.ProductCategoryID
		inner join tbl_CategoryTerritories CT on CTE_1.ProductCategoryID = CT.ProductCategoryID
		inner join tbl_contacts CC on CT.TerritoryID = CC.TerritoryID
		where CC.ContactCompanyID = @ContactCompanyID and CC.ContactID = @ContactID and 
		items.EstimateID IS NULL and items.IsPublished = 1 and items.IsEnabled = 1 
		and (items.IsArchived = 0 or items.IsArchived IS NULL)
		order by items.ProductName ASC
	end
	else
	begin
		WITH CTE(SortOrder, ProductCategoryID, ParentCategoryID, ThumbnailPath, CategoryName,Description1, level)  
		AS (SELECT   p2.DisplayOrder, p2.ProductCategoryID, p2.ParentCategoryID,p2.ThumbnailPath, p2.CategoryName, p2.Description1 , 0 as level  
	                             
		from tbl_ProductCategory p2   
		where p2.ContactCompanyId = @ContactCompanyID
		 and p2.isArchived = 0 and p2.isPublished = 1 and p2.isEnabled = 1
		UNION ALL  
	      
		SELECT    PC.DisplayOrder, PC.ProductCategoryID, pc.ParentCategoryID,PC.ThumbnailPath, pc.CategoryName, PC.Description1, level - 1  
		from tbl_ProductCategory PC  
		Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
		where pc.isArchived = 0 and pc.isPublished = 1 and pc.isEnabled = 1  
	      
		)
		SELECT   items.ItemID, CTE_1.CategoryName, items.ProductName ,items.ProductCategoryID, items.IsFinishedGoods, items.isMarketingBrief from tbl_items items
		inner join  CTE AS CTE_1  on items.ProductCategoryID =  CTE_1.ProductCategoryID
		where items.EstimateID IS NULL and items.IsPublished = 1 and items.IsEnabled = 1 
		and (items.IsArchived = 0 or items.IsArchived IS NULL)
		order by items.ProductName ASC
	end
END