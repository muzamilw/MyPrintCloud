CREATE PROCEDURE dbo.sp_StockInventory_UPDATE_ITEM_HISTORY

AS
	 SELECT tbl_stockitem_history.ChangeDate,tbl_stockitem_history.UserID,
        tbl_stockitem_history.OldPackPrice,tbl_stockitem_history.NewPackPrice,tbl_stockitem_history.GRNID,
        tbl_stockitem_history.ItemID FROM tbl_stockitem_history
	RETURN