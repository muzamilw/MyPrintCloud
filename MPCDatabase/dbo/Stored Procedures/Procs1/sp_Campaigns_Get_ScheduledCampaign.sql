CREATE PROCEDURE dbo.sp_Campaigns_Get_ScheduledCampaign
	@StartDateTime DateTime
AS
	Select 
CampaignID,
UID,
CampaignName,
DataSourceType,
SQLQuery,
EnableSchedule,
StartDateTime,
CampaignType
FROM tbl_campaigns
Where tbl_campaigns.EnableSchedule<>0 and StartDateTime <= @StartDateTime and Status = 0
	RETURN