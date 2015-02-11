CREATE PROCEDURE dbo.sp_regionalsettings_get_countrylist
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	SELECT * from tbl_country order by countryname
	RETURN