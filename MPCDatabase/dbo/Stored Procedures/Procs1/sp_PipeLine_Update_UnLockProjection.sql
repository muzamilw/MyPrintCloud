CREATE PROCEDURE dbo.sp_PipeLine_Update_UnLockProjection
	@ProjectionID int
AS
	update tbl_estimate_projection set IsLocked = 0 where ProjectionID=@ProjectionID
	RETURN