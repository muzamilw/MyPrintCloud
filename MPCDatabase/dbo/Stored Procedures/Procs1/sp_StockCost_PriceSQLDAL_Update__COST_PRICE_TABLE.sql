CREATE PROCEDURE dbo.sp_StockCost_PriceSQLDAL_Update__COST_PRICE_TABLE

	(
		@ItemID integer,
		@CostPrice float,
		@PackCostPrice float,
		@FromDate datetime,
		@ToDate datetime,
		@CostOrPriceIdentifier smallint,
		@ProcessingCharge float,
		@CostPriceID integer
		--@parameter2 datatype OUTPUT
	)

AS
	UPDATE tbl_stock_cost_and_price SET ItemID=@ItemID, CostPrice=@CostPrice,
	PackCostPrice=@PackCostPrice, FromDate=@FromDate, ToDate=@ToDate,
	CostOrPriceIdentifier=@CostOrPriceIdentifier,ProcessingCharge=@ProcessingCharge 
	WHERE CostPriceID=@CostPriceID
	RETURN