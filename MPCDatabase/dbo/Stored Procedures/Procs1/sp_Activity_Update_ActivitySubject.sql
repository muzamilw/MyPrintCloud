CREATE PROCEDURE dbo.sp_Activity_Update_ActivitySubject
	@ActivityID int	,@ActivityRef varchar(255)
AS
	UPDATE tbl_activity
        SET ActivityRef=@ActivityRef 
        WHERE ActivityID=@ActivityID
	RETURN