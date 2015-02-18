CREATE PROCEDURE dbo.sp_PipeLine_Update_PipeLineDeduction
	@PipeLineDeductionID int,
	@DeductionMonths int,
	@DeductionPercentage int
AS
	update tbl_pipeline_deduction set Months=@DeductionMonths,Percentage=@DeductionPercentage 
	where PipeLineDeductionID=@PipeLineDeductionID
	RETURN