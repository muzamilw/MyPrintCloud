CREATE PROCEDURE dbo.sp_JournalLadger_Get_AUDITTRAIL 
	
	@AuditID bigint 
	
AS

SELECT AuditNo, InvoiceType,InvoiceNo,Account, Detail,TransactionDate,Reference,Net,Tax,
	Paid,AmountPaid,  Reconciled,BankReconciledDate,TransactionUser,VAT, Nominal ,SystemSiteID
	FROM tbl_audit where AuditNo = @AuditID

RETURN