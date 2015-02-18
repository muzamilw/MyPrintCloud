CREATE PROCEDURE dbo.sp_JournalLadger_get_ACCOUNT_RECONCILIATION_INFORAMTION 
	@AccountNo int,
	@Todate datetime ,
	@SystemSiteID int

AS


SELECT   tbl_voucher.InvoiceType,tbl_voucher.VoucherID,  tbl_voucher.VoucherDate  as VoucherDate
,tbl_voucher.Reference,tbl_voucher.Description, tbl_voucherdetail.Debit as Receipts ,0 as Payments, 
tbl_voucher.Reconciled ,tbl_voucher.reconciledDate 
FROM tbl_voucherdetail INNER JOIN tbl_voucher ON (tbl_voucherdetail.VoucherID = tbl_voucher.VoucherID) 
where  tbl_voucherdetail.DebitAccount = @AccountNo and voucherdate <= @ToDate 
and tbl_voucher.Reconciled ='N'  and tbl_voucher.SystemSiteID=@SystemSiteID

UNION ALL 

SELECT   tbl_voucher.InvoiceType,tbl_voucher.VoucherID,  tbl_voucher.VoucherDate  as VoucherDate , 
tbl_voucher.Reference,tbl_voucher.Description,0 as Receipts,tbl_voucherdetail.credit as Payments, 
tbl_voucher.Reconciled ,tbl_voucher.reconciledDate                                                                           
FROM tbl_voucherdetail INNER JOIN tbl_voucher ON (tbl_voucherdetail.VoucherID = tbl_voucher.VoucherID) 
where  tbl_voucherdetail.CreditAccount = @AccountNo and voucherdate <= @ToDate  
and tbl_voucher.Reconciled ='N' and tbl_voucher.SystemSiteID=@SystemSiteID
order by tbl_voucher.VoucherID

RETURN