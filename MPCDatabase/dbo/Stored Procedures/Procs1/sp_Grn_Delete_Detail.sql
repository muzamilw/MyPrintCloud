CREATE PROCEDURE dbo.sp_Grn_Delete_Detail
			   @GoodsReceivedDetailID int
AS
	DELETE FROM tbl_goodsreceivednotedetail where @GoodsReceivedDetailID =@GoodsReceivedDetailID 
	RETURN