CREATE PROCEDURE [dbo].[sp_Customers_deleteCustomerAddress]
(		
           @AddressID  int
)
AS

		DELETE FROM tbl_Addresses
         WHERE tbl_Addresses.AddressID=@AddressID
        
	RETURN