CREATE PROCEDURE [dbo].[sp_Suppliers_Address_GetAddressesDBySupplierID]
(
@SupplierID int
)
AS
	SELECT tbl_Addresses.AddressID,
         tbl_Addresses.AddressName,tbl_Addresses.Address1,tbl_Addresses.City,tbl_Addresses.State,
         tbl_Addresses.Country,tbl_Addresses.Tel1,
         
         case IsDefaultAddress
			when '1' then 'True'
			when '0' then 'False'
		end as DefaultAddress	
         
         FROM tbl_Addresses
         WHERE tbl_Addresses.ContactCompanyID=@SupplierID
	RETURN