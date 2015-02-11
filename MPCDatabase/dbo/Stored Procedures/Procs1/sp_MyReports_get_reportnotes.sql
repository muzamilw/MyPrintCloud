
Create PROCEDURE [dbo].[sp_MyReports_get_reportnotes]
(@SystemSiteID int,
@UserID int)
AS
	SELECT tbl_report_notes.HeadNotes,tbl_report_notes.FootNotes,tbl_report_notes.AdvertitorialNotes 
         FROM tbl_report_notes 
        WHERE ((tbl_report_notes.UserID = @UserID) or (tbl_report_notes.UserID is null)) and SystemSiteID=@SystemSiteID
        order by tbl_report_notes.ReportCategoryID
	RETURN 

--select top 1 * from tbl_report_notes