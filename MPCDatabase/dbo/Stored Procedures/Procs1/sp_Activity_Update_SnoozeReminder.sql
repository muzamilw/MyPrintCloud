CREATE PROCEDURE dbo.sp_Activity_Update_SnoozeReminder
	@AlarmDate datetime,
	@AlarmTime datetime,
	@ActivityID int
AS
	UPDATE tbl_activity 
    SET AlarmDate=@AlarmDate, AlarmTime=@AlarmTime 
    WHERE ActivityID=@ActivityID
	RETURN