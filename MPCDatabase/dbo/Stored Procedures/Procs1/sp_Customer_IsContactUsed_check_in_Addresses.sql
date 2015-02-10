CREATE PROCEDURE [dbo].[sp_Customer_IsContactUsed_check_in_Addresses]
	(
		@CustomerContactID int
	)

AS
	SELECT tbl_Contacts.ContactID
        FROM tbl_Contacts WHERE tbl_Contacts.IsDefaultContact<>0 
        AND tbl_Contacts.ContactID=@CustomerContactID

	RETURN