﻿CREATE PROCEDURE dbo.sp_RecordLocking_Get_CheckPurchaseOrderLockbyPurechaseOrderID
(
	@RecordID int
)
AS
	SELECT tbl_Purchase.LockedBy, tbl_Purchase.PurchaseID, tbl_systemusers.UserName, tbl_systemusers.FullName, 
tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers
 right outer JOIN tbl_Purchase ON (tbl_systemusers.SystemUserID = tbl_Purchase.LockedBy) where tbl_Purchase.PurchaseID =@RecordID
RETURN