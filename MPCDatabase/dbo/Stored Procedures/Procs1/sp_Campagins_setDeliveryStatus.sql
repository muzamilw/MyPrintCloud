CREATE PROCEDURE dbo.sp_Campagins_setDeliveryStatus

	(
		@EmailTrackinIDs varchar(5000)

	)

AS

--if CHARINDEX(',',@EmailTrackinIDs,LEN(@EmailTrackinIDs)) <>0
--	set @EmailTrackinIDs =  left(@EmailTrackinIDs, CHARINDEX(',',@EmailTrackinIDs,LEN(@EmailTrackinIDs))-1)
	
--print @EmailTrackinIDs


exec ('Update tbl_EmailCampaignTracking set isDeliverd=1 where EmailCampaignTrakingID in ('+@EmailTrackinIDs+')')

	RETURN