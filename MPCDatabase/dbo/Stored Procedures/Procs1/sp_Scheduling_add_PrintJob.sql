CREATE PROCEDURE dbo.sp_Scheduling_add_PrintJob
	(
	
		@ItemSectionID int ,
		@MachineID int,
		@MakeReadyType int,
		@Speed float ,
		@RepeatDays int ,
		@RepeatOccurrencs int ,
		@Notes text ,
		@CustomerId int,
		@JobCode varchar(255),
		@JobID int,
		@Quantity int,
		@NoOfUp int,
		@MachineName varchar(255),
		@GullotineName varchar(255),
		@Estimate_Code varchar(255),
		@CustomerName varchar(255),
		@GullotineID int,
		@Pagination int ,
		@ItemSectionName varchar(255),
		@Discription text,
		@SelectedQtyIndex int,
		@StartTime datetime,
		@EndTime datetime,
		@MachineType int,
		@TargetPrintDate datetime ,
		@ArtworkByDate datetime ,
		@DataByDate datetime ,
		@TargetBindDate datetime

	)
AS
	Insert into tbl_scheduled_printjobs( 
                                                 tbl_scheduled_printjobs.ItemSectionID,
                                                 tbl_scheduled_printjobs.MachineID,
                                                 tbl_scheduled_printjobs.MakeReadyType,
                                                 tbl_scheduled_printjobs.Speed,
                                                 tbl_scheduled_printjobs.RepeatDays,
                                                 tbl_scheduled_printjobs.RepeatOccurrencs,
                                                 tbl_scheduled_printjobs.Notes,
                                                 tbl_scheduled_printjobs.CustomerId,
                                                 tbl_scheduled_printjobs.JobCode, 
                                                 tbl_scheduled_printjobs.JobID, 
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
                                                 tbl_scheduled_printjobs.StartTime,
                                                 tbl_scheduled_printjobs.EndTime,
                                                tbl_scheduled_printjobs.MachineType,
												tbl_scheduled_printjobs.TargetPrintDate,
												tbl_scheduled_printjobs.ArtworkByDate,
												tbl_scheduled_printjobs.DataByDate	,
												tbl_scheduled_printjobs.TargetBindDate,
												tbl_scheduled_printjobs.GullotineName

				)
                                                 Values( @ItemSectionID,@MachineID,@MakeReadyType,@Speed,@RepeatDays,@RepeatOccurrencs,
                                                 @Notes,@CustomerId,@JobCode,@JobID,@Quantity,@NoOfUp,@MachineName,@Estimate_Code,@CustomerName,
                                                 @GullotineID,@Pagination,@ItemSectionName,@Discription,@SelectedQtyIndex,@StartTime,@EndTime,@MachineType,
				@TargetPrintDate,@ArtworkByDate,@DataByDate,@TargetBindDate,@GullotineName )
                                                 ; SELECT @@IDENTITY AS ScheduledPrintJobID
	RETURN