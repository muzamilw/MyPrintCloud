CREATE PROCEDURE dbo.sp_Scheduling_setItemSchedulingFlag

	(
	@JobId int 
		)

AS

	declare @Status int
	exec sp_Scheduling_getItemSchedulingFlag @JobId,@Status output
	
	print @Status
	update tbl_Items set IsScheduled=@Status where ItemID = @JobId
	
	

	RETURN