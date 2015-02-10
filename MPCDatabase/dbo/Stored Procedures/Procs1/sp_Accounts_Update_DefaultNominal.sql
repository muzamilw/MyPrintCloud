CREATE PROCEDURE dbo.sp_Accounts_Update_DefaultNominal
 (
 @StartFinancialYear datetime, 
 @EndFinancialYear datetime, 
 @Bank int, 
 @Sales int, 
 @Till int, 
 @Purchase int, 
 @Debitor int, 
 @Creditor int, 
 @Prepayment int, 
 @VATonSale int, 
 @VATonPurchase int, 
 @DiscountAllowed int, 
 @DiscountTaken int, 
 @OpeningBalance int, 
 @SystemSiteID int)	
AS
	UPDATE    tbl_accountdefault
SET              StartFinancialYear =@StartFinancialYear , EndFinancialYear =@EndFinancialYear , Bank =@Bank, Sales =@Sales , Till =@Till , Purchase =@Purchase , Debitor =@Debitor , Creditor =@Creditor , Prepayment =@Prepayment , VATonSale =@VATonSale , VATonPurchase =@VATonPurchase , 
                      DiscountAllowed =@DiscountAllowed , DiscountTaken =@DiscountTaken , OpeningBalance =@OpeningBalance where SystemSiteID =@SystemSiteID 
RETURN