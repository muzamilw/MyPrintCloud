




CREATE PROCEDURE [dbo].[sp_ChartOfAccounts_get_CONTROLACCOUNTS]  
(@SystemSiteID int)
AS

 select DefaultID,StartFinancialYear,EndFinancialYear,Bank,Sales,Till,Purchase, 
 Debitor,Creditor,Prepayment,VATonSale,VATonPurchase,DiscountAllowed,DiscountTaken,OpeningBalance,SystemSiteID
 from  tbl_accountdefault where SystemSiteID=@SystemSiteID

RETURN