CREATE PROCEDURE dbo.sp_Grn_Update_GrnStatus

	(
		@GoodsreceivedID int,
		@Status varchar
		
	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_goodsreceivednote set Status=@Status where GoodsreceivedID=@GoodsreceivedID
	
	RETURN