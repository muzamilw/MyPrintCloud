
Create PROCEDURE [dbo].[sp_MyReports_Get_OtherNotes]
(@SystemSiteID int,
@UserID int,
@type int)
AS
	SELECT tbl_report_notes.UserID,tbl_report_notes.HeadNotes,
         tbl_report_notes.FootNotes,tbl_report_notes.AdvertitorialNotes 
         FROM tbl_report_notes 
         WHERE ((tbl_report_notes.UserID = @UserID) and (tbl_report_notes.reportcategoryid = @type)) and SystemSiteID=@SystemSiteID
	RETURN