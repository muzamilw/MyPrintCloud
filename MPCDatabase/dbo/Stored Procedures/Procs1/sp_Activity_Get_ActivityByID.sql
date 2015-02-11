
CREATE PROCEDURE [dbo].[sp_Activity_Get_ActivityByID]
	@ActivityID int
AS
	select tbl_activity.activityid,tbl_activity.activitytypeid,
tbl_activity.activitycode,tbl_activity.activityref,tbl_activity.activitydate,
tbl_activity.activitytime,tbl_activity.activitystarttime,tbl_activity.activityendtime,tbl_activity.activityProbability,tbl_activity.activityUnit,
tbl_activity.activityPrice,tbl_activity.activitynotes,
tbl_activity.isactivityalarm,tbl_activity.alarmdate,tbl_activity.alarmtime,tbl_activity.activitylink,tbl_activity.iscustomeractivity,tbl_activity.ContactID,tbl_activity.SupplierContactID,tbl_activity.ProspectContactID,
tbl_activity.systemuserid,tbl_activity.isprivate,
tbl_activity.iscomplete,tbl_activity.completiondate,tbl_activity.completiontime,tbl_activity.completionsuccess,
tbl_activity.completionresult,tbl_activity.isfollowedup,tbl_activity.followedactivityid,
tbl_activity.lastmodifieddate,tbl_activity.lastmodifiedtime,
tbl_activitytype.ActivityName,A.FullName as FullName,B.FullName as CreatedByFullName,tbl_activity.ContactCompanyID,tbl_contactcompanies.IsCustomer
from tbl_activity 
INNER JOIN tbl_activitytype ON tbl_activitytype.ActivityTypeID= tbl_activity.ActivityTypeID 
INNER JOIN tbl_systemusers A on A.systemuserid=tbl_activity.systemuserid 
INNER JOIN tbl_systemusers B on B.systemuserid=tbl_activity.CreatedBy 
inner join  tbl_contactcompanies on tbl_contactcompanies.contactcompanyid = tbl_activity.contactcompanyid 
    where tbl_activity.activityid=@ActivityID
	RETURN