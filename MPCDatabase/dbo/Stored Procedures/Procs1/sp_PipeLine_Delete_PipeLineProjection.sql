CREATE PROCEDURE dbo.sp_PipeLine_Delete_PipeLineProjection
	@ProjectionID int
AS
	Delete FROM tbl_estimate_projection where ProjectionID=@ProjectionID
	RETURN