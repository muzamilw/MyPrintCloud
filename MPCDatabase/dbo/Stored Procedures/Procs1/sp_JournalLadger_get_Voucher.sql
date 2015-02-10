CREATE PROCEDURE [dbo].[sp_JournalLadger_get_Voucher]

	@VoucherID bigint

AS

Select VoucherID , VoucherDate,Description,Reference,VoucherType,
TotalAmount,InvoiceType,CSCode,CSType,UserID,ITEMID, RIPType,RIPID,PaymentMethod , 
Balance,Reconciled ,reconciledDate,SystemSiteID from tbl_voucher where VoucherID = @VoucherID

RETURN