CREATE PROCEDURE dbo.sp_Suppliers_Supplier_UpdateFlag
(
 @SupplierID int,
 @FlagID int
)
AS
UPDATE tbl_suppliers SET FlagID=@FlagID WHERE SupplierID=@SupplierID

	RETURN