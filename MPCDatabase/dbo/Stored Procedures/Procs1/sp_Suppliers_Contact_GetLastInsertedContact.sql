CREATE PROCEDURE [dbo].[sp_Suppliers_Contact_GetLastInsertedContact]

AS
Select Max(ContactID) as ContactID  from tbl_contacts

	RETURN