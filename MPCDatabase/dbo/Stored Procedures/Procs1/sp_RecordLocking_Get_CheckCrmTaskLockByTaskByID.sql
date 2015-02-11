CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckCrmTaskLockByTaskByID
(
	@RecordID int	
)
AS
	SELECT tbl_Tasks.LockedBy, tbl_Tasks.TaskID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right Outer JOIN tbl_tasks ON (tbl_systemusers.SystemUserID = tbl_tasks.LockedBy) where tbl_tasks.TaskID =@RecordID
	RETURN