
Create PROCEDURE [dbo].[sp_SystemLog_Get_DeliveryNotesByDeliveryNoteID]
(
	@RecordID int
)
AS
	SELECT tbl_Deliverynotes.UserNotes,tbl_Deliverynotes.NotesUpdateDateTime,tbl_SystemUsers.FullName as name from tbl_Deliverynotes inner join tbl_SystemUsers on (tbl_Deliverynotes.NotesUpdatedByUserID = tbl_SystemUsers.SystemUserID) where tbl_Deliverynotes.DeliveryNoteID=@RecordID
	RETURN