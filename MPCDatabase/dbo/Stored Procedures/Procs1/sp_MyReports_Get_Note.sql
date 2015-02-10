
create PROCEDURE [dbo].[sp_MyReports_Get_Note]
(@SystemSiteID int,
@UserID int,
@type int)
AS
	SELECT tbl_report_notes.HeadNotes,
         tbl_report_notes.FootNotes,tbl_report_notes.AdvertitorialNotes,tbl_report_notes.UserID 
         FROM tbl_report_notes 
         WHERE ((tbl_report_notes.UserID = @UserID) and (tbl_report_notes.ReportCategoryID = @type)) and SystemSiteID=@SystemSiteID
	RETURN