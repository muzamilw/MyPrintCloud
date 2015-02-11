CREATE PROCEDURE [dbo].[sp_JournalLadger_Update_SUPPLIERBALANCE]
		
	@BalanceAmount float ,
	@CSID int
AS
	Update  tbl_ContactCompanies set Accountbalance  = Accountbalance + @BalanceAmount 
	where ContactCompanyID = @CSID

RETURN