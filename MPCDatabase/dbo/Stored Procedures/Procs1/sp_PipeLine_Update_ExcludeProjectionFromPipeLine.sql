CREATE PROCEDURE dbo.sp_PipeLine_Update_ExcludeProjectionFromPipeLine
	@ProjectionID int
AS
	update tbl_estimate_projection set IsIncludedInPipeLine = 0 where ProjectionID=@ProjectionID
	RETURN