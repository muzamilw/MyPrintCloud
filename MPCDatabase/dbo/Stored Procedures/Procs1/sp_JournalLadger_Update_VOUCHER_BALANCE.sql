CREATE PROCEDURE dbo.sp_JournalLadger_Update_VOUCHER_BALANCE 
	@Balance Float,
	@VoucherID int
AS
	Update tbl_Voucher set Balance = Balance - @Balance  where VoucherId = @VoucherID

RETURN