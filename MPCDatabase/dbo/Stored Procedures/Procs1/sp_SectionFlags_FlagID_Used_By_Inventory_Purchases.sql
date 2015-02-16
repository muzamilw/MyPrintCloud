CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_Purchases

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
	SELECT PurchaseID from tbl_purchase WHERE FlagID=@flagID
	RETURN