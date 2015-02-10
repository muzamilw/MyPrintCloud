CREATE PROCEDURE [dbo].[sp_Suppliers_IsContactUsed_SQL_CHK_SUPPLIER_IS_DEFAULT_CONTACT]
	(
		@SupplierContactID int
	)
AS
SELECT tbl_contacts.ContactID
         FROM tbl_contacts WHERE tbl_contacts.IsDefaultContact<>0 AND tbl_contacts.ContactID=@SupplierContactID

	RETURN