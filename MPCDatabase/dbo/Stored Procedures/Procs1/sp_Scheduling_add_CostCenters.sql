CREATE PROCEDURE dbo.sp_Scheduling_add_CostCenters

	(
	 @SectionCostCenterID int,
	 @CostCenterID int,
	 @SystemCostCenterType int,
	 @ResourcesID int,
	 @CritMints float,
	 @SetupTime float,
	 @RunTime float,
	 @CostCenterName varchar(255),
	 @WorkInstructions text,
	 @EstimatedStartTime as datetime,
	 @EstimatedEndTime as datetime

	)

AS
Insert Into   tbl_scheduled_costcenters (
                                            tbl_scheduled_costcenters.SectionCostCenterID,
                                           tbl_scheduled_costcenters.CostCenterID,
                                            tbl_scheduled_costcenters.SystemCostCenterType,
                                            tbl_scheduled_costcenters.ResourcesID,
                                            tbl_scheduled_costcenters.CritMints,
                                            tbl_scheduled_costcenters.SetupTime,
                                            tbl_scheduled_costcenters.RunTime,
                                            tbl_scheduled_costcenters.CostCenterName,
                                            tbl_scheduled_costcenters.WorkInstructions,
                                            EstimatedStartTime,EstimatedEndTime)
                                           Values (
                                           @SectionCostCenterID,@CostCenterID,@SystemCostCenterType,@ResourcesID,
                                           @CritMints,@SetupTime,@RunTime,@CostCenterName,@WorkInstructions,
                                           @EstimatedStartTime,@EstimatedEndTime)
                                           ; SELECT @@IDENTITY AS ScheduledCostCenterID 
	RETURN