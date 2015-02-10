CREATE PROCEDURE dbo.sp_PipeLine_Update_IncludeProjectionInPipeLine
	@ProjectionID int
AS
	update tbl_estimate_projection set IsIncludedInPipeLine = 1 where ProjectionID=@ProjectionID
	RETURN