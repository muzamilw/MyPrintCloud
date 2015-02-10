CREATE PROCEDURE [dbo].[sp_JournalLadger_Update_CUSTOMERBALANCE]
		
	@BalanceAmount float ,
	@CSID int
	

AS
	Update  tbl_ContactCompanies set Accountbalance  = Accountbalance - @BalanceAmount where ContactCompanyID = @CSID
	Select AccountNumber from tbl_ContactCompanies where ContactCompanyID=@CSID

RETURN