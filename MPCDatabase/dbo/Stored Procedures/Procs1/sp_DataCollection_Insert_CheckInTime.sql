CREATE PROCEDURE dbo.sp_DataCollection_Insert_CheckInTime

	(
		@UserID int,
		@CheckInDateTime datetime
	)

AS
	insert into tbl_systemuser_checkins (UserID,CheckInDateTime) VALUES (@UserID,@CheckInDateTime)
	RETURN