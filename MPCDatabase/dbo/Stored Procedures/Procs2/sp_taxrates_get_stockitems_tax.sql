
Create PROCEDURE [dbo].[sp_taxrates_get_stockitems_tax]
(@TaxID int)
AS
	SELECT tbl_stockitems.StockItemID FROM tbl_stockitems WHERE tbl_stockitems.TaxID = @TaxID
	RETURN