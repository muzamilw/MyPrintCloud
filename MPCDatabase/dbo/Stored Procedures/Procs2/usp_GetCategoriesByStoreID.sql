CREATE PROCEDURE usp_GetCategoriesByStoreID
	(
	@CompanyId bigint
	)
AS
BEGIN
	WITH CTE(SortOrder, ProductCategoryID, ParentCategoryID, ThumbnailPath, CategoryName,Description1, level)  
    AS (SELECT   p2.DisplayOrder, p2.ProductCategoryID, p2.ParentCategoryID,p2.ThumbnailPath, p2.CategoryName, p2.Description1 , 0 as level  
                             
    from ProductCategory p2   
    where p2.CompanyId = @CompanyId  
     and p2.isArchived = 0
    UNION ALL  
      
    SELECT    PC.DisplayOrder, PC.ProductCategoryID, pc.ParentCategoryID,PC.ThumbnailPath, pc.CategoryName, PC.Description1, level - 1  
    from ProductCategory PC  
    Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
    where pc.isArchived = 0  
      
    )  
	SELECT distinct SortOrder, ProductCategoryID, ParentCategoryID, ThumbnailPath,CategoryName
    
     FROM         CTE AS CTE_1 
END