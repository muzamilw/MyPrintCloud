CREATE PROCEDURE dbo.sp_Scheduling_update_TimeItem

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
		@OleColorCode int,
		@IsInEditing smallint,
		@ScheduledTimeActivityID int
	)

AS
	Update  tbl_scheduled_time_activities Set 
                                                         tbl_scheduled_time_activities.CreationDateTime = @CreationDateTime,
                                                         tbl_scheduled_time_activities.StartTime =@StartTime,
                                                         tbl_scheduled_time_activities.EndTime = @EndTime,
                                                         tbl_scheduled_time_activities.UserLockedBy= @UserLockedBy,
                                                         tbl_scheduled_time_activities.LockDateTime=@LockDateTime,
                                                         tbl_scheduled_time_activities.DeliveryTime=@DeliveryTime,
                                                         tbl_scheduled_time_activities.ActivityStatusID=@ActivityStatusID,
                                                         tbl_scheduled_time_activities.JobID=@JobID,
                                                         tbl_scheduled_time_activities.CostCenterID=@CostCenterID,
                                                         tbl_scheduled_time_activities.JobBindID=@JobBindID,
                                                         tbl_scheduled_time_activities.IsLocked=@IsLocked,
                                                         tbl_scheduled_time_activities.IsCompleted=@IsCompleted,
                                                         tbl_scheduled_time_activities.OleColorCode= @OleColorCode, 
                                                         tbl_scheduled_time_activities.IsInEditing= @IsInEditing ,
                                                         tbl_scheduled_time_activities.LinkedScheduledTimeActivityID = @LinkedScheduledTimeActivityID,
                                                         tbl_scheduled_time_activities.Mode= @Mode 
                                                         where ScheduledTimeActivityID= @ScheduledTimeActivityID
	RETURN