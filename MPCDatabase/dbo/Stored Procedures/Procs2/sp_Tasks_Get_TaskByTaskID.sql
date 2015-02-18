CREATE PROCEDURE dbo.sp_Tasks_Get_TaskByTaskID
	@TaskID int	
AS
	SELECT tbl_tasks.TaskID,
    tbl_tasks.Subject,tbl_tasks.DueDate,tbl_tasks.StartDate,tbl_tasks.Status,tbl_tasks.Priority,
    tbl_tasks.Completion,tbl_tasks.IsTaskAlarmed,tbl_tasks.TaskAlarmDate,tbl_tasks.TaskAlarmTime,
    tbl_tasks.Owner,tbl_tasks.IsComplete,tbl_tasks.CompletionDate,tbl_tasks.CompletionTime,tbl_tasks.TotalWorkHours,
    tbl_tasks.ActualWorkHours,tbl_tasks.Notes,tbl_tasks.IsPrivate,tbl_tasks.IsTaskLinked,tbl_tasks.LinkType,
    tbl_tasks.LinkID,tbl_tasks.CreationDateTime,tbl_tasks.CreatedBy,tbl_tasks.LastUpdationDateTime,tbl_tasks.LastUpdatedBy,tbl_systemusers.FullName as CreatedByFullName 
    FROM tbl_tasks 
    LEFT OUTER JOIN tbl_systemusers ON tbl_tasks.CreatedBy = tbl_systemusers.SystemUserID
    WHERE 
    (tbl_tasks.TaskID = @TaskID)
	RETURN