CREATE PROCEDURE dbo.sp_taxrates_get_purchase_detail_tax
(@TaxID int)
AS
	SELECT tbl_purchasedetail.PurchaseDetailID FROM tbl_purchasedetail WHERE tbl_purchasedetail.TaxID = @TaxID
	RETURN