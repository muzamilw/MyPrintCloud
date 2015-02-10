CREATE PROCEDURE dbo.sp_Campaigns_Insert_CampaignGroup

	@CampaignID int,@GroupID int
    
AS
	INSERT INTO tbl_campaign_groups (CampaignID,GroupID) VALUES
    (@CampaignID,@GroupID)
    
	RETURN