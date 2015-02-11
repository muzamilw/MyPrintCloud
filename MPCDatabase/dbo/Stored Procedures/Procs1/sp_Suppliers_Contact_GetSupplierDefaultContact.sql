CREATE PROCEDURE [dbo].[sp_Suppliers_Contact_GetSupplierDefaultContact]
(
@SupplierID int
)
AS
SELECT tbl_Contacts.ContactID,tbl_Contacts.AddressID,
        tbl_Contacts.Title,tbl_Contacts.FirstName,tbl_Contacts.MiddleName,tbl_Contacts.LastName,
        (isnull(Title,'') + ' ' + isnull(tbl_Contacts.FirstName,'') + ' ' + isnull(tbl_Contacts.MiddleName,'') + ' ' + isnull(tbl_Contacts.LastName,'')) as FullName,
        tbl_Contacts.Mobile,tbl_Contacts.Pager,tbl_Contacts.JobTitle,
        tbl_Contacts.DOB,tbl_Contacts.Notes,tbl_Contacts.IsDefaultContact,
        tbl_Addresses.AddressName,tbl_Addresses.Address1,tbl_Addresses.Address2,tbl_Addresses.Address3,
        tbl_Addresses.PostCode,tbl_Addresses.Fax,tbl_Addresses.Email,tbl_Addresses.URL,
        tbl_Addresses.City,tbl_Addresses.State,tbl_Addresses.Country,tbl_Addresses.Tel1,tbl_Addresses.Tel2,
        case IsDefaultContact
                when '1' then 'True'
                when '0' then 'False'
        end as DefaultContact
        
         FROM tbl_ContactCompanies
         
         INNER JOIN tbl_Contacts ON (tbl_ContactCompanies.ContactCompanyID = tbl_Contacts.ContactCompanyID)
        INNER JOIN tbl_Addresses ON (tbl_Contacts.ContactCompanyID = tbl_Addresses.ContactCompanyID)
        WHERE (tbl_Contacts.IsDefaultContact = 1 AND tbl_ContactCompanies.ContactCompanyID = @SupplierID)
	RETURN