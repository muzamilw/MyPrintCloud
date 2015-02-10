CREATE PROCEDURE [dbo].[sp_Group_Get_GroupDetailByGroupID]
@GroupID int
AS

SELECT     tbl_group_detail.GroupDetailID, tbl_group_detail.ContactID, tbl_group_detail.IsCustomerContact, tbl_group_detail.GroupID, 
ISNULL(tbl_contacts.FirstName,'')+ ' '+ ISNULL(tbl_contacts.MiddleName,'') + ' '+ ISNULL(tbl_contacts.LastName,'') as ContactName,
tbl_contactCompanies.Name CompanyName
FROM         tbl_group_detail 
LEFT OUTER JOIN tbl_Contacts ON tbl_group_detail.ContactID = tbl_Contacts.ContactID 
LEFT OUTER JOIN tbl_contactCompanies ON tbl_Contacts.ContactcompanyID = tbl_contactCompanies.ContactcompanyID
WHERE     (tbl_group_detail.GroupID = @GroupID)
	RETURN