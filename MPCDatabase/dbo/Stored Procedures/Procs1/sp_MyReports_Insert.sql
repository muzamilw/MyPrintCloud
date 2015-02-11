
create PROCEDURE [dbo].[sp_MyReports_Insert]
(
@SystemSiteID int,
@FootNotes text,
@HeadNotes text,
@AdvNotes text,
@UserID int,
@Type int)
AS
	insert into tbl_report_notes (SystemSiteID,FootNotes,HeadNotes,AdvertitorialNotes,UserID,ReportCategoryID) VALUES  (@SystemSiteID,@FootNotes,@HeadNotes,@AdvNotes,@UserID,@Type)
	RETURN