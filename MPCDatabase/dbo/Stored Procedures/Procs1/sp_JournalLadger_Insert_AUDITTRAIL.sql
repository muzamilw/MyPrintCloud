CREATE PROCEDURE dbo.sp_JournalLadger_Insert_AUDITTRAIL

	@VoucherID bigint

AS

	--insert into tbl_audittrail  select * from tbl_Voucher  where VoucherID=@VoucherID

RETURN