CREATE PROCEDURE dbo.sp_Campaigns_Get_CampaignGroupIDs
	@CampaignID int
AS
	SELECT     GroupID
FROM         tbl_campaign_groups
WHERE     (CampaignID = @CampaignID)
	RETURN