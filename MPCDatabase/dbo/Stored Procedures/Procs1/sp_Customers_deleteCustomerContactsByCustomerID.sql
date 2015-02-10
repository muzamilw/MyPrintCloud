CREATE PROCEDURE [dbo].[sp_Customers_deleteCustomerContactsByCustomerID]

	(
		@CustomerID int
	)

AS
	DELETE FROM tbl_contacts
         WHERE tbl_contacts.contactcompanyid=@CustomerID
        
	RETURN