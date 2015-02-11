CREATE PROCEDURE dbo.sp_Activity_Delete_Activity
	@ActivityID int
AS
	Delete FROM tbl_activity WHERE ActivityID=@ActivityID
	RETURN