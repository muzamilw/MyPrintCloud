Create PROCEDURE [dbo].[sp_PipeLine_Insert_PipeLineSource]
	@Description varchar(100)
AS
	Insert into tbl_pipeline_Source (Description)
	Values(@Description)
	RETURN