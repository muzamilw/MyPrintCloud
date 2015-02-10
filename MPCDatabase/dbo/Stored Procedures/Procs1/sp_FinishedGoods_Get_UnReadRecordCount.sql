CREATE PROCEDURE [dbo].[sp_FinishedGoods_Get_UnReadRecordCount]

AS
	/* SET NOCOUNT ON */
	--SELECT COUNT(ID) AS UnReadRecords FROM tbl_finishedgoods WHERE  (IsRead = 0)
	Select 0 as UnReadRecords
	RETURN