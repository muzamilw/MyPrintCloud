CREATE PROCEDURE dbo.sp_JournalLadger_get_VoucherDetail

	@VoucherID bigint

AS

Select VoucherDetailID,VoucherID,DebitAccount, Description,Debit,Credit,CreditAccount,DepartmentID from tbl_VoucherDetail where VoucherID = @VoucherID


RETURN