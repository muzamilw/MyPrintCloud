CREATE PROCEDURE [dbo].[sp_Customers_deleteCustomerAddressesByCustomerID]

	(
		@CustomerID int
	)

AS
	DELETE FROM tbl_addresses
         WHERE tbl_addresses.contactcompanyid=@CustomerID
         
	RETURN