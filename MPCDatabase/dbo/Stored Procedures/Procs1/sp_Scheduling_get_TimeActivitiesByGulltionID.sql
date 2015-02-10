CREATE PROCEDURE dbo.sp_Scheduling_get_TimeActivitiesByGulltionID

	(
	@StartTime datetime,
	@EndTime datetime,
	@GullotineID int,
	@MachineType int,
	@IsInEditing smallint
	
	)

AS
DECLARE @Sqlstring varchar(255)

--set @Sqlstring = " tbl_scheduled_costcenters.ScheduledCostCenterID, " 

			--		set @Sqlstring = " select * from tbl_scheduled_costcenters.ScheduledCostCenter " 
                                                     

	--SELECT @Sqlstring
                                                     
	RETURN