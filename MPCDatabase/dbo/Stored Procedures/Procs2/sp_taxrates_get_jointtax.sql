CREATE PROCEDURE dbo.sp_taxrates_get_jointtax

AS
	SELECT TaxCode, TaxName + '   ' + CONVERT(varchar, Tax1) + ' %' AS Tax1, TaxID, TaxName, Tax1 FROM tbl_taxrate ORDER BY TaxCode;
	RETURN