CREATE PROCEDURE dbo.sp_Scheduling_get_EditingFlags

	(
	@ScheduledTimeActivityID int
	)

AS
 Select  tbl_scheduled_time_activities.IsInEditing from  tbl_scheduled_time_activities
                                                    where   tbl_scheduled_time_activities.ScheduledTimeActivityID =@ScheduledTimeActivityID


	RETURN