/*Gets all the groups from groups table and also gets whether it is used in the campaign or not.*/
Create PROCEDURE [dbo].[sp_Campaigns_Get_AllGroups_By_Campaign]
	@CampaignID int
AS
	SELECT     tbl_groups.GroupID,tbl_groups.GroupName, tbl_groups.GroupDescription,tbl_groups.CreationDateTime,tbl_systemusers.FullName AS CreatedBy,tbl_groups.SystemSiteID,tbl_company_sites.CompanySiteName, 
    (SELECT     CampaignGroupID FROM tbl_campaign_groups WHERE GroupID = tbl_groups.GroupID AND campaignID = @CampaignID) AS CampaignGroupID
    FROM         tbl_groups                        
                            LEFT OUTER JOIN tbl_systemusers ON tbl_groups.CreatedBy = tbl_systemusers.SystemUserID 
                                               INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_groups.SystemSiteID) 
                           

	RETURN