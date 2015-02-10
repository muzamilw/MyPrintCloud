CREATE PROCEDURE dbo.sp_RecordLocking_update_CrmTasks
	(
	
		@UserID int,
		@RecordID int
	
	)

AS
	update tbl_tasks set LockedBy=@UserID where Taskid=@RecordID
	RETURN