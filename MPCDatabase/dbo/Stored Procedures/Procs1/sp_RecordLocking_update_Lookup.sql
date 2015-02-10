CREATE PROCEDURE dbo.sp_RecordLocking_update_Lookup
	(
	
	@UserID int,
	@RecordID int
	
	)

AS
	update tbl_lookup_methods set LockedBy=@UserID where MethodID=@RecordID
	RETURN