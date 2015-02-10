CREATE PROCEDURE dbo.sp_FinishedGoods_Update_PriceMatrix

	(
		@Quantity int,
		@Price float,
		@CustomerID int,
		@CategoryID int,
		@FinishedGoodPriceMatrixID int
		
	)

AS
	/* SET NOCOUNT ON */
	
	UPDATE tbl_finishedgoodpricematrix set Quantity=@Quantity,Price=@Price, 
	CustomerID=@CustomerID, CategoryID=@CategoryID where FinishedGoodPriceMatrixID=@FinishedGoodPriceMatrixID
	
	RETURN