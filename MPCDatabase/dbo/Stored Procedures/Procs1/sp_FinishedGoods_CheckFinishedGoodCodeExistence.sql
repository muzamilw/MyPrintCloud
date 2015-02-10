CREATE PROCEDURE dbo.sp_FinishedGoods_CheckFinishedGoodCodeExistence

	(
		@FinishedGoodCode varchar(50),
		@ID int
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT ID FROM tbl_finishedgoods where FinishedGoodCode=@FinishedGoodCode and ID<>@ID
	
	RETURN