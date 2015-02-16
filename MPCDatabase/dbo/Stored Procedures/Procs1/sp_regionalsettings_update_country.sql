CREATE PROCEDURE dbo.sp_regionalsettings_update_country
(@CountryName varchar(50),
@CountryCode varchar(50),
@CountryID int)
AS
	update tbl_country set CountryName=@CountryName,CountryCode=@CountryCode where CountryID=@CountryID
	RETURN