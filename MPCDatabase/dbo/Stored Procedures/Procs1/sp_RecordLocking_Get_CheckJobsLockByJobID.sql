CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckJobsLockByJobID
(
	@RecordID int
)
AS
	SELECT tbl_jobs.LockedBy, tbl_jobs.JobID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers Right Outer Join tbl_jobs ON (tbl_systemusers.SystemUserID = tbl_jobs.LockedBy) where tbl_jobs.JobID =@RecordID
	RETURN