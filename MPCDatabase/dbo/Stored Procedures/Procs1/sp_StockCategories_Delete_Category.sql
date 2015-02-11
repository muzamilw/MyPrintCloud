CREATE PROCEDURE dbo.sp_StockCategories_Delete_Category

	(
		@CategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
	delete from tbl_stockcategories where CategoryID=@CategoryID
	RETURN