CREATE PROCEDURE dbo.sp_Scheduling_delete_CostCenters

	(
		@ScheduledCostCenterID int
	)

AS
Delete from tbl_scheduled_costcenters
where ScheduledCostCenterID= @ScheduledCostCenterID

	RETURN