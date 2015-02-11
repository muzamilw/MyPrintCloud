CREATE PROCEDURE dbo.sp_StockSubCategories_Add_SubCategory

	(
		@Fixed varchar(1),
		@Code varchar(5),
		@Name varchar(50),
		@Description varchar(255),
		@CategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
	INSERT into tbl_stocksubcategories (Fixed,Code,Name,Description,CategoryID)
	VALUES
        (@Fixed,@Code,@Name,@Description,@CategoryID)
	RETURN