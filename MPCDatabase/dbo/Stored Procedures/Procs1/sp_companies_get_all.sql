



create PROCEDURE [dbo].[sp_companies_get_all]
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select * from tbl_company order by CompanyName
	RETURN