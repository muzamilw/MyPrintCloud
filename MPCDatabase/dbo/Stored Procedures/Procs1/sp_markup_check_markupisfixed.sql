CREATE PROCEDURE dbo.sp_markup_check_markupisfixed
(@MarkUpID int)
AS
	Select MarkUpID from tbl_markup where IsFixed=1 and MarkUpID=@MarkUpID
        RETURN