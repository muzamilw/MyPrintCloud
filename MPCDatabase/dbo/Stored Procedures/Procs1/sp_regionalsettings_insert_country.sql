CREATE PROCEDURE dbo.sp_regionalsettings_insert_country
(@CountryName varchar(50),
@CountryCode varchar(50))
AS
	insert into tbl_country (CountryName,CountryCode) VALUES (@CountryName,@CountryCode);Select @@Identity
	RETURN