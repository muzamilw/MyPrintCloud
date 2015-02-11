CREATE PROCEDURE [dbo].[sp_Customers_Addresses_GetLastInsertedAddress]

AS

	Select Max(AddressID) as AddressID from tbl_addresses

	RETURN