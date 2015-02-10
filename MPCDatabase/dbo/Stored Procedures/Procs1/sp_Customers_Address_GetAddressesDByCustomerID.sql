CREATE PROCEDURE [dbo].[sp_Customers_Address_GetAddressesDByCustomerID]

	(
       @CustomerID INT
	)

AS
	SELECT tbl_Addresses.AddressID,
       tbl_Addresses.AddressName,tbl_Addresses.Address1,tbl_Addresses.City,tbl_Addresses.State,
        tbl_Addresses.Country,tbl_Addresses.Tel1,IsDefaultShippingAddress,
        
        CASE IsDefaultAddress
			WHEN 0 THEN 'False'
			ELSE 'True'
		END AS DefaultAddress
	              
        FROM tbl_Addresses
       WHERE tbl_Addresses.ContactCompanyID=@CustomerID
	RETURN