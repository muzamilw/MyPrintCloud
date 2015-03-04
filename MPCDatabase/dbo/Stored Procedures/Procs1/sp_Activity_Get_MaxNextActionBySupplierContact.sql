﻿CREATE PROCEDURE dbo.sp_Activity_Get_MaxNextActionBySupplierContact
	@SystemUserID int,
	@CustomerContactID int
AS
	select tbl_activity.activityid,max(tbl_activity.activitystarttime) as activitydate,tbl_activity.activitytime from tbl_activity         
        where tbl_activity.IsCustomerActivity=0 and tbl_activity.SupplierContactID=@CustomerContactID and tbl_activity.systemuserid=@SystemUserID and tbl_activity.activitytypeid=2 and tbl_activity.iscomplete=0 
        group by tbl_activity.activitystarttime,activityid,activitytime
        
	RETURN