CREATE PROCEDURE dbo.sp_Customer_Category_DeleteCategory_MYSQL_IS_PRODUCTS_AVAILABLE
	(
	@category_id int
	)
AS
	select product_id from tbl_products where ID=@category_id

	RETURN