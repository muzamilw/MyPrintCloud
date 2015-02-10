CREATE PROCEDURE dbo.sp_ChartOfAccounts_Validate_NominalCode

	@AccountNo  int,@SystemSiteID int

AS

Select Count(AccountNo) from tbl_chartofaccount where AccountNo=@AccountNo and SystemSiteID=@SystemSiteID

RETURN