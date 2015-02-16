CREATE PROCEDURE dbo.sp_Scheduling_delete_TimeItem

	(
		@ScheduledTimeActivityID int

	)

AS
	Delete From tbl_scheduled_time_activities  where ScheduledTimeActivityID= @ScheduledTimeActivityID
	RETURN