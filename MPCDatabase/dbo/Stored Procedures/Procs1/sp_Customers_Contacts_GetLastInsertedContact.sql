CREATE PROCEDURE [dbo].[sp_Customers_Contacts_GetLastInsertedContact]

	

AS
	Select Max(ContactID) as ContactID  from tbl_contacts

	RETURN