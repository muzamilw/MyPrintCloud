CREATE PROCEDURE dbo.sp_Tasks_Update_UnlockTask
	@TaskID int
AS
	update tbl_tasks set IsLocked = 0 where TaskID=@TaskID
	RETURN