
CREATE PROCEDURE [dbo].[sp_taxrates_get_tax]
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	select TaxID,TaxCode,TaxName,Tax1 as TaxRates from tbl_taxrate order by TaxCode
	RETURN