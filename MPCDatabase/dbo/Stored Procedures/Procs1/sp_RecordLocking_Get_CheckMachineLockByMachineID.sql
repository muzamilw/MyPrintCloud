CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckMachineLockByMachineID
(
	@RecordID int
)
AS
	SELECT tbl_machines.LockedBy, tbl_machines.MachineID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right outer JOIN tbl_machines ON (tbl_systemusers.SystemUserID = tbl_machines.LockedBy) where tbl_machines.MachineID =@RecordID
	RETURN