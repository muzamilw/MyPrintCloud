CREATE PROCEDURE dbo.sp_regionalsettings_check_countryinsuppliercontacts
(@CountryID int)
AS
	select HomeCountryID from tbl_suppliercontacts where HomeCountryID=@CountryID
	RETURN