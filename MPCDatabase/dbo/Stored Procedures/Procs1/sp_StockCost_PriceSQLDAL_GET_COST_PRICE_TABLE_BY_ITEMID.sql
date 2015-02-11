CREATE PROCEDURE dbo.sp_StockCost_PriceSQLDAL_GET_COST_PRICE_TABLE_BY_ITEMID

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT * FROM tbl_stock_cost_and_price WHERE ItemID=@ItemID 
	RETURN