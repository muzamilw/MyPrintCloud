CREATE PROCEDURE [dbo].[usp_CreateUser]
		
		@Password		varchar(20),
		@FirstName		varchar(100),
		@LastName		varchar(100),
		@CustomerID		numeric = null,
		@Email			varchar(255),
		@SecretQuestion	varchar(200),
		@SecretAns		varchar(200),
		@EmailSubscription bit,
		@NewsletterSubscription bit

AS
BEGIN
		declare @Result int
		declare @AddressID numeric
		if(Exists(Select Email from tbl_contacts where email = @Email))
			Begin
			--Delete from tbl_contactcompanies where ContactCompanyID = @CustomerID
			Select @Result = -1
			End
		Else
			Begin
				insert into tbl_addresses
					(ContactCompanyID, CountryID, IsDefaultAddress)
				values(@CustomerID, 213, 1)
				select @AddressID = SCOPE_IDENTITY()
			Insert into tbl_contacts
				(ContactCompanyID, AddressID, PASSWORD, FirstName, LastName,Email, SecretQuestion, SecretAnswer, IsEmailSubscription, IsNewsLetterSubscription)
				
			Values
				(@CustomerID, @AddressID, @Password, @FirstName, @LastName, @Email, @SecretQuestion, @SecretAns, @EmailSubscription, @NewsletterSubscription)
				 Select scope_identity()
			End
END