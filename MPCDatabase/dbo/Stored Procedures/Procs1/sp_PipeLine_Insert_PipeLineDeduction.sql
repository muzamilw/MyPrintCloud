CREATE PROCEDURE dbo.sp_PipeLine_Insert_PipeLineDeduction
	@DeductionMonths int ,@DeductionPercentage int
AS
	insert into tbl_pipeline_deduction (Months,Percentage) VALUES (@DeductionMonths,@DeductionPercentage)
	RETURN