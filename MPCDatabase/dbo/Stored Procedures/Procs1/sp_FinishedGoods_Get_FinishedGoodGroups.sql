CREATE PROCEDURE dbo.sp_FinishedGoods_Get_FinishedGoodGroups

	(
	@CustomerID int	
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_finishgood_categories.ID,tbl_finishgood_categories.CustomerID,tbl_finishgood_categories.ItemLibrarayGroupName from tbl_finishgood_categories where (CustomerID=@CustomerID) 
	RETURN