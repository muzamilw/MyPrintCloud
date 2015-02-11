CREATE PROCEDURE dbo.sp_PipeLine_Get_PipeLineDeductions
	
AS
	select * from tbl_pipeline_deduction order by months asc
	RETURN