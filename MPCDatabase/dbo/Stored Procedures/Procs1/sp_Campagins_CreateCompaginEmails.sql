CREATE PROCEDURE [dbo].[sp_Campagins_CreateCompaginEmails]

	(
		@CampaignID int ,
		@EmailQry as varchar(MAX),
		@EmailQry1 as varchar(8000) = " "
	)

AS
	

delete from tbl_EmailCampaignTracking where CampaignID = @CampaignID




exec('insert into tbl_EmailCampaignTracking (CampaignID,EmailAddress,ID,ContactID,MailFlag) 
		 '+@emailQry+@emailQry1+'')

--  for Testing Only
/*exec('insert into tbl_EmailCampaignTracking (CampaignID,EmailAddress) 
		 select 5 as compaginID,Email from tbl_customercontacts')
*/

	
	RETURN