
create PROCEDURE [dbo].[sp_StockInventory_GET_STOCKNAME_BY_STOCK_ID]

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_stockitems.ItemName from tbl_stockitems 
	where tbl_stockitems.StockItemID=@ItemID
	RETURN