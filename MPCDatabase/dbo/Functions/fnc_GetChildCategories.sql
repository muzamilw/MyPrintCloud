-- =============================================
-- Author:		Muhammad Naveed
-- Create date: December 19, 2013
-- Description:	Get Child Categories up to nth level by Category ID
-- =============================================
--select * from fnc_GetChildCategories(233)
CREATE FUNCTION [dbo].[fnc_GetChildCategories]
	(
	@CategoryID int
	)
RETURNS @table_childs TABLE ( ProductCategoryID int) 
AS
	BEGIN
			WITH CTE(ProductCategoryID, ParentCategoryID, CategoryName,level)  
			AS (SELECT   p2.ProductCategoryID, p2.ParentCategoryID, p2.CategoryName, 0 as level  
		                             
			from tbl_ProductCategory p2
			where 
				p2.ProductCategoryID = @CategoryID 
			UNION ALL        
			SELECT    PC.ProductCategoryID, pc.ParentCategoryID, pc.CategoryName, level - 1  
			from tbl_ProductCategory PC  
			Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
		    
			) 			 
			 insert into @table_childs
			 SELECT     distinct ProductCategoryID
			 FROM CTE AS CTE_1 
			RETURN
	END