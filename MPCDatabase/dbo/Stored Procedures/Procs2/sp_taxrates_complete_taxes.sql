CREATE PROCEDURE dbo.sp_taxrates_complete_taxes
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select * from tbl_taxrate order by TaxCode
	RETURN