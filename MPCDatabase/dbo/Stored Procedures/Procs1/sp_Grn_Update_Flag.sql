CREATE PROCEDURE dbo.sp_Grn_Update_Flag

	(
		@FlagID int,
		@GoodsreceivedID int

	)

AS
	/* SET NOCOUNT ON */
	
	update tbl_goodsreceivednote set FlagID=@FlagID where GoodsreceivedID=@GoodsreceivedID
	
	RETURN