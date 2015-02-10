CREATE PROCEDURE dbo.sp_FinishedGoods_Get_FinishedGoodCustomers

	(
		@ItemID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT distinct tbl_finishedgoodpricematrix.CustomerID,tbl_customers.CustomerName FROM tbl_customers INNER JOIN tbl_finishedgoodpricematrix ON (tbl_customers.CustomerID = tbl_finishedgoodpricematrix.CustomerID) where ItemID=@ItemID
	
	RETURN