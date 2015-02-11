-- =============================================
-- Author:		Muzzammil
-- Create date: 24 nov 2006
-- Description:	clear data
-- =============================================
CREATE PROCEDURE [dbo].[DeleteData] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    --clear invoices
	delete tbl_invoicedetails
	delete dbo.tbl_invoices

	--clear campaigns
	delete dbo.tbl_campaigns
	delete dbo.tbl_CampaignReports
	delete dbo.tbl_campaign_groups
	delete dbo.tbl_campaign_clickthroughs


END