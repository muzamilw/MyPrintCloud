CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckUserLockByID
(
	@RecordID int
)
AS
	SELECT tbl_LockedUsers.UserName,tbl_LockedUsers.FullName,tbl_LockedUsers.CurrentMachineName,
        tbl_LockedUsers.CurrentMachineIP,tbl_systemusers.LockedBy 
        FROM tbl_systemusers 
        left outer JOIN tbl_systemusers tbl_LockedUsers ON (tbl_systemusers.LockedBy = tbl_LockedUsers.SystemUserID) where tbl_systemusers.SystemUserID=@RecordID
	RETURN