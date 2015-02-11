create PROCEDURE dbo.sp_ChartOfAccounts_get_CHECKNOMINALUSED 

	@CreditAccount as int,
	@DebitAccount as int
	
AS


Select Count(VoucherID) from tbl_VoucherDetail  where creditaccount = @CreditAccount or DebitAccount = @DebitAccount 

RETURN