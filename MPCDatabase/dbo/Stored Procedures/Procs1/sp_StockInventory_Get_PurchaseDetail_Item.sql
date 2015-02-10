CREATE PROCEDURE dbo.sp_StockInventory_Get_PurchaseDetail_Item

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT tbl_purchasedetail.PurchaseDetailID fROM tbl_purchasedetail
	WHERE (((tbl_purchasedetail.ItemID = @ItemID)))
	RETURN