--Exec [usp_GetProductsCatalogueByCategoryID]145
CREATE PROCEDURE [dbo].[usp_GetProductsCatalogueByCategoryID]
@CATEGORYID int
AS
BEGIN
	declare @iCategoryID int 
	declare @pCategoryName varchar(255)
		select @iCategoryID = ParentCategoryID, @pCategoryName = CategoryName from tbl_ProductCategory where ProductCategoryID = @CATEGORYID
		
		select top 1 p.ItemID,p.ProductName,
		isnull(p.ImagePath,'images/products/a4_wall_13.png') As ProductImage, 
		(select MIN(Price) from tbl_items_PriceMatrix pp where pp.ItemID = p.ItemID) As Price1,
		(select MIN(Quantity) from tbl_items_PriceMatrix pp where pp.ItemID = p.ItemID) As Quantity1,
		c.CategoryName ,
		 CASE
			 WHEN @iCategoryID > 0
			  THEN (select count(ProductCategoryID)
					 from tbl_ProductCategory
						where ParentCategoryID = @iCategoryID
						)
			 ELSE '0'
		 END as SubCategoriesCount,		
		@iCategoryID As ParentID, 
		(select categoryName from tbl_ProductCategory where ProductCategoryID = @iCategoryID) As ParentCategoryName
		from	dbo.tbl_items p
		inner join tbl_ProductCategory c on c.ProductCategoryID = p.ProductCategoryID
		where p.ProductCategoryID = @CATEGORYID
		AND		isPublished = 1
		
END


--if a category has parentID then get get that parentID and get its sub categories