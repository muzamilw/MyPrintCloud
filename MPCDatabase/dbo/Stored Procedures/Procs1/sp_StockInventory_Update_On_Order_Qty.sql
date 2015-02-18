
create PROCEDURE [dbo].[sp_StockInventory_Update_On_Order_Qty]

	(
		@onStockOrder float,
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	update tbl_stockitems set onOrder=@onStockOrder where stockitemid=@ItemID
	RETURN