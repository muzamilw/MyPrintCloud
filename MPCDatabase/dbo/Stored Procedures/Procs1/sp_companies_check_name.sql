CREATE PROCEDURE dbo.sp_companies_check_name
(@CompanyName varchar(50),
@CompanyID int)
                  AS
select CompanyID from tbl_company where CompanyID<>@CompanyID and CompanyName=@CompanyName



return