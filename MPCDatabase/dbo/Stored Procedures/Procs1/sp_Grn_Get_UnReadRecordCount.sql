CREATE PROCEDURE dbo.sp_Grn_Get_UnReadRecordCount
/*
	(
		@parameter1 datatype = default value,
		@parameter2 datatype OUTPUT
	)
*/
AS
	/* SET NOCOUNT ON */
	SELECT GoodsReceivedID, Status FROM tbl_goodsreceivednote where IsRead=0
	
	RETURN