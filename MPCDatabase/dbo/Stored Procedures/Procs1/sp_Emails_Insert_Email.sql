CREATE PROCEDURE dbo.sp_Emails_Insert_Email

	(	  
		    @To varchar(200),
		    @Cc varchar(200),
		    @From varchar(200),
		    @Type smallint,
		    @Body text,
		    @Images text,
		    @SMTPUserName varchar(50),
		    @SMTPPassword varchar(50),
			@SMTPServer	varchar	(50),
			@Subject varchar(50)

	)

AS
	/* SET NOCOUNT ON */
	
	
	insert into tbl_Emails_MailBox ( [TO],Cc ,[From],Type ,Body ,Images ,SMTPUserName,SMTPPassword,SMTPServer,Subject) 
									values(@TO,@Cc ,@From,@Type ,@Body ,@Images ,@SMTPUserName,@SMTPPassword,@SMTPServer,@Subject);select @@identity
	
	
	
	RETURN