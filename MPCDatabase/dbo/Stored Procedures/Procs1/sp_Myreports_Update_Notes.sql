
Create PROCEDURE [dbo].[sp_Myreports_Update_Notes]
(@FootNotes text,
@HeadNotes text,
@AdvNotes text,
@UserID int,
@Type int)
AS
	update tbl_report_notes set FootNotes=@FootNotes,HeadNotes=@HeadNotes,AdvertitorialNotes=@AdvNotes 
         WHERE ((tbl_report_notes.UserID = @UserID) and (tbl_report_notes.ReportCategoryID = @Type))
	RETURN