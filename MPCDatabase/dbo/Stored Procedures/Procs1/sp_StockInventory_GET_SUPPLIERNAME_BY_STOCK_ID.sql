CREATE PROCEDURE dbo.sp_StockInventory_GET_SUPPLIERNAME_BY_STOCK_ID

	(
		@ItemID integer
		--@parameter2 datatype OUTPUT
	)

AS
	 SELECT tbl_suppliers.SupplierName FROM tbl_stockitems   INNER JOIN tbl_suppliers ON (tbl_stockitems.SupplierID = tbl_suppliers.SupplierID) where tbl_stockitems.ItemID=@ItemID
	RETURN