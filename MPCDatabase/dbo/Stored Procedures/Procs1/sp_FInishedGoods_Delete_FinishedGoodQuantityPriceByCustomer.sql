CREATE PROCEDURE dbo.sp_FInishedGoods_Delete_FinishedGoodQuantityPriceByCustomer

	(
		@CustomerID int,
		@ItemID int
	)

AS
	/* SET NOCOUNT ON */
	
	DELETE from tbl_finishedgoodpricematrix where ItemID=@ItemID and CustomerID=@CustomerID
	
	RETURN