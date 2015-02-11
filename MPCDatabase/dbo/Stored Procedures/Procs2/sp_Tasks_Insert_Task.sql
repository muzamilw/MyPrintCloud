CREATE PROCEDURE dbo.sp_Tasks_Insert_Task

        @Subject varchar(500),@DueDate datetime=null,@StartDate datetime=null,@Status int,@Priority int,@Completion int,@IsTaskAlarmed int,@TaskAlarmDate datetime=null,
        @TaskAlarmTime datetime=null,@Owner int,@IsComplete int,@CompletionDate datetime=null,@CompletionTime datetime=null,@Notes ntext,@IsPrivate int,@IsTaskLinked int,
        @LinkType int,@LinkID int,@CreationDateTime datetime,@CreatedBy int,@LastUpdationDateTime datetime,@LastUpdatedBy	int,@SystemSiteID int
AS
		insert into tbl_tasks (Subject,DueDate,StartDate,
        Status,Priority,Completion,IsTaskAlarmed,TaskAlarmDate,TaskAlarmTime,Owner,IsComplete,
        CompletionDate,CompletionTime,Notes,IsPrivate,IsTaskLinked,LinkType,LinkID,CreationDateTime,
        CreatedBy,LastUpdationDateTime,LastUpdatedBy,SystemSiteID) VALUES 
        (@Subject,@DueDate,@StartDate,@Status,@Priority,@Completion,@IsTaskAlarmed,@TaskAlarmDate,
        @TaskAlarmTime,@Owner,@IsComplete,@CompletionDate,@CompletionTime,@Notes,@IsPrivate,@IsTaskLinked,
        @LinkType,@LinkID,@CreationDateTime,@CreatedBy,@LastUpdationDateTime,@LastUpdatedBy,@SystemSiteID);select @@Identity as TaskID
	RETURN