
Create PROCEDURE [dbo].[sp_companies_site_updateSiteSettings]

	(
		@DepartmentID varchar(50),
		@SiteID int,
		@JobManagerID int ,
		@OrderManagerID int,
		@MarkupID int
	)

AS
	update tbl_company_sites set ProductionManagerID=@JobManagerID,OrderManagerID=@OrderManagerID,MarkupID=@MarkupID where CompanySiteID=@SiteID
	RETURN