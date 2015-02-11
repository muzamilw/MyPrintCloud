CREATE PROCEDURE dbo.sp_RecordLocking_update_GRN
	(
	
		@UserID int,
		@RecordID int
	
	)

AS
	update tbl_goodsreceivednote set LockedBy=@UserID where GoodsReceivedID=@RecordID
	RETURN