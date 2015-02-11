CREATE PROCEDURE dbo.sp_Tasks_Update_MarkTaskCompleted
	@Status int,@Completion int,@IsComplete int,
    @CompletionDate datetime,@CompletionTime datetime,
    @LastUpdationDateTime datetime,@LastUpdatedBy int, 
    @TaskID int
AS
	update tbl_tasks set Status=@Status,Completion=@Completion,IsComplete=@IsComplete,
    CompletionDate=@CompletionDate,CompletionTime=@CompletionTime,
    LastUpdationDateTime=@LastUpdationDateTime,LastUpdatedBy=@LastUpdatedBy 
    where (TaskID=@TaskID)
	RETURN