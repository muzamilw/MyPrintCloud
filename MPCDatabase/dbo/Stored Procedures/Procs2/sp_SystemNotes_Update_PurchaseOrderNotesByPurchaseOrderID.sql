CREATE PROCEDURE dbo.sp_SystemNotes_Update_PurchaseOrderNotesByPurchaseOrderID
(
@UserNotes text,
@UpdateDateTime datetime,
@SystemUserID int,
@RecordID int
)
AS
	Update tbl_purchase set UserNotes=@UserNotes,NotesUpdateDateTime=@UpdateDateTime,NotesUpdatedByUserID=@SystemUserID  where PurchaseID=@RecordID
	RETURN