CREATE PROCEDURE dbo.sp_RecordLocking_update_Roles
	(
	
	@UserID int,
	@RecordID int
	
	)

AS
	update tbl_roles set LockedBy=@UserID where RoleID=@RecordID
	RETURN