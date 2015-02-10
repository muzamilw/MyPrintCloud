CREATE PROCEDURE dbo.sp_JournalLadger_Insert_AUDITTRAIL_DETAIL

	@VoucherID bigint

AS

--insert into tbl_audittraildetail  select * from tbl_Voucherdetail where VoucherID=@VoucherID

RETURN