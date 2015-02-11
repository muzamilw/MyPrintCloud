
CREATE PROCEDURE [dbo].[sp_papersizes_get_papersizetable]
(@Region varchar(50))
AS
	select tbl_papersize.PaperSizeID,tbl_papersize.Name,
         tbl_papersize.Height,tbl_papersize.Width,tbl_papersize.SizeMeasure,tbl_papersize.Area,tbl_papersize.IsFixed,
         tbl_papersize.Region,tbl_papersize.isArchived from tbl_papersize where Region like @Region order by Area desc 
	RETURN