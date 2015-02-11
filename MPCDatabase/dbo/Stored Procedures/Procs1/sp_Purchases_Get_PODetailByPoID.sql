CREATE PROCEDURE dbo.sp_Purchases_Get_PODetailByPoID

	(
	@PurchaseID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT ItemID, quantity, price, packqty FROM tbl_purchasedetail where PurchaseID=@PurchaseID
	
	RETURN