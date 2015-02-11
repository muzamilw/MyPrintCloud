CREATE PROCEDURE dbo.sp_ChartOfAccounts_get_CHECKCUSTOMER
	
	@AccountNo int,
	@SystemSiteID int
	
AS


SELECT count(DefaultNominalCode) FROM tbl_customers where DefaultNominalCode =  @AccountNo and tbl_customers.SystemSiteID=@SystemSiteID

RETURN