
Create PROCEDURE [dbo].[sp_RecordLocking_Get_CheckStockItemLockByStockItemID]
(
	@RecordID int
)
AS
	SELECT tbl_stockitems.LockedBy, tbl_stockitems.StockItemID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right outer JOIN tbl_stockitems ON (tbl_systemusers.SystemUserID = tbl_stockitems.LockedBy) where tbl_stockitems.StockItemID =@RecordID
	RETURN