
Create PROCEDURE [dbo].[sp_SystemNotes_Get_EstimateNotesByEstimateID]
(
	@RecordID int
)
AS
	SELECT tbl_estimates.UserNotes,tbl_estimates.NotesUpdateDateTime,tbl_SystemUsers.FullName as name from tbl_estimates inner join tbl_SystemUsers on (tbl_estimates.NotesUpdatedByUserID = tbl_SystemUsers.SystemUserID) where tbl_estimates.EstimateID =@RecordID
	RETURN