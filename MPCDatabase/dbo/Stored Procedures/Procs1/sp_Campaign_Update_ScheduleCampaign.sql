CREATE PROCEDURE dbo.sp_Campaign_Update_ScheduleCampaign
@CampaignID int,
@ScheduleDateTime datetime
AS
	update tbl_campaigns set EnableSchedule=1,StartDateTime=@ScheduleDateTime where CampaignID=@CampaignID
	RETURN