CREATE PROCEDURE dbo.sp_SectionFlags_FlagID_Used_By_Inventory_GoodsReceivedNotes

	(
		@flagID int
		--@parameter2 datatype OUTPUT
	)

AS
SELECT GoodsReceivedID from tbl_goodsreceivednote WHERE FlagID=@flagID
	RETURN