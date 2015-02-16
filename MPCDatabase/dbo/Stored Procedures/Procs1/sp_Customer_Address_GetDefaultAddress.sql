CREATE PROCEDURE [dbo].[sp_Customer_Address_GetDefaultAddress]

	(
		@CustomerID int
	)

AS
	SELECT tbl_Addresses.AddressID,tbl_Addresses.AddressName,tbl_Addresses.Address1,
	tbl_Addresses.City,tbl_Addresses.State,tbl_Addresses.Country,tbl_Addresses.Tel1,
	
      CASE IsDefaultAddress
         WHEN '0' THEN 'True'
         WHEN '1' THEN 'False'
       END AS DefaultAddress
    
    FROM tbl_Addresses
    WHERE tbl_Addresses.ContactCompanyID=@CustomerID AND tbl_Addresses.IsDefaultAddress=1
	
	RETURN