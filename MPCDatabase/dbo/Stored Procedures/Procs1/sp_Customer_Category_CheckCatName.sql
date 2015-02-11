CREATE PROCEDURE dbo.sp_Customer_Category_CheckCatName
	(

		@CustomerID int,
		@category_id int,
		@category_name varchar(50)
	)
		

AS
	Select id from tbl_finishgood_categories where ItemLibrarayGroupName=@category_name and ID<>@category_id and CustomerID=@CustomerID

	RETURN