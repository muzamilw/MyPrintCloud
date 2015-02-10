CREATE PROCEDURE [dbo].[sp_Suppliers_IsAddressUsed_SQL_CHK_SUPPLIER_ADDRESS_DEFAULT]
	(
		@AddressID int
	)

AS
SELECT tbl_addresses.AddressID
        FROM tbl_addresses WHERE tbl_addresses.IsDefaultAddress<>0 AND tbl_addresses.AddressID=@AddressID
	RETURN