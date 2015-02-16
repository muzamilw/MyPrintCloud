CREATE PROCEDURE dbo.sp_Task_Update_SnoozeTaskAlarm
	@TaskAlarmDate datetime, @TaskAlarmTime datetime,
    @TaskID	int
AS
	UPDATE tbl_tasks
    SET TaskAlarmDate=@TaskAlarmDate, TaskAlarmTime=@TaskAlarmTime 
    WHERE TaskID=@TaskID
	RETURN