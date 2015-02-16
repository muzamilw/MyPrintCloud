CREATE PROCEDURE dbo.sp_SystemLog_Update_InvoiceNotesByInvoiceID
(
	@UserNotes text,
	@UpdateDateTime datetime,
	@SystemUserID int,
	@RecordID int
)
AS
	Update tbl_Invoices set UserNotes=@UserNotes,NotesUpdateDateTime=@UpdateDateTime,NotesUpdatedByUserID=@SystemUserID  where InvoiceID=@RecordID
RETURN