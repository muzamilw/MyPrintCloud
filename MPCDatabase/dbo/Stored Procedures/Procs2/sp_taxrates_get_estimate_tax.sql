CREATE PROCEDURE dbo.sp_taxrates_get_estimate_tax
(@TaxID  int)
AS
	SELECT tbl_items.ItemID FROM tbl_items WHERE (tbl_items.Tax1 = @TaxID or tbl_items.Tax2 = @TaxID) or tbl_items.Tax3 = @TaxID
	RETURN