CREATE PROCEDURE dbo.sp_Campagins_Update_DeliveryStatusByCampaign
	(
		@IsDelivered bit,
		@CampaignID int
	)
AS
Update tbl_EmailCampaignTracking set isDeliverd=@IsDelivered where CampaignID =@CampaignID
	RETURN