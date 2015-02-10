CREATE PROCEDURE dbo.sp_companies_get_companysites
(
@CompanyID int)
                  AS
select * from tbl_company_sites where CompanyID=@CompanyID
return