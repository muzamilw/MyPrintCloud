CREATE PROCEDURE dbo.sp_Purchases_Delete_Detail
@PurchaseDetailID int
AS

DELETE FROM tbl_purchasedetail where PurchaseDetailID=@PurchaseDetailID
	RETURN