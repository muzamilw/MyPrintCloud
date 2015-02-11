CREATE PROCEDURE dbo.sp_regionalsettings_delete_country
(@CountryID int)
AS
	delete from tbl_country where (CountryID=@CountryID)
	RETURN