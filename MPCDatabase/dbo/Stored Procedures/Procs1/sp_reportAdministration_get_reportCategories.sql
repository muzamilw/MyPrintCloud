CREATE PROCEDURE dbo.sp_reportAdministration_get_reportCategories
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select tbl_reportcategory.CategoryID,tbl_reportcategory.CategoryName,tbl_reportcategory.Description from tbl_reportcategory
	RETURN