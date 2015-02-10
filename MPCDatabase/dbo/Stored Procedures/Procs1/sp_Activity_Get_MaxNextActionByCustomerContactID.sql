
Create PROCEDURE [dbo].[sp_Activity_Get_MaxNextActionByCustomerContactID]
	@CustomerContactID int,
	@SystemUserID	int
AS
	select tbl_activity.activityid,max(tbl_activity.activitystarttime) as activitydate,tbl_activity.activitytime from tbl_activity     
    where tbl_activity.IsCustomerActivity=1 and tbl_activity.ContactID=@CustomerContactID and tbl_activity.systemuserid=@SystemUserID and tbl_activity.activitytypeid=2 and tbl_activity.iscomplete=0 
    group by tbl_activity.activitystarttime,ActivityID,ActivityTime
	RETURN