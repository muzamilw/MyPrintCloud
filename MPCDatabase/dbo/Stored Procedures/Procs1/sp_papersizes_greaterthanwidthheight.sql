CREATE PROCEDURE dbo.sp_papersizes_greaterthanwidthheight
(@Width float,
@Height float,
@Region varchar(50)
)
AS
	SELECT tbl_papersize.PaperSizeID,tbl_papersize.Name,
         tbl_papersize.Height,tbl_papersize.Width,tbl_papersize.SizeMeasure,tbl_papersize.Area,tbl_papersize.IsFixed,
         tbl_papersize.Region FROM tbl_papersize WHERE (tbl_papersize.Width >= @Width and tbl_papersize.Height >= @Height) and Region like @Region order by Area desc
	RETURN