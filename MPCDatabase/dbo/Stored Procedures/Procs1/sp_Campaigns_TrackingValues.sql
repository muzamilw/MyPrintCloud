CREATE PROCEDURE dbo.sp_Campaigns_TrackingValues
	@CampaignID int,@TotalRec int
AS
	SELECT     EmailCampaignTrakingID AS Total,IsDeliverd
FROM         tbl_EmailCampaignTracking
WHERE     (CampaignID = @CampaignID)
	RETURN