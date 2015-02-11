
CREATE PROCEDURE sp_Get_Contact_Activities_Summary
	@CustomerContactID int,
	@SystemUserID int
AS
BEGIN

select tbl_activity.activityid,max(tbl_activity.activitystarttime) as activitydate,tbl_activity.activitytime 
,tbl_activity.activitytypeid
from tbl_activity     
where tbl_activity.IsCustomerActivity=1 and tbl_activity.ContactID=@CustomerContactID and tbl_activity.systemuserid=@SystemUserID
 and tbl_activity.iscomplete=0 
group by tbl_activity.activitystarttime,activityid,activitytime,tbl_activity.activitytypeid

select max(tbl_activity.activitystarttime) as activitydate from tbl_activity 
where contactid=@CustomerContactID and systemuserid=@SystemUserID and iscomplete=0

select max(tbl_activity.Completiondate) as ActivityDate from tbl_activity 
where  contactid=@CustomerContactID and systemuserid=@SystemUserID and iscomplete=0

select max(lastmodifieddate) AS LastModifiedDate from tbl_activity 
where contactid=@CustomerContactID and systemuserid=@SystemUserID
	

END