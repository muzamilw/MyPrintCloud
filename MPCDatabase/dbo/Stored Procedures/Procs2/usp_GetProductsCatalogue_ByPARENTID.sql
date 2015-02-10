CREATE PROCEDURE [dbo].[usp_GetProductsCatalogue_ByPARENTID]
  @PARENTID int
AS
BEGIN
		SELECT ct.*, isnull(ct.ThumbnailPath ,'images/products/a4_wall_13.png') As ImageThumbnail, 
		(select count(ProductCategoryID)
		 from tbl_ProductCategory
						where ParentCategoryID = ct.ProductCategoryID) as SubCategoriesCount
		FROM tbl_ProductCategory ct
        WHERE ct.ParentCategoryID = @PARENTID
END


-----------------