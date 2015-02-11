CREATE PROCEDURE [dbo].[usp_CheckEmailExist]--'naveed@inbox.com'
	@Email	varchar(50)
	
AS
BEGIN
		Select email from tbl_contacts
		where	Email = @Email
		
			
		
END