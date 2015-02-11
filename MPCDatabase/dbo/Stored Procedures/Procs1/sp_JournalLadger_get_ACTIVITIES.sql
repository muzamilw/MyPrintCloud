CREATE PROCEDURE dbo.sp_JournalLadger_get_ACTIVITIES

	@Type Varchar(5),
	@Code int,
	@AccountNo int,
	@FromDate datetime,
	@ToDate datetime,
	@SystemSiteID varchar(500)

AS
Declare @String varchar(2000)

Set @String = 'SELECT  tbl_voucher.VoucherID,tbl_voucher.InvoiceType,tbl_voucher.VoucherDate  VoucherDate, 
tbl_voucher.Reference,tbl_voucher.Description,tbl_voucherdetail.Debit,tbl_voucherdetail.Credit, 
tbl_voucherdetail.Description  , 0 As Balance 
FROM tbl_voucherdetail 
INNER JOIN tbl_voucher ON (tbl_voucherdetail.VoucherID = tbl_voucher.VoucherID) 
INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_voucher.SystemSiteID)
where  (tbl_voucher.cstype= ''' + @Type + '''   and  tbl_voucher.cscode= ' + convert(varchar(150),@Code) + ' and 
tbl_voucherdetail.DebitAccount = ' + convert(varchar(150),@AccountNo) + ' and  tbl_voucherdetail.Debit>0 and 
voucherdate between ''' + convert(varchar(150),@FromDate) + ''' and ''' + convert(varchar(150),@ToDate) + ''' ) and ' + @SystemSiteID + '  

UNION ALL

SELECT tbl_voucher.VoucherID,   tbl_voucher.InvoiceType,   tbl_voucher.VoucherDate  as VoucherDate ,   
tbl_voucher.Reference,      tbl_voucher.Description,   tbl_voucherdetail.Debit,tbl_voucherdetail.Credit, 
tbl_voucherdetail.Description, 0 As Balance 
FROM tbl_voucherdetail 
INNER JOIN tbl_voucher ON (tbl_voucherdetail.VoucherID = tbl_voucher.VoucherID) 
INNER JOIN tbl_company_sites ON (tbl_company_sites.SiteID=tbl_voucher.SystemSiteID)
where  (tbl_voucher.cstype= ''' + @Type + ''' and  tbl_voucher.cscode= ' + convert(varchar(150), @Code) + '    and    
tbl_voucherdetail.CreditAccount = ' + convert(varchar(150),@AccountNo) + '  and  tbl_voucherdetail.Credit>0 and 
voucherdate between ''' +  convert(varchar(150),@FromDate) + ''' and ''' + convert(varchar(150),@ToDate) + ''' ) and ' + @SystemSiteID

Exec(@String)
RETURN