CREATE PROCEDURE dbo.sp_Emails_Update_Emailreport

	(
		    @EmailID int,
		    @IsDeliverd bit,
		    @ErrorResponse varchar(100)=''
		    

	)

AS
	/* SET NOCOUNT ON */
	
	
	update tbl_Emails_MailBox set IsDeliverd=@IsDeliverd , ErrorResponse=@ErrorResponse
	where EmailID = @EmailID
	
	
	
	RETURN