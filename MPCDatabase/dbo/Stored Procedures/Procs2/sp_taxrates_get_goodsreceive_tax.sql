CREATE PROCEDURE dbo.sp_taxrates_get_goodsreceive_tax
(@TaxID int)
AS
	SELECT tbl_goodsreceivednotedetail.GoodsReceivedDetailID FROM tbl_goodsreceivednotedetail WHERE tbl_goodsreceivednotedetail.TaxID = @TaxID
	RETURN