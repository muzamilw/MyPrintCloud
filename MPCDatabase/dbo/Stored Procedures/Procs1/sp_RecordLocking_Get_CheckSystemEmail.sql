CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckSystemEmail
	(
		@RecordID int
	)

AS
SELECT tbl_systemusers.UserName,tbl_systemusers.FullName,tbl_systemusers.CurrentMachineName,
         tbl_systemusers.CurrentMachineIP,tbl_roles.LockedBy 
         FROM tbl_roles 
         left outer JOIN tbl_systemusers ON (tbl_roles.LockedBy = tbl_systemusers.SystemUserID) 
         WHERE tbl_roles.RoleID = @RecordID
         
RETURN