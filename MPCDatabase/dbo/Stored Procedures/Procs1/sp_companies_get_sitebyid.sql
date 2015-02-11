



create PROCEDURE [dbo].[sp_companies_get_sitebyid]
(
@SiteID int)
                  AS
select tbl_company_sites.* from tbl_company_sites 
	
where CompanySiteID=@SiteID
return