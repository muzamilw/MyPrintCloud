CREATE PROCEDURE [dbo].[sp_Customer_Users_ReadByCustomer]

(
	@CustomerID int
)
AS
	SELECT tbl_Contacts.ContactID,tbl_Contacts.ContactCompanyID,
(isnull(tbl_Contacts.FirstName,'') + ' ' + isnull(tbl_Contacts.MiddleName,'') + ' ' + isnull(tbl_Contacts.LastName,'')) as UserName,
tbl_Contacts.DepartmentID,tbl_ContactRoles.ContactRoleName,
CASE IsApprover
WHEN '0' THEN 'False'
WHEN '1' THEN 'True'
END AS PowerUser,

tbl_Contacts.Password,tbl_Contacts.ContactRoleID,tbl_Contacts.FirstName,tbl_Contacts.MiddleName,
tbl_Contacts.LastName,tbl_Contacts.Title,tbl_addresses.AddressName as Address,tbl_addresses.City,tbl_addresses.State,
tbl_addresses.Country,tbl_addresses.PostCode,tbl_addresses.Tel1,tbl_Contacts.Url,tbl_Contacts.Fax,
tbl_Contacts.Mobile,tbl_Contacts.Email,tbl_Contacts.JobTitle,tbl_Contacts.DOB,tbl_Contacts.IsApprover
FROM tbl_ContactRoles 
INNER JOIN tbl_Contacts ON (tbl_ContactRoles.ContactRoleID = tbl_Contacts.ContactRoleID)
INNER Join tbl_addresses ON (tbl_addresses.addressid = tbl_contacts.addressid) 
        where tbl_Contacts.ContactCompanyId=@CustomerID


	RETURN