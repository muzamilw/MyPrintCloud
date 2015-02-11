CREATE PROCEDURE dbo.sp_ChartOfAccounts_Insert_NOMINALDETAIL

	@AccountNo  int,	
	@BankDescription Varchar(255),
     	@NoReconciliation smallint, 
	@BankName Varchar(255),
	@BankAddress Varchar(255),
	@BankTown Varchar(50),
	@BankCounty Varchar(50),
	@BankPostcode Varchar(50),
	@AccountName Varchar(255),
	@AccountNumber Varchar(50),
	@SortCode Varchar(50),
	@ExpiryDate datetime,
	@ContactName Varchar(255),
	@ContactTel Varchar(50),
	@ContactFax Varchar(50),
	@ContactEmail Varchar(255),
	@ContactURL Varchar(255) ,
	@SystemSiteID int,
	@IBN varchar(50)
AS



insert into  tbl_nominaldetail
 (AccountNo,BankDescription,NoReconciliation, BankName,BankAddress,BankTown,BankCounty,
 BankPostcode,AccountName,AccountNumber,SortCode,ExpiryDate,ContactName,ContactTel,
 ContactFax,  ContactEmail,ContactURL,SystemSiteID,IBN ) VALUES 
 (
 @AccountNo,@BankDescription,@NoReconciliation, @BankName,@BankAddress,@BankTown,
 @BankCounty,@BankPostcode,@AccountName,@AccountNumber,@SortCode,@ExpiryDate,
 @ContactName,@ContactTel,@ContactFax,  @ContactEmail,@ContactURL,@SystemSiteID ,@IBN
 )

RETURN