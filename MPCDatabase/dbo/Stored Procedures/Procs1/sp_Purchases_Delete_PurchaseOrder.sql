CREATE PROCEDURE [dbo].[sp_Purchases_Delete_PurchaseOrder]

	(
		@PurchaseID int
	)

AS
	/* SET NOCOUNT ON */
	
	Delete from tbl_purchasedetail where PurchaseID = @PurchaseID
	
	Delete from tbl_purchase where PurchaseID=@PurchaseID
	
	RETURN