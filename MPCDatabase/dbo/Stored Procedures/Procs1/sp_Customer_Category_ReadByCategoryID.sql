CREATE PROCEDURE dbo.sp_Customer_Category_ReadByCategoryID
	(
		@category_id int
)
AS
	select * from tbl_finishgood_categories where ID=@category_id
	
	RETURN