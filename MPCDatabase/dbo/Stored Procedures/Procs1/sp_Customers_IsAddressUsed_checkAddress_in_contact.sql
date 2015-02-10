CREATE PROCEDURE [dbo].[sp_Customers_IsAddressUsed_checkAddress_in_contact]

	(
		@AddressID int
	)
AS
	SELECT tbl_Contacts.AddressID
         FROM tbl_Contacts WHERE tbl_Contacts.AddressID=@AddressID
	
	RETURN