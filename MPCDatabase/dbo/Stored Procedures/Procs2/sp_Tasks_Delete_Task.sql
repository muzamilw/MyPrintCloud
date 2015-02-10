CREATE PROCEDURE dbo.sp_Tasks_Delete_Task
	@TaskID int	
AS
	delete from tbl_tasks where TaskID=@TaskID
	RETURN