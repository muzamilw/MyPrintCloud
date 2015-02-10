CREATE PROCEDURE dbo.sp_RecordLocking_update_CrmPipeLineProjection
	(
	
		@UserID int,
		@RecordID int
	
	)

AS
	update tbl_estimate_projection set LockedBy=@UserID where projectionid=@RecordID
	RETURN