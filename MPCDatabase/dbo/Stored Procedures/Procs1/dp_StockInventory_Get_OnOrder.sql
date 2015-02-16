
Create PROCEDURE [dbo].[dp_StockInventory_Get_OnOrder]

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	Select onOrder from  tbl_stockitems where stockitemid=@ItemID
	RETURN