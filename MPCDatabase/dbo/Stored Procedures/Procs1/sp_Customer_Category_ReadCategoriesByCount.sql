CREATE PROCEDURE dbo.sp_Customer_Category_ReadCategoriesByCount
	(
		@parent_category_id int
	)
		
AS
	select * from tbl_finishgood_categories where ParentID=@parent_category_id

	RETURN