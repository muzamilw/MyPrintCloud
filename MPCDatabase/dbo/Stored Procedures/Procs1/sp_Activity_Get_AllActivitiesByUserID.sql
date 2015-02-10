CREATE PROCEDURE dbo.sp_Activity_Get_AllActivitiesByUserID
	@SystemUserID int
AS
	SELECT     tbl_activity.ActivityID, tbl_activity.ActivityTypeID, tbl_activitytype.ActivityName, tbl_activity.ActivityCode, tbl_activity.ActivityRef, 
                      tbl_activity.ActivityDate, tbl_activity.ActivityTime, tbl_activity.ActivityStartTime, tbl_activity.ActivityEndTime, tbl_activity.ActivityNotes, 
                      tbl_activity.IsActivityAlarm, tbl_activity.AlarmDate, tbl_activity.AlarmTime, tbl_activity.ActivityLink, tbl_activity.IsCustomerActivity, 
                      tbl_activity.CustomerContactID, tbl_activity.ProspectContactID, tbl_activity.SupplierContactID, tbl_activity.SystemUserID, tbl_activity.IsPrivate, 
                      tbl_activity.LastModifiedDate, tbl_activity.LastModifiedtime, tbl_activity.IsComplete, tbl_activity.LastModifiedBy,
(select case when tbl_activity.IsCustomerActivity = 0 and tbl_activity.ActivityLink <> 0  then (isnull(tbl_suppliercontacts.FirstName,'')+ isnull(tbl_suppliercontacts.LastName,'')+ ' [ ' +isnull(tbl_suppliers.SupplierName,'')+' ]') when tbl_activity.IsCustomerActivity = 1 and tbl_activity.ActivityLink <> 0  then (isnull( tbl_customercontacts.FirstName,'')+ isnull(  tbl_customercontacts.LastName,'')+ ' [ ' +isnull(tbl_customers_1.CustomerName,'')+' ]')  when tbl_activity.IsCustomerActivity = 2 and tbl_activity.ActivityLink <> 0  then (isnull( tbl_customercontacts_1.FirstName,'')+ isnull(  tbl_customercontacts_1.LastName,'')+ ' [ ' +isnull(tbl_customers.CustomerName,'')+' ]') end ) as Contact


FROM         tbl_customers INNER JOIN
                      tbl_customercontacts tbl_customercontacts_1 ON tbl_customers.CustomerID = tbl_customercontacts_1.CustomerID RIGHT OUTER JOIN
                      tbl_activity INNER JOIN
                      tbl_activitytype ON tbl_activitytype.ActivityTypeID = tbl_activity.ActivityTypeID LEFT OUTER JOIN
                      tbl_suppliers INNER JOIN
                      tbl_suppliercontacts ON tbl_suppliers.SupplierID = tbl_suppliercontacts.SupplierID ON 
                      tbl_activity.SupplierContactID = tbl_suppliercontacts.SupplierContactID ON 
                      tbl_customercontacts_1.CustomerContactID = tbl_activity.ProspectContactID LEFT OUTER JOIN
                      tbl_customers tbl_customers_1 INNER JOIN
                      tbl_customercontacts ON tbl_customers_1.CustomerID = tbl_customercontacts.CustomerID ON 
                      tbl_activity.CustomerContactID = tbl_customercontacts.CustomerContactID
						WHERE (tbl_activity.SystemUserID=@SystemUserID) OR (tbl_activity.IsPrivate=0)
	/*select tbl_activity.activityid,
    tbl_activity.activitytypeid,tbl_activitytype.ActivityName,tbl_activity.activitycode,
    tbl_activity.activityref,tbl_activity.activitydate,tbl_activity.activitytime,tbl_activity.activitystarttime,
    tbl_activity.activityendtime,tbl_activity.activitynotes,tbl_activity.isactivityalarm,
    tbl_activity.alarmdate,tbl_activity.alarmtime,tbl_activity.activitylink,
    tbl_activity.iscustomeractivity,tbl_activity.customercontactid,tbl_activity.prospectcontactid,tbl_activity.suppliercontactid,
    tbl_activity.systemuserid,
    tbl_activity.isprivate,tbl_activity.lastmodifieddate,tbl_activity.lastmodifiedtime,tbl_activity.IsComplete,
    tbl_activity.lastmodifiedby FROM tbl_activity 
    INNER JOIN tbl_activitytype ON tbl_activitytype.ActivityTypeID= tbl_activity.ActivityTypeID 
    WHERE (tbl_activity.SystemUserID=@SystemUserID) OR (tbl_activity.IsPrivate=0)*/
	RETURN