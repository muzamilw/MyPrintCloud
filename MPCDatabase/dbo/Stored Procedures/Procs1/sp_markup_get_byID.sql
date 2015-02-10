CREATE PROCEDURE dbo.sp_markup_get_byID
(@MarkUpID int)
AS
	select MarkUpID,MarkUpName,MarkUpRate from tbl_markup where MarkUpID=@MarkUpID order by MarkUpRate
	RETURN