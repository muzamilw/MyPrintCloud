CREATE PROCEDURE dbo.sp_Customer_Category_DeleteCategory_deleteCustomerCategory_MYSQL_DELETE_CUSTOMER_CATEGORIES
	(
	@CategoryID int
	)

AS
DELETE FROM tbl_finishgood_categories
       WHERE tbl_finishgood_categories.ID=@CategoryID
	RETURN