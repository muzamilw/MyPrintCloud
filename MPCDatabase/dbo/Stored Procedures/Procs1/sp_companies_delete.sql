CREATE PROCEDURE dbo.sp_companies_delete
(
@CompanyID int)
                  AS
delete from tbl_company  where CompanyID=@CompanyID


return