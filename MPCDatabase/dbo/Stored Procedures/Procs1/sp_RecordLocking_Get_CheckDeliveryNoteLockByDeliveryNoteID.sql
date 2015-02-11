
Create PROCEDURE [dbo].[sp_RecordLocking_Get_CheckDeliveryNoteLockByDeliveryNoteID]
(
	@RecordID int
)
AS
	SELECT tbl_deliverynotes.LockedBy, tbl_deliverynotes.DeliveryNoteID, tbl_systemusers.UserName, tbl_systemusers.FullName, tbl_systemusers.CurrentMachineName, tbl_systemusers.CurrentMachineIP FROM tbl_systemusers right Outer Join tbl_deliverynotes ON (tbl_systemusers.SystemUserID = tbl_deliverynotes.LockedBy) where tbl_deliverynotes.DeliveryNoteID =@RecordID
	RETURN