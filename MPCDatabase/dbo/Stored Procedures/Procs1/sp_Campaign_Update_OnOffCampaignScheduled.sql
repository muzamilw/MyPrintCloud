CREATE PROCEDURE dbo.sp_Campaign_Update_OnOffCampaignScheduled
@CampaignID int,
@IsScheduled bit
AS
	update tbl_campaigns set EnableSchedule=@IsScheduled where CampaignID=@CampaignID
	RETURN