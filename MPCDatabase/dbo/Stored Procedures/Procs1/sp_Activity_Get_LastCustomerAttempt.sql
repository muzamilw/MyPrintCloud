
Create PROCEDURE [dbo].[sp_Activity_Get_LastCustomerAttempt]
	@SystemUserID int, 
	@CustomercontactID int
AS
	select max(tbl_activity.activitystarttime) as activitydate from tbl_activity where contactid=@CustomercontactID and systemuserid=@SystemUserID and iscomplete=0
	RETURN