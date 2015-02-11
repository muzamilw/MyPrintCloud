



create PROCEDURE [dbo].[sp_companies_site_regionalsettings_update]

	(
		@Tax1 varchar(50),
		@Tax2 varchar(50),
		@Tax3 varchar(50),
		@SiteID int
	)

AS
	update tbl_company_sites set Tax1=@Tax1,Tax2=@Tax2,Tax3=@Tax3 where CompanySiteID=@SiteID
	RETURN