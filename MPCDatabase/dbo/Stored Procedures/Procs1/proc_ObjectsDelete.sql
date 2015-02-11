

CREATE PROCEDURE [dbo].[proc_ObjectsDelete]
(
	@ObjectID int
)
AS
BEGIN

	SET NOCOUNT OFF
	DECLARE @Err int

	DELETE
	FROM [Objects]
	WHERE
		[ObjectID] = @ObjectID
	SET @Err = @@Error

	RETURN @Err
END