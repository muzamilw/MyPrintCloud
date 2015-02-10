
Create PROCEDURE [dbo].[sp_StockInventory_Delete_Stock_By_ItemID]

	(
		@ItemID Integer
		--@parameter2 datatype OUTPUT
	)

AS
	DELETE from tbl_stockitems where StockItemID=@ItemID
	RETURN