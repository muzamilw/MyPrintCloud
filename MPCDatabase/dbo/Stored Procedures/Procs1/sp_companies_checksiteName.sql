



Create PROCEDURE [dbo].[sp_companies_checksiteName]
(
@SiteID int,@SiteName varchar(50))
                  AS
select CompanySiteID from tbl_company_sites where CompanySiteID<>@SiteID and CompanySiteName=@SiteName
return