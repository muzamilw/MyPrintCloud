CREATE PROCEDURE dbo.sp_Customer_Category_DeleteCategory_MYSQL_DELETE_CATEGORIES
	(
	@category_id int
	)

AS
	delete from tbl_product_categories where category_id=@category_id

	RETURN