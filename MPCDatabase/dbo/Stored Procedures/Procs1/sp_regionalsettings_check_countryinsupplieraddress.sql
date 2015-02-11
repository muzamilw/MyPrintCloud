CREATE PROCEDURE dbo.sp_regionalsettings_check_countryinsupplieraddress
(@CountryID int)
AS
	select CountryID from tbl_supplieraddresses where CountryID=@CountryID
	RETURN