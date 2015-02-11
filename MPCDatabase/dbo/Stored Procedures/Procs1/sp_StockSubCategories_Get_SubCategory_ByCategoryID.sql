CREATE PROCEDURE dbo.sp_StockSubCategories_Get_SubCategory_ByCategoryID

	(
		@CategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
	select * from tbl_stocksubcategories where CategoryID = @CategoryID
	RETURN