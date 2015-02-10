CREATE PROCEDURE dbo.sp_StockCategories_Check_Name

	(
		@CategoryID integer,
		@Name varchar(50),
		@Code varchar(5),
		@CompanyID int
		--@parameter2 datatype OUTPUT
	)

AS
	Select CategoryID from tbl_stockcategories where (CategoryID<>@CategoryID and (Name=@Name and Code=@Code)) and CompanyID=@CompanyID
	RETURN