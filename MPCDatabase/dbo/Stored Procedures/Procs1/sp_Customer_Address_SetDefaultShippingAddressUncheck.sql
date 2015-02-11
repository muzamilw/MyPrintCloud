CREATE PROCEDURE [dbo].[sp_Customer_Address_SetDefaultShippingAddressUncheck]

	(
	
	@CustomerID int
	)
AS
	UPDATE tbl_Addresses 
           SET IsDefaultShippingAddress=0 
           WHERE ContactCompanyID = @CustomerID
          
	RETURN