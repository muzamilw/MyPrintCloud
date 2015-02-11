
Create PROCEDURE [dbo].[sp_StockInventory_Get_InStock]

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	Select inStock from  tbl_stockitems where Stockitemid=@ItemID
	RETURN