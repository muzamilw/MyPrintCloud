CREATE PROCEDURE dbo.sp_StockCost_Price_DELETE_FROM_COST_PRICE_TABLE

	(
		@CostPriceID integer
		--@parameter2 datatype OUTPUT
	)

AS
	DELETE from tbl_stock_cost_and_price where CostPriceID=@CostPriceID
	RETURN