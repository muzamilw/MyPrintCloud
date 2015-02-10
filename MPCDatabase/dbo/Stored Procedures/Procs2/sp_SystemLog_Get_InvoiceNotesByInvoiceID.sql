CREATE PROCEDURE dbo.sp_SystemLog_Get_InvoiceNotesByInvoiceID
(
	@RecordID int
)
AS
	SELECT tbl_Invoices.UserNotes,tbl_Invoices.NotesUpdateDateTime,tbl_SystemUsers.FullName as name from tbl_Invoices inner join tbl_SystemUsers on (tbl_Invoices.NotesUpdatedByUserID = tbl_SystemUsers.SystemUserID) where tbl_Invoices.InvoiceID=@RecordID
	RETURN