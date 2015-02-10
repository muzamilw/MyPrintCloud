CREATE PROCEDURE dbo.sp_StockSubCategories_Get_BySubCategoryID

	(
		@SubCategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_stocksubcategories.SubCategoryID,tbl_stocksubcategories.Code,
        tbl_stocksubcategories.Name,tbl_stocksubcategories.Description,
        tbl_stocksubcategories.CategoryID,tbl_stocksubcategories.Fixed
        FROM tbl_stocksubcategories where SubCategoryID=@SubCategoryID
	RETURN