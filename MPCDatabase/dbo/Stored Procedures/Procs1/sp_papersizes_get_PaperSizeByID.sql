CREATE PROCEDURE dbo.sp_papersizes_get_PaperSizeByID
(
@PaperSizeID int)
AS
	select PaperSizeID,Name,Height,Width,SizeMeasure,Area from tbl_papersize where PaperSizeID=@PaperSizeID;
		RETURN