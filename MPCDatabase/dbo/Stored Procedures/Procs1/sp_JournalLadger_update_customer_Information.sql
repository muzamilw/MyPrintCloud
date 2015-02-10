
CREATE PROCEDURE [dbo].[sp_JournalLadger_update_customer_Information]

	@InvoiceID  int,
	@CustomerID int,
	@UserID int,
	@UserName varchar(200),
	@SystemSiteID int,
	@InvoiceType varchar(5)
AS

Declare @NominalAccount int
Declare @ACCPayable int
Declare @CustomerBalance float

Declare @VoucherType varchar(15)
Declare @CSType varchar(5)

Declare @RIPType varchar(15)
Declare @Reconciled varchar(15)
Declare @PaymentMethod int

Declare @SalesAccount int
Declare @VAAccount int
Declare @CreditorAccount int	
Declare @VoucherID int

Declare @DetailID int
Declare @GrandTotal float
Declare @Tax float
Declare @ItemCode varchar(50)
Declare @Ref varchar(150)
Declare @AccountNo varchar(10)

Declare @VaAccountBalance float
Declare @SalesAccountBalance float
Declare @NominalAccountBalance float
Declare @DepartmentID int

set @VoucherType='JV'
set @CSType = 'C'
set @RIPType = 'I'
set @Reconciled = '-'
set @PaymentMethod = '0'


	--Get Customers Balance, Nominal Account,Account No
	Select @CustomerBalance=AccountBalance,@AccountNo=AccountNumber from tbl_contactcompanies where contactcompanyid=@CustomerID
	
	Select @VAAccount=VATonSale,@NominalAccount=Debitor from  tbl_accountdefault  where SystemSiteID=@SystemSiteID
	
	
	Select @VaAccountBalance = Balance from tbl_chartofAccount where AccountNo=@VAAccount and SystemSiteId=@SystemSiteID
		
	Select @NominalAccountBalance = Balance from tbl_chartofAccount where AccountNo=@NominalAccount and SystemSiteId=@SystemSiteID
	
	--Insert into voucher 
	insert into tbl_voucher SELECT GETDATE(), convert(varchar(250),Terms), OrderNo, @VoucherType,@InvoiceType, GrandTotal, @CSType, ContactCompanyID, @UserID, InvoiceID, @RIPType, 0,@PaymentMethod, GrandTotal, @Reconciled,getDate(),@SystemSiteID
							FROM tbl_invoices where InvoiceID=@InvoiceID
				
	Select @VoucherID=@@Identity			
					
	Select @GrandTotal=GrandTotal,@ref = OrderNo FROM tbl_invoices where InvoiceID=@InvoiceID
		
	---Update Customer Balance
	if (@InvoiceType = 'SI')
		update tbl_contactcompanies set AccountBalance=@CustomerBalance + @GrandTotal where ContactCompanyID=@CustomerID
	else
		update tbl_contactcompanies set AccountBalance=@CustomerBalance - @GrandTotal where ContactCompanyID=@CustomerID
		
	Declare InvoiceDetail Cursor for SELECT  tbl_invoicedetails.ItemCharge, tbl_invoicedetails.ItemTaxValue, tbl_invoicedetails.InvoiceTitle, tbl_invoicedetails.NominalCode,tbl_invoicedetails.DepartmentID
									 FROM tbl_invoicedetails where tbl_invoicedetails.InvoiceID=@InvoiceID

	Open InvoiceDetail 
	Fetch Next from InvoiceDetail into @GrandTotal,@Tax,@ItemCode,@SalesAccount,@DepartmentID
	
	While @@FETCH_STATUS = 0 
		BEGIN
			
			if (@InvoiceType = 'SI')
			Begin
				--insert into voucher detial
				insert into tbl_voucherdetail Select @VoucherID,@NominalAccount,@GrandTotal ,@GrandTotal,@ItemCode,@SalesAccount,@DepartmentID
				
				Set @NominalAccountBalance = @NominalAccountBalance + @GrandTotal
			
				update tbl_chartofAccount set Balance=Balance + @GrandTotal  where AccountNo=@SalesAccount and SystemSiteID = @SystemSiteID
					
				--insert into audit
				insert into tbl_Audit(InvoiceType, InvoiceNo, Account, Detail, TransactionDate, Reference, Net, Tax, Paid, AmountPaid, Reconciled, BankReconciledDate, TransactionUser, VAT, Nominal,SystemSiteID) values 
									(@InvoiceType,@InvoiceID,@AccountNo,'Sales of ' + @ItemCode,getDate(),@Ref,@GrandTotal,@Tax,0,@GrandTotal+@Tax,'-',null,@UserName,'N',@NominalAccount,@SystemSiteID)
			
				--Tax Deduction if present
				if (@Tax > 0 )
					Begin 
						insert into tbl_voucherdetail Select @VoucherID,@NominalAccount,@Tax,@Tax,@ItemCode,@VAAccount,@DepartmentID
						
						Set @NominalAccountBalance = @NominalAccountBalance + @Tax
						Select @VAAccount 
			
						
						update tbl_chartofAccount set Balance=Balance + @Tax  where AccountNo=@VAAccount and SystemSiteID = @SystemSiteID
						
						--insert into audit
						insert into tbl_Audit(InvoiceType, InvoiceNo, Account, Detail, TransactionDate, Reference, Net, Tax, Paid, AmountPaid, Reconciled, BankReconciledDate, TransactionUser, VAT, Nominal,SystemSiteID) values 
											(@InvoiceType,@InvoiceID,@AccountNo,'Tax on '+ @ItemCode,getDate(),@Ref,@GrandTotal,@Tax,0,@GrandTotal+@Tax,'-',null,@UserName,'N',@VAAccount,@SystemSiteID)
				
					End
				
				End
				
			Else
				BEGIN
				
					--insert into voucher detial for Credit invoice
					insert into tbl_voucherdetail Select @VoucherID,@SalesAccount,@GrandTotal ,@GrandTotal,@ItemCode,@NominalAccount,@DepartmentID
					
					Set @NominalAccountBalance = @NominalAccountBalance - @GrandTotal
				
					update tbl_chartofAccount set Balance=Balance - @GrandTotal  where AccountNo=@SalesAccount and SystemSiteID = @SystemSiteID
							
					--Tax Deduction if present
					if (@Tax > 0 )
						Begin 
							insert into tbl_voucherdetail Select @VoucherID,@VAAccount,@Tax,@Tax,@ItemCode,@NominalAccount,@DepartmentID
							
							Set @NominalAccountBalance = @NominalAccountBalance - @Tax
							update tbl_chartofAccount set Balance=Balance - @Tax  where AccountNo=@SalesAccount and SystemSiteID = @SystemSiteID
				
						End
					
					--insert into audit
					insert into tbl_Audit(InvoiceType, InvoiceNo, Account, Detail, TransactionDate, Reference, Net, Tax, Paid, AmountPaid, Reconciled, BankReconciledDate, TransactionUser, VAT, Nominal,SystemSiteID) values 
										(@InvoiceType,@InvoiceID,@AccountNo,@ItemCode,getDate(),@Ref,@GrandTotal,@Tax,0,@GrandTotal+@Tax,'-',null,@UserName,'N',@NominalAccount,@SystemSiteID)
					End		
			Fetch Next from InvoiceDetail into @GrandTotal,@Tax,@ItemCode,@SalesAccount,@DepartmentID
		END
	Close InvoiceDetail 
	Deallocate InvoiceDetail 
						
		--Update Chart of accounts
		update tbl_chartofAccount set Balance=@NominalAccountBalance where AccountNo=@NominalAccount and SystemSiteID = @SystemSiteID
			
	
RETURN