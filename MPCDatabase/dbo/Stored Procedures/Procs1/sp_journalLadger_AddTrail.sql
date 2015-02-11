CREATE PROCEDURE dbo.sp_journalLadger_AddTrail
(		
	@InvoiceType varchar(50),
	@InvoiceNo int,
	@Account varchar(50),
	@Detail varchar(50),
	@TRANSACTIONDATE datetime,
	@Reference varchar(50),
	@Net Float,
	@Tax Float,
	@Paid int,
	@AmountPaid float,
	@Reconciled varchar(1),
	@BankReconciledDate datetime,
	@TransactionUser varchar(50),
	@VAT varchar(50),
	@Nominal int,
	@SystemSiteID int
)	

AS
	Insert into   tbl_audit (InvoiceType, InvoiceNo, Account, Detail, TransactionDate, Reference, Net, Tax, Paid, AmountPaid, Reconciled, BankReconciledDate, TransactionUser,VAT, Nominal,SystemSiteID)
    Values ( @InvoiceType, @InvoiceNo, @Account, @Detail, @TransactionDate, @Reference, @Net, @Tax, @Paid, @AmountPaid, @Reconciled, @BankReconciledDate, @TransactionUser,@VAT, @Nominal,@SystemSiteID)

RETURN