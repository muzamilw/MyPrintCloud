
Create PROCEDURE [dbo].[sp_Activity_Get_LastCustomerContact]
	@CustomerContactID	int,
	@SystemUserID	int
AS
	select max(tbl_activity.Completiondate) as ActivityDate from tbl_activity where  contactid=@CustomerContactID and systemuserid=@Systemuserid and iscomplete=0
	RETURN