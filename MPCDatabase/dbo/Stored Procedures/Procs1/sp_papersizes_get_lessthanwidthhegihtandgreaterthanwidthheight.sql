CREATE PROCEDURE dbo.sp_papersizes_get_lessthanwidthhegihtandgreaterthanwidthheight
(
@MinWidth float,
@MinHeight float,
@MaxWidth float,
@MaxHeight float,
@Region varchar(50))
AS
	SELECT tbl_papersize.PaperSizeID,tbl_papersize.Name,
         tbl_papersize.Height,tbl_papersize.Width,tbl_papersize.SizeMeasure,tbl_papersize.Area,tbl_papersize.IsFixed,
         tbl_papersize.Region FROM tbl_papersize WHERE (((tbl_papersize.Width >= @MinWidth) and (tbl_papersize.Height >= @MinHeight)) and ( (tbl_papersize.Height <= @MaxHeight) and (tbl_papersize.Width <= @MaxWidth) ) ) and Region like @Region order by Area desc
	RETURN