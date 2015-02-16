CREATE PROCEDURE dbo.sp_Customers_Contacts_GetCustomerContactWithBusinessAddressByContactID
(
 @CustomerContactID int
)
AS
	SELECT tbl_customercontacts.CustomerContactID,tbl_customercontacts.AddressID,
        (isnull(tbl_customercontacts.Title,'') + ' ' + isnull(tbl_customercontacts.FirstName,'') + ' '+ isnull(tbl_customercontacts.MiddleName,'') + ' ' + isnull(tbl_customercontacts.LastName,'')) as FullName,tbl_customercontacts.FirstName,tbl_customercontacts.LastName, 
         tbl_customercontacts.Department,tbl_customeraddresses.Address1,tbl_customeraddresses.Address2,tbl_customeraddresses.Address3,tbl_customeraddresses.City,tbl_state.StateName ,tbl_country.CountryName ,tbl_customeraddresses.PostCode,
         tbl_customeraddresses.Fax,tbl_customeraddresses.Tel1,tbl_customercontacts.IsDefaultContact 
         FROM tbl_state 
         INNER JOIN tbl_customeraddresses ON (tbl_state.StateID = tbl_customeraddresses.StateID) 
         INNER JOIN tbl_customercontacts ON (tbl_customeraddresses.AddressID = tbl_customercontacts.AddressID) 
         INNER JOIN tbl_country ON (tbl_customeraddresses.CountryID = tbl_country.CountryID) 
         WHERE tbl_customercontacts.CustomerContactID = @CustomerContactID
	RETURN