CREATE PROCEDURE [dbo].[sp_Suppliers_Address_SetDefaultAddressUncheck]
(
@SupplierID int
)
AS
UPDATE tbl_addresses
         SET IsDefaultAddress=0 WHERE ContactCompanyID = @SupplierID
	RETURN