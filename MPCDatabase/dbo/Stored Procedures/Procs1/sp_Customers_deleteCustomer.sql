CREATE PROCEDURE [dbo].[sp_Customers_deleteCustomer]
(		
            @CustomerID  int
)
	
AS
	DELETE FROM tbl_contactcompanies
         WHERE tbl_contactcompanies.ContactCompanyID=@CustomerID
          
	RETURN