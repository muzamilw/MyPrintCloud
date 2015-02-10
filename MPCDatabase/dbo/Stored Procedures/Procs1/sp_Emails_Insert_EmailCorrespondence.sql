CREATE PROCEDURE dbo.sp_Emails_Insert_EmailCorrespondence

	(
		@EmailID	int,
		@RefTableName	varchar	(100),
		@RefFieldName	varchar	(100),
		@RefKeyID	int	
	)

AS
	
	insert into tbl_Emails_Correspondence (EmailID,RefTableName,RefFieldName,RefKeyID)
	values(@EmailID,@RefTableName,@RefFieldName,@RefKeyID)
	
	RETURN