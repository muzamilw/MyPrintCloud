CREATE PROCEDURE dbo.sp_PipeLine_Update_LockProjection
	@ProjectionID int
AS
	update tbl_estimate_projection set IsLocked = 1 where ProjectionID=@ProjectionID
	RETURN