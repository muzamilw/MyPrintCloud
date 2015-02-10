CREATE PROCEDURE dbo.sp_StockSubCategories_Delete_SubCategory

	(
		@SubCategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
	delete from tbl_stocksubcategories where SubCategoryID=@SubCategoryID
	RETURN