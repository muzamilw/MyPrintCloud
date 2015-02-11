CREATE PROCEDURE dbo.sp_JournalLadger_Get_FINANCIALLIST  
	
	@FromDate datetime ,
	@ToDate datetime ,
	@SystemSiteID int

	
AS

SELECT AuditNo, InvoiceType as Type, Account , Nominal, Detail,TransactionDate  [Date],
Reference Ref,Net,Tax,TransactionUser as [User] ,Paid,AmountPaid, Reconciled,
BankReconciledDate,VAT ,InvoiceNo 
FROM tbl_audit 
where convert(varchar,TransactionDate,11) between convert(varchar,@FromDate,11) and convert(varchar,@ToDate,11)
	and SystemSiteID=@SystemSiteID

RETURN