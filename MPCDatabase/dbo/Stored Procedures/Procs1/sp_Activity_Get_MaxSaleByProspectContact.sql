CREATE PROCEDURE dbo.sp_Activity_Get_MaxSaleByProspectContact
	@CustomerContactID int,
	@SystemUserID int
AS
	select tbl_activity.activityid,max(tbl_activity.activitystarttime) as activitydate,tbl_activity.activitytime from tbl_activity 
                INNER JOIN tbl_activitytype ON tbl_activitytype.ActivityTypeID= tbl_activity.ActivityTypeID 
                where tbl_activity.IsCustomerActivity=2 and tbl_activity.ProspectContactID=@CustomerContactID and tbl_activity.systemuserid=@SystemUserID and tbl_activity.activitytypeid=4 and tbl_activity.iscomplete=0 
                group by tbl_activity.activitystarttime,activityid,activitytime
	RETURN