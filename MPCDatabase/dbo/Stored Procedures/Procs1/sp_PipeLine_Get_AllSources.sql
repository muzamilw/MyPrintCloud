CREATE PROCEDURE dbo.sp_PipeLine_Get_AllSources
	
AS
	SELECT *       
       FROM tbl_pipeline_source
	RETURN