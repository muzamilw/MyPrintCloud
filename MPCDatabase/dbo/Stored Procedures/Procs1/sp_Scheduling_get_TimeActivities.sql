CREATE PROCEDURE dbo.sp_Scheduling_get_TimeActivities

	(
	@StartTime datetime,
	@EndTime datetime
	)

AS
	SELECT  
                                               tbl_scheduled_costcenters.ScheduledCostCenterID, 
                                               tbl_scheduled_costcenters.SectionCostCenterID, 
                                              tbl_scheduled_costcenters.CostCenterID, 
                                               tbl_scheduled_costcenters.SystemCostCenterType, 
                                               tbl_scheduled_costcenters.ResourcesID, 
                                               tbl_scheduled_costcenters.CritMints, 
                                               tbl_scheduled_costcenters.SetupTime, 
                                               tbl_scheduled_costcenters.RunTime, 
                                               tbl_scheduled_costcenters.WorkInstructions, 
                                               tbl_scheduled_printjobs.ScheduledPrintJobID, 
                                               tbl_scheduled_printjobs.ItemSectionID, 
                                               tbl_scheduled_printjobs.MachineID, 
                                               tbl_scheduled_printjobs.MachineType, 
                                               tbl_scheduled_printjobs.MakeReadyType, 
                                               tbl_scheduled_printjobs.Speed, 
                                               tbl_scheduled_printjobs.RepeatDays, 
                                               tbl_scheduled_printjobs.RepeatOccurrencs, 
                                               tbl_scheduled_printjobs.Notes, 
                                               tbl_scheduled_printjobs.CustomerId, 
                                               tbl_scheduled_printjobs.JobCode, 
                                               tbl_scheduled_printjobs.JobID as ItemJobID, 
                                               tbl_scheduled_printjobs.Quantity, 
                                               tbl_scheduled_printjobs.NoOfUp, 
                                               tbl_scheduled_printjobs.MachineName, 
                                               tbl_scheduled_printjobs.Estimate_Code, 
                                               tbl_scheduled_printjobs.CustomerName, 
                                               tbl_scheduled_printjobs.GullotineID, 
                                               tbl_scheduled_printjobs.Pagination, 
                                               tbl_scheduled_printjobs.ItemSectionName, 
                                               tbl_scheduled_printjobs.Discription, 
                                               tbl_scheduled_printjobs.SelectedQtyIndex, 
                                               tbl_scheduled_printjobs.StartTime AS JobStartTime, 
                                               tbl_scheduled_printjobs.EndTime AS JobEndTime, 
                                               tbl_scheduled_time_activities.ScheduledTimeActivityID, 
                                               tbl_scheduled_time_activities.CreationDateTime, 
                                               tbl_scheduled_time_activities.StartTime, 
                                               tbl_scheduled_time_activities.EndTime, 
                                               tbl_scheduled_time_activities.UserLockedBy, 
                                               tbl_scheduled_time_activities.LockDateTime, 
                                               tbl_scheduled_time_activities.DeliveryTime, 
                                               tbl_scheduled_time_activities.ActivityStatusID, 
                                               tbl_scheduled_time_activities.JobID, 
                                               tbl_scheduled_time_activities.CostCenterID AS ScheduledCostCenterID, 
											   tbl_scheduled_time_activities.JobBindID, 
                                               tbl_scheduled_time_activities.IsLocked, 
                                               tbl_scheduled_time_activities.IsCompleted, 
                                               tbl_scheduled_time_activities.OleColorCode 
                                               FROM 
                                               tbl_scheduled_time_activities 
                                               INNER JOIN tbl_scheduled_costcenters ON (tbl_scheduled_time_activities.CostCenterID = tbl_scheduled_costcenters.ScheduledCostCenterID) 
                                               INNER JOIN tbl_scheduled_printjobs ON (tbl_scheduled_time_activities.JobID = tbl_scheduled_printjobs.ScheduledPrintJobID) 
                                               where  (tbl_scheduled_time_activities.StartTime >=  @StartTime and   tbl_scheduled_time_activities.EndTime <=  @EndTime )  
                                               Or  
                                               (tbl_scheduled_time_activities.StartTime <  @EndTime  and   tbl_scheduled_time_activities.StartTime <=  @StartTime)
                                               Or  
                                               (tbl_scheduled_time_activities.EndTime >  @StartTime  and   tbl_scheduled_time_activities.EndTime <=  @EndTime) 
                                               Or 
                                               (tbl_scheduled_time_activities.StartTime <  @EndTime  and   tbl_scheduled_time_activities.EndTime >  @StartTime)
	RETURN