CREATE PROCEDURE dbo.sp_Activity_Update_LockActivity
	@ActivityID int
AS
	update tbl_activity set IsLocked = 1 where ActivityID=@ActivityID
	RETURN