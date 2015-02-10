create PROCEDURE dbo.sp_JournalLadger_get_CREDITCARDINFORMATION
		
	@TransactionID bigint

AS

	SELECT TransactionID,VoucherID,CardHolder,BillingAddress,City,State,Zip,Country,
	Comments,CardNumber,ExpireDate,AuthorizationCode 
	FROM tbl_CreditCardInformation Where TransactionID = @TransactionID
					
RETURN