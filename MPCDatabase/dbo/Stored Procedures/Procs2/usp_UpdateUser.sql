CREATE PROCEDURE [dbo].[usp_UpdateUser]
		@UserID			numeric, 
		@LastName		varchar(100),
		@CellNumber		varchar(50),
		@ContactNumber	varchar(50),
		@FaxNumber		varchar(50),
		@URL		varchar(50),
		@Jobtitle		varchar(50)
		


AS
BEGIN
		update tbl_contacts
		Set		
			LastName = @LastName,
			Mobile = @CellNumber,
			HomeTel1 = @ContactNumber,
			Fax = @FaxNumber,
			JobTitle = @Jobtitle,
			URL	= @URL
			
		WHERE ContactID	= @UserID
END