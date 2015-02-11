CREATE PROCEDURE dbo.sp_PipeLine_Update_AllSources
	
AS
	SELECT * FROM tbl_pipeline_source where SourceID=0
	RETURN