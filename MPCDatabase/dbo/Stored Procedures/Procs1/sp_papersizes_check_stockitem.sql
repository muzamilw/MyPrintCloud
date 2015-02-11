
Create PROCEDURE [dbo].[sp_papersizes_check_stockitem]
(
@PaperSizeID int)
AS
	SELECT tbl_stockitems.StockItemID FROM tbl_stockitems WHERE ((tbl_stockitems.ItemSizeID = @PaperSizeID))
		RETURN