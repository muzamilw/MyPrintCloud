CREATE PROCEDURE dbo.sp_Activity_Update_DismissAlarm
	@ActivityID int
AS
	UPDATE tbl_activity 
    SET IsActivityAlarm=0 
    WHERE ActivityID=@ActivityID
	RETURN