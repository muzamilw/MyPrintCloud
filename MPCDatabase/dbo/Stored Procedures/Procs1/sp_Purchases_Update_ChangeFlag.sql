CREATE PROCEDURE dbo.sp_Purchases_Update_ChangeFlag

	(
		@PurchaseID int,
		@FlagID int
	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_purchase set FlagID=@FlagID where PurchaseID=@PurchaseID
	
	RETURN