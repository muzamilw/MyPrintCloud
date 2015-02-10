CREATE PROCEDURE dbo.sp_StockSubCategories_Check_Name

	(
		@SubCategoryID  integer,
		@Name varchar(50),
		@Code varchar(255),
		@CompanyID int
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT SubCategoryID from  tbl_stocksubcategories 
	inner join tbl_stockcategories  on (tbl_stocksubcategories.CategoryID =  tbl_stockcategories.CategoryID)
	where (tbl_stocksubcategories.SubCategoryID<>@SubCategoryID 
	and (tbl_stocksubcategories.Name=@Name and tbl_stocksubcategories.Code=@Code) and tbl_stockcategories.CompanyID=@CompanyID)
	RETURN