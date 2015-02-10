CREATE PROCEDURE dbo.sp_RecordLocking_update_Pagination
	(
	
	@UserID int,
	@RecordID int
	
	)

AS
	update tbl_pagination_profile set LockedBy=@UserID where ID=@RecordID
	RETURN