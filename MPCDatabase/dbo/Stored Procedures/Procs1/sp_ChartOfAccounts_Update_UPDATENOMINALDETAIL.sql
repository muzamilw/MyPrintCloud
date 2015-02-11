CREATE PROCEDURE dbo.sp_ChartOfAccounts_Update_UPDATENOMINALDETAIL 

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



Update tbl_nominaldetail  set 
	IBN=@IBN,BankDescription=@BankDescription,NoReconciliation =@NoReconciliation, BankName=@BankName,BankAddress=@BankAddress,BankTown=@BankTown,BankCounty=@BankCounty,BankPostcode=@BankPostcode,AccountName=@AccountName, 
 	AccountNumber=@AccountNumber,SortCode=@SortCode,ExpiryDate=@ExpiryDate,ContactName=@ContactName,ContactTel=@ContactTel,ContactFax=@ContactFax,  ContactEmail=@ContactEmail,ContactURL=@ContactURL where AccountNo=@AccountNo and SystemSiteID=@SystemSiteID



RETURN