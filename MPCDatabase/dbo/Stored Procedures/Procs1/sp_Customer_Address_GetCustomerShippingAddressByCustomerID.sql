CREATE PROCEDURE [dbo].[sp_Customer_Address_GetCustomerShippingAddressByCustomerID]

	(
		@CustomerID int
	)

AS
	SELECT     tbl_addresses.AddressID, tbl_addresses.contactcompanyid, tbl_addresses.AddressName, tbl_addresses.Address1, 
                      tbl_addresses.Address2, tbl_addresses.Address3, tbl_addresses.City, tbl_addresses.State, 
                      tbl_addresses.Country, tbl_addresses.PostCode, tbl_addresses.Fax, tbl_addresses.Email, 
                      tbl_addresses.URL, tbl_addresses.Tel1, tbl_addresses.Tel2, tbl_addresses.Extension1, 
                      tbl_addresses.Extension2, tbl_addresses.Reference, tbl_addresses.FAO, tbl_addresses.IsDefaultAddress, 
                      tbl_addresses.IsDefaultShippingAddress
FROM         tbl_addresses
        WHERE (tbl_addresses.contactcompanyid = @CustomerID AND IsDefaultShippingAddress <> 0)
        
	RETURN

drop procedure sp_Customer_Address_GetCustomerShippingAddressBycontactcompanyid