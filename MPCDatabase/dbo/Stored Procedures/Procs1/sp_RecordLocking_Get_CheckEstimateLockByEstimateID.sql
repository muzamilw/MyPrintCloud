
CREATE PROCEDURE [dbo].[sp_RecordLocking_Get_CheckEstimateLockByEstimateID]
(
	@RecordID int
)
AS
	SELECT tbl_estimates.LockedBy, tbl_systemusers.UserName, tbl_systemusers.FullName, 
	tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP 
	FROM tbl_systemusers right Outer Join tbl_estimates 
	ON (tbl_systemusers.SystemUserID = tbl_estimates.LockedBy) 
	where tbl_estimates.EstimateID = @RecordID
	RETURN