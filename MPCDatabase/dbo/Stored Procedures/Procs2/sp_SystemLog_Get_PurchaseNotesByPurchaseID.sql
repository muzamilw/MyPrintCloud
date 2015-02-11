CREATE PROCEDURE dbo.sp_SystemLog_Get_PurchaseNotesByPurchaseID
(
	@RecordID int
)
AS
	SELECT tbl_purchase.UserNotes,tbl_purchase.NotesUpdateDateTime,tbl_SystemUsers.FullName as name from tbl_purchase inner join tbl_SystemUsers on (tbl_purchase.NotesUpdatedByUserID = tbl_SystemUsers.SystemUserID) where tbl_purchase.PurchaseID=@RecordID
	RETURN