
Create PROCEDURE [dbo].[sp_StockInventory_GET_STOCK_BY_ITEM_NAME]

	(
		@Region varchar(10),
		@ItemName varchar(255)
		--@parameter2 datatype OUTPUT
	)

AS
	Select StockItemId from tbl_stockitems where ItemName=@ItemName and Region=@Region
	
	RETURN