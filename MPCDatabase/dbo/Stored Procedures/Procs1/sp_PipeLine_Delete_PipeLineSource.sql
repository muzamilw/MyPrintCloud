Create PROCEDURE [dbo].[sp_PipeLine_Delete_PipeLineSource]
	@SourceID int
AS
	delete from tbl_pipeline_Source where SourceID = @SourceID
	RETURN