CREATE PROCEDURE dbo.sp_RecordLocking_update_CrmActivities
	(
	
		@UserID int,
		@RecordID int
	
	)

AS
	update tbl_activity set LockedBy=@UserID where ActivityId=@RecordID
	RETURN