CREATE PROCEDURE dbo.sp_Scheduling_delete_PrintJob
	(
	@ScheduledPrintJobID int
		
	)

AS
	Delete From tbl_scheduled_printjobs 
 where ScheduledPrintJobID= @ScheduledPrintJobID


	RETURN