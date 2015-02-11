
Create PROCEDURE [dbo].[sp_StockInventory_Update_On_InStock_Qty]

	(
		@inStock float,
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	 UPDATE tbl_stockitems set inStock=@inStock where Stockitemid=@ItemID
	RETURN