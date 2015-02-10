CREATE PROCEDURE [dbo].[sp_Suppliers_DeleteSupplierAddress]
	(
		@AddressID int
	)
AS
DELETE FROM tbl_addresses
       WHERE tbl_addresses.AddressID=@AddressID
	RETURN