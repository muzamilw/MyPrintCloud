CREATE PROCEDURE dbo.sp_companies_check_companyinusers
(
@CompanyID int)
                  AS
select SystemUserID from tbl_systemusers where OrganizationID=@CompanyID

return