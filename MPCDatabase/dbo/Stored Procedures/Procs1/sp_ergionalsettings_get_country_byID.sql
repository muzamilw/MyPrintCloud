CREATE PROCEDURE dbo.sp_ergionalsettings_get_country_byID
(@CountryID int)
AS
	Select * from tbl_country where CountryID=@CountryID
	RETURN