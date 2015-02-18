--exec [usp_UserChangePssword] 502, 'mnz','d'

CREATE PROCEDURE [dbo].[usp_UserChangePassword]
		@UserID			numeric, 
		@UserPassword	varchar(20),
		@NewPassword	varchar(20),
		@isReset		bit = 0
		
AS
BEGIN

	declare @result		int
	if(@isReset = 0)
		Begin
			if not exists(Select ContactID	
							from tbl_contacts 
							where	ContactID = @UserID AND Password = @UserPassword)
				Begin
					set @result = 0
				End
			else
				begin
					update tbl_contacts
					Set	[Password]	= @NewPassword
					WHERE ContactID	= @UserID
					
					set @result = 1
				end
			End
	else
		Begin
				update tbl_contacts
					Set	[Password]	= @NewPassword
					WHERE ContactID	= @UserID
					
					set @result = 1
		End
		select @result
	 
END