CREATE PROCEDURE dbo.sp_companies_get_byid
(@CompanyID int)
                  AS
SELECT     tbl_company.*,tbl_country.CountryName,tbl_state.StateName
FROM         tbl_company INNER JOIN
                      tbl_country ON tbl_company.Country = tbl_country.CountryID INNER JOIN
                      tbl_state ON tbl_state.StateID = tbl_company.State 

where tbl_company.CompanyID=@CompanyID


return