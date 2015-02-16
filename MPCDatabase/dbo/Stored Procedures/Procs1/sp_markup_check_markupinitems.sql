CREATE PROCEDURE dbo.sp_markup_check_markupinitems
(@MarkUpID int)
AS
	SELECT tbl_items.ItemID fROM tbl_items 
         WHERE (((tbl_items.RunOnMarkUpID = @MarkUpID) or (tbl_items.Qty1MarkUpID1 = @MarkUpID)) or ((tbl_items.Qty2MarkUpID2 = @MarkUpID) or (tbl_items.Qty3MarkUpID3 = @MarkUpID)))
        RETURN