CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckPagination

	(
	@RecordID int 

	)

AS
	SELECT tbl_systemusers.UserName,tbl_systemusers.FullName,
         tbl_systemusers.CurrentMachineName,tbl_systemusers.CurrentMachineIP,tbl_pagination_profile.LockedBy 
         FROM tbl_pagination_profile 
         left outer join tbl_systemusers ON (tbl_pagination_profile.LockedBy = tbl_systemusers.SystemUserID) 
         WHERE tbl_pagination_profile.ID = @RecordID
	RETURN