CREATE PROCEDURE dbo.sp_regionalsettings_check_countryincustomercontact
(@CountryID int)
AS
	select HomeCountryID from tbl_customercontacts where HomeCountryID=@CountryID
	RETURN