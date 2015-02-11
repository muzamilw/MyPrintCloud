CREATE PROCEDURE dbo.sp_Activity_Update_UnLockActivity
	@ActivityID int
AS
	update tbl_activity set IsLocked = 0 where ActivityID=@ActivityID
	RETURN