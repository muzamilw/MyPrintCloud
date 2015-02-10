CREATE PROCEDURE [dbo].[sp_ChartOfAccounts_get_NOMINALACTIVITIES] 
	
	@AccountNo int 	,
	@SystemSiteID int
	
AS


Declare @FinancialYearStartDate datetime
Declare @FinancialYearEndDate datetime

Select @FinancialYearStartDate=StartFinancialYear ,@FinancialYearEndDate=EndFinancialYear from tbl_accountdefault where SystemSiteID=@SystemSiteID 

	
SELECT     tbl_voucher.InvoiceType,tbl_voucher.VoucherID, tbl_voucher.VoucherDate,  tbl_voucher.VoucherType, 
                       tbl_voucher.TotalAmount, tbl_systemusers.UserName, tbl_voucherdetail.VoucherDetailID, tbl_voucherdetail.DebitAccount,
                      (case tbl_voucher.RIPType when 'P' then tbl_goodsreceivednote.Code when 'I' then tbl_invoices.InvoiceCode else '' end) as Code,  
                      (case tbl_voucher.CSType when 'C' then tbl_ContactCompanies.Name when 'S' then tbl_ContactCompanies.Name else '' end) as  Contact, tbl_voucher.Reference,tbl_voucherdetail.Description,  
                      tbl_voucherdetail.Debit, 0 AS Credit, tbl_voucherdetail.CreditAccount, tbl_voucher.UserID, 0 AS Balance, 
                      tbl_voucher.Reconciled AS R, tbl_voucher.reconciledDate AS DateR, 
                      tbl_voucher.RIPID,tbl_voucher.RIPType
					 FROM   tbl_voucher 
INNER JOIN  tbl_voucherdetail  ON tbl_voucherdetail.VoucherID = tbl_voucher.VoucherID 
INNER JOIN    tbl_company_sites ON tbl_company_sites.CompanySiteID = tbl_voucher.SystemSiteID 
INNER JOIN  tbl_systemusers ON tbl_voucher.UserID = tbl_systemusers.SystemUserID 
LEFT OUTER JOIN tbl_ContactCompanies ON tbl_voucher.CSCode  = tbl_ContactCompanies.ContactCompanyID 
LEFT OUTER JOIN tbl_goodsreceivednote ON tbl_voucher.RIPID  = tbl_goodsreceivednote.GoodsReceivedID 
LEFT OUTER JOIN  tbl_invoices ON tbl_voucher.RIPID = tbl_invoices.InvoiceID 


where  ((tbl_voucherdetail.DebitAccount = @AccountNo  and  tbl_voucherdetail.Debit >0 ) and  tbl_Voucher.SystemSiteID=@SystemSiteID) And tbl_voucher.VoucherDate between @FinancialYearStartDate and @FinancialYearEndDate

UNION ALL 

SELECT tbl_voucher.InvoiceType,tbl_voucher.VoucherID,tbl_voucher.VoucherDate,tbl_voucher.VoucherType, tbl_voucher.TotalAmount, 
tbl_systemusers.UserName, tbl_voucherdetail.VoucherDetailID,tbl_voucherdetail.DebitAccount,
(case tbl_voucher.RIPType when 'P' then tbl_goodsreceivednote.Code when 'I' then tbl_invoices.InvoiceCode else '' end) as Code, 
(case tbl_voucher.CSType when 'C' then tbl_ContactCompanies.Name when 'S' then tbl_ContactCompanies.Name else '' end) as  Contact, tbl_voucher.Reference,tbl_voucherdetail.Description,
 0 as Debit,tbl_voucherdetail.Credit,
 tbl_voucherdetail.CreditAccount, tbl_voucher.UserID,0 as Balance ,Reconciled as R ,ReconciledDate as DateR,tbl_voucher.RIPID,tbl_voucher.RIPType
FROM   tbl_voucher 
INNER JOIN  tbl_voucherdetail  ON tbl_voucherdetail.VoucherID = tbl_voucher.VoucherID 
INNER JOIN    tbl_company_sites ON tbl_company_sites.CompanySiteID = tbl_voucher.SystemSiteID 
INNER JOIN  tbl_systemusers ON tbl_voucher.UserID = tbl_systemusers.SystemUserID 
LEFT OUTER JOIN tbl_ContactCompanies ON tbl_voucher.CSCode  = tbl_ContactCompanies.ContactCompanyID 
LEFT OUTER JOIN tbl_goodsreceivednote ON tbl_voucher.RIPID  = tbl_goodsreceivednote.GoodsReceivedID 
LEFT OUTER JOIN  tbl_invoices ON tbl_voucher.RIPID = tbl_invoices.InvoiceID 


                      where  ((tbl_voucherdetail.CreditAccount =@AccountNo and  tbl_voucherdetail.Credit >0 ) and tbl_voucher.SystemSiteID=@SystemSiteID ) And tbl_voucher.VoucherDate between @FinancialYearStartDate and @FinancialYearEndDate
order by tbl_voucher.VoucherId ,Voucherdate, tbl_voucherdetail.VoucherDetailID



RETURN