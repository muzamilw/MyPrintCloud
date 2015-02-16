CREATE PROCEDURE dbo.sp_PipeLine_Update_Snooze_ProjectionAlarm
	@ProjectionID int,
	@AlarmDate datetime,
	@AlarmTime datetime
AS
	UPDATE tbl_estimate_projection 
    SET AlarmDate=@AlarmDate, AlarmTime=@AlarmTime 
    WHERE ProjectionID=@ProjectionID
	RETURN