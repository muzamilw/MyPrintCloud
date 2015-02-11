CREATE PROCEDURE dbo.sp_Jobs_Update_JobStatus
@ItemID int , @JobStatusID int
AS
Begin
	declare @ExistingStatus int

	select @ExistingStatus=JobStatusID from tbl_items where ItemID=@ItemID

	-- in progress to complete
	if ( @ExistingStatus = 2 and @JobStatusID = 3) 
	begin
		update tbl_items set JobStatusID=@JobStatusID,JobActualCompletionDateTime=getdate() where ItemID=@ItemID
	end
	else if ( @ExistingStatus = 1 and @JobStatusID = 2) -- from not started to in progress
	begin
		update tbl_items set JobStatusID=@JobStatusID,JobActualStartDateTime =getdate() where ItemID=@ItemID
	end
	else if ( @ExistingStatus = 1 and @JobStatusID = 3) -- from not started to complete
	begin
		update tbl_items set JobStatusID=@JobStatusID,JobActualStartDateTime =getdate(),JobActualCompletionDateTime=getdate() where ItemID=@ItemID
	end
	else
	begin
		update tbl_items set JobStatusID=@JobStatusID where ItemID=@ItemID
	end

end