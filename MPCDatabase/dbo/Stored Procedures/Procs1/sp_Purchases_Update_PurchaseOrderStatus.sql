CREATE PROCEDURE [dbo].[sp_Purchases_Update_PurchaseOrderStatus]

	(
		@PurchaseID int,
		@Status int
	)

AS
	/* SET NOCOUNT ON */
	update tbl_purchase set Status=@Status where PurchaseID=@PurchaseID
	RETURN