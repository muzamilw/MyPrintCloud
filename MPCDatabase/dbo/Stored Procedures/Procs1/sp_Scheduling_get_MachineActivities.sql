CREATE PROCEDURE dbo.sp_Scheduling_get_MachineActivities

	(		@CostCenterID  int
	--		@parameter2 datatype OUTPUT
	)

AS
		SELECT tbl_scheduled_time_activities.StartTime,  
                                                     tbl_scheduled_time_activities.EndTime,  
                                                     tbl_scheduled_time_activities.DeliveryTime,  
                                                     tbl_scheduled_time_activities.ActivityStatusID,  
                                                     tbl_scheduled_time_activities.ScheduledTimeActivityID,   
                                                     tbl_scheduled_time_activities.JobId,  
                                                     tbl_scheduled_time_activities.UserLockedBy,  
                                                     tbl_scheduled_time_activities.LockDateTime,   
                                                     tbl_scheduled_time_activities.CreationDateTime,   
                                                     tbl_scheduled_time_activities.IsInEditing,   
                                                     tbl_scheduled_costcenters.SetupTime,   
                                                     tbl_scheduled_costcenters.Notes,  
                                                     tbl_scheduled_costcenters.ResourcesID,   
                                                     tbl_scheduled_costcenters.SystemCostCenterType,  
                                                     tbl_scheduled_costcenters.CostCenterID,  
                                                    tbl_scheduled_costcenters.SectionCostCenterID,  
                                                    tbl_scheduled_costcenters.ScheduledCostCenterID  
                                               FROM tbl_scheduled_costcenters  
                                               INNER JOIN tbl_scheduled_time_activities ON (tbl_scheduled_costcenters.CostCenterID = tbl_scheduled_time_activities.CostCenterID)  
                                               Where tbl_scheduled_time_activities.CostCenterID = @CostCenterID

	
	
	RETURN