CREATE PROCEDURE dbo.sp_FinishedGoods_Insert_PriceMatrixValues

	(
		@Quantity int,
		@Price float,
		@CustomerID int,
		@ItemID int,
		@CategoryID int
	)

AS
	/* SET NOCOUNT ON */
	
	INSERT INTO tbl_finishedgoodpricematrix(Quantity,Price,CustomerID,ItemID,CategoryID) VALUES(@Quantity,@Price,@CustomerID,@ItemID,@CategoryID)
	
	RETURN