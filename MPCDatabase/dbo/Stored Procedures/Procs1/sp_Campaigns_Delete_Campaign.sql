CREATE PROCEDURE dbo.sp_Campaigns_Delete_Campaign
	@CampaignID int
AS
	Delete FROM tbl_campaigns WHERE CampaignID=@CampaignID
	RETURN