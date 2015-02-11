CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_Suppliers

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
SELECT SupplierID from tbl_suppliers WHERE FlagID=@flagID
	RETURN