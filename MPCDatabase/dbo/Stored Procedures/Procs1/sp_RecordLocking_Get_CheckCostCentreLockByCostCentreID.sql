
Create PROCEDURE [dbo].[sp_RecordLocking_Get_CheckCostCentreLockByCostCentreID]
(
	@RecordID int	
)
AS
	SELECT tbl_costcentres.LockedBy, tbl_costcentres.CostCentreID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right Outer JOIN tbl_costcentres ON (tbl_systemusers.SystemUserID = tbl_costcentres.LockedBy) where tbl_costcentres.CostCentreID =@RecordID
	RETURN