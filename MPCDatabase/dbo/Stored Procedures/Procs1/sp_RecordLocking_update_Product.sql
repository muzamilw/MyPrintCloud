CREATE PROCEDURE dbo.sp_RecordLocking_update_Product
	(
	
	@UserID int,
	@RecordID int
	
	)

AS
	update tbl_profile set LockedBy=@UserID where ID=@RecordID
	
	RETURN