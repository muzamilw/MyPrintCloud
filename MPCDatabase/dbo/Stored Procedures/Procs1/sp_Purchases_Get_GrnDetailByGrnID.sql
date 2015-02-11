CREATE PROCEDURE dbo.sp_Purchases_Get_GrnDetailByGrnID

	(
	@GoodsreceivedID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT ItemID, QtyReceived, Price, PackQty,FreeItems FROM tbl_goodsreceivednotedetail where GoodsreceivedID=@GoodsreceivedID
	
	RETURN