/*Gets all the groups and also with a field checking whether this contact is used in this group or not for each group.*/
CREATE PROCEDURE [dbo].[sp_Groups_Get_AllGroups_ByContactID]
	@ContactID int,@CompanyType smallint
AS
	SELECT     GroupID, GroupName, tbl_systemusers.FullName AS CreatedBy,tbl_company_sites.CompanySiteName,
      (SELECT     GroupDetailID
      FROM          tbl_group_detail
      WHERE      GroupID = tbl_groups.GroupID AND ContactID = @ContactID AND IsCustomerContact =@CompanyType) AS GroupDetailID                            
	FROM  tbl_groups
LEFT OUTER JOIN tbl_systemusers ON tbl_groups.CreatedBy = tbl_systemusers.SystemUserID
INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_groups.SystemSiteID) 
	RETURN