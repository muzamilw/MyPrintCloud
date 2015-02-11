CREATE PROCEDURE dbo.sp_taxrates_get_tax_byid
(@TaxID int)
AS
	select * from tbl_taxrate where TaxID=@TaxID
	RETURN