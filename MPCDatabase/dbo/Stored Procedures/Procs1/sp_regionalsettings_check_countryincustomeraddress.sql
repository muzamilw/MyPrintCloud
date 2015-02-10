CREATE PROCEDURE dbo.sp_regionalsettings_check_countryincustomeraddress
(@CountryID int)
AS
	select CountryID from tbl_customeraddresses where CountryID=@CountryID
	RETURN