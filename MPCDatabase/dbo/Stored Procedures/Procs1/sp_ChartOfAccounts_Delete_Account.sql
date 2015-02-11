CREATE PROCEDURE dbo.sp_ChartOfAccounts_Delete_Account
	@AccountNo int,
	@SystemSiteID int
AS
	Delete from tbl_chartofaccount where accountNo = @AccountNo and SystemSiteID=@SystemSiteID
RETURN