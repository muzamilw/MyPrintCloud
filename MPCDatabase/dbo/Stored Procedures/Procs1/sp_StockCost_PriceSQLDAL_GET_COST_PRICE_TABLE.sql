CREATE PROCEDURE dbo.sp_StockCost_PriceSQLDAL_GET_COST_PRICE_TABLE

	
AS
	SELECT * FROM tbl_stock_cost_and_price 
	RETURN