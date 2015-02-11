CREATE PROCEDURE [dbo].[sp_Customers_GetCustomerContactsByCustomerID]

	(
		@CustomerID int
     )
AS
	SELECT tbl_Contacts.ContactID,
           (isnull(tbl_Contacts.Title,'') + ' '+ isnull(tbl_Contacts.FirstName,'')+ ' '+ isnull(tbl_Contacts.MiddleName,'') +' '+ isnull(tbl_Contacts.LastName,'')) as FullName,
            tbl_Contacts.AddressID,tbl_Contacts.FirstName,tbl_Contacts.MiddleName,tbl_Contacts.LastName,
           tbl_Contacts.Title,tbl_Contacts.JobTitle,tbl_Contacts.DOB,
           tbl_Contacts.Notes,tbl_Contacts.IsDefaultContact,tbl_Addresses.AddressName,tbl_Addresses.Address1,
           tbl_Addresses.Address2,tbl_Addresses.Address3,tbl_Addresses.City,
           tbl_Addresses.PostCode,tbl_Addresses.Fax,tbl_Addresses.Email,
           tbl_Addresses.URL,tbl_Addresses.Tel1,tbl_Addresses.Tel2,tbl_Addresses.Extension2,
           tbl_Addresses.Extension1,tbl_Addresses.Reference,tbl_Addresses.FAO,tbl_Addresses.IsDefaultAddress,
           tbl_Addresses.State as StateName,tbl_Addresses.Country as CountryName
           FROM tbl_Contacts 
           INNER JOIN tbl_Addresses ON (tbl_Contacts.AddressID = tbl_Addresses.AddressID)
           INNER JOIN tbl_contactCompanies ON (tbl_Contacts.ContactCompanyID = tbl_contactCompanies.ContactCompanyID)
           WHERE (tbl_Contacts.ContactCompanyID = @CustomerID)
           
	RETURN