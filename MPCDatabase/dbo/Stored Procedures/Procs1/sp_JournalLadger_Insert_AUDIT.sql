Create PROCEDURE dbo.sp_JournalLadger_Insert_AUDIT 

	@InvoiceType Varchar(50),
	@InvoiceNo int,
	@Account Varchar(50) ,
	@Detail Varchar(50),
	@TransactionDate datetime,
	@Reference Varchar(50),
	@Net float,
	@Tax float,
	@Paid int,
	@AmountPaid float,
	@Reconciled varchar(1), 
	@BankReconciledDate datetime,
	@TransactionUser Varchar(50),
	@Vat Varchar(50),
	@Nominal int

AS

insert into tbl_audit (InvoiceType,InvoiceNo,Account,Detail,TransactionDate,Reference,Net,Tax,Paid,
		AmountPaid,Reconciled, BankReconciledDate,TransactionUser,VAT,Nominal) values 
		(@InvoiceType,@InvoiceNo,@Account,@Detail,@TransactionDate,@Reference,@Net,@Tax,@Paid,
		@AmountPaid,@Reconciled, @BankReconciledDate,@TransactionUser,@Vat,@Nominal)

RETURN