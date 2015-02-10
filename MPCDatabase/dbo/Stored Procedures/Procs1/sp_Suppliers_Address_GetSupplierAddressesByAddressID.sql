CREATE PROCEDURE [dbo].[sp_Suppliers_Address_GetSupplierAddressesByAddressID]
(
@AddressID int
)
AS
SELECT  tbl_addresses.ContactCompanyID,
      tbl_addresses.AddressName,tbl_addresses.Address1,tbl_addresses.Address2,tbl_addresses.Address3,
      tbl_addresses.City,tbl_addresses.State,tbl_addresses.Country,tbl_addresses.PostCode,
      tbl_addresses.Fax,tbl_addresses.Email,tbl_addresses.URL,tbl_addresses.Tel1,
      tbl_addresses.Tel2,tbl_addresses.Extension1,tbl_addresses.Extension2,tbl_addresses.Reference,
      tbl_addresses.FAO,tbl_addresses.IsDefaultAddress FROM tbl_addresses
       WHERE (tbl_addresses.AddressID = @AddressID)
	RETURN