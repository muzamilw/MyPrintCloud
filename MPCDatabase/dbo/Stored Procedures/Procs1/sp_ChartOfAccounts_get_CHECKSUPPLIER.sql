CREATE PROCEDURE dbo.sp_ChartOfAccounts_get_CHECKSUPPLIER
	
	@AccountNo int,
	@SystemSiteID int
	
AS


SELECT count(DefaultNominalCode) FROM tbl_Suppliers where DefaultNominalCode = @AccountNo and tbl_Suppliers.SystemSiteID=@SystemSiteID

RETURN