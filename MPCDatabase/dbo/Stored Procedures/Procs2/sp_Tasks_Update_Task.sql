CREATE PROCEDURE dbo.sp_Tasks_Update_Task
		@Subject varchar(500) ,@DueDate datetime =null,@StartDate datetime =null,
        @Status int,@Priority int,@Completion int,@IsTaskAlarmed int,@TaskAlarmDate datetime=null,@TaskAlarmTime datetime=null,@IsComplete int,
        @CompletionDate datetime =null,@CompletionTime datetime=null,@Notes ntext,@IsPrivate int,@isTaskLinked int,@LinkType int,@LinkID int,
        @LastUpdationDateTime datetime,@LastUpdatedBy int,
        @TaskID int
AS
		update tbl_tasks set Subject=@Subject,DueDate=@DueDate,StartDate=@StartDate,
        Status=@Status,Priority=@Priority,Completion=@Completion,IsTaskAlarmed=@IsTaskAlarmed,TaskAlarmDate=@TaskAlarmDate,TaskAlarmTime=@TaskAlarmTime,IsComplete=@IsComplete,
        CompletionDate=@CompletionDate,CompletionTime=@CompletionTime,Notes=@Notes,IsPrivate=@IsPrivate,IsTaskLinked=@isTaskLinked,LinkType=@LinkType,LinkID=@LinkID,
        LastUpdationDateTime=@LastUpdationDateTime,LastUpdatedBy=@LastUpdatedBy 
        where (TaskID=@TaskID)
	RETURN