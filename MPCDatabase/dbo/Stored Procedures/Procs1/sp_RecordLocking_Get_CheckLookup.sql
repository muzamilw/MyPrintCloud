CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckLookup
	(
		@RecordID int
	)

AS
SELECT tbl_systemusers.UserName,tbl_systemusers.FullName,tbl_systemusers.CurrentMachineName,
         tbl_systemusers.CurrentMachineIP,tbl_lookup_methods.LockedBy 
         FROM tbl_lookup_methods 
         Left outer JOIN tbl_systemusers ON (tbl_lookup_methods.LockedBy = tbl_systemusers.SystemUserID) 
         WHERE tbl_lookup_methods.MethodID = @RecordID
         
RETURN