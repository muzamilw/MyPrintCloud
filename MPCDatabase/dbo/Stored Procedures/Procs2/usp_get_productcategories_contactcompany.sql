
CREATE Procedure [dbo].[usp_get_productcategories_contactcompany]
@StoreID int
AS
Begin
DECLARE @CATS AS TABLE(
     ROWID  INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
     ProCatID int     ,
     ImagePath varchar(100),
     ThumbnailPath varchar(100),
     CategoryName varchar(100)
     );
     
WITH CTE(ProductCategoryID, ParentCategoryID, CategoryName,level,ImagePath,thumbnailpath)  
AS (SELECT   p2.ProductCategoryID, p2.ParentCategoryID, p2.CategoryName, 0 as level  , p2.ImagePath,p2.thumbnailpath
from tbl_ProductCategory p2
inner join (select  *
from tbl_productcategory parent
where parent.contactcompanyid = @StoreID  and  parent.isarchived = 0 ) maincat on maincat.productcategoryid = p2.productcategoryid
UNION ALL        
SELECT    PC.ProductCategoryID, pc.ParentCategoryID, pc.CategoryName, level - 1 , PC.ImagePath,pc.thumbnailpath
from tbl_ProductCategory PC  
Inner join CTE on CTE.ProductCategoryID = PC.ParentCategoryID
where PC.isarchived = 0
) 			 
insert into @CATS(ProCatID,ImagePath,ThumbnailPath,CategoryName)
SELECT    ProductCategoryID,ImagePath,ThumbnailPath,CategoryName
FROM CTE AS CTE_1 

select * from @CATS
End