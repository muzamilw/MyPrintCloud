--exec [usp_GetProductByID] 16
CREATE PROCEDURE [dbo].[usp_GetProductByID]
	@ProductID numeric
AS
BEGIN
		
		Select itm.*, c.CategoryName
		from	tbl_items itm
		inner join tbl_ProductCategory c on c.ProductCategoryID = itm.ProductCategoryID
		Where itm.ItemID = @ProductID

END