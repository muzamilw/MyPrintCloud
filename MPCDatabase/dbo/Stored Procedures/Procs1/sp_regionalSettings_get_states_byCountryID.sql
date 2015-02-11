CREATE PROCEDURE dbo.sp_regionalSettings_get_states_byCountryID
(@CountryID int)
AS
	SELECT * FROM tbl_state WHERE CountryID=@CountryID order by Statename
	RETURN