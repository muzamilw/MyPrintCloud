CREATE PROCEDURE dbo.sp_Activity_Update_ActivityTime
	@ActivityStartTime datetime,@ActivityEndTime datetime, @ActivityID int
AS
	UPDATE tbl_activity 
        SET ActivityStartTime=@ActivityStartTime,ActivityEndTime=@ActivityEndTime 
        WHERE ActivityID=@ActivityID
	RETURN