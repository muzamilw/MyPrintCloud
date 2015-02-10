CREATE PROCEDURE dbo.StoredProcedure13
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT     tbl_campaigns.CampaignID, tbl_campaigns.CampaignName, tbl_campaigns.Description, tbl_campaigns.EnableSchedule, tbl_campaigns.UID, 
                      tbl_campaigns.Private, tbl_systemusers.FullName AS Owner
FROM         tbl_campaigns LEFT OUTER JOIN
                      tbl_systemusers ON tbl_campaigns.UID = tbl_systemusers.SystemUserID
	RETURN