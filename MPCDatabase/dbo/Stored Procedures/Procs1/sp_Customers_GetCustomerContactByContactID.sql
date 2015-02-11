CREATE PROCEDURE [dbo].[sp_Customers_GetCustomerContactByContactID]

	(
		@CustomercontactID int
     )
AS
	SELECT  tbl_Contacts.ContactID,tbl_Contacts.AddressID,
        tbl_Contacts.ContactCompanyID,tbl_Contacts.FirstName,tbl_Contacts.MiddleName,tbl_Contacts.LastName,
        tbl_Contacts.Title,tbl_Contacts.HomeTel1,tbl_Contacts.HomeTel2,tbl_Contacts.HomeExtension1,
        tbl_Contacts.HomeExtension2,tbl_Contacts.Mobile,tbl_Contacts.Pager,tbl_Contacts.JobTitle,
        isnull(tbl_Contacts.DOB,'') as DOB,tbl_Contacts.Notes,tbl_Contacts.IsDefaultContact,
        tbl_Contacts.HomeAddress1,tbl_Contacts.HomeAddress2,tbl_Contacts.HomeCity,
        isnull(tbl_Contacts.HomeState,'') as HomeState,tbl_Contacts.HomePostCode,isnull(tbl_Contacts.HomeCountry,'') as HomeCountry,tbl_Contacts.Email
        FROM tbl_Contacts WHERE (tbl_Contacts.ContactID = @CustomercontactID)
	
	RETURN