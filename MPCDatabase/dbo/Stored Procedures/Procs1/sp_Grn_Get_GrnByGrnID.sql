CREATE PROCEDURE dbo.sp_Grn_Get_GrnByGrnID

	(
		@GoodsReceivedID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT tbl_systemusers.UserName,tbl_suppliers.SupplierName,tbl_goodsreceivednote.UserID,tbl_goodsreceivednote.SupplierID,tbl_goodsreceivednote.*
        FROM tbl_goodsreceivednote
        INNER JOIN tbl_purchase ON (tbl_goodsreceivednote.PurchaseID = tbl_purchase.PurchaseID)
        INNER JOIN tbl_systemusers ON (tbl_goodsreceivednote.UserID = tbl_systemusers.SystemUserID)
        INNER JOIN tbl_suppliers ON (tbl_goodsreceivednote.SupplierID = tbl_suppliers.SupplierID)
        where GoodsReceivedID=@GoodsReceivedID
	
	RETURN