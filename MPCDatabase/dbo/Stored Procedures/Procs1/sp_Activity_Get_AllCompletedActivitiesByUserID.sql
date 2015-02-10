
CREATE PROCEDURE [dbo].[sp_Activity_Get_AllCompletedActivitiesByUserID]
		@SystemUserID int
AS
		select tbl_activity.activityid,
		tbl_activity.activitytypeid,tbl_activitytype.ActivityName,tbl_activity.activitycode,
		tbl_activity.activityref,tbl_activity.activitydate,tbl_activity.activitytime,tbl_activity.activitystarttime,
		tbl_activity.activityendtime,tbl_activity.activitynotes,tbl_activity.isactivityalarm,
		tbl_activity.alarmdate,tbl_activity.alarmtime,tbl_activity.activitylink,
		tbl_activity.iscustomeractivity,tbl_activity.ContactCompanyid,tbl_activity.Contactid,tbl_activity.prospectcontactid,tbl_activity.suppliercontactid,
		tbl_activity.systemuserid,
		tbl_activity.isprivate,tbl_activity.lastmodifieddate,tbl_activity.lastmodifiedtime,
		tbl_activity.lastmodifiedby FROM tbl_activity 
		INNER JOIN tbl_activitytype ON tbl_activitytype.ActivityTypeID= tbl_activity.ActivityTypeID 
		WHERE (tbl_activity.iscomplete<>0 and tbl_activity.SystemUserID=@SystemUserID)
	RETURN