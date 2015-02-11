Create PROCEDURE [dbo].[sp_PipeLine_Update_PipeLineSource]
	@SourceID int,
	@Description varchar(100)
AS
	update tbl_pipeline_Source set Description = @Description
	where SourceID=@SourceID
	RETURN