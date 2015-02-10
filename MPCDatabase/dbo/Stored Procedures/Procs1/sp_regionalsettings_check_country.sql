CREATE PROCEDURE dbo.sp_regionalsettings_check_country
(@CountryID int,
@CountryName varchar(50),
@CountryCode varchar(50))
AS
	Select CountryID from tbl_country where CountryID<>@CountryID and (CountryName=@CountryName or CountryCode=@CountryCode)
	RETURN