CREATE PROCEDURE dbo.sp_StockInventory_GET_STOCK_COUNT_BY_SUBCATEGORY_ID

	(
		@SubCategoryID integer
		--@parameter2 datatype OUTPUT
	)

AS
SELECT count(tbl_stockitems.SubCategoryID) from tbl_stockitems 
WHERE tbl_stockitems.SubCategoryID = @SubCategoryID
	RETURN