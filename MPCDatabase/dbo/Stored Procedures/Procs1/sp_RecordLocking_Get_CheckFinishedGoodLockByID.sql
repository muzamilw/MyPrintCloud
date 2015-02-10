CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckFinishedGoodLockByID
( 
	@RecordID int
)
AS
	SELECT tbl_finishedgoods.LockedBy, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right Outer Join tbl_finishedgoods ON (tbl_systemusers.SystemUserID = tbl_finishedgoods.LockedBy) WHERE tbl_finishedgoods.ID = @RecordID
	RETURN