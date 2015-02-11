CREATE PROCEDURE dbo.sp_customers_get_categories_by_parentCategoryandCustomer

	(
		@parentid int,
		@CustomerID int
	)

AS

	select * from tbl_finishgood_categories where (ParentID=@parentid and CustomerID=@CustomerID) and dbo.PrintlinkCategoryView(ID)<> 0
	RETURN