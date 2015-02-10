CREATE PROCEDURE dbo.sp_Scheduling_update_SchedulingPrefrances
(
		@SystemUserID int,
		@SchedulingPreference image
	)

AS
	Update tbl_systemuser_preferences set SchedulingPreference=@SchedulingPreference  where  SystemUserID = @SystemUserID
	RETURN