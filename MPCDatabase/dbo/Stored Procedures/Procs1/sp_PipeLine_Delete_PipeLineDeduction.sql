CREATE PROCEDURE dbo.sp_PipeLine_Delete_PipeLineDeduction
	@PipeLineDeductionID int
AS
	delete from tbl_pipeline_deduction where PipeLineDeductionID = @PipeLineDeductionID
	RETURN