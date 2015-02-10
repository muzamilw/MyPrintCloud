CREATE PROCEDURE dbo.sp_Grn_Get_GrnDetail

	(
		@GoodsreceivedID int
	)

AS
	/* SET NOCOUNT ON */
	
	select GoodsReceivedDetailID,ItemID,GoodsreceivedID,ItemCode,[Name],packqty,TotalOrderedqty,qtyReceived,price,Details,TotalPrice,TaxID,NetTax,Discount,freeitems from tbl_goodsreceivednotedetail where GoodsreceivedID=@GoodsreceivedID
	
	RETURN