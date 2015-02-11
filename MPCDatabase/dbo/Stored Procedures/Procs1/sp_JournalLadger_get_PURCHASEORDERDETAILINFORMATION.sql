CREATE PROCEDURE dbo.sp_JournalLadger_get_PURCHASEORDERDETAILINFORMATION

	@GRNID  int

AS

SELECT GoodsReceivedDetailID,ItemID, QtyReceived as quantity,Price,ItemCode,Details as ServiceDetail,TotalPrice,Discount,
NetTax,GoodsreceivedID,Name as ItemName,TaxID,DepartmentID 
FROM tbl_goodsreceivednotedetail 
where GoodsreceivedID=@GRNID order by GoodsReceivedDetailID

RETURN