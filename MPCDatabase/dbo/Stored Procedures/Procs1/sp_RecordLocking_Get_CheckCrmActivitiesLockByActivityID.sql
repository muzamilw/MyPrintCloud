CREATE PROCEDURE [dbo].[sp_RecordLocking_Get_CheckCrmActivitiesLockByActivityID]
(
	@RecordID int	
)
AS
	SELECT tbl_activity.LockedBy, tbl_activity.ActivityID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right Outer JOIN tbl_activity ON (tbl_systemusers.SystemUserID = tbl_activity.LockedBy) where tbl_activity.ActivityID =@RecordID 
	RETURN