CREATE PROCEDURE dbo.sp_regionalsettins_check_countryinsites
(@CountryID int)
AS
	select Country from tbl_company_sites where Country=@CountryID
	RETURN