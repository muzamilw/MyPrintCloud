CREATE PROCEDURE dbo.sp_StockInventory_GET_ITEM_HISTORY

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_stockitem_history.ItemHID,tbl_stockitem_history.ChangeDate,
        tbl_stockitem_history.UserID,tbl_stockitem_history.OldPackPrice,
        tbl_stockitem_history.NewPackPrice,tbl_stockitem_history.GRNID,
        tbl_stockitem_history.ItemID FROM tbl_stockitem_history 
        WHERE tbl_stockitem_history.ItemID = @ItemID

	RETURN