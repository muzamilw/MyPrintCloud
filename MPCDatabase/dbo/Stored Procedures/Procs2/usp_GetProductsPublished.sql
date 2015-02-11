CREATE PROCEDURE [dbo].[usp_GetProductsPublished]

AS
BEGIN
		select top 6 p.ItemID, p.ProductName, p.ProductCategoryID, 
		isnull(p.ImagePath,'images/products/a4_wall_13.png') As ProductImage, 
		ISNULL(p.ThumbnailPath,'images/products/a4_wall_13.png') As ProductThumbnail,
		 isnull(( select MIN(Price) from tbl_items_PriceMatrix pp where pp.ItemID = p.ItemID),0) As Price1
		from dbo.tbl_items p
		where	isPublished = 1
		
		
END