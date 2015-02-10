CREATE PROCEDURE dbo.sp_Tasks_Update_LockTask
	@TaskID int
AS
	update tbl_tasks set IsLocked = 1 where TaskID=@TaskID
	RETURN