CREATE PROCEDURE [dbo].[sp_Suppliers_DeleteSupplier_SQL_DELETE_SUPPLIER_CONTACTS]
	(
		@SupplierID int
	)
AS
DELETE FROM tbl_contacts
      WHERE tbl_contacts.Contactcompanyid=@SupplierID
	RETURN