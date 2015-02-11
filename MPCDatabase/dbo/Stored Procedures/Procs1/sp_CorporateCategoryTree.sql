CREATE PROCEDURE [dbo].[sp_CorporateCategoryTree] --1707  
 -- Add the parameters for the stored procedure here  
 -- dbo.[sp_CorporateCategoryTree] 2057  
 @ContactCompanyID int = 0  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
WITH CTE(SortOrder, ProductCategoryID, ParentCategoryID, ThumbnailPath, CategoryName,Description1, level)  
    AS (SELECT   p2.DisplayOrder, p2.ProductCategoryID, p2.ParentCategoryID,p2.ThumbnailPath, p2.CategoryName, p2.Description1 , 0 as level  
                             
    from tbl_ProductCategory p2   
    where p2.ContactCompanyId = @ContactCompanyID  
     and p2.isArchived = 0
    UNION ALL  
      
    SELECT    PC.DisplayOrder, PC.ProductCategoryID, pc.ParentCategoryID,PC.ThumbnailPath, pc.CategoryName, PC.Description1, level - 1  
    from tbl_ProductCategory PC  
    Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
    where pc.isArchived = 0  
      
    )  
  
      
    SELECT     distinct ProductCategoryID, ISNULL(ParentCategoryID, 0) AS ParentCategoryID, CategoryName ,ISNULL(SortOrder, 0) as SortOrder,ISNULL(ThumbnailPath, '') as ThumbnailPath, ISNULL(Description1, '') as Description1  
     FROM         CTE AS CTE_1  
   
   
   
END