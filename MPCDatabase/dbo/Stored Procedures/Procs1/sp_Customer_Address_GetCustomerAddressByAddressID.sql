CREATE PROCEDURE [dbo].[sp_Customer_Address_GetCustomerAddressByAddressID]

	(
		@AddressID int
	)

AS
	SELECT     tbl_Addresses.AddressID, tbl_Addresses.ContactCompanyID, tbl_Addresses.AddressName, tbl_Addresses.Address1, 
                      tbl_Addresses.Address2, tbl_Addresses.Address3, tbl_Addresses.City, tbl_Addresses.State, 
                      tbl_Addresses.Country, tbl_Addresses.PostCode, tbl_Addresses.Fax, tbl_Addresses.Email, 
                      tbl_Addresses.URL, tbl_Addresses.Tel1, tbl_Addresses.Tel2, tbl_Addresses.Extension1, 
                      tbl_Addresses.Extension2, tbl_Addresses.Reference, tbl_Addresses.FAO, tbl_Addresses.IsDefaultAddress, 
                      tbl_Addresses.IsDefaultShippingAddress
FROM         tbl_Addresses 
        WHERE (tbl_Addresses.AddressID = @AddressID)
        
	RETURN