CREATE PROCEDURE [dbo].[sp_Suppliers_DeleteSupplier_SQL_DELETE_SUPPLIER_ADDRESSES]
	(
		@SupplierID int
	)
AS
	DELETE FROM tbl_addresses
       WHERE tbl_addresses.Contactcompanyid=@SupplierID
	RETURN