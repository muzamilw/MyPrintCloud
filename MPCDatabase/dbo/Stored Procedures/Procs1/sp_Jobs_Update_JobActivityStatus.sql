CREATE PROCEDURE dbo.sp_Jobs_Update_JobActivityStatus
	@SectionCostcentreID int , @Status int, @UserID int
AS

	declare @ExistingStatus int
BEgin
	select @ExistingStatus = Status from tbl_section_costcentres where SectionCostcentreID=@SectionCostcentreID
	
	
	if ( @ExistingStatus = 1 and @Status = 2) 
	BEGIN
		update tbl_section_costcentres set status=@Status,ActualStartDateTime=Getdate(),ActivityUser=@UserID where SectionCostcentreID=@SectionCostcentreID
	END
	ELSE if ( @ExistingStatus = 2 and @Status = 3) 
	BEGIN
		update tbl_section_costcentres set status=@Status,ActualEndTime=Getdate() where SectionCostcentreID=@SectionCostcentreID
	END
	ELSE
	BEGIN
		update tbl_section_costcentres set status=@Status where SectionCostcentreID=@SectionCostcentreID
	END
	
	RETURN 

end