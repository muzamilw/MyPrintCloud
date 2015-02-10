CREATE PROCEDURE dbo.sp_FinishedGoods_UpdateFlag

	(
		@ID int,
		@FlagID int
	)

AS
	/* SET NOCOUNT ON */
	
	UPDATE tbl_finishedgoods SET FlagID = @FlagID where ID=@ID
	
	RETURN