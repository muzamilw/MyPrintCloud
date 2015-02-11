CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckProfileLockByID
(
	@RecordID int
)
AS
	SELECT tbl_systemusers.UserName,tbl_systemusers.FullName,tbl_systemusers.CurrentMachineName,
        tbl_systemusers.CurrentMachineIP,tbl_profile.LockedBy 
        FROM tbl_profile 
        left outer JOIN tbl_systemusers ON (tbl_profile.LockedBy = tbl_systemusers.SystemUserID) 
        WHERE tbl_profile.ID = @RecordID
	RETURN