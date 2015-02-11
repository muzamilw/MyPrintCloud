CREATE PROCEDURE dbo.sp_StockInventory_Get_GoodsReceivedNotes_Detail_Item

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_goodsreceivednotedetail.GoodsReceivedDetailID
	FROM tbl_goodsreceivednotedetail 
    WHERE (((tbl_goodsreceivednotedetail.ItemID = @ItemID)))
	RETURN