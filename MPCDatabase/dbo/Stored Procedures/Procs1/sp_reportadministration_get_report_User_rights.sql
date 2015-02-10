CREATE PROCEDURE dbo.sp_reportadministration_get_report_User_rights
(@SystemUserID int)
AS
	select userReportID,SystemUserID,ReportID,ReportCategoryID from tbl_userreports where systemuserid=@SystemUserID
	RETURN