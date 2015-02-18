CREATE PROCEDURE dbo.sp_StockSubCategories_Update_SubCategories

	(
		@Code varchar(5),
		@Name varchar(50),
		@Description varchar(255),
		@CategoryID integer,
		@SubCategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
	UPDATE tbl_stocksubcategories set Code=@Code,Name=@Name,Description=@Description,
	CategoryID=@CategoryID where SubCategoryID=@SubCategoryID
	
	RETURN