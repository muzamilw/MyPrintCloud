CREATE PROCEDURE dbo.sp_Campaigns_Get_CampaignsByUser
	@UID int
AS
	SELECT     tbl_campaigns.CampaignID, tbl_campaigns.CampaignName, tbl_campaigns.Description, tbl_campaigns.EnableSchedule, tbl_campaigns.UID, 
                      tbl_campaigns.Private, tbl_systemusers.FullName AS Owner,tbl_campaigns.Status
FROM         tbl_campaigns LEFT OUTER JOIN
                      tbl_systemusers ON tbl_campaigns.UID = tbl_systemusers.SystemUserID
                      
                      Where tbl_campaigns.UID=@UID or tbl_campaigns.Private=0 ORDER BY tbl_Campaigns.CampaignID DESC
	RETURN