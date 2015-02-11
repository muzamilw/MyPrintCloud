CREATE PROCEDURE [dbo].[sp_Suppliers_Address_GetLastInsertedAddress]

AS
	Select Max(AddressID) as AddressID from tbl_Addresses
	RETURN