CREATE PROCEDURE dbo.sp_RecordLocking_update_User
	(
	
	@UserID int,
	@RecordID int
	
	)

AS
	update tbl_systemusers set LockedBy=@UserID where SystemUserID=@RecordID
	RETURN