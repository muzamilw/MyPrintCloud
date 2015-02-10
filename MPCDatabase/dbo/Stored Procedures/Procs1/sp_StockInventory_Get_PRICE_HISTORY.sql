CREATE PROCEDURE dbo.sp_StockInventory_Get_PRICE_HISTORY

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_goodsreceivednote.code,tbl_stockitem_history.OldPackPrice,tbl_stockitem_history.NewPackPrice,
    tbl_systemusers.UserName,tbl_stockitem_history.ChangeDate,tbl_stockitem_history.GRNID 
    FROM tbl_stockitem_history 
    INNER JOIN tbl_systemusers ON (tbl_stockitem_history.UserID = tbl_systemusers.SystemUserID) 
    INNER JOIN tbl_goodsreceivednote ON (tbl_stockitem_history.GRNID = tbl_goodsreceivednote.GoodsReceivedID) 
    WHERE tbl_stockitem_history.ItemID = @ItemID

	RETURN