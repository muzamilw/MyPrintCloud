CREATE PROCEDURE [dbo].[sp_JournalLadger_get_SUPPLIERINFORMATION]

	@SupplierID as int
AS
	SELECT ContactCompanyID,AccountNumber,Name ,AccountBalance,DefaultNominalCode FROM tbl_ContactCompanies where ContactCompanyID = @SupplierID
RETURN