CREATE PROCEDURE dbo.sp_Scheduling_Add_TimeItem

	(
		@CreationDateTime datetime ,
		@StartTime datetime ,
		@EndTime datetime , 
		@UserLockedBy int ,
		@LockDateTime datetime,
		@DeliveryTime datetime,
		@ActivityStatusID int,
		@JobID int,
		@CostCenterID int,
		@JobBindID int,
		@IsLocked smallint,
		@IsCompleted smallint,
		@Mode int,
		@LinkedScheduledTimeActivityID int,
		@OleColorCode int
				
	)

AS
	Insert into tbl_scheduled_time_activities ( 
                                                         tbl_scheduled_time_activities.CreationDateTime,
                                                         tbl_scheduled_time_activities.StartTime,
                                                         tbl_scheduled_time_activities.EndTime,
                                                         tbl_scheduled_time_activities.UserLockedBy,
                                                         tbl_scheduled_time_activities.LockDateTime,
                                                         tbl_scheduled_time_activities.DeliveryTime,
                                                         tbl_scheduled_time_activities.ActivityStatusID,
                                                         tbl_scheduled_time_activities.JobID,
                                                         tbl_scheduled_time_activities.CostCenterID,
                                                         tbl_scheduled_time_activities.JobBindID,
                                                         tbl_scheduled_time_activities.IsLocked,
                                                         tbl_scheduled_time_activities.IsCompleted,
                                                         tbl_scheduled_time_activities.Mode,
                                                         tbl_scheduled_time_activities.LinkedScheduledTimeActivityID,
                                                         tbl_scheduled_time_activities.OleColorCode )
                                                         Values(@CreationDateTime,@StartTime,@EndTime,@UserLockedBy,@LockDateTime,
                                                         @DeliveryTime,@ActivityStatusID,@JobID,@CostCenterID,@JobBindID,@IsLocked,
                                                         @IsCompleted,@Mode,@LinkedScheduledTimeActivityID,@OleColorCode) ; SELECT @@IDENTITY AS ScheduledTimeActivityID
	RETURN