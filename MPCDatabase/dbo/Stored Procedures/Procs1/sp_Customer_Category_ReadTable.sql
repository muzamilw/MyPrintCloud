CREATE PROCEDURE dbo.sp_Customer_Category_ReadTable
	(
	@parentid int
	)
AS

		select * from tbl_finishgood_categories where ParentID=@parentid and dbo.PrintlinkCategoryView(ID)<> 0

	RETURN