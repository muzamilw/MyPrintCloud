CREATE PROCEDURE dbo.sp_JournalLadger_Update_BANK_OPENINGBALANCE
	@OpeningBalance float,
	@AccountNo int,
	@SystemSiteID int
AS

Update tbl_chartofAccount set openingbalance = @OpeningBalance  where Accountno = @Accountno and SystemSiteID=@SystemSiteID

RETURN