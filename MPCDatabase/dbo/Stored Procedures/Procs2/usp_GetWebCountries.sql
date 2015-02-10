CREATE PROCEDURE [dbo].[usp_GetWebCountries]

AS
BEGIN
		select CountryID, CountryName, CountryCode
		from tbl_country
		
END