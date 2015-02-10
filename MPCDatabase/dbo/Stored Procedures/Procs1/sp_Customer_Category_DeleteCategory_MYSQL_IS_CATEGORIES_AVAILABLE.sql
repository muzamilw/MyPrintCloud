CREATE PROCEDURE dbo.sp_Customer_Category_DeleteCategory_MYSQL_IS_CATEGORIES_AVAILABLE
	(
	@category_id int
	)
AS
select ID from tbl_finishgood_categories where ParentID=@category_id

	RETURN