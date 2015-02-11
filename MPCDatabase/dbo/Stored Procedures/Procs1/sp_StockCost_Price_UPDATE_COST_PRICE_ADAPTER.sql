CREATE PROCEDURE dbo.sp_StockCost_Price_UPDATE_COST_PRICE_ADAPTER

	AS
	SELECT tbl_stock_cost_and_price.ItemID,tbl_stock_cost_and_price.CostPrice,
        tbl_stock_cost_and_price.PackCostPrice,tbl_stock_cost_and_price.FromDate,
        tbl_stock_cost_and_price.ToDate, tbl_stock_cost_and_price.CostOrPriceIdentifier,
        tbl_stock_cost_and_price.ProcessingCharge FROM tbl_stock_cost_and_price
	RETURN