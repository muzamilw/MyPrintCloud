CREATE PROCEDURE dbo.sp_Compnay_Copy_Accounts

	(
		@OldSiteID int,
		@NewSiteID int
	)

AS
	
		Insert into tbl_chartofaccount	SELECT     AccountNo, Name, OpeningBalance, OpeningBalanceType, TypeID, SubTypeID, Description, Nature, IsActive, IsFixed, LastActivityDate, 
							            IsForReconciliation, Balance, IsRead, @NewSiteID
										FROM         tbl_chartofaccount where SystemSiteID=@OldSiteID
		
		
		Insert into tbl_nominaldetail SELECT   AccountNo, BankDescription, NoReconciliation, BankName, BankAddress, BankTown, BankCounty, BankPostcode, AccountName, AccountNumber, 
                                      SortCode, ExpiryDate, ContactName, ContactTel, ContactFax, ContactEmail, ContactURL,@NewSiteID,IBN
									  FROM     tbl_nominaldetail	where SystemSiteID=@OldSiteID							
	
		insert into tbl_accountdefault	SELECT     StartFinancialYear, EndFinancialYear, Bank, Sales, Till, Purchase, Debitor, Creditor, Prepayment, VATonSale, VATonPurchase, DiscountAllowed, 
										DiscountTaken, OpeningBalance, @NewSiteID
										FROM         tbl_accountdefault where SystemSiteID=@OldSiteID							
																				
		insert into tbl_item_titles SELECT     ItemTitle, DepartmentID, NominalID, @NewSiteID
									FROM         tbl_item_titles where SystemSiteID=@OldSiteID
									
	RETURN