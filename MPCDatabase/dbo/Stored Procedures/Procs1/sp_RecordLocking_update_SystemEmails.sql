CREATE PROCEDURE dbo.sp_RecordLocking_update_SystemEmails
	(
	
	@UserID int,
	@RecordID int
	
	)

AS
	update tbl_system_emails set LockedBy=@UserID where ID=@RecordID
	RETURN