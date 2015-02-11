CREATE PROCEDURE dbo.sp_taxrates_check_fixed_taxrate
(@TaxID int)
AS
	select TaxID from tbl_taxrate where IsFixed=1 and TaxID=@TaxID
	RETURN