CREATE PROCEDURE [dbo].[sp_Catlog_Get_UnReadRecordCount]
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	/* SET NOCOUNT ON */
	
	--SELECT COUNT(ID) AS UnReadRecords FROM tbl_finishedgoods_catalogue WHERE (IsRead = 0)
	Select 0 as UnReadRecords 
	
	RETURN