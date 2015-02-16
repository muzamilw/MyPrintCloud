CREATE PROCEDURE dbo.sp_JournalLadger_get_ACCOUNT_INFORMATION 
	@AccountNo int,
	@SystemSiteID int
AS

Select AccountNo, Name , Balance, OpeningBalance 
from tbl_chartofaccount 
where accountNO = @AccountNo and SystemSiteID=@SystemSiteID

RETURN