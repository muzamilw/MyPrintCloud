
Create PROCEDURE [dbo].[sp_StockInventory_GET_STOCK_BY_ITEM_CODE]

	(
		@ItemCode varchar(50),
		@Region varchar(10)
		--@parameter2 datatype OUTPUT
	)

AS
	 Select StockItemId from tbl_stockitems where ItemCode=@ItemCode and Region=@Region
	RETURN