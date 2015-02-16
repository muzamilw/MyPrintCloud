CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckCrmPipeLineProjectionLockByProjectionID
(
	@RecordID int
)
AS
	SELECT tbl_estimate_projection.LockedBy, tbl_estimate_projection.ProjectionID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right outer JOIN tbl_estimate_projection ON (tbl_systemusers.SystemUserID = tbl_estimate_projection.LockedBy) where tbl_estimate_projection.ProjectionID =@RecordID
	RETURN