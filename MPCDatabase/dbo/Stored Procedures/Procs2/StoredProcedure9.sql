CREATE PROCEDURE dbo.StoredProcedure9

	(
		@FinishedGoodCode varchar(50)
	)

AS
	/* SET NOCOUNT ON */
	
	SELECT FinishedGoodCode FROM tbl_finishedgoods where FinishedGoodCode=@FinishedGoodCode
	
	RETURN