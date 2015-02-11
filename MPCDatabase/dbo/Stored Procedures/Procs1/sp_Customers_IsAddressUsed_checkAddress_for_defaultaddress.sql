CREATE PROCEDURE [dbo].[sp_Customers_IsAddressUsed_checkAddress_for_defaultaddress]

	(
		@AddressID int
	)

AS
	SELECT tbl_Addresses.AddressID
         FROM tbl_Addresses WHERE tbl_Addresses.IsDefaultAddress<>0 
         AND tbl_Addresses.AddressID=@AddressID

	RETURN