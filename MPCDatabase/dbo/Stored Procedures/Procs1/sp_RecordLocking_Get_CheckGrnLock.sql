CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckGrnLock
(
	@RecordID int
)
AS
	SELECT tbl_goodsreceivednote.LockedBy, tbl_goodsreceivednote.GoodsReceivedID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers Right Outer Join tbl_goodsreceivednote ON (tbl_systemusers.SystemUserID = tbl_goodsreceivednote.LockedBy) where tbl_goodsreceivednote.GoodsReceivedID =@RecordID
	RETURN