CREATE PROCEDURE [dbo].[sp_Customers_Contacts_GetContactsDByCustomerID]

	(
       @CustomerID int
	)

AS
SELECT tbl_Contacts.ContactID,
	(isnull(Title,' ') + ' ' + isnull(tbl_Contacts.FirstName,' ') + ' ' + isnull(tbl_Contacts.MiddleName,' ') + ' ' + isnull(tbl_Contacts.LastName,' ')) as FullName,
tbl_Addresses.AddressName,tbl_Addresses.City,tbl_Addresses.State,tbl_Addresses.Country,
CASE IsDefaultContact
WHEN '1' THEN 'True'
WHEN '0' THEN 'False'
END AS DefaultContact

FROM tbl_Contacts
INNER JOIN tbl_Addresses ON (tbl_Contacts.AddressID = tbl_Addresses.AddressID)
WHERE (tbl_Contacts.ContactCompanyID = @CustomerID)
	RETURN