CREATE PROCEDURE dbo.sp_Campaign_Update_CampaignStatus
@Status int,
@CampaignID int
AS
	update tbl_campaigns set Status=@Status where CampaignID=@CampaignID
	RETURN