CREATE PROCEDURE dbo.sp_Tasks_Update_DismissTaskAlarm

	@TaskID int
AS
	UPDATE tbl_tasks
    SET IsTaskAlarmed=0 
    WHERE TaskID=@TaskID
	RETURN