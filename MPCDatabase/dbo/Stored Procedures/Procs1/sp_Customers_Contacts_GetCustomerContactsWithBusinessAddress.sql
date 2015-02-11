CREATE PROCEDURE [dbo].[sp_Customers_Contacts_GetCustomerContactsWithBusinessAddress]
(
@CustomerID int
)
AS
	
SELECT tbl_Contacts.ContactID,tbl_Contacts.AddressID,
        (isnull(tbl_Contacts.Title,'') + ' ' + isnull(tbl_Contacts.FirstName,'') + ' '+ isnull(tbl_Contacts.MiddleName,'') + ' ' + isnull(tbl_Contacts.LastName,'')) as FullName,tbl_Contacts.FirstName,tbl_Contacts.LastName, 
        tbl_Addresses.Address1,tbl_Addresses.Address2,tbl_Addresses.Address3,tbl_Addresses.City,tbl_Addresses.state as StateName ,tbl_Addresses.country as CountryName ,tbl_Addresses.PostCode,
        tbl_Addresses.Fax,tbl_Addresses.Tel1,tbl_Contacts.IsDefaultContact,tbl_Contacts.Email 
        FROM tbl_Contacts 
        INNER JOIN tbl_Addresses ON (tbl_Addresses.AddressID = tbl_Contacts.AddressID) 
        WHERE tbl_Contacts.ContactCompanyID = @CustomerID
	RETURN