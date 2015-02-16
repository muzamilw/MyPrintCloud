CREATE PROCEDURE dbo.sp_Campaigns_Get_CampaignTrackingValues
	@CampaignID int,@TotalRec int=0 output,@Sent int=0 output
AS
	SELECT   @TotalRec = Count(EmailCampaignTrakingID) 
FROM         tbl_EmailCampaignTracking
WHERE     (CampaignID = @CampaignID)

	SELECT   @Sent = Count(EmailCampaignTrakingID) 
FROM         tbl_EmailCampaignTracking
WHERE     (CampaignID = @CampaignID AND IsDeliverd <>0)
	
print @TotalRec
print @Sent