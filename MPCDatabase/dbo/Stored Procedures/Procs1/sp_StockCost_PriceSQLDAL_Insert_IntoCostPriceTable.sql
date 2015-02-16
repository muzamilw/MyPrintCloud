CREATE PROCEDURE dbo.sp_StockCost_PriceSQLDAL_Insert_IntoCostPriceTable

	(
		@ItemID integer,
		@CostPrice float,
		@PackCostPrice float,
		@FromDate Datetime,
		@ToDate Datetime,
		@CostOrPriceIdentifier smallint,
		@ProcessingCharge float
		--@parameter2 datatype OUTPUT
	)

AS
	INSERT INTO tbl_stock_cost_and_price (ItemID,CostPrice,PackCostPrice, FromDate,
	ToDate,CostOrPriceIdentifier,ProcessingCharge) values 
	(@ItemID, @CostPrice,@PackCostPrice,@FromDate, @ToDate,@CostOrPriceIdentifier,
	@ProcessingCharge)
	RETURN