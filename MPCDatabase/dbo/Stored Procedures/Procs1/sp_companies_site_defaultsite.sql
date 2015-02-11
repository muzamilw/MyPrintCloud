



Create PROCEDURE [dbo].[sp_companies_site_defaultsite]
(@CompanyID int)
                  AS
select CompanySiteID from tbl_company_sites where CompanyID=@CompanyID 
return