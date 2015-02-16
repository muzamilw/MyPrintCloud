CREATE PROCEDURE dbo.ap_Suppliers_Supplier_SupplierTypes_IsSupplierTypeUsed

	(
		@SupplierTypeID int
	)

AS
SELECT tbl_suppliers.SupplierTypeID FROM tbl_suppliers WHERE tbl_suppliers.SupplierTypeID=@SupplierTypeID
	RETURN