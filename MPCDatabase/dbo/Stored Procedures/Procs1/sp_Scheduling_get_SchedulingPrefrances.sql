CREATE PROCEDURE dbo.sp_Scheduling_get_SchedulingPrefrances
	(
		@SystemUserID int
		
	)

AS
	select  SchedulingPreference from tbl_systemuser_preferences where   SystemUserID = @SystemUserID 
	RETURN