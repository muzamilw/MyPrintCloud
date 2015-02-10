--exec [usp_GetUserPassword] 'naveed@inbox.com'
CREATE PROCEDURE [dbo].[usp_GetUserPassword]
	@Email	varchar(255)
	
AS
BEGIN
	if Exists(select Email from tbl_contacts where Email = @Email)
		Begin
			Select Password, FirstName, LastName , SecretQuestion, SecretAnswer, ContactID
			from tbl_contacts
			where	Email = @Email
		End
END