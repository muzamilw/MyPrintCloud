CREATE PROCEDURE dbo.sp_Scheduling_update_EditingFlags
	(
@IsInEditing smallint,
@LockedBy int,
@ScheduledTimeActivityID int
	)

AS
	update      tbl_scheduled_time_activities   set    tbl_scheduled_time_activities.IsInEditing   = @IsInEditing , LockedBy = @LockedBy  
	where     tbl_scheduled_time_activities.ScheduledTimeActivityID   =@ScheduledTimeActivityID
	RETURN