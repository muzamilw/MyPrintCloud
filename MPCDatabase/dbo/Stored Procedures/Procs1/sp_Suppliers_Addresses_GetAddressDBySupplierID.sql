CREATE PROCEDURE dbo.sp_Suppliers_Addresses_GetAddressDBySupplierID
(
 @SupplierID int
)
AS

SELECT tbl_supplieraddresses.AddressID,
        tbl_supplieraddresses.AddressName,tbl_supplieraddresses.Address1,tbl_supplieraddresses.Address2,tbl_supplieraddresses.Address3,
         tbl_supplieraddresses.City,tbl_supplieraddresses.StateID,tbl_state.StateName,tbl_supplieraddresses.CountryID,
         tbl_country.CountryName,tbl_supplieraddresses.Tel1,
         case IsDefaultAddress
         when '0' then 'False'
         when '1' then 'True'
         end as DefaultAddress
                           
         FROM tbl_supplieraddresses
         INNER JOIN tbl_state ON (tbl_supplieraddresses.StateID = tbl_state.StateID)
         INNER JOIN tbl_country ON (tbl_supplieraddresses.CountryID = tbl_country.CountryID)
        WHERE tbl_supplieraddresses.SupplierID=@SupplierID
	RETURN