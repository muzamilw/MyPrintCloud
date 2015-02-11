CREATE PROCEDURE dbo.sp_regionalsettings_check_countryincompany
(@CountryID int)
AS
	select Country from tbl_company where Country=@CountryID
	RETURN