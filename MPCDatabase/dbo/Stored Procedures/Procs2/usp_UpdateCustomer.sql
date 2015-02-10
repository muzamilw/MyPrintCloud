--exec [usp_GetUserInfo] 3
CREATE PROCEDURE [dbo].[usp_UpdateCustomer]
		@CustomerID				numeric, 
		@CustomerName			varchar(100),
		@EmailSubscription		bit,
		@NewsletterSubscription	bit
		

AS
BEGIN
		Update tbl_contactcompanies
		Set IsEmailSubscription = @EmailSubscription,
			IsMailSubscription = @NewsletterSubscription,
			Name = @CustomerName
		Where ContactCompanyID = @CustomerID
		
		
END