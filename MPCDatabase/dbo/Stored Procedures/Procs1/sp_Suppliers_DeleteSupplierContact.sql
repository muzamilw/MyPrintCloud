CREATE PROCEDURE [dbo].[sp_Suppliers_DeleteSupplierContact]
	(
		@SupplierContactID int
	)
AS
	DELETE FROM tbl_contacts
        WHERE tbl_contacts.ContactID=@SupplierContactID
	RETURN