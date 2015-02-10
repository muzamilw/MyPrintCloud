CREATE PROCEDURE dbo.sp_Campaigns_Delete_CampaignGroupByCampaignID

	@CampaignID int
    
AS
	Delete FROM tbl_campaign_groups where CampaignID = @CampaignID
    
	RETURN