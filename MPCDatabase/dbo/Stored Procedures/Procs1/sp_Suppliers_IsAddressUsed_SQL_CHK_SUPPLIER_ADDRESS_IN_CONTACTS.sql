CREATE PROCEDURE [dbo].[sp_Suppliers_IsAddressUsed_SQL_CHK_SUPPLIER_ADDRESS_IN_CONTACTS]
	(
		@AddressID int
	)


AS
	SELECT tbl_contacts.AddressID
       FROM tbl_contacts WHERE tbl_contacts.AddressID=@AddressID
	RETURN