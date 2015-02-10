



create PROCEDURE [dbo].[sp_companies_site_departments]
(@SiteID int)
                  AS
select * from tbl_company_site_departments where CompanySiteID= @SiteID order by DepartmentName
return