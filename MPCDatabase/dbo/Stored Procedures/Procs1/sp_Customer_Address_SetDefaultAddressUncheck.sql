CREATE PROCEDURE [dbo].[sp_Customer_Address_SetDefaultAddressUncheck]

	(
	
	@CustomerID int
	)
AS
	UPDATE tbl_Addresses 
           SET IsDefaultAddress=0 
           WHERE ContactCompanyID = @CustomerID
          
	RETURN