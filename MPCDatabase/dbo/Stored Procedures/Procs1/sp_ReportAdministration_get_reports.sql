CREATE PROCEDURE dbo.sp_ReportAdministration_get_reports

	(
		@CompanyID int
	)

AS
	select ReportID,Name,CategoryID from tbl_reports where CompanyID=@CompanyID
	RETURN