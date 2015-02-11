Create PROCEDURE [dbo].[usp_UpdateUserLoginDetail]
		@UserID			numeric, 
		@LastName		varchar(100),
		@FirstName		varchar(100),
		@Password		varchar(50),
		@Email			varchar(30),
		@SecretQuestion		varchar(200),
		@SecretAns		varchar(200)

AS
BEGIN
		update tbl_contacts
		Set		
			LastName = @LastName,
			FirstName = @FirstName,
			Password = @Password,
			Email = @Email,
			SecretQuestion = @SecretQuestion,
			SecretAnswer	= @SecretAns
			
		WHERE ContactID	= @UserID
END