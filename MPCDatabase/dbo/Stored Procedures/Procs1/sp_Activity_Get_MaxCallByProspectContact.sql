CREATE PROCEDURE dbo.sp_Activity_Get_MaxCallByProspectContact
	@SystemUserID int,
	@CustomerContactID int
AS
	select tbl_activity.activityid,max(tbl_activity.activitystarttime) as activitydate,tbl_activity.activitytime from tbl_activity         
        where tbl_activity.IsCustomerActivity=2 and tbl_activity.ProspectContactID=@CustomerContactID and tbl_activity.systemuserid=@SystemUserID and tbl_activity.activitytypeid=1 and tbl_activity.iscomplete=0 
        group by tbl_activity.activitystarttime,tbl_activity.ActivityID,tbl_activity.ActivityTime
	RETURN