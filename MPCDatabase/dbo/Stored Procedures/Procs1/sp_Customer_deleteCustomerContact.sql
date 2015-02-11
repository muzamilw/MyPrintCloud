CREATE PROCEDURE [dbo].[sp_Customer_deleteCustomerContact]

	(
		@CustomerContactID int
	)

AS
	DELETE FROM tbl_contacts
          WHERE tbl_contacts.ContactID=@CustomerContactID
          
	RETURN