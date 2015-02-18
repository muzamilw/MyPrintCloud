CREATE PROCEDURE dbo.sp_Correspondence_insert_Correspondence

	(
	
	@RefTableName	varchar	(100),
	@RefFieldName	varchar	(100),
	@RefKeyID	int	,
	@Type	smallint,
	@Reference	varchar	(100),
	@ReferenceID	int	,
	@ReferenceType	smallint,
	@Title	varchar	(255),
	@Date	datetime	,
	@Direction	bit	,
	@Address	varchar	(255),
	@Discriptions text,
	@UID int
	)

AS

	
declare @CorrespondenceID int

		insert into tbl_Correspondence(RefTableName,RefFieldName,RefKeyID,Type,
											Reference,ReferenceID,ReferenceType,Title,[Date],Direction,
											Address,UID)
											values(@RefTableName,@RefFieldName,@RefKeyID,@Type,
											@Reference,@ReferenceID,@ReferenceType,@Title,@Date,@Direction,
											@Address,@UID)
											
		SELECT @CorrespondenceID =SCOPE_IDENTITY()
									
											
		insert into tbl_Correspondence_Details(CorrespondenceID,Discriptions)
												values(@CorrespondenceID,@Discriptions)

	/* SET NOCOUNT ON */
	RETURN