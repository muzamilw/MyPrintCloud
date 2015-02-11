CREATE PROCEDURE dbo.sp_Scheduling_Update_CostCenters

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
	 @ScheduledCostCenterID	 int,
	 @EstimatedStartTime as datetime,
	 @EstimatedEndTime as datetime

	)

AS
Update tbl_scheduled_costcenters set
                                             tbl_scheduled_costcenters.SectionCostCenterID=@SectionCostCenterID,
                                            tbl_scheduled_costcenters.CostCenterID=@CostCenterID,
                                             tbl_scheduled_costcenters.SystemCostCenterType=@SystemCostCenterType,
                                             tbl_scheduled_costcenters.ResourcesID=@ResourcesID,
                                             tbl_scheduled_costcenters.CritMints=@CritMints,
                                             tbl_scheduled_costcenters.SetupTime=@SetupTime,
                                             tbl_scheduled_costcenters.RunTime=@RunTime, 
                                             tbl_scheduled_costcenters.CostCenterName=@CostCenterName, 
                                             tbl_scheduled_costcenters.WorkInstructions=@WorkInstructions,
                                             tbl_scheduled_costcenters.EstimatedStartTime = @EstimatedStartTime,
                                             tbl_scheduled_costcenters.EstimatedEndTime = @EstimatedEndTime
                                            where ScheduledCostCenterID= @ScheduledCostCenterID	
                                            
 RETURN